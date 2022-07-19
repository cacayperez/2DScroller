using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCS.Mio.UI;

namespace SCS.Mio.Scene
{
    public class InMissionAnnouncementHandler : MonoBehaviour
    {
        [SerializeField] private UI_Announcement _missionStart;
        [SerializeField] private UI_Announcement _missionComplete;
        [SerializeField] private UI_Announcement _fight321;
        

        private void Awake()
        {
            Mission.IInMissionEventDispatcher dispatcher = FindObjectOfType<Mission.InMissionEventDispatcher>();
            dispatcher.Subscribe(HandleOnMissionStateChanged);
        }

        private void HandleOnMissionStateChanged(Mission.MissionState state)
        {
            switch (state)
            {
                case Mission.MissionState.Start_Begin:
                    _missionStart?.Show();
                    break;
                case Mission.MissionState.Encounter_Begin:
                    _fight321?.Show();
                    break;
                case Mission.MissionState.Exit_Begin:
                    _missionComplete?.Show();
                    break;
                default:
                    break;
            }
        }
    }
}
