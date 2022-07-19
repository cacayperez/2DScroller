using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    [System.Serializable]
    public struct FContextMessage
    {
        public string Sender;
        public string Message;
        public float Delay;
    }

    [System.Serializable]
    public enum ContextType
    {
        SystemMessage,
        Dialogue
    }

    //[CreateAssetMenu(fileName = "Context", menuName = "Mio/General/Context")]
    [System.Serializable]
    public class Context<TContextType> 
        where TContextType : System.Enum
    {
        public string ContextID;
        public TContextType ContextType;
        public FContextMessage[] Messages;
    }


}

