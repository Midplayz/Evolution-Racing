using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class UserData
{
    public int level;
    public int skinIndex;
    public int[] ownedSkinsIndex;
    public string Date_And_Time;
    public string Player_Username;

    public UserData(UserDataHandler userDataHandler)
    {
        level = userDataHandler.level;
        skinIndex = userDataHandler.skinIndex;
        ownedSkinsIndex = userDataHandler.ownedSkinsIndex;
        Date_And_Time = userDataHandler.Date_And_Time;
        Player_Username = userDataHandler.Player_Username;
    }
}
