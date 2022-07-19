using System;
using UnityEngine;

namespace SCS.Mio.Character
{
    public enum CombatActionState
    {
        Attacking,
        ReceivingDamage,
        //ApplyingDamage,
        Idling,
        Evading,
        Blocking,
        TakingDefeat
    }

    public struct FCombatActionContext
    {
        public CombatActionState actionType;
        public ICombatSystem target;
        public int damage;
    }

    // To-Do maybe use a dictionary for storing event id's
    // instead of using enums...
    // using a unified event dispatcher....
    public interface ICombatEventDispatcher
    {
        public void Request(FCombatActionContext context);
        public void RequestComplete(CombatActionState type);
        public void Subscribe(CombatActionState actionType, System.Action action);
        public void Subscribe(CombatActionState actionType, System.Action<int> action);
        public void SubscribeToOnAttack(System.Action<ICombatSystem> action);
        public void SubscribeToCompletedAction(System.Action<CombatActionState> action);
        public void UnSubscribe(CombatActionState actionType, System.Action action);
        public void UnSubscribe(CombatActionState actionType, System.Action<int> action);
        public void UnSubscribe(CombatActionState actionType, System.Action<ICombatSystem> action);
        public void UnSubscribeToCompletedAction(System.Action<CombatActionState> action);
    }

    public class CharacterCombatEventDispatcher : MonoBehaviour, ICombatEventDispatcher
    {
        #region private variables
        private System.Action<CombatActionState> _onAnyActionCompleted;
        private System.Action<int> _onReceiveDamage;
/*        private System.Action _onApplyDamage;*/
        private System.Action<ICombatSystem> _onAttack;
        private System.Action _onEvade;
        private System.Action _onIdle;
        private System.Action _onBlock;
        private System.Action _onTakeDefeat;
        #endregion private variables

        #region private functions
        private void HandleOnAttack(ICombatSystem target)
        {
            _onAttack?.Invoke(target);
        }

        private void HandleOnReceiveDamage(int damage)
        {
            _onReceiveDamage?.Invoke(damage);
        }

/*        private void HandleOnApplyDamage()
        {
            _onApplyDamage?.Invoke();
        }
*/
        private void HandleOnIdle()
        {
            _onIdle?.Invoke();
        }

        private void HandleOnEvade()
        {
            _onEvade?.Invoke();
        }

        private void HandleOnBlock()
        {
            _onBlock?.Invoke();
        }

        private void HandleOnTakeDefeat()
        {
            _onTakeDefeat?.Invoke();
        }

        private void HandleAnyAction(CombatActionState type)
        {
            _onAnyActionCompleted?.Invoke(type);
        }

        private void OnDestroy()
        {
            _onAnyActionCompleted = null;
            _onReceiveDamage = null;
            //_onApplyDamage = null;
            _onAttack = null;
            _onEvade = null;
            _onIdle = null;
            _onBlock = null;
        }

        #endregion private functions

        #region public functions
        #region invoke functions
        public void Request(FCombatActionContext context)
        {
            switch (context.actionType)
            {
                case CombatActionState.Attacking:
                    HandleOnAttack(context.target);
                    break;
                case CombatActionState.ReceivingDamage:
                    HandleOnReceiveDamage(context.damage);
                    break;
                /*                case CombatActionState.ApplyingDamage:
                                    HandleOnApplyDamage();
                                    break;*/
                case CombatActionState.Idling:
                    HandleOnIdle();
                    break;
                case CombatActionState.Evading:
                    HandleOnEvade();
                    break;
                case CombatActionState.Blocking:
                    HandleOnBlock();
                    break;
                case CombatActionState.TakingDefeat:
                    HandleOnTakeDefeat();
                    break;
                default:
                    break;
            }
        }

        public void RequestComplete(CombatActionState type)
        {
            HandleAnyAction(type);
        }
        #endregion invoke functions

        #region subscribe functions
        public void Subscribe(CombatActionState actionType, System.Action action)
        {
            switch (actionType)
            {
/*                case CombatActionState.ApplyingDamage:
                    _onApplyDamage += action;
                    break;*/
                case CombatActionState.Idling:
                    _onIdle += action;
                    break;
                case CombatActionState.Evading:
                    _onEvade += action;
                    break;
                case CombatActionState.Blocking:
                    _onBlock += action;
                    break;                
                case CombatActionState.TakingDefeat:
                    _onTakeDefeat += action;
                    break;
                default:
                    break;
            }
        }

        public void Subscribe(CombatActionState actionType, System.Action<int> action)
        {
            switch (actionType)
            {
                case CombatActionState.ReceivingDamage:
                    _onReceiveDamage += action;
                    break;
                default:
                    break;
            }
        }

        public void SubscribeToOnAttack(Action<ICombatSystem> action)
        {
            _onAttack += action;
        }

        public void SubscribeToCompletedAction(System.Action<CombatActionState> action)
        {
            _onAnyActionCompleted += action;
        }
        #endregion subscribe functions

        #region unsubscribe functions
        public void UnSubscribe(CombatActionState actionType, System.Action action)
        {
            switch (actionType)
            {
/*                case CombatActionState.ApplyingDamage:
                    _onApplyDamage -= action;
                    break;*/
                case CombatActionState.Idling:
                    _onIdle -= action;
                    break;
                case CombatActionState.Evading:
                    _onEvade -= action;
                    break;
                case CombatActionState.Blocking:
                    _onBlock -= action;
                    break;
                case CombatActionState.TakingDefeat:
                    _onTakeDefeat -= action;
                    break;
                default:
                    break;
            }
        }

        public void UnSubscribe(CombatActionState actionType, System.Action<int> action)
        {
            switch (actionType)
            {
                case CombatActionState.ReceivingDamage:
                    _onReceiveDamage -= action;
                    break;
                default:
                    break;
            }
        }

        public void UnSubscribeToCompletedAction(System.Action<CombatActionState> action)
        {
            _onAnyActionCompleted -= action;
        }

        public void UnSubscribe(CombatActionState actionType, Action<ICombatSystem> action)
        {
            switch (actionType)
            {
                case CombatActionState.Attacking:
                    _onAttack -= action;
                    break;
            }
        }
        #endregion unsubscribe functions
        #endregion public functions
    }

}