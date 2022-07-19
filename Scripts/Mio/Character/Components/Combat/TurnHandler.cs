using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Character
{
    public enum TurnState
    {
        None,
        Waiting,
        FinishedWaiting,
        Paused,
        DoingAction,
        FinishedDoingAction,
        End
    }
    public class TurnHandler : MonoBehaviour
    {
        #region private variables
        private SceneCombatTurnHandler _combatManager;
        private AIScript _aIScript;
        private TurnState _state = TurnState.None;
        private TurnState _stateBeforePause = TurnState.None;
        private Timer _waitTimer;
        private System.Action _onTurnComplete;
        #endregion

        #region public variables
        public TurnState State { get { return _state; } }
        #endregion

        #region private functions
        /// <summary>
        /// Default Awake method initializing some variables
        /// If either _combatManager is null,
        /// object will be destroyed.
        /// </summary>
        private void Awake()
        {
            _combatManager = FindObjectOfType<SceneCombatTurnHandler>();
            if (_combatManager == null)
            {
                Destroy(this);
            }

            _aIScript = gameObject.GetComponent<AIScript>();
            _aIScript.onCompleteScript += OnFinishedDoingAction;

            _waitTimer = new Timer();
            _waitTimer.onTimerEnd += HandleWaitTimerEnd;

           
        }

        private void Update()
        {
            switch (State)
            {
                case TurnState.Waiting:
                    OnWait();
                    break;
                default:
                    break;
            }
        }

        public void Reset()
        {
            _state = TurnState.None;
            _waitTimer.SetTime(0.0f);
        }

        private void HandleWaitTimerEnd()
        {
            OnFinishedWait();
        }

        private void OnWait() 
        {
            _waitTimer.Tick(Time.deltaTime);
        }


        private void OnFinishedWait()
        {
            _state = TurnState.FinishedWaiting;
            _combatManager.QueueTurn(new TurnMessage { turnHandler = this, waitAction = DoAction});
        }

        private void DoAction() 
        {
            _aIScript.RunAIScript();

            // TODO do something here to make sure the turn finishes
            // like adding a max timer...
        }

        private void OnFinishedDoingAction()
        {
            _state = TurnState.FinishedDoingAction;

            _combatManager.Next();
            StartWait();
            
        }

        private void OnDestroy()
        {
            _aIScript.onCompleteScript -= OnFinishedDoingAction;
        }
        #endregion private functions

        #region public functions
        public void StartCombat()
        {
            Reset();
            enabled = true;
            StartWait();
        }
        public void StartWait()
        {
            _state = TurnState.Waiting;
            _waitTimer.SetTime(UnityEngine.Random.Range(1f, 2f));
        }

        public void Pause()
        {
            _stateBeforePause = _state;
            _state = TurnState.Paused;
        }
        
        public void End()
        {
            enabled = false;
            _state = TurnState.End;
        }

        public void Resume()
        {
            _state = _stateBeforePause;
        }
        #endregion public functions
    }


}

