using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    [System.Serializable]
    public enum EncounterContextType
    {
        Friendly,
        Minion,
        Boss
    }

    //[CreateAssetMenu(fileName = "Encounter", menuName = "Mio/Mission/Context/Encounter")]
    [System.Serializable]
    public class EncounterContext : Context<EncounterContextType>
    {
        public string EncounterID;
        public GameObject Prefab;
    }

}