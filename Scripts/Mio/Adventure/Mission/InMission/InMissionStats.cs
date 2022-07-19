using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Mission
{
    public interface IInMissionStats
    {

    }
    public class InMissionStats : MonoBehaviour, IInMissionStats
    {
        #region private variables
        private IInMissionEventDispatcher _eventDispatcher;
        private int _numberOfEncounters = 0;
        #endregion private variables
        
        #region  public variables
        #endregion public variables
        
        #region private functions
        private void Awake()
        {
            _eventDispatcher = GetComponent<InMissionEventDispatcher>();
            _eventDispatcher.Subscribe(HandleOnMissionStateChanged);
        }

        private void HandleOnMissionStateChanged(MissionState state)
        {
            switch (state)
            {
                case MissionState.Encounter_Begin:
                    IncrementEncounterCount();
                    break;
                default:
                    break;
            }
        }

        private void IncrementEncounterCount()
        {
            _numberOfEncounters++;
            Debug.Log(_numberOfEncounters);
        }
        #endregion private functions
        
        #region public functions
        #endregion public functions
    }
}