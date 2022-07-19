using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SonicBloom.Koreo;
using SCS.Mio.Mission.Morale;

namespace SCS.Mio.Scene
{
    public interface ISceneNodeSpawner
    {
        public SceneRhythmHandler RhythmHandler { get; set; }
        public void AddEvent(KoreographyEvent koreoEvent);
        //public void Spawn(KoreographyEvent koreoEvent);
    }

    public class SceneNodeSpawner : MonoBehaviour, ISceneNodeSpawner
    {
        #region private variables
        [SerializeField] private NoteBase _nodePrefab;
        [SerializeField] private int _poolSize;
        private List<KoreographyEvent> koreoEvents;
        private Queue<INoteBase> _noteQueue;
        private int _pendingEventIdx;
        private SceneRhythmHandler _rhythmHandler;
        #endregion private variables

        #region  public variables
        public SceneRhythmHandler RhythmHandler { get { return _rhythmHandler; } set { _rhythmHandler = value; } }
        #endregion public variables

        #region private functions
        private void Awake()
        {
            koreoEvents = new List<KoreographyEvent>();
            _noteQueue = new Queue<INoteBase>();
            _pendingEventIdx = 0;
           PopulateNodes();
        }

        private void PopulateNodes()
        {
            if (_nodePrefab == null) return;

            for (int i = 0; i < _poolSize; i++)
            {
                var obj = GameObject.Instantiate<NoteBase>(_nodePrefab, Vector3.zero, Quaternion.identity, transform);
                if(obj != null)
                {
                    obj.RhythmHandler = _rhythmHandler;
                    obj.gameObject.SetActive(false);
                    _noteQueue.Enqueue(obj);
                }
            }
        }

        private void Update()
        {
            CheckNextSpawn();
        }

        private void Spawn(KoreographyEvent koreoEvent)
        {
            FPositionData positionData = _rhythmHandler.PositionHandler.FindRandomPosition();

            var node = _noteQueue.Dequeue();
            node.RhythmHandler = _rhythmHandler;
            node.Initialize(koreoEvent, positionData);

            _noteQueue.Enqueue(node);
        }

        private void CheckNextSpawn()
        {
            int currentTime = _rhythmHandler.DelayedSampleTime;
            int sampleRate = _rhythmHandler.SampleRate;

            while (_pendingEventIdx < koreoEvents.Count &&
                   koreoEvents[_pendingEventIdx].StartSample < currentTime + sampleRate)
            {
                KoreographyEvent evt = koreoEvents[_pendingEventIdx];
                Spawn(evt);
                _pendingEventIdx++;
            }
        }

        #endregion private functions

        #region public functions
        public void AddEvent(KoreographyEvent koreoEvent)
        {
            koreoEvents.Add(koreoEvent);
        }

        public void Clear(int newSampleTime = 0)
        {
            koreoEvents.Clear();
            _pendingEventIdx = 0;
        }

        public void FreePosition(int index)
        {
            _rhythmHandler.PositionHandler.FreePositionAt(index);
        }
        #endregion public functions
    }
}