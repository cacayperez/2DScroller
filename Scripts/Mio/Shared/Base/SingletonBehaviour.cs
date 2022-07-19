using System.Collections;
using UnityEngine;

namespace SCS.Mio
{
    public abstract class SingletonBehaviour<T> : MonoBehaviour
        where T : SingletonBehaviour<T>
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectsOfType<T>() as T;

                return _instance;
            }
            set
            {
                _instance = value;
            }
        }
        private static T _instance;


        
        protected virtual void Awake()
        {
            if(Instance == null)
            {
                Instance = this as T;
                DontDestroyOnLoad(this);
                OnInitialize();
            }
            else
            {
                Destroy(this);
            }
            
           
        }

        // Called on Awake
        protected abstract void OnInitialize();
             
    }

}