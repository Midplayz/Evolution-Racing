using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class SkinsCountData
{
    public List<bool> SkinsOwned;
    public int activeIndex;
    public List<int> Price;
    public SkinsCountData(PlayerDataHandler skinsDataHandler)
    {
        SkinsOwned = skinsDataHandler.skinsOwnership;
        activeIndex = skinsDataHandler.activeSkins;
        Price = skinsDataHandler.skinsCost;
    }
}
