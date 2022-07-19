using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace SCS.Mio.Mission
{
    public class InMissionManager : MonoBehaviour, IGameRunnable
    {
        #region private variables
        #region main components
        private IInMissionEventDispatcher _eventDispatcher;
        private IInMissionSystemDisplayHandler _progressHandler;
        private IInMissionVelocityHandler _velocityHandler;
        private IInMissionLoopHandler _missionLoopHandler;
        private IInMissionSpawnHandler _spawnHandler;
        private IInMissionStats _stats;
        private IInMissionBGHandler _bgHandler;
        #endregion main components

        #region serialized fields
        [SerializeField] private Transform _enemySpawnPoint;
        [SerializeField] private Canvas _enemyCanvas;
        #endregion serialized fields

        #endregion private variables

        #region  public variables
        public IInMissionEventDispatcher EventDispatcher { get { return _eventDispatcher; } }
        #endregion public variables
        
        #region private functions
        private void Awake()
        {
            _eventDispatcher = GetComponent<InMissionEventDispatcher>();
            _eventDispatcher ??= gameObject.AddComponent(typeof(InMissionEventDispatcher)) as InMissionEventDispatcher;

            _velocityHandler = GetComponent<InMissionVelocityHandler>();
            _velocityHandler ??= gameObject.AddComponent(typeof(InMissionVelocityHandler)) as InMissionVelocityHandler;

            _missionLoopHandler = GetComponent<InMissionLoopHandler>();
            _missionLoopHandler ??= gameObject.AddComponent(typeof(InMissionLoopHandler)) as InMissionLoopHandler;
            
            _spawnHandler = GetComponent<InMissionSpawnHandler>();
            _spawnHandler ??= gameObject.AddComponent(typeof(InMissionSpawnHandler)) as InMissionSpawnHandler;            
            
            _stats = GetComponent<InMissionStats>();
            _stats ??= gameObject.AddComponent(typeof(InMissionStats)) as InMissionStats;

            _bgHandler = GetComponent<InMissionBGHandler>();
            _bgHandler ??= gameObject.AddComponent(typeof(InMissionBGHandler)) as InMissionBGHandler;

            _progressHandler = GetComponent<InMissionSystemDisplayHandler>();
            _progressHandler = gameObject.AddComponent(typeof(InMissionSystemDisplayHandler)) as InMissionSystemDisplayHandler;

            _spawnHandler.SpawnPosition = _enemySpawnPoint.position;
            _spawnHandler.EnemyCanvas = _enemyCanvas;

            GameController.Instance.SubscribeAll(this);
      
        }

/*        private void HandleOnMissionStateChanged(MissionState state)
        {
            switch (state)
            {
*//*              case MissionState.Start_Begin:
                    break;
                case MissionState.Start_End:
                    break;
                case MissionState.Stroll_Begin:
                    break;
                case MissionState.Stroll_End:
                    break;
                case MissionState.Stroll_EndComplete:
                    break;
                case MissionState.Encounter_Begin:
                    break;
                case MissionState.Encounter_Spawned:
                    break;
                case MissionState.Encounter_End:
                    break;*//*
                case MissionState.Exit_Begin:
                    ExitMission();
                    break;
                case MissionState.Exit_End:
                    break;
                default:
                    break;
            }
        }

        private void ExitMission()
        {
            StartCoroutine(ExitDelay());
        }

        private IEnumerator ExitDelay()
        {
            // disables background tiles in scene
            _bgHandler.StopAll();

            yield return new WaitForSeconds(3);
            GameController.Instance.ExitMission();
        }*/

        #endregion private functions
        
        #region public functions

        public void SpawnEnemy()
        {
            var ctx = _missionLoopHandler.Encounter;
            _spawnHandler.Spawn(ctx.Prefab);
        }
        #region IGameRunnable implementation
        public void OnPause()
        {
            Debug.Log("Manager Pause");
        }

        public void OnResume()
        {
            
        }

        public void OnExit()
        {
            
        }
        #endregion IGameRunnable implementation
        #endregion public functions
    }
}