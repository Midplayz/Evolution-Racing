using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using HyperCasual.Runner;

public class LevelManager : MonoBehaviour
{
    [Header("Level Manager")]
    public static LevelManager instance;
    [SerializeField] private UserDataHandler _userData;
    [SerializeField] private float _baseFinishLineDistance = 200;
    [SerializeField] private float _maxFinishLineDistance = 500;
    [field:SerializeField] public Transform playerTransform { get; private set; }
    public float Finish_Line_Z_Position { get; private set; }
    public float Start_Position { get; private set; }

    public int level { get; private set; } = 10;
    public Action<float, float> OnRaceStarted;
    public Action OnStartSpawningObstacleStarted;  
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {

        GameManager.Instance.OnAiCharacterGenerated += startCountDown;
    }
    private void GenerateFinishLine()
    {
        Finish_Line_Z_Position = playerTransform.position.z + Mathf.Clamp(_baseFinishLineDistance, _baseFinishLineDistance, _maxFinishLineDistance);
        startGame();
    }
    private void startCountDown()
    {
        //3 2 1  Text Animation
        //After that completes start game
        GenerateFinishLine();
    }
    private void startGame()
    {
        OnRaceStarted?.Invoke(playerTransform.position.z, Finish_Line_Z_Position);
        OnStartSpawningObstacleStarted?.Invoke();
    }
  

}
