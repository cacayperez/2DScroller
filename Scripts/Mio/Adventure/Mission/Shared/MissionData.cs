using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    [System.Serializable]
    public struct MissionTask
    {
        public string Label;
        public float Duration;
        public float StartDelay;
        public float EndDelay;
    }

    [System.Serializable]
    public struct MissionInfo
    {
        public string ID;
        public string Title;
        public string Description;
    }



    [CreateAssetMenu(fileName = "Mission", menuName = "Mio/Mission/ChapterData")]
    public class MissionData : ScriptableObject
    {
        public MissionInfo GeneralInformation;
        public MissionPoint[] Points;


    }
}
