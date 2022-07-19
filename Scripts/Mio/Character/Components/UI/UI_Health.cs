using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SCS.Mio.Character;

namespace SCS.Mio.UICharacter
{
    public interface IUIHealth
    {
        public Character.HealthSystem HealthSystem { get; }
        public Character.CharacterCombatEventDispatcher EventDispatcher { get; }
    }
    public class UI_Health : UIBase, IUIHealth
    {
        #region private variables
        private Character.HealthSystem _healthSystem;
        private Character.CharacterCombatEventDispatcher _dispatcher;
        [SerializeField] private GameObject _uIHealthPrefab;
        [SerializeField] private Transform _healthBarPosition;
        [SerializeField] private Canvas _targetCanvas;
        private Slider _slider;
        private GameObject _uIHealthInstance;
        #endregion public variables

        #region public variables
        public Canvas TargetCanvas { get { return _targetCanvas; } set { _targetCanvas = value; } }
        public HealthSystem HealthSystem { get { return _healthSystem; } }
        public CharacterCombatEventDispatcher EventDispatcher { get { return _dispatcher; } }
        #endregion public variables

        private void Awake()
        {
            var canvas = GameObject.FindWithTag("HUD");
            if (canvas != null)
            {
                Vector3 rawPosition = Camera.main.ScreenToWorldPoint(gameObject.transform.position);
                _uIHealthInstance = Instantiate(_uIHealthPrefab, canvas.transform);
                _uIHealthInstance.transform.position = _healthBarPosition.position;
                
                _slider = _uIHealthInstance.gameObject.GetComponent<Slider>();
            }

            // Set component dependencies
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
            float percent = (float) cur / max;
            _slider.value = percent;
        }

        private void OnDestroy()
        {
            Destroy(_uIHealthInstance);
        }
    }

}