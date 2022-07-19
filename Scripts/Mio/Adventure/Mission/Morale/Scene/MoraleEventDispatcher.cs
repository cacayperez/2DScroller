using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SCS.Mio.Scene
{
    public enum MoraleEventState
    {
        MoraleChanged
    }

    public interface IMoraleEventDispatcher
    {
        public void Subscribe(MoraleEventState state, System.Action<int> action);
        public void UnSubscribe(MoraleEventState state, System.Action<int> action);
        public void Send(MoraleEventState state, int value);
    }

    class MoraleEventDispatcher : MonoBehaviour, IMoraleEventDispatcher
    {
        private System.Action<int> _onMoraleStateChanged;

        public void Subscribe(MoraleEventState state, Action<int> action)
        {
            switch (state)
            {
                case MoraleEventState.MoraleChanged:
                    _onMoraleStateChanged += action;
                    break;
                default:
                    break;
            }
        }

        public void UnSubscribe(MoraleEventState state, Action<int> action)
        {
            switch (state)
            {
                case MoraleEventState.MoraleChanged:
                    _onMoraleStateChanged -= action;
                    break;
                default:
                    break;
            }
        }

        public void Send(MoraleEventState state, int value)
        {
            switch (state)
            {
                case MoraleEventState.MoraleChanged:
                    HandleOnMoraleStateChanged(value);
                    break;
                default:
                    break;
            }
        }

        private void HandleOnMoraleStateChanged(int value)
        {
            _onMoraleStateChanged?.Invoke(value);
        }

    }
}
