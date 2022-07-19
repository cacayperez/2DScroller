using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SCS.Mio.Mission
{
    public interface IInMissionSpawnHandler
    {
        public Vector3 SpawnPosition { get; set; }
        public Canvas EnemyCanvas { get; set; }
        public void Spawn(GameObject prefab);
    }

    public class InMissionSpawnHandler : MonoBehaviour, IInMissionSpawnHandler
    {
        #region private variables
        private IInMissionEventDispatcher _dispatcher;
        private Vector3 _defaultSpawnPosition;
        private Canvas _enemyCanvas;
        #endregion private variables

        #region  public variables
        public Vector3 SpawnPosition { get { return _defaultSpawnPosition; } set { _defaultSpawnPosition = value; } }
        public Canvas EnemyCanvas { get { return _enemyCanvas; } set { _enemyCanvas = value; } }
        #endregion public variables

        #region private functions
        private void Awake()
        {
            _dispatcher = gameObject.GetComponent<InMissionEventDispatcher>();
        }

        public void Spawn(GameObject prefab)
        {
            if(prefab != null)
            {
                var enemy = Instantiate(prefab, _defaultSpawnPosition, Quaternion.identity);
                var health = enemy.GetComponent<UICharacter.UI_Health>();
                health.TargetCanvas = _enemyCanvas;
                _dispatcher.Request(MissionState.Encounter_Spawned);
            }
        }
        #endregion private functions
        
        #region public functions
        #endregion public functions
    }
}