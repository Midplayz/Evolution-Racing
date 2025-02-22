using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OfflineEarnings : MonoBehaviour
{
    [Header("Offline Earnings Rates")]
    [SerializeField] private int _perHourCoinRate = 1;
    [SerializeField] private int _offlineCoins;
    [SerializeField] private int _maxOfflineEarningsHours = 3;

    [Header("Offline Earning UI")]
    [SerializeField] private GameObject _offlineEarningsPanel;
    [SerializeField] private TextMeshProUGUI _offlineCoinsText;
    private void Start()
    {
        if (!string.IsNullOrEmpty(UserDataHandler.instance.Date_And_Time))
            
        {
            CalculateOfflineEarnings();
            if(_offlineCoins>0)
            {
                _offlineCoinsText.text = _offlineCoins.ToString();
                _offlineEarningsPanel.SetActive(true);
            }
        }
    }
    private void CalculateOfflineEarnings()
    {
        DateTime lastLogIn = DateTime.Parse(UserDataHandler.instance.Date_And_Time);
        TimeSpan timeSpan = DateTime.Now - lastLogIn;
        if(timeSpan.TotalHours > _maxOfflineEarningsHours)
        {
            _offlineCoins = (_perHourCoinRate * _maxOfflineEarningsHours) * (int)MultiplierDataHandler.instance.Bonus_Multiplier;
        }
        else
        { 
        _offlineCoins = (_perHourCoinRate * (int)timeSpan.TotalHours) * (int)MultiplierDataHandler.instance.Bonus_Multiplier;
        }
    }
    public void OnClaiming()
    {
        _offlineEarningsPanel.SetActive(false);
        UserDataHandler.instance.NullifyDate();
        CurrencyDataHandler.instance.OnCoinAdded(_offlineCoins);
    }
}
