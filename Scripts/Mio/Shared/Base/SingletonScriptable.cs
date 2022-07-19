using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonScriptable<T> : ScriptableObject where T : SingletonScriptable<T>
{
    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectsOfType<T>() as T;
             
            return instance;
        }
        set
        {
            instance = value;
        }
    }
    private static T instance;
}
