using HyperCasual.Runner;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesUIManager : MonoBehaviour
{
    [Header("Upgrades")]
    [SerializeField] private float _minMultiplier = 1.1f;
    [SerializeField] private float _maxMultiplier = 1.5f;
    [SerializeField] private float _maxPerformanceMultiplier = 6f;
    [SerializeField] private float _maxBoostMultiplier = 5f;
    [SerializeField] private float _maxBonusMultiplier = 5f;
    [SerializeField] private TextMeshProUGUI _performanceLevel;
    [SerializeField] private TextMeshProUGUI _performanceUpgradeCost;
    [SerializeField] private TextMeshProUGUI _boostLevel;
    [SerializeField] private TextMeshProUGUI _boostUpgradeCost;
    [SerializeField] private TextMeshProUGUI _bonusLevel;
    [SerializeField] private TextMeshProUGUI _bonusUpgradeCost;
    public Action OnCoinChanged;
    private void Awake()
    {
        OnCoinChanged += UpdateInteractable;
    }
    private void OnDisable()
    {
        OnCoinChanged -= UpdateInteractable;
    }

    public void PerformanceUpgraded()
    {
        UpdateUIOnPurchase(MultiplierDataHandler.instance.Performance_Multiplier, _maxPerformanceMultiplier, MultiplierDataHandler.instance.Performance_Upgrade_Cost, GameManager.Instance.OnUpgradedPerformance);
    }
    public void BoostUpgraded()
    {
        UpdateUIOnPurchase(MultiplierDataHandler.instance.Boost_Multiplier, _maxBoostMultiplier, MultiplierDataHandler.instance.Boost_Upgrade_Cost, GameManager.Instance.OnUpgradedBoost);
    }
    public void BonusUpgraded()
    {
        UpdateUIOnPurchase(MultiplierDataHandler.instance.Bonus_Multiplier, _maxBonusMultiplier, MultiplierDataHandler.instance.Bonus_Upgrade_Cost, GameManager.Instance.OnUpgradedBonus);
    }
    private void UpdateUIOnPurchase(float multiplier, float maxMultiplier, int upgradeCostMain, Action action)
    {
        
        if (multiplier != maxMultiplier && CurrencyDataHandler.instance.Coins >= upgradeCostMain)
        {
            action?.Invoke();
            CurrencyDataHandler.instance.OnPurchasedWithCoin(upgradeCostMain);
        }
    }
    private void UpdateUIOnStart (float multiplier, float maxMultiplier, float levelTextMain, int upgradeCostMain, TextMeshProUGUI levelText, TextMeshProUGUI upgradeCost)
    {
        if (multiplier == maxMultiplier)
        {
            levelText.text = "MAX LEVEL";
            upgradeCost.gameObject.transform.parent.gameObject.SetActive(false);
            levelText.GetComponentInParent<Button>().interactable = false;
        }
        else if (multiplier != maxMultiplier && CurrencyDataHandler.instance.Coins >= upgradeCostMain)
        {
            levelText.GetComponentInParent<Button>().interactable = true;
            levelText.text = "LVL: " + levelTextMain;
            upgradeCost.text = upgradeCostMain.ToString();
        }
        else if (multiplier != maxMultiplier && CurrencyDataHandler.instance.Coins < upgradeCostMain)
        {
            levelText.GetComponentInParent<Button>().interactable = false;
            levelText.text = "LVL: " + levelTextMain;
            upgradeCost.text = upgradeCostMain.ToString();
        }
    }
    private void UpdateInteractable()
    {
        UpdateUIOnStart(MultiplierDataHandler.instance.Performance_Multiplier, _maxPerformanceMultiplier, MultiplierDataHandler.instance.Performance_Level, MultiplierDataHandler.instance.Performance_Upgrade_Cost, _performanceLevel, _performanceUpgradeCost);

        UpdateUIOnStart(MultiplierDataHandler.instance.Boost_Multiplier, _maxBoostMultiplier, MultiplierDataHandler.instance.Boost_Level, MultiplierDataHandler.instance.Boost_Upgrade_Cost, _boostLevel, _boostUpgradeCost);

        UpdateUIOnStart(MultiplierDataHandler.instance.Bonus_Multiplier, _maxBonusMultiplier, MultiplierDataHandler.instance.Bonus_Level, MultiplierDataHandler.instance.Bonus_Upgrade_Cost, _bonusLevel, _bonusUpgradeCost);
    }
}
