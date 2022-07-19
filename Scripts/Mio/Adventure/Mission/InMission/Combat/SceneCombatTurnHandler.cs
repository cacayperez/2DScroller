using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    [System.Serializable]
    public struct TurnMessage
    {
        public Character.TurnHandler turnHandler;
        public System.Action waitAction;
    }

    public class SceneCombatTurnHandler : MonoBehaviour
    {
        #region private variables

        private Queue<TurnMessage> _queue;
        private Character.ICombatSystem[] _targets;
        private System.Action OnTurnComplete;
        private Mission.InMissionManager _missionManager;
        #endregion private variables

        #region public variables
        public Character.ICombatSystem[] Targetables { get { return _targets; } }
        public bool IsDone = false;
        #endregion public variables
        
        #region private functions
        private void Awake()
        {
            _queue = new Queue<TurnMessage>();
            _missionManager = FindObjectOfType<Mission.InMissionManager>();
            _missionManager.EventDispatcher.Subscribe(HandleMissionStateChanged);
        }

        public void HandleMissionStateChanged(Mission.MissionState state)
        {
            switch (state)
            {
                case Mission.MissionState.Encounter_Begin:
                    _missionManager.SpawnEnemy();
                    break;
                case Mission.MissionState.Encounter_Spawned:
                    PopulateTargets();
                    break;
                default:
                    break;
            }
        }

        private void PopulateTargets()
        {
            _targets = FindObjectsOfType<Character.CombatSystem>();
            _queue.Clear();
            IsDone = false;
            for (int i = 0; i < _targets.Length; i++)
            {
                _targets[i].BeginCombat();
            }
        }

        private void ExecuteTurn()
        {
            var message = _queue?.Peek();
            message?.waitAction?.Invoke();
        }

        #endregion private functions

        #region public functions
        public void QueueTurn(TurnMessage message)
        {
            if(_queue.Count == 0)
            {
                _queue.Enqueue(message);
                ExecuteTurn();
            }
            else
            {
                _queue.Enqueue(message);
            }
        }

        public void CharacterDefeated()
        {
            foreach(var t in _targets)
            {
                if (t.OwnerObject != null) t.TurnHandler.End();
            }
            IsDone = true;

            _missionManager.EventDispatcher.Request(Mission.MissionState.Encounter_End);
        }

        public void Next()
        {
            _queue.Dequeue();
            if (IsDone == true) _queue.Clear();
            if (_queue.Count > 0) ExecuteTurn();
        }

        #endregion public functions
    }
}


