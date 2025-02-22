using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGateProperties : MonoBehaviour
{
    [field: Header("Custom Properties Gates")]
    [field: SerializeField] public float Property { get; private set; }
    [field: SerializeField] public float Starting_Property_Value { get; private set; }

    //Optional Ones
    //[field: SerializeField] public float Max_Property_Value { get; private set; }
    //[field: SerializeField] public float Min_Property_Value { get; private set; }

    private void Awake()
    {
        Property = Starting_Property_Value;
    }
    public void AdjustPropertyValue(float value)
    {
        Property += value;
        if(Property<0)
        { 
            Property = 0; 
        }
    }
    public void MultiplyPropertyValue(float value)
    {
        Property *= value;
    }
    public void DividePropertyValue(float value)
    {
        Property /= value;
    }

    public void AdjustStartingPropertyValue (float value)
    {
        Starting_Property_Value += value;
    }
}
