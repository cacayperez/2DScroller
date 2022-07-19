using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    public enum SceneCombatEvent
    {
        TakingDamage
    }

    public struct SceneCombatMessage
    {
        public Vector3 position;
        public int damage;
    }

    public interface ISceneCombatEventDispatcher
    {
        public void Subscribe(SceneCombatEvent sceneCombatEvent, System.Action<Vector3, int> action);
        public void UnSubscribe(SceneCombatEvent sceneCombatEvent, System.Action<Vector3, int> action);
        public void Send(SceneCombatEvent sceneCombatEvent, SceneCombatMessage message);
    }

    public class SceneCombatEventDispatcher : MonoBehaviour, ISceneCombatEventDispatcher
    {
        #region private variables
        private System.Action<Vector3, int> _onDamage;


        #endregion private variables

        #region  public variables
        #endregion public variables

        #region private functions
        private void HandleOnDamage(SceneCombatMessage message)
        {
            _onDamage?.Invoke(message.position, message.damage);
        }
        #endregion private functions

        #region public functions
        public void Send(SceneCombatEvent sceneCombatEvent, SceneCombatMessage message)
        {
            switch (sceneCombatEvent)
            {
                case SceneCombatEvent.TakingDamage:
                    HandleOnDamage(message);
                    break;
                default:
                    break;
            }
        }

        public void Subscribe(SceneCombatEvent sceneCombatEvent, Action<Vector3, int> action)
        {
            switch (sceneCombatEvent)
            {
                case SceneCombatEvent.TakingDamage:
                    _onDamage += action;
                    break;
                default:
                    break;
            }
        }
        public void UnSubscribe(SceneCombatEvent sceneCombatEvent, Action<Vector3, int> action)
        {
            switch (sceneCombatEvent)
            {
                case SceneCombatEvent.TakingDamage:
                    _onDamage -= action;
                    break;
                default:
                    break;
            }
        }
        #endregion public functions
    }
}