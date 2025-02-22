using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class BoostData 
{
    public float Base_Boost;

    public BoostData(BoostDataHandler BoostDataHandler)
    {
        Base_Boost = BoostDataHandler.Base_Boost;
    }
}
