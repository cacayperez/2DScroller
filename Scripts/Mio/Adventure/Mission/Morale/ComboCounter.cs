using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Scene
{
    public interface IComboCounter
    {
        public int HitCount { get; }
        public IComboEventDispatcher EventDispatcher { get; }
        public void IncrementCombo();
        public void ResetCombo();
    }

    public class ComboCounter : MonoBehaviour, IComboCounter
    {
        [SerializeField] private int _hitCount = 0;
        private IComboEventDispatcher _eventDispatcher;

        public int HitCount { get { return _hitCount; } }

        public IComboEventDispatcher EventDispatcher { get { return _eventDispatcher; } }

        private void Awake()
        {
           _eventDispatcher = gameObject.AddComponent<ComboEventDispatcher>();
        }

        public void IncrementCombo()
        {
            _hitCount++;

            _eventDispatcher.Send(_hitCount);
            Debug.Log("Combo Hit! " + _hitCount);
        }

        public void ResetCombo()
        {
            _hitCount = 0;
            _eventDispatcher.Send(_hitCount);
        }
    }

}