using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Character
{
    public interface IDamageHandler
    {

    }
    public class DamageHandler : MonoBehaviour, IDamageHandler
    {
        #region private variables
        private ICombatEventDispatcher _eventDispatcher;
        #endregion private variables

        public int damage = 0;
        #region private functions
        private void Awake()
        {
            _eventDispatcher = gameObject.GetComponent<CharacterCombatEventDispatcher>();
            _eventDispatcher?.SubscribeToOnAttack(OnAttack);
            
        }

        private void OnAttack(ICombatSystem target)
        {
            //TODO compute actual damage based on stats
            var computedDamage = damage;
            target.ReceiveDamage(computedDamage);

        }
        #endregion private functions
    }
}