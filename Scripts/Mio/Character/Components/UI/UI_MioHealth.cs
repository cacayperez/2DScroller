using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SCS.Mio.Character;

namespace SCS.Mio.UICharacter
{
    public class UI_MioHealth : UIBase, IUIHealth
    {
        [SerializeField] private GameObject _uIHealthPrefab;
        private Character.HealthSystem _healthSystem;
        private Character.CharacterCombatEventDispatcher _dispatcher;
        
        private Slider _slider;

        public HealthSystem HealthSystem { get { return _healthSystem; } }
        public CharacterCombatEventDispatcher EventDispatcher { get { return _dispatcher; } }

        private void Awake()
        {
            if(_uIHealthPrefab != null)
            {
                _slider = _uIHealthPrefab.gameObject.GetComponent<Slider>();
            }

            _healthSystem = gameObject.GetComponent<Character.HealthSystem>();
            _dispatcher = gameObject.GetComponent<Character.CharacterCombatEventDispatcher>();

#if UNITY_EDITOR
            if (_healthSystem == null) Debug.LogWarning("Warning, Health System is null: " + gameObject.name);
            if (_dispatcher == null) Debug.LogWarning("Warning, Dispatcher System is null: " + gameObject.name);
#endif

            _dispatcher.Subscribe(CombatActionState.ReceivingDamage, OnHealthUpdated);
        }

        private void OnHealthUpdated(int damage)
        {
            var max = _healthSystem.MaxHealth;
            var cur = _healthSystem.CurrentHealth;
            float percent = (float)cur / max;
            _slider.value = percent;
        }

        private void OnDestroy()
        {
            
        }
    }

}