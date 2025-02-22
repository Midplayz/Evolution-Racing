using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseTrackerDataHandler : MonoBehaviour
{
    [field: Header("Purchase Tracker Data Handler")]
    [field: SerializeField] public int Test { get; private set; } = 69;

    PurchaseTrackerData purchaseTrackerData;
    private void Start()
    {
        purchaseTrackerData = SaveLoadManager.LoadData<PurchaseTrackerData>();
        Test = purchaseTrackerData.Test;
    }

    public void SavePurchases()
    {
        purchaseTrackerData = new PurchaseTrackerData(this);
        SaveLoadManager.SaveData(purchaseTrackerData);
    }
}
