using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SCS.Mio.Character
{
    public interface IHealth
    {
        public int MaxHealth { get; }
        public int CurrentHealth { get; }
        public int MinHealth { get; }
    }

    public class HealthSystem : ComponentBase, IHealth
    {
        #region private variables
        private int _minHealth;
        [SerializeField] private int _maxHealth = 100;
        private int _currentHealth;
        private DataSystem _dataSystem;
        private CharacterCombatEventDispatcher _dispatcher;
        #endregion private variables

        #region public variables
        public int MaxHealth { get { return _maxHealth; } }
        public int MinHealth { get { return _minHealth; } }
        public int CurrentHealth { get { return _currentHealth; } }
        #endregion public variables

        private void Awake()
        {
            _dataSystem = GetComponent<DataSystem>();
            _dispatcher = GetComponent<CharacterCombatEventDispatcher>();
            _dispatcher.Subscribe(CombatActionState.ReceivingDamage, HandleOnReceiveDamage);

            InitializeHealth();
        }

        private void InitializeHealth()
        {
            _currentHealth = _maxHealth;
        }

        private void HandleOnReceiveDamage(int damage)
        {
            if(_currentHealth > 0)
            {
                ReduceHealth(damage);
            }

            if(_currentHealth <= 0)
            {
                _dispatcher.Request(new FCombatActionContext { actionType = CombatActionState.TakingDefeat });
            }
            
        }

        private void ReduceHealth(int amount)
        {
            _currentHealth = _currentHealth - amount;
            if (_currentHealth <= 0) _currentHealth = 0;
        }

        private void IncreaseHealth(int amount)
        {
            _currentHealth = _currentHealth + amount;
            if (_currentHealth >= _maxHealth) _currentHealth = _maxHealth;
        }
    }

}