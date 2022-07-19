using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{

    [System.Serializable]
    public enum MissionPointType
    {
        SystemMessage,
        Dialogue,
        Player_Prompt,
        Encounter
    }

    public interface IMissionPoint
    {
        public float TotalDistance { get; set; }
        public MissionPointType PointType { get; set; }
        public EncounterContext EncounterContext { get; }
    }

    [CreateAssetMenu(fileName = "Encounter", menuName = "Mio/InMission/MissionPoint")]
    public class MissionPoint : ScriptableObject, IMissionPoint
    {
        [SerializeField] private float _totalDistance = 50;

        [SerializeField] private MissionPointType _pointType;
        [SerializeField] private IntroContext _introContext;
        [SerializeField] private EncounterContext[] _encounters;
        [SerializeField] private OutroContext _OutroContext;

        public float TotalDistance { get => _totalDistance; set => _totalDistance = value; }
        public MissionPointType PointType { get => _pointType; set => _pointType = value; }

        public EncounterContext EncounterContext => GetEncounter();

        private EncounterContext GetEncounter()
        {
            if(_encounters.Length > 1)
            {
                return GetRandomEncounter();
            }
            else
            {
                return _encounters[0];
            }
        }

        private EncounterContext GetRandomEncounter()
        {
            return _encounters[0];
        }
    }
}