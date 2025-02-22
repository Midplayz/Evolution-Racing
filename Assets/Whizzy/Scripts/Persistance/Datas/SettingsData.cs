using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class SettingsData
{
    public int Test;

    public SettingsData(SettingsDataHandler settingsDataHandler)
    {
        Test = settingsDataHandler.Test;
    }
}
