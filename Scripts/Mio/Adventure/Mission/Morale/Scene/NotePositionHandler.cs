using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio.Scene
{
    public struct FPositionData
    {
        public int key;
        public bool occupied;
        public Vector3 position;
    }
    public class NotePositionHandler : MonoBehaviour
    {
        [SerializeField] private GameObject _parent;
        [SerializeField] private Dictionary<int, FPositionData> _positionMap;
        //private int _trackedIdx = 0;
        //private int[] _trackedPattern;

        // Start is called before the first frame update
        private void Awake()
        {
            InitializeLocations();
        }

        private void InitializeLocations()
        { 
            if (_parent == null) return;
            _positionMap = new Dictionary<int, FPositionData>(40);

            int length = _parent.transform.childCount;

            for (int i = 0; i < length; i++)
            {
                var pos = _parent.transform.GetChild(i).position;
                var newpos = new Vector3(pos.x, pos.y, 0);
                FPositionData data =
                new FPositionData
                {
                    key = i,
                    position = newpos,
                    occupied = false
                };
                _positionMap.Add(i, data);
            }
        }

        public bool FindAvailablePosition(out FPositionData positionData)
        {
            bool found = false;
            for (int i = 0; i < _positionMap.Count; i++)
            {
                var data = _positionMap[i];

                if(data.occupied == false)
                {
                    positionData = data;
                    data.occupied = true;
                    return true;
                }

            }
            positionData = new FPositionData { }; 
            return found;
        }

        public FPositionData FindRandomPosition()
        {
            var index = Random.Range(0, _positionMap.Count-1);
            return _positionMap[index];
        }

        public void FreePositionAt(int index)
        {
            
            if (index >= _positionMap.Count || index < 0) return;
            var data = _positionMap[index];
            data.occupied = false;
            _positionMap[index] = data;
        }
        
    }
}

