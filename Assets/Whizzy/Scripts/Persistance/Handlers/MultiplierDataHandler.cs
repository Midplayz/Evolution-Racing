using HyperCasual.Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierDataHandler : MonoBehaviour
{
    [field: Header("Multiplier Data Handler (Saved)")]
    public static MultiplierDataHandler instance;
    [field: SerializeField] public float Performance_Multiplier { get; private set; } = 1f;
    [field: SerializeField] public int Performance_Level { get; private set; } = 0;
    [field: SerializeField] public int Performance_Upgrade_Cost { get; private set; } = 100;
    [field: SerializeField] public float Boost_Multiplier { get; private set; } = 1.1f;
    [field: SerializeField] public int Boost_Level { get; private set; } = 0;
    [field: SerializeField] public int Boost_Upgrade_Cost { get; private set; } = 100;
    [field: SerializeField] public float Bonus_Multiplier { get; private set; } = 1f;
    [field: SerializeField] public int Bonus_Level { get; private set; } = 0;
    [field: SerializeField] public int Bonus_Upgrade_Cost { get; private set; } = 100;

    [field: Header("Upgrade Increase Rate and Cost Multiplier (Not Saved)")]
    //How much will each upgrade affect the current values of the property
    [field: SerializeField] private float _performanceMultiplierIncreaseRate = 0.1f;
    [field: SerializeField] private float _boostMultiplierIncreaseRate = 0.1f;
    [field: SerializeField] private float _bonusMultiplierIncreaseRate = 0.1f;
    //How much the prices of the Upgrades Should Increase
    [field: SerializeField] private float _minCostMultiplier = 1.0f;
    [field: SerializeField] private float _maxCostMultiplier = 1.5f;
    [field: SerializeField] private float _costMultiplier = 1.5f;

    MultiplierData multiplierData;
    private void Awake()
    {
        instance= this;
        multiplierData = SaveLoadManager.LoadData<MultiplierData>();
        if (multiplierData != null)
        {
            Performance_Multiplier = multiplierData.Performance_Multiplier;
            Performance_Level = multiplierData.Performance_Level;
            Performance_Upgrade_Cost = multiplierData.Performance_Upgrade_Cost;

            Boost_Multiplier = multiplierData.Boost_Multiplier;
            Boost_Level = multiplierData.Boost_Level;
            Boost_Upgrade_Cost = multiplierData.Boost_Upgrade_Cost;

            Bonus_Multiplier = multiplierData.Bonus_Multiplier;
            Bonus_Level = multiplierData.Bonus_Level;
            Bonus_Upgrade_Cost = multiplierData.Bonus_Upgrade_Cost;
        }
    }
    private void Start()
    {
        GameManager.Instance.OnUpgradedPerformance += UpdatePerformanceMultiplierAndSave;
        GameManager.Instance.OnUpgradedBoost += UpdateBoostMultiplierAndSave;
        GameManager.Instance.OnUpgradedBonus += UpdateBonusMultiplierAndSave;
    }

    public void SaveMultiplier()
    {
        multiplierData = new MultiplierData(this);
        SaveLoadManager.SaveData(multiplierData);
    }
    public void UpdatePerformanceMultiplierAndSave()
    {
            Performance_Multiplier += _performanceMultiplierIncreaseRate;
            Performance_Level++;
            Performance_Upgrade_Cost = (int)(Performance_Upgrade_Cost * _costMultiplier);    
            Performance_Multiplier = Mathf.Round(Performance_Multiplier * 10) * 0.1f ;
            SaveMultiplier();
    }
    public void UpdateBoostMultiplierAndSave()
    {
            Boost_Multiplier += _boostMultiplierIncreaseRate;
            Boost_Level++;
            Boost_Upgrade_Cost = (int)(Boost_Upgrade_Cost * _costMultiplier);    
            Boost_Multiplier = Mathf.Round(Boost_Multiplier * 10) * 0.1f;
            SaveMultiplier();
    } 
    public void UpdateBonusMultiplierAndSave()
    {
            Bonus_Multiplier += _bonusMultiplierIncreaseRate;
            Bonus_Level++;
            Bonus_Upgrade_Cost = (int)(Bonus_Upgrade_Cost * _costMultiplier);    
            Bonus_Multiplier = Mathf.Round(Bonus_Multiplier * 10) * 0.1f;
            SaveMultiplier();
    }
}
