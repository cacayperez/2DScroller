using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    public interface IDataSystem
    {

    }

    public class DataSystem : MonoBehaviour, IDataSystem
    {
        //private System.Action _onDataUpdated;
        private CharacterData _data;

    }

}