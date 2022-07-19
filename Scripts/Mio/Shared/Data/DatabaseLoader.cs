using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SCS.Mio.Data;

namespace SCS.Mio
{
    public interface IDatabaseLoader
    {
        public TextAsset ItemsTextAsset { get; set; }
        public List<FItem> Items { get; }
        public void LoadItemsDatabase();
        public FItem FindItemData(string ItemID);
    }

    public class DatabaseLoader : SingletonBehaviour<DatabaseLoader>, IDatabaseLoader
    {
        [SerializeField] private TextAsset _itemsTextAsset;
        [SerializeField] private List<FItem> _items;
        
        public List<FItem> Items { get { return _items; } }
        public TextAsset ItemsTextAsset { get { return _itemsTextAsset; } set { _itemsTextAsset = value; } }


        protected override void OnInitialize()
        {
#if UNITY_EDITOR
            Debug.Log(name + ": Loading Database.....");
#endif 
            LoadItemsDatabase();
        }

        public void LoadItemsDatabase()
        {
            ItemDataList itemList = Helper.JSONParser.Read<ItemDataList>(_itemsTextAsset);
            _items = new List<FItem>(itemList.Items.Length);
            if (itemList != null)
            {
                for (int i = 0; i < itemList.Items.Length; i++)
                {
                    Item ii = itemList.Items[i];

                    ItemCategory category = ItemCategory.Empty;

                    switch(ii.Category)
                    {
                        case "Food":
                            category = ItemCategory.Food;
                            break;
                        case "Toy":
                            category = ItemCategory.Toy;
                            break;
                        case "Book":
                            category = ItemCategory.Book;
                            break;
                        case "Misc":
                            category = ItemCategory.Misc;
                            break;
                        default:
                            break;
                    }

                    FItem itemData = new FItem
                    {
                        ID = ii.ID,
                        Name = ii.Name,
                        Description = ii.Description,
                        Category = category
                    };
                    _items.Add(itemData);
                }
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning("item list is null");
            }
#endif
        }

        public FItem FindItemData(string ItemID)
        {
            FItem result = Items.Find(x => x.ID == ItemID);
            return result;
        }
    }

}