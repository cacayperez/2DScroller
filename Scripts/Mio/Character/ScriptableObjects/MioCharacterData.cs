using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    [CreateAssetMenu(fileName = "Mio", menuName = "Mio/CharacterData/Mio")]
    public class MioCharacterData : CharacterData
    {
        private Stat<float> Happiness;
        private Stat<float> Affection;
        private Stat<float> Fatigue;
    }
}

