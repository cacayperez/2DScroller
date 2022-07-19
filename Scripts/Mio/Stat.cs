using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    [System.Serializable]
    public struct Stat<T>
    {
        public T Min;
        public T Max;
        public T Current;
    }
}