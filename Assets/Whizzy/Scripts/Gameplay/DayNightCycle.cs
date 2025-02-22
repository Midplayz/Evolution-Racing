using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    [field: SerializeField] public float Current_Time;
    [field: SerializeField] public float Day_Length_Minutes;
    [field: SerializeField] private Material skybox;

    [field: SerializeField] private float _rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _rotationSpeed = 360 / Day_Length_Minutes / 60;
        skybox.SetFloat("_CubemapTransition", 0f);
        StartCoroutine(LightRotation());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator LightRotation()
    {
        while (true)
        {
            if (Current_Time >= 24)
            {
                Current_Time = 0;
            }
            if (Current_Time >= 0 && Current_Time <= 12)
            {
                float timeValue = (Current_Time / 12) * 100;
                skybox.SetFloat("_CubemapTransition", (1 * (timeValue / 100)));
            }
            else if (Current_Time > 12 && Current_Time <= 24)
            {
                float timeValue = ((Current_Time - 12) / 12) * 100;
                skybox.SetFloat("_CubemapTransition", (1 - (1 * (timeValue / 100))));
            }
            Current_Time += 1 * Time.deltaTime;
            transform.Rotate(new Vector3(1, 0, 0) * _rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
