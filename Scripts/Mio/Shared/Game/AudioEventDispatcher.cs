using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    public enum AudioEventType
    {
        MasterVolumeChange
    }

    public struct FAudioEventContext
    {
        public float prevVolume;
        public float newVolume;
    }
    public interface IAudioEventDispatcher
    {
        public void Subscribe(AudioEventType type, System.Action<FAudioEventContext> action);
        public void Unsubscribe(AudioEventType type, System.Action<FAudioEventContext> action);
        public void Send(AudioEventType type, FAudioEventContext context);
    }

    public class AudioEventDispatcher : MonoBehaviour, IAudioEventDispatcher
    {
        private System.Action<FAudioEventContext> _onMasterVolumeChange;

        public void Send(AudioEventType type, FAudioEventContext context)
        {
            switch (type)
            {
                case AudioEventType.MasterVolumeChange:
                    HandleMasterVolumeChange(context);
                    break;
                default:
                    break;
            }
        }

        public void Subscribe(AudioEventType type, Action<FAudioEventContext> action)
        {
            switch (type)
            {
                case AudioEventType.MasterVolumeChange:
                    _onMasterVolumeChange += action;
                    break;
                default:
                    break;
            }
        }

        public void Unsubscribe(AudioEventType type, Action<FAudioEventContext> action)
        {
            switch (type)
            {
                case AudioEventType.MasterVolumeChange:
                    _onMasterVolumeChange -= action;
                    break;
                default:
                    break;
            }
        }

        private void HandleMasterVolumeChange(FAudioEventContext context)
        {
            _onMasterVolumeChange?.Invoke(context);
        }



    }
}
