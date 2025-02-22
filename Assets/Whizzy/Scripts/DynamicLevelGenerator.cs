using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicLevelGenerator : MonoBehaviour
{
    public static DynamicLevelGenerator Instance;
    public int mingateLength, maxGateLength, spawnFreezeLength, gateBufferBetweenCoins, spawnFreezeWidthBuffer = 1;

    public List<HyperCasual.Runner.LevelDefinition.SpawnableObject> gateSpawnables;
    private void Awake()
    {
        Instance = this;
    }
    [field: SerializeField] public List<LevelSpawnableProperties> spawnableDataInformation { get; private set; }
    [field: SerializeField] public LevelSpawnableProperties currentSpawnableDataInformation { get; private set; }
    [SerializeField] private int currentLevel;
    public HyperCasual.Runner.LevelDefinition.SpawnableObject spawnableObjects = new();
    public HyperCasual.Runner.LevelDefinition currentLevelDef = new();
    public float lanes = 3, totalLanes = 3;

    public HyperCasual.Runner.LevelDefinition GetDefinition(int level)
    {
        currentSpawnableDataInformation = GetLevelProperties(level);
        currentLevelDef = new HyperCasual.Runner.LevelDefinition()
        {

            LevelLength = Random.Range(currentSpawnableDataInformation.minLength, currentSpawnableDataInformation.maxLength),
            LevelLengthBufferStart = currentSpawnableDataInformation.startBuffer,
            LevelLengthBufferEnd = currentSpawnableDataInformation.endBuffer,
            LevelThickness = 70,
            LevelWidth = 8,
            SnapToGrid = true,
            GridSize = 1,
            Spawnables = new(),
            TerrainMaterial = currentSpawnableDataInformation.availableTerrainMaterial[Random.Range(0, currentSpawnableDataInformation.availableTerrainMaterial.Count)],
            StartPrefab = currentSpawnableDataInformation.startLinePrefab,
            EndPrefab = currentSpawnableDataInformation.endLinePrefab
        };
        GenerateLevel(level);
        return currentLevelDef;
    }

    public void GenerateLevel(int level)
    {
        Random.InitState(currentSpawnableDataInformation.seed + level);
        GenerateGates();
    }
    private HyperCasual.Runner.LevelDefinition GenerateGates()
    {
        gateSpawnables = new();
        GameObject go;

        for (int i = spawnFreezeLength; i < currentLevelDef.LevelLength; i = i + Random.Range(mingateLength, maxGateLength))
        {

            gateSpawnables.Add(new HyperCasual.Runner.LevelDefinition.SpawnableObject { SpawnablePrefab = currentSpawnableDataInformation.gate, Position = new Vector3(2, 1.5f, i), Scale = currentSpawnableDataInformation.gate.transform.localScale});
            gateSpawnables.Add(new HyperCasual.Runner.LevelDefinition.SpawnableObject { SpawnablePrefab = currentSpawnableDataInformation.gate, Position = new Vector3(-2, 1.5f, i), Scale = currentSpawnableDataInformation.gate.transform.localScale });
        }
        currentLevelDef.Spawnables.AddRange(gateSpawnables);
        GeneratRewardAndObstacles(gateSpawnables);
        return currentLevelDef;


    }
    private HyperCasual.Runner.LevelDefinition GeneratRewardAndObstacles(List<HyperCasual.Runner.LevelDefinition.SpawnableObject> gates)
    {
        float i;
        foreach (HyperCasual.Runner.LevelDefinition.SpawnableObject gate in gates)
        {
            for (i = spawnFreezeWidthBuffer; i < currentLevelDef.LevelWidth - spawnFreezeWidthBuffer; i += (currentLevelDef.LevelWidth - (spawnFreezeWidthBuffer * 2)) / lanes)
            {
                if (Random.Range(0f, 1f) > currentSpawnableDataInformation.laneSkipPercentage)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (Random.Range(0f, 1f) <= currentSpawnableDataInformation.coinPercentage)
                        {
                            currentLevelDef.Spawnables.Add(new HyperCasual.Runner.LevelDefinition.SpawnableObject { SpawnablePrefab = currentSpawnableDataInformation.currency, Position = Vector3.forward * gate.Position.z + new Vector3(i - ((currentLevelDef.LevelWidth - (spawnFreezeWidthBuffer * 2)) / 2), 1.5f, -j - 5f) });
                            continue;
                        }
                        else if (Random.Range(0f, 1f) <= currentSpawnableDataInformation.enemyPercentage)
                        {
                            currentLevelDef.Spawnables.Add(new HyperCasual.Runner.LevelDefinition.SpawnableObject { SpawnablePrefab = currentSpawnableDataInformation.enemies[Random.Range(0, currentSpawnableDataInformation.enemies.Count)], Position = Vector3.forward * gate.Position.z + new Vector3(i - ((currentLevelDef.LevelWidth - (spawnFreezeWidthBuffer * 2)) / 2), 0, -j - 5f), EulerAngles = new Vector3(0, 180, 0) });
                            continue;
                        }
                        else if (Random.Range(0f, 1f) <= currentSpawnableDataInformation.obstaclePercentage)
                        {
                            currentLevelDef.Spawnables.Add(new HyperCasual.Runner.LevelDefinition.SpawnableObject { SpawnablePrefab = currentSpawnableDataInformation.obstaclesThatCanSpawned[Random.Range(0, currentSpawnableDataInformation.obstaclesThatCanSpawned.Count)], Position = Vector3.forward * gate.Position.z + new Vector3(i  , 0, -j - 5f), EulerAngles = new Vector3(0, 180, 0) });
                            
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
        }
        return currentLevelDef;
    }
    public LevelSpawnableProperties GetLevelProperties(int level)
    {
        if (level < spawnableDataInformation[0].minLevel)
        {
            return spawnableDataInformation[0];
        }
        else if (level > spawnableDataInformation[spawnableDataInformation.Count - 1].maxLevel)
        {
            return spawnableDataInformation[spawnableDataInformation.Count - 1];
        }
        foreach (LevelSpawnableProperties levelSpawnableProperties in spawnableDataInformation)
        {
            if (level >= levelSpawnableProperties.minLevel && level <= levelSpawnableProperties.maxLevel)
            {
                return levelSpawnableProperties;
            }

        }
        return null;
    }

}
