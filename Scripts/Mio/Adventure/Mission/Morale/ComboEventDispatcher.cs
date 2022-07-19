using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Scene
{
    public interface IComboEventDispatcher
    {
        public void Subscribe(System.Action<int> action);
        public void UnSubscribe(System.Action<int> action);
        public void Send(int value);
    }
    public class ComboEventDispatcher : MonoBehaviour, IComboEventDispatcher
    {
        private System.Action<int> _onComboAction;

        private void HandleOnComboAction(int value)
        {
            _onComboAction?.Invoke(value);
        }

        public void Send(int value)
        {
            HandleOnComboAction(value);
        }

        public void Subscribe(Action<int> action)
        {
            _onComboAction += action;
        }

        public void UnSubscribe(Action<int> action)
        {
            _onComboAction -= action;
        }
    }
}
