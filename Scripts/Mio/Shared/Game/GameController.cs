using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SCS.Mio.Inventory;

namespace SCS.Mio
{
    static class Constants
    {
        public const string MissingString = "<Missing String>.";
    }


    public interface IGameController
    {
        public IGameEventDispatcher GameEventDispatcher { get; }
        public IInventoryManager InventoryManager { get; }
        public void PauseGame();
        public void ResumeGame();
        public void ExitGame();
        public void SubscribeAll(IGameRunnable runnable);
    }

    public class GameController : SingletonBehaviour<GameController>, IGameController
    {
        #region private variables
        [SerializeField] private MissionData _activeMission;
        private IGameEventDispatcher _eventDispatcher;
        private IInventoryManager _inventoryManager;
        #endregion private variables

        #region public variables
        public MissionData ActiveMission { get { return _activeMission; } set { _activeMission = value; } }
        public IGameEventDispatcher GameEventDispatcher { get { return _eventDispatcher; } }
       
        public IInventoryManager InventoryManager { get { return _inventoryManager; } }
        #endregion public variables

        #region public functions
        public void StartMission()
        {

            SceneController.Instance.ReplaceScene(GameScene.InMission);
        }

        public void EndMission()
        {
            _activeMission = null;
        }

        public void SubscribeAll(IGameRunnable runnable)
        {
            _eventDispatcher.Subscribe(GameEvent.Exit, runnable.OnExit);
            _eventDispatcher.Subscribe(GameEvent.Pause, runnable.OnPause);
            _eventDispatcher.Subscribe(GameEvent.Resume, runnable.OnResume);
        }

        public void UnSubscribeAll(IGameRunnable runnable)
        {
            _eventDispatcher.UnSubscribe(GameEvent.Exit, runnable.OnExit);
            _eventDispatcher.UnSubscribe(GameEvent.Pause, runnable.OnPause);
            _eventDispatcher.UnSubscribe(GameEvent.Resume, runnable.OnResume);
        }

        public void PauseGame()
        {
            _eventDispatcher.Request(GameEvent.Pause);
            Time.timeScale = 0;
            DOTween.PauseAll();
        }

        public void ResumeGame()
        {
            _eventDispatcher.Request(GameEvent.Resume);
            Time.timeScale = 1;
            DOTween.PlayForwardAll();
        }

        public void ExitGame()
        {
            _eventDispatcher.Request(GameEvent.Exit);
        }

        public void ExitMission()
        {
            Time.timeScale = 1;
            SceneController.Instance.ReturnHome();
        }
        #endregion public functions

        #region protected functions
        protected override void OnInitialize()
        {
            Application.targetFrameRate = 60;
#if UNITY_EDITOR
            Debug.Log("Running in Debug Mode");
#endif
            _eventDispatcher = gameObject.AddComponent(typeof(GameEventDispatcher)) as GameEventDispatcher;
            _inventoryManager = gameObject.AddComponent(typeof(InventoryManager)) as InventoryManager;


            if (DOTween.instance == null) DOTween.Init();

        }
        #endregion protected functions

        #region private functions
        #endregion private functions

    }
}


