
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCS.Mio.Data;
using System.Linq;

namespace SCS.Mio.Inventory
{
    public interface IInventoryManager
    {
        public Dictionary<int,string> Items { get; set; }
        public int Capacity { get; }
        public bool RemoveItemAt(int index);
        public int AddItem(FItem item, int key);
        public int AddItem(FItem item);
        public int AddItemByID(string itemID);
        public int AddItemByID(string itemID, int key);
        public string GetItemIDAt(int index);
        public string EmptyItemID { get; }
    }

    public class InventoryManager : MonoBehaviour, IInventoryManager
    {
        [SerializeField] private int _capacity = 256;
        //private Dictionary<int, FItem> _items;
        private Dictionary<int, string> _items;
        private int _lastAvailableIndex;

        /// <summary>
        /// 1 is the beginning index. 
        /// -1 is the empty index. 
        /// 0 is for uninitialized value for item.
        /// </summary>
        private int _startIndex = 1;    

        public Dictionary<int, string> Items { get { return _items; } set { _items = value; } }
        public int Capacity { get { return _capacity; } }
        public string EmptyItemID { get { return "EMPTY"; } }

        private void Awake()
        {
            _items = new Dictionary<int, string>(_capacity);
            
        }

        private int FindAvailableIndex()
        {
            int index = -1;
            if (_items == null) return index;

            int len = _capacity;
            for (int i = _startIndex; i < len; i++)
            {
                string item;
                bool occupied = _items.TryGetValue(i, out item);
                if(occupied == false)
                {
                    index = i;
                    return index;
                }
            }

            return index;
        }

        private void HandleAddItem(string itemID, int index)
        {
            if (index >= _startIndex)
            {
                _items.Add(index, itemID);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Index of item in stored Item Dictionary</returns>
        public int AddItem(FItem item)
        {
            int index = FindAvailableIndex();

            HandleAddItem(item.ID, index);

            return index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="key"> Optional: Specify index or slot to store item</param>
        /// <returns>Index of item in stored Item Dictionary</returns>
        public int AddItem(FItem item, int key = -1)
        {
            int index;

            // if no key is provided look for an empty slot
            if(key < 0)
            {
                index = FindAvailableIndex();
            }
            else
            {
                index = key;
            }

            HandleAddItem(item.ID, index);

            return index;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemID"></param>
        /// <returns>Index of item in stored Item Dictionary</returns>
        public int AddItemByID(string itemID)
        {
            int index = FindAvailableIndex();

            HandleAddItem(itemID, index);

            return index;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="key"> Optional: Specify index or slot to store item</param>
        /// <returns>Index of item in stored Item Dictionary</returns>
        public int AddItemByID(string itemID, int key = -1)
        {
            int index;

            // if no key is provided look for an empty slot
            if (key < 0)
            {
                index = FindAvailableIndex();
            }
            else
            {
                index = key;
            }

            HandleAddItem(itemID, index);

            return index;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns>Item Id</returns>
        public string GetItemIDAt(int index)
        {
            string itemID;
            if(_items.TryGetValue(index, out itemID))
            {
                return itemID;
            } else
            {
                return EmptyItemID;
            }

        }

        public bool RemoveItemAt(int index)
        {
            if (_items == null) return false;

            string itemID = GetItemIDAt(index);
            if(itemID != EmptyItemID)
            {
                _items.Remove(index);
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}