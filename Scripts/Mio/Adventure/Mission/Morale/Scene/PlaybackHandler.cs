using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Scene
{
    public class PlaybackHandler : MonoBehaviour
    {
        [SerializeField] private Mission.InMissionManager _inMissionManager;
        private SceneRhythmHandler _rhythmHandler;
        private void Start()
        {
            if(_inMissionManager != null)
            {
                _inMissionManager.EventDispatcher.Subscribe(HandleOnMissionStateChanged);
                _rhythmHandler = GetComponent<SceneRhythmHandler>();
            }
        }

        private void HandleOnMissionStateChanged(Mission.MissionState state)
        {
            switch (state)
            {
                case Mission.MissionState.Start_Begin:
                    Debug.Log("Play");
                    _rhythmHandler.Play();
                    break;
                case Mission.MissionState.Encounter_Begin:
                    _rhythmHandler?.ToggleNodes();
                    break;
               case Mission.MissionState.Encounter_Spawned:
                    break;
                case Mission.MissionState.Encounter_End:
                    _rhythmHandler?.ToggleNodes();
                    break;
                default:
                    break;
            }
        }
    }
}
