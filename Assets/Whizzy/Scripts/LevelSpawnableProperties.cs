using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
[System.Serializable]
public class LevelSpawnableProperties
{
    [Header("seed")]
    public int seed;
    [Header("Level Min and Max")]
    public int minLevel;
    public int maxLevel;
    [Header("Length of Level")]
    public int minLength;
    public int maxLength;
    [Header("Buffer")]
    public int startBuffer;
    public int endBuffer;
    public List<GameObject> obstaclesThatCanSpawned;
    public List<GameObject> enemies;
    public GameObject gate;
    public List<Material> availableTerrainMaterial;
    public GameObject currency;
    public GameObject startLinePrefab;
    public GameObject endLinePrefab;
    [Range(0,1f)]
    public float coinPercentage;
    [Range(0,1f)]
    public float enemyPercentage;
    [Range(0, 1f)]
    public float obstaclePercentage;
    [Range(0f,1f)]
    public float laneSkipPercentage;

}