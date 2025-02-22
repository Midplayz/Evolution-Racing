using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] public class MultiplierData
{
    public float Performance_Multiplier;
    public int Performance_Level;
    public int Performance_Upgrade_Cost;

    public float Boost_Multiplier;
    public int Boost_Level;
    public int Boost_Upgrade_Cost;

    public float Bonus_Multiplier;
    public int Bonus_Level;
    public int Bonus_Upgrade_Cost;
    public MultiplierData(MultiplierDataHandler multiplierDataHandler)
    {
        Performance_Multiplier = multiplierDataHandler.Performance_Multiplier;
        Performance_Level= multiplierDataHandler.Performance_Level;
        Performance_Upgrade_Cost = multiplierDataHandler.Performance_Upgrade_Cost;

        Boost_Multiplier= multiplierDataHandler.Boost_Multiplier;
        Boost_Level= multiplierDataHandler.Boost_Level;
        Boost_Upgrade_Cost = multiplierDataHandler.Boost_Upgrade_Cost;

        Bonus_Multiplier= multiplierDataHandler.Bonus_Multiplier;
        Bonus_Level= multiplierDataHandler.Bonus_Level;
        Bonus_Upgrade_Cost = multiplierDataHandler.Bonus_Upgrade_Cost;
    }
}
