using UnityEngine;

namespace SCS.Mio.Mission
{
    public enum MissionState
    {
        None,
        Start_Begin,
        Start_End,
        Stroll_Begin,
        Stroll_End,
        Stroll_EndComplete,
        Encounter_Begin,
        Encounter_Spawned,
        Encounter_End,
        Exit_Begin,
        Exit_End
    }

    public interface IInMissionEventDispatcher
    {
        public void Request(MissionState state);
        public void Subscribe(System.Action<MissionState> action);
        public void UnSubscribe(System.Action<MissionState> action);
    }

    public class InMissionEventDispatcher : MonoBehaviour, IInMissionEventDispatcher
    {
        #region private variables
        private System.Action<MissionState> _onMissionStateChanged;
        #endregion private variables

        #region private functions
        private void HandleOnMissionStateChanged(MissionState state)
        {
            _onMissionStateChanged?.Invoke(state);
        }
        #endregion private functions

        public void Request(MissionState state)
        {
            _onMissionStateChanged?.Invoke(state);
        }

        public void Subscribe(System.Action<MissionState> action)
        {
            _onMissionStateChanged += action;
        }

        public void UnSubscribe(System.Action<MissionState> action)
        {
            _onMissionStateChanged -= action;
        }
    }
}
