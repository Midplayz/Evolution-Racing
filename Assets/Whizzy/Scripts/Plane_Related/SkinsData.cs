using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinsData : MonoBehaviour
{
    [Header("Skins Ownership")]
    public bool Is_Owned;
    [field:SerializeField]public int cost { get; private set; }
    [field: SerializeField] public int index { get; private set; }
    // Start is called before the first frame update

    public void SetIndexPosition(int value)
    {
        index = value;
    }
    public void SetSkinsOwnedOrNot(bool isOwned)
    {
        Is_Owned = isOwned;
    }
    public void setCost(int value)
    {
        cost = value;
    }

}
