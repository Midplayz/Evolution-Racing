using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsDataHandler : MonoBehaviour
{
    [field: Header("Settings Data Handler")]
    [field: SerializeField] public int Test { get; private set; } = 69;

    SettingsData settingsData;
    private void Start()
    {
        settingsData = SaveLoadManager.LoadData<SettingsData>();
        Test = settingsData.Test;
    }

    public void SaveSettings()
    {
        settingsData = new SettingsData(this);
        SaveLoadManager.SaveData(settingsData);
    }
}
