using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    [System.Serializable]
    public enum IntroContextType
    {
        Friendly,
    }

    [System.Serializable]
    public class IntroContext : Context<ContextType>
    {
    }
}
