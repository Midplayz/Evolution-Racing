using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class PurchaseTrackerData
{
    public int Test;

    public PurchaseTrackerData(PurchaseTrackerDataHandler purchaseTrackerDataHandler)
    {
        Test = purchaseTrackerDataHandler.Test;
    }
}
