using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Mission
{
    public interface IInMissionSystemDisplayHandler
    {

    }
    public class InMissionSystemDisplayHandler : MonoBehaviour, IInMissionSystemDisplayHandler
    {
        private IInMissionEventDispatcher _eventDispatcher;
        private void Awake()
        {
            _eventDispatcher = GetComponent<InMissionEventDispatcher>();
            _eventDispatcher.Subscribe(HandleOnMissionStateChanged);
        }

        private void HandleOnMissionStateChanged(MissionState state)
        {
            switch (state)
            {
                case MissionState.Start_Begin:
                    break;
                case MissionState.Start_End:
                    break;
                case MissionState.Stroll_Begin:
                    break;
                case MissionState.Stroll_End:
                    break;
                case MissionState.Stroll_EndComplete:
                    break;
                case MissionState.Encounter_Begin:
                    break;
                case MissionState.Encounter_Spawned:
                    break;
                case MissionState.Encounter_End:
                    break;
                case MissionState.Exit_Begin:
                    break;
                case MissionState.Exit_End:
                    break;
                default:
                    break;
            }
        }
    }
}

