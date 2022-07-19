using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SCS.Mio.Mission
{
    public interface IInMissionVelocityHandler
    {
        public float Velocity { get; }
        public void Stop();
    }

    public class InMissionVelocityHandler : MonoBehaviour, IInMissionVelocityHandler, IGameRunnable
    {
        #region private variables
        [SerializeField] private float _currentVelocity;
        [SerializeField] private float _velocity;
        private int _stopIdx;
        private static readonly float[] _velocityStops = new float[3] { 5.0f, 13.0f, 21.0f };
        private float _targetVelocity;
        private IInMissionEventDispatcher _dispatcher;
        private MissionState _missionState;
        #endregion private variables

        #region  public variables
        public float Velocity { get { return _velocity; } }

        #endregion public variables

        #region private functions
        private void Awake()
        {
            _currentVelocity = 0.0f;
            _stopIdx = 1;
            _targetVelocity = _velocityStops[_stopIdx];

            _dispatcher = GetComponent<InMissionEventDispatcher>();
            _dispatcher.Subscribe(HandleOnMissionStateChanged);

            GameController.Instance.SubscribeAll(this);
        }

        private void HandleOnMissionStateChanged(MissionState state)
        {
            switch (state)
            {
                case MissionState.Stroll_Begin:
                    Stroll();
                    break;
                case MissionState.Stroll_End:
                    StopStroll();
                    break;
                case MissionState.Exit_Begin:
                    Stop();
                    break;
                default:
                    break;
            }
            _missionState = state;
        }

        private void Update()
        {
            _velocity = _currentVelocity;
        }

        private void Stroll()
        {
            enabled = true;
            DOTween.To(() => _currentVelocity, val => _currentVelocity = val, _targetVelocity, 1.0f)
                .OnComplete(()=> {
                    if (_stopIdx < _velocityStops.Length)
                    {

                        IncrementStop();
                    }
                });
        }

        private void IncrementStop()
        {
            _stopIdx++;
            if(_stopIdx < _velocityStops.Length)
            {
                _targetVelocity = _velocityStops[_stopIdx];
                Stroll();
            }
        }

        private void StopStroll()
        {
         
            _stopIdx = 0;
            _targetVelocity = _velocityStops[_stopIdx];
            DOTween.To(() => _currentVelocity, val => _currentVelocity = val, _targetVelocity, 1.0f)
               .OnComplete
                (
                  StopStrollComplete
                );
        }

        private void StopStrollComplete()
        {
            enabled = false;
            _dispatcher.Request(MissionState.Stroll_EndComplete);
        }
        
        private void OnDestroy()
        {
            GameController.Instance.UnSubscribeAll(this);
        }

        #endregion private functions
        public void Stop()
        {
            _currentVelocity = _velocity = 0;
            DOTween.Kill(this);
            enabled = false;
        }
        #region IGameRunnable implementation
        public void OnPause()
        {
            enabled = false;
        }

        public void OnResume()
        {
            enabled = true;
/*            switch (_missionState)
            {
                case MissionState.Start_End:
                case MissionState.Stroll_Begin:
                case MissionState.Stroll_End:
                    enabled = true;
                    break;
                default:
                    break;
            }*/
        }

        public void OnExit()
        {
            throw new System.NotImplementedException();
        }

        #endregion IGameRunnable implementation
    }
}