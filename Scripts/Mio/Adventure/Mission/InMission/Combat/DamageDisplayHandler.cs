using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SCS.Mio
{
    public interface IDamageDisplayHandler
    {
        public void ShowDamage(Vector3 position, int damage);
    }

    public class DamageDisplayHandler : MonoBehaviour, IDamageDisplayHandler
    {
        #region private variables
        [SerializeField] private int _poolSize;
        [SerializeField] private GameObject prefab;
        private SceneCombatEventDispatcher _dispatcher;
        private Queue<IUI_DamageDisplay> _queue;
        //private List<IUI_DamageDisplay> _ddObjects;
        #endregion private variables

        #region  public variables
        #endregion public variables

        #region private functions
        private void Awake()
        {
            if(prefab != null)
            {
                _queue = new Queue<IUI_DamageDisplay>();
                for (int i = 0; i <= _poolSize; i++)
                {
                    GameObject obj = Instantiate(prefab);
                    var dd = obj.GetComponent<UI_DamageDisplay>();
                    dd.Init();
                    _queue.Enqueue(dd);
                    //_ddObjects.Add(dd);
                }
            }

            _dispatcher = FindObjectOfType<SceneCombatEventDispatcher>();

#if UNITY_EDITOR
            if (_dispatcher == null)
                Debug.Log("Could not find an instance of SceneCombatEventDispatcher");
#endif

            _dispatcher.Subscribe(SceneCombatEvent.TakingDamage, ShowDamage);
        }
        #endregion private functions

        #region public functions
        private IEnumerator DelayDamageDisplay(Vector3 position, int damage)
        {
            yield return Helper.Yielder.Get(0.5f);
            var dd = _queue.Dequeue();
            dd.SetDamage(position, damage);
            _queue.Enqueue(dd);
        }
        public void ShowDamage(Vector3 position, int damage)
        {
            StartCoroutine(DelayDamageDisplay(position, damage));
        }
        #endregion public functions
    }
}