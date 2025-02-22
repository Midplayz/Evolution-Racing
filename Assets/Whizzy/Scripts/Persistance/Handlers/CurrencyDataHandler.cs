using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class CurrencyDataHandler : MonoBehaviour
{
    [field: Header("Currency Handler")]
    [field: SerializeField] public int Coins { get; private set; } = 200;
    [SerializeField] private UpgradesUIManager _upgradesUIManager;
    [SerializeField] private TextMeshProUGUI _coinsAmount;
    public static CurrencyDataHandler instance;
    public Action<int> OnCoinAdded, OnPurchasedWithCoin;
    CurrencyData currencyData;
    private void Awake()
    {
        instance = this;
        OnPurchasedWithCoin += SubtractCoin;
        OnCoinAdded += AddCoins;
        currencyData = SaveLoadManager.LoadData<CurrencyData>();
        if (currencyData != null)
        {
            Coins = currencyData.coins;
        }
        _coinsAmount.text = Coins.ToString();
    }
    private void Start()
    {
        _upgradesUIManager.OnCoinChanged?.Invoke();
    }
    public void AddCoins(int value)
    {
        Coins += value;
        _coinsAmount.text = Coins.ToString();
        SaveCurrency();
        _upgradesUIManager.OnCoinChanged?.Invoke();
    }
    public void SubtractCoin(int value)
    {
        Coins -= value;
        if (Coins < 0)
        {
            Coins = 0;
        }
        _coinsAmount.text = Coins.ToString();
        SaveCurrency();
        _upgradesUIManager.OnCoinChanged?.Invoke();
    }
    public void SaveCurrency()
    {
        currencyData = new CurrencyData(this);
        SaveLoadManager.SaveData(currencyData);
    }
    //private void OnDisable()
    //{
    //    SaveCurrency();
    //}
    private void Update()
    {
    }
}
