using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    public enum GameEvent
    {
        Resume,
        Pause,
        Exit
    }

    public interface IGameEventDispatcher
    {
        public void Subscribe(GameEvent gameEvent, System.Action action);
        public void UnSubscribe(GameEvent gameEvent, System.Action action);
        public void Request(GameEvent gameEvent);
    }

    public class GameEventDispatcher : MonoBehaviour, IGameEventDispatcher
    {
        #region private variables
        private System.Action _onStart;
        private System.Action _onPause;
        private System.Action _onExit;
        #endregion private variables
        
        #region  public variables
        #endregion public variables
        
        #region private functions
        private void HandleOnStart()
        {
            _onStart?.Invoke();
        }

        private void HandleOnPause()
        {
            _onPause?.Invoke();
        }

        private void HandleOnExit()
        {
            _onExit?.Invoke();
        }

        private void OnDestroy()
        {
            _onStart = null;
            _onPause = null;
            _onExit = null;
        }
        #endregion private functions

        #region public functions
        public void Subscribe(GameEvent gameEvent, System.Action action)
        {
            switch (gameEvent)
            {
                case GameEvent.Resume:
                    _onStart += action;
                    break;
                case GameEvent.Pause:
                    _onPause += action;
                    break;
                case GameEvent.Exit:
                    _onExit += action;
                    break;
                default:
                    break;
            }
        }

        public void UnSubscribe(GameEvent gameEvent, System.Action action)
        {
            switch (gameEvent)
            {
                case GameEvent.Resume:
                    _onStart -= action;
                    break;
                case GameEvent.Pause:
                    _onPause -= action;
                    break;
                case GameEvent.Exit:
                    _onExit -= action;
                    break;
                default:
                    break;
            }
        }
        public void Request(GameEvent gameEvent)
        {
            switch (gameEvent)
            {
                case GameEvent.Resume:
                    HandleOnStart();
                    break;
                case GameEvent.Pause:
                    HandleOnPause();
                    break;
                case GameEvent.Exit:
                    HandleOnExit();
                    break;
                default:
                    break;
            }
        }
        #endregion public functions
    }
}