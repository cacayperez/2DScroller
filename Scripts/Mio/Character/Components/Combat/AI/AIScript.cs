using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SCS.Mio.Character
{
    public class AIScript : MonoBehaviour
    {
        #region private variables
        private System.Action _onComplete;
        private CharacterCombatEventDispatcher _dispatcher;
        private TargetHandler _targetHandler;
        #endregion private variables

        #region public variables
        public System.Action onCompleteScript;
        #endregion public variables

        #region private functions
        private void Awake()
        {
            _dispatcher = GetComponent<CharacterCombatEventDispatcher>();
            _dispatcher.SubscribeToCompletedAction(HandleCompletedAction);

            _targetHandler = GetComponent<TargetHandler>();

#if UNITY_EDITOR
            if (_targetHandler == null) Debug.Log("TARGET HANDLER ITS NULL");
#endif
        }

        private void Attack(in ICombatSystem target)
        {
            _dispatcher.Request(new FCombatActionContext { actionType = CombatActionState.Attacking, target = target });
        }

        private void HandleCompletedAction(CombatActionState state)
        {
            switch (state)
            {
                case CombatActionState.Attacking:
                    onCompleteScript?.Invoke();
                    break;
                default:
                    break;
            }
        }
        #endregion private functions

        #region public functions
        public virtual void RunAIScript()
        {
            var hostileTarget = _targetHandler.FindHostileTarget();

            if (hostileTarget != null) Attack(hostileTarget);
            else Debug.Log("Target is null");
            //
        }
        #endregion public functions

    }
}
