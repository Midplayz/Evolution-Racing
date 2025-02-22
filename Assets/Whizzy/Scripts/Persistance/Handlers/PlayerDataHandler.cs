using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HyperCasual.Runner;

public class PlayerDataHandler : MonoBehaviour
{
    [field: Header("Skins Count Data Handler")]
    [field: SerializeField] public List<bool> skinsOwnership { get; private set; } = new List<bool>{ true };
    [field: SerializeField] public List<int> skinsCost { get; private set; } = new List<int>{ 0 };

    public List<SkinsData> skinsDatas;
    SkinsCountData skinsCountData;
    public static PlayerDataHandler instance { get; private set; }
    public Action<int> OnSkinDataLoaded;
    public int activeSkins=0;
    private void Awake()
    {
        instance = this;
        if(GameManager.Instance!=null)
        {
            GameManager.Instance.OnGameStart += LoadData;
        }
    }
    private void LoadData()
    {
        skinsDatas = new();

        skinsCountData = SaveLoadManager.LoadData<SkinsCountData>();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<SkinsData>(out SkinsData skinsData))
            {
                skinsDatas.Add(skinsData);
                skinsCost.Add(skinsData.cost);
            }
        }

        if (skinsCountData != null)
        {
            skinsOwnership = skinsCountData.SkinsOwned;
            skinsCost = skinsCountData.Price;
            if (skinsOwnership.Count < skinsDatas.Count)
            {
                for (int i = 0; i < skinsOwnership.Count; i++)
                {
                    skinsDatas[i].SetSkinsOwnedOrNot(skinsOwnership[i]);
                    skinsDatas[i].setCost(skinsCost[i]);
                }
            }
            else if (skinsOwnership.Count > skinsDatas.Count)
            {
                for (int i = 0; i < skinsDatas.Count; i++)
                {
                    skinsDatas[i].SetSkinsOwnedOrNot(skinsOwnership[i]);
                    skinsDatas[i].setCost(skinsCost[i]);
                }
            }
            else
            {
                for (int i = 0; i < skinsOwnership.Count; i++)
                {
                    skinsDatas[i].SetSkinsOwnedOrNot(skinsOwnership[i]);
                    skinsDatas[i].setCost(skinsCost[i]);
                }
            }

        }
        
        OnSkinDataLoaded?.Invoke(activeSkins);
    }
    private void OnDisable()
    {
        SaveSkinsCount();
    }
    public void SaveSkinsCount()
    {
        skinsOwnership.Clear();
        skinsCost.Clear();
        for (int i=0;i<skinsDatas.Count;i++)
        {
            skinsOwnership.Add(skinsDatas[i].Is_Owned);
            skinsCost.Add(skinsDatas[i].cost);
        }
        skinsCountData = new SkinsCountData(this);
        SaveLoadManager.SaveData(skinsCountData);
    }
}
