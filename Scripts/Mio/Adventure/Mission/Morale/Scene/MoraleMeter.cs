using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Scene
{
    public enum MoraleQuality
    {
        Ok,
        Good,
        Great
    }

    public interface IMoraleMeter
    {
        public int CurrentMorale { get; }
        public IMoraleEventDispatcher EventDispatcher { get; }
        public void Increment(MoraleQuality quality);
    }

    public class MoraleMeter : MonoBehaviour, IMoraleMeter
    {
        private int _currentMorale = 0;
        private IMoraleEventDispatcher _eventDispatcher;

        public int CurrentMorale { get { return _currentMorale; } }
        public IMoraleEventDispatcher EventDispatcher{ get { return _eventDispatcher; } }

        private void Awake()
        {
            _eventDispatcher = gameObject.AddComponent(typeof(MoraleEventDispatcher)) as MoraleEventDispatcher;
        }

        public void Increment(MoraleQuality quality)
        {
            int value = 1;

            switch (quality)
            {
                case MoraleQuality.Ok:
                    value = 1;
                    break;
                case MoraleQuality.Good:
                    value = 2;
                    break;
                case MoraleQuality.Great:
                    value = 4;
                    break;
                default:
                    break;
            }

            _currentMorale = _currentMorale + value;
            _currentMorale = (_currentMorale > 100) ? 100 : _currentMorale;

            _eventDispatcher.Send(MoraleEventState.MoraleChanged, _currentMorale);
        }



     
    }

}