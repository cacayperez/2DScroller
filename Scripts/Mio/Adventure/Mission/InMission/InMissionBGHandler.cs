using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Mission
{
    public interface IInMissionBGHandler
    {
        public void StopAll();
    }

    public class InMissionBGHandler : MonoBehaviour, IInMissionBGHandler, IGameRunnable
    {
        #region private variables
        private BackgroundTile[] _bgTiles;
        private IInMissionEventDispatcher _eventDispatcher;
        private MissionState _missionState;
        #endregion private variables
        
        #region  public variables
        #endregion public variables
        
        #region private functions
        private void Awake()
        {
            _eventDispatcher = GetComponent<InMissionEventDispatcher>();
            _bgTiles = FindObjectsOfType<BackgroundTile>();

            _eventDispatcher.Subscribe(HandleOnMissionStateChanged);
            GameController.Instance.SubscribeAll(this);
        }

        private void HandleOnMissionStateChanged(MissionState state)
        {
            _missionState = state;
            switch (state)
            {
/*                case MissionState.Start_Begin:
                    break;
                case MissionState.Start_End:
                    break;*/
                case MissionState.Stroll_Begin:
                    ResumeAll();
                    break;
/*                case MissionState.Stroll_End:
                    break;*/
                case MissionState.Stroll_EndComplete:
                    StopAll();
                    break;
/*                case MissionState.Encounter_Begin:
                    break;
                case MissionState.Encounter_Spawned:
                    break;
                case MissionState.Encounter_End:
                    break;
                case MissionState.Exit_Begin:
                    break;
                case MissionState.Exit_End:
                    break;*/
                default:
                    break;
            }
        }
        #endregion private functions
        
        #region public functions
        public void StopAll()
        {
            for (int i = 0; i < _bgTiles.Length; i++)
            {
                _bgTiles[i].DisableTiling();
            }
        }

        public void ResumeAll()
        {
            for (int i = 0; i < _bgTiles.Length; i++)
            {
                _bgTiles[i].EnableTiling();
            }
        }

        private void OnDestroy()
        {
            GameController.Instance?.UnSubscribeAll(this);
        }
        public void OnPause()
        {
            StopAll();
        }

        public void OnResume()
        {
            switch (_missionState)
            {

                case MissionState.Stroll_Begin:
                case MissionState.Stroll_End:
                    ResumeAll();
                    break;
                default:
                    break;
            }
        }

        public void OnExit()
        {
            
        }
        #endregion public functions
    }
}