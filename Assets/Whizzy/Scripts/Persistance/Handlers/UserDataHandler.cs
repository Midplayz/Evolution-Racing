using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using HyperCasual.Runner;

public class UserDataHandler : MonoBehaviour
{
    public static UserDataHandler instance;
    [field: Header("User Data Handler")]
    [field: SerializeField] public int level { get; private set; } = 0;
    [field: SerializeField] public string Date_And_Time { get; private set; } = null;
    [field: SerializeField] public string Player_Username { get; private set; } = null;

    [field: SerializeField] private GameObject _usernamePopUp;
    [field: SerializeField] private TMP_InputField _usernameInput;
    public int skinIndex { get; private set; } = 0;
    public int[] ownedSkinsIndex { get; private set; } = new int[1] { 1 };

    UserData userData;
    private void Awake()
    {
        instance = this;
        userData = SaveLoadManager.LoadData<UserData>();
        if (userData != null)
        {
            level = userData.level;
            Date_And_Time = userData.Date_And_Time;
            Player_Username = userData.Player_Username;
        }
    }
    private void Start()
    {
        if(string.IsNullOrEmpty(Player_Username))
        {
            Debug.Log("No Name");
            _usernamePopUp.SetActive(true);
        }
        else
        {
            _usernamePopUp.SetActive(false);
        }
            GameManager.Instance.OnGameWon += GoToNextLevel;
    }
    public void GoToNextLevel()
    {
        level += 1;
        SaveUserData();
    }
    public void NullifyDate()
    {
        Date_And_Time = null;
    }
    public void SaveUserData()
    {
        userData = new UserData(this);
        SaveLoadManager.SaveData(userData);
    }
    public void AssignUsername()
    {
        Player_Username = _usernameInput.text;
        SaveUserData();
        _usernamePopUp.SetActive(false);
    }
    private void OnApplicationQuit()
    {
        Date_And_Time = DateTime.Now.ToString();
        SaveUserData();
    }
}
