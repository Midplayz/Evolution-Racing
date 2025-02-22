using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostDataHandler : MonoBehaviour
{
    [field: Header("Boost Handler")]
    [field: SerializeField, Range(0f, 100f)] public float Base_Boost { get; private set; } = 5f;
    [field: SerializeField] public float Boost_Usage_Rate { get; private set; } = 1f;
    [field: SerializeField] public ParticleSystem Boost_Particle { get; private set; }
    [field: SerializeField] public bool Is_Boosting = false;
}
