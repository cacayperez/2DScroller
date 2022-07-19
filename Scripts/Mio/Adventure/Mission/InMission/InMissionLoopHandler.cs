using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Mission
{
    public interface IInMissionLoopHandler
    {
        public EncounterContext Encounter { get; }
    }
    public class InMissionLoopHandler : MonoBehaviour, IInMissionLoopHandler, IGameRunnable
    {
        #region private variables
        [SerializeField] private float _distanceCovered;
        [SerializeField] private float _targetDistance;
        [SerializeField] private int _pointCount;
        private Queue<MissionPoint> _pointQueue;
        private MissionPoint _targetPoint;
        private IInMissionEventDispatcher _dispatcher;
        private IInMissionVelocityHandler _velocityHandler;
        private MissionState _currentState;
        private bool _isStrolling = false;
        #endregion private variables

        #region  public variables
        public EncounterContext Encounter => _targetPoint.EncounterContext;
        #endregion public variables

        #region private functions
        private void Awake()
        {
            _velocityHandler = gameObject.GetComponent<InMissionVelocityHandler>();
            _dispatcher = gameObject.GetComponent<InMissionEventDispatcher>();
            _dispatcher.Subscribe(HandleOnMissionStateChanged);
            _pointQueue = new Queue<MissionPoint>();

            GameController.Instance.SubscribeAll(this);

            var points = GameController.Instance.ActiveMission.Points;
            if(points.Length > 0)
            {
                foreach (var p in points)
                {
                    _pointQueue.Enqueue(p);
                    _pointCount++;
                }
                SetNextTarget(true);
            }

        }

        private IEnumerator Start()
        {
            yield return Helper.Yielder.Get(1);

            _dispatcher.Request(MissionState.Start_Begin);

            yield return Helper.Yielder.Get(1);

            _dispatcher.Request(MissionState.Start_End);

            yield return Helper.Yielder.Get(1);

            _dispatcher.Request(MissionState.Stroll_Begin);
        }

        private void Update()
        {
            if(_isStrolling == true)
            {
                _distanceCovered = _distanceCovered + _velocityHandler.Velocity * Time.deltaTime;
                if (_distanceCovered >= _targetDistance)
                {
                    CheckPoint();
                }
            }    
        }

        private void HandleOnMissionStateChanged(MissionState state)
        {
            _currentState = state;
            switch (state)
            {
                case MissionState.Stroll_Begin:
                    _isStrolling = true;
                    break;
                case MissionState.Stroll_End:
                    _isStrolling = false;
                    break;
                case MissionState.Stroll_EndComplete:
                    OnStrollFullStop();
                    break;
                case MissionState.Encounter_End:
                    OnEncounterComplete();
                    break;
                default:
                    break;
            }
        }

        private void OnEncounterComplete()
        {
            SetNextTarget();
            if(_pointCount != 0 & _currentState < MissionState.Exit_Begin)
            {
                _dispatcher.Request(MissionState.Stroll_Begin);
            }
        }

        private void CheckPoint()
        {
            switch (_targetPoint.PointType)
            {
                case MissionPointType.SystemMessage:
                case MissionPointType.Dialogue:
                    SetNextTarget();
                    break;
                case MissionPointType.Player_Prompt:
                    break;
                case MissionPointType.Encounter:
                    _dispatcher.Request(MissionState.Stroll_End);
                    break;
                default:
                    break;
            }
        }

        private void OnStrollFullStop()
        {
            _dispatcher.Request(MissionState.Encounter_Begin);
        }

        private void SetNextTarget(bool firstpoint = false)
        {
            if(_pointQueue.Count > 0)
            {
                var tp = _pointQueue?.Dequeue();
                _targetPoint = tp;
                _targetDistance = tp.TotalDistance;
                _distanceCovered = 0;

                if(!firstpoint)
                    _pointCount--;
            }
            else
            {
                EndMissionLoop();
            }
        }

        private void EndMissionLoop()
        {
            _dispatcher.Request(MissionState.Exit_Begin);
            _velocityHandler.Stop();
            _pointQueue.Clear();
            enabled = false;
        }
        private void OnDestroy()
        {
            GameController.Instance.UnSubscribeAll(this);
        }

        #endregion private functions

        #region public functions
        #region IGameRunnable implementation
        public void OnPause()
        {
            enabled = false;
        }

        public void OnResume()
        {
            if (_isStrolling == true)
                enabled = true;
        }

        public void OnExit()
        {
            throw new NotImplementedException();
        }
        #endregion IGameRunnable implementation
        #endregion public functions
    }
}