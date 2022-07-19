using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace SCS.Mio
{
    static class GameSceneConstants
    {
        public const string SCENE_DEFAULT = "Default";
        public const string SCENE_IDLE = "Idle";
        public const string SCENE_MISSION_SELECT = "MissionSelect";
        public const string SCENE_IN_MISSION = "InMission";
    }
    [System.Serializable]
    public enum GameScene
    {
        Default,
        Idle,
        MissionSelect,
        InMission
    }
    public interface ISceneController
    {
        public System.Action OnSceneChanged { get; }
    }

    public class SceneController : SingletonBehaviour<SceneController>, ISceneController
    {
        #region private variables
        private System.Action _onSceneChanged;
        [SerializeField] private GameScene _activeScene = GameScene.Idle;
        private GameScene _previousScene;
        #endregion private variables

        #region public variables
        public System.Action OnSceneChanged { get { return _onSceneChanged; } }
        public GameScene ActiveScene { get { return _activeScene; } }
        public GameScene PreviousScene { get { return _previousScene; } set { _previousScene = value; } }
        #endregion public variables

        #region private functions
        private void SetScene(GameScene scene, LoadSceneMode mode)
        {
            DOTween.KillAll();
            Instance.PreviousScene = scene;

            switch (scene)
            {
                case GameScene.Default:
                    SceneManager.LoadSceneAsync(GameSceneConstants.SCENE_DEFAULT, mode);
                    break;
                case GameScene.Idle:
                    SceneManager.LoadSceneAsync(GameSceneConstants.SCENE_IDLE, mode);
                    break;
                case GameScene.MissionSelect:
                    SceneManager.LoadSceneAsync(GameSceneConstants.SCENE_MISSION_SELECT, mode);
                    break;
                case GameScene.InMission:
                    SceneManager.LoadSceneAsync(GameSceneConstants.SCENE_IN_MISSION, mode);
                    break;
                default:
                    break;
            }

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var s = SceneManager.GetSceneAt(i);
                if (s != SceneManager.GetActiveScene())
                {
                    SceneManager.UnloadSceneAsync(s);
                }
            }
        }

        private void OnLoadDefault()
        {
            PlaceScene(GameScene.Idle);
        }
        #endregion private functions
        #region protected functions
        protected override void OnInitialize()
        {
            if(SceneManager.GetActiveScene().name == GameSceneConstants.SCENE_DEFAULT)
                ReturnHome();
            
        }
        #endregion protected functions
        #region public functions
        public void PlaceScene(string sceneName)
        {
            GameScene scene = (GameScene)System.Enum.Parse(typeof(GameScene), sceneName);
            SetScene(scene, LoadSceneMode.Additive);
        }

        public void PlaceScene(GameScene scene)
        {
            SetScene(scene, LoadSceneMode.Additive);
        }

        public void ReplaceScene(GameScene scene)
        {
            SetScene(scene, LoadSceneMode.Single);
        }

        public void ReplaceScene(string sceneName)
        {
            
            GameScene scene = (GameScene)System.Enum.Parse(typeof(GameScene), sceneName);
            SetScene(scene, LoadSceneMode.Single);
        }

        public void ReturnHome()
        {
            ReplaceScene(GameScene.Default);
            OnLoadDefault();
        }

        public static GameObject Create(GameObject prefab, GameScene scene)
        {
            var obj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            var scn = SceneManager.GetSceneByName(scene.ToString());
            switch (scene)
            {
                case GameScene.Idle:
                    break;
                case GameScene.MissionSelect:
                    break;
                case GameScene.InMission:
                    SceneManager.MoveGameObjectToScene(obj, scn);
                    break;
                default:
                    break;
            }

            return obj;
        }
/*        public void ChangeScene(Object sceneObject)
        {
            if (sceneObject == null)
            {
                Debug.Log("Scene is NULL");
                return;
            }

            var sceneName = sceneObject.name;
            SceneManager.LoadScene(sceneName);

            Debug.Log("Loaded Scene : " + sceneObject.name);

        }

        public void ChangeScene(string sceneName)
        {
            DOTween.KillAll();
            SceneManager.LoadScene(sceneName);

            Debug.Log("Loaded Scene : " + sceneName);
           
        }*/


        #endregion public functions
    }

}
