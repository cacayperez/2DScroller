using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SCS.Mio.Inventory;
using SCS.Mio.Data;

namespace SCS.Mio.UI
{
    public interface IUI_QuickInventory
    {

        public void SelectCategory(ItemCategory category);
    }

    public class UI_QuickInventory : MonoBehaviour, IUI_QuickInventory
    {
        //private IInventoryManager _manager;
        private List<IUI_QuickInventoryTabContainer> _tabs;
        private List<FItem> _activeItems;
        private List<FItem> _items;

        [SerializeField] private ItemCategory _currentCategory;
        [SerializeField] private UI_QuickInventoryTabContainer _itemsContainer;

        private IInventoryManager InventoryManager
        {
            get
            {
                return GameController.Instance.InventoryManager;
            }
        }
        private Dictionary<int, string> Items { get { return GameController.Instance.InventoryManager.Items; } }

        private void Awake()
        {
            _activeItems = new List<FItem>();
            _items = new List<FItem>();
            InventoryManager.AddItemByID("item_001");
            InventoryManager.AddItemByID("ITEM_0001");
            LoadItems();
        }

        private void LoadItems()
        {
            _items.Clear();

            foreach (var key in Items.Keys)
            {
                string itemID = InventoryManager.GetItemIDAt(key);
               
                FItem itemData = DatabaseLoader.Instance.FindItemData(itemID);
                if(itemData.ID != null)
                {
                    itemData.Index = key;
                    _items.Add(itemData);
#if UNITY_EDITOR
                    Debug.Log("Found item ID: " + itemID);
#endif
                }
#if UNITY_EDITOR
                else
                {
                    Debug.LogWarning("Invalid item ID: " + itemID);
                }
#endif
            }
        }
 
        private void GetItemsByCategory(ItemCategory category, ref List<FItem> itemsList)
        {
            itemsList.Clear();
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i].Category == category)
                    itemsList.Add(_items[i]);
            }
        }


        public void SelectCategory(ItemCategory category)
        {
            _currentCategory = category;

            GetItemsByCategory(_currentCategory, ref _activeItems);

            _itemsContainer.LoadItems(ref _activeItems);
        }


    }
}
