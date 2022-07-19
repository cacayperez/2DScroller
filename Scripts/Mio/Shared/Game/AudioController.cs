using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    public class AudioController : SingletonBehaviour<AudioController>
    {
        private IAudioEventDispatcher _eventDispatcher;
        public IAudioEventDispatcher EventDispatcher { get { return _eventDispatcher; } }


        protected override void OnInitialize()
        {
            _eventDispatcher = gameObject.AddComponent(typeof(AudioEventDispatcher)) as AudioEventDispatcher;
        }
    }
}

