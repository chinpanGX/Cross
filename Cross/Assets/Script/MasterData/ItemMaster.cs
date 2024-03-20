using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MasterData
{
    [CreateAssetMenu(fileName = "ItemMaster", menuName = "MasterData/Item")]
    public class ItemMaster : ScriptableObject
    {
        [SerializeField]
        private List<ItemData> itemData = new ();
        
        public ItemData Get(int id)
        {
            MasterDataUtility.CheckInvalidId(id, itemData.Count);
            return itemData[id];
        }
        
        public List<ItemData> GetItemListByName(string itemName)
        {
            return itemData.Where(x => x.Name == itemName).ToList();
        }
    }


    [Serializable]
    public struct ItemData
    {
        [SerializeField] private int id;
        [SerializeField] private string name;
        [SerializeField] private string description;

        public int Id => id;
        public string Name => name;
        public string Description => description;
    }
}