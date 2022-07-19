using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCS.Mio.Data;

namespace SCS.Mio.UI
{
    public interface IUI_QuickInventoryTabContainer
    {
        public void LoadItems(ref List<FItem> items);
    }

    public class UI_QuickInventoryTabContainer : MonoBehaviour, IUI_QuickInventoryTabContainer
    {
        private ItemCategory _category;
        private List<FItem> _currentItems;
        [SerializeField] private UI_QuickInventoryItem _qIPrefab;

        private void Awake()
        {
            _currentItems = new List<FItem>();

        }
/*
        public void LoadItems(ItemCategory category, List<FItem> items)
        {
            _category = category;

            if (_qIPrefab == null) return;

            //StartCoroutine(OnLoadItemDelay());
            SetItems(items);
        }*/
        
        private void SetItems(List<FItem> items)
        {
            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }

            _currentItems.Clear();
            _currentItems = items;

            // TO-DO create a pool instead of instantiating?
            for (int i = 0; i < _currentItems.Count; i++)
            {
                UI_QuickInventoryItem obj = Instantiate<UI_QuickInventoryItem>(_qIPrefab);
                obj.transform.SetParent(transform);
            }
        }

        private IEnumerator OnLoadItemDelay()
        {
            yield return Helper.Yielder.Get(0.1f);
            OnLoadItemsComplete();
        }

        private void OnLoadItemsComplete()
        {

        }

        public void LoadItems(ref List<FItem> items)
        {
            StartCoroutine(OnLoadItemDelay());
        }
    }
}
