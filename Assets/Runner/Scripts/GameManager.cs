using System.Collections;
using System.Collections.Generic;
using HyperCasual.Core;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HyperCasual.Runner
{
    /// <summary>
    /// A class used to store game state information, 
    /// load levels, and save/load statistics as applicable.
    /// The GameManager class manages all game-related 
    /// state changes.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Returns the GameManager.
        /// </summary>
        public static GameManager Instance => s_Instance;
        static GameManager s_Instance;
        [field: SerializeField] public GameObject bulletParent { get; private set; }

        [SerializeField] AbstractGameEvent m_WinEvent;

        [SerializeField] AbstractGameEvent m_LoseEvent;

        LevelDefinition m_CurrentLevel;

        /// <summary>
        /// Returns true if the game is currently active.
        /// Returns false if the game is paused, has not yet begun,
        /// or has ended.
        /// </summary>
        public bool IsPlaying => m_IsPlaying;
        bool m_IsPlaying;
        GameObject m_CurrentLevelGO;
        GameObject m_CurrentTerrainGO;
        GameObject m_LevelMarkersGO;
        public GameObject m_PostGameBoss;
        public bool isInPostGame;
        public Action OnGameStart, OnGameMenuLoaded, OnPlayerClickedOnScreen, OnAiCharacterGenerated, OnGameWon, OnGameLost, OnReachingMainMenuFromGameMenu, OnUpgradedBoost, OnUpgradedPerformance, OnUpgradedBonus;

        List<Spawnable> m_ActiveSpawnables = new List<Spawnable>();
        [SerializeField]private int level;

#if UNITY_EDITOR
        bool m_LevelEditorMode;
#endif

        void Awake()
        {
            if (s_Instance != null && s_Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            s_Instance = this;


#if UNITY_EDITOR
            if (LevelManager.Instance != null)
            {
                StartGame();
                m_LevelEditorMode = true;
            }

#endif
        }
        private void Start()
        {

            LevelDefinition levelDefinition = DynamicLevelGenerator.Instance.GetDefinition(level);
            LoadLevel(levelDefinition);
            OnGameStart?.Invoke();
            OnGameLost += LostGame;
            OnGameWon += StartPostGame;

        }

        /// <summary>
        /// This method calls all methods necessary to load and
        /// instantiate a level from a level definition.
        /// </summary>
        public void LoadLevel(LevelDefinition levelDefinition)
        {
            m_CurrentLevel = levelDefinition;
            LoadLevel(m_CurrentLevel, ref m_CurrentLevelGO);
            CreateTerrain(m_CurrentLevel, ref m_CurrentTerrainGO);
            PlaceLevelMarkers(m_CurrentLevel, ref m_LevelMarkersGO);
            PlacePostGameBoss(m_CurrentLevel, ref m_PostGameBoss);
            StartGame();
        }

        /// <summary>
        /// This method calls all methods necessary to restart a level,
        /// including resetting the player to their starting position
        /// </summary>
        public void ResetLevel()
        {
            if (PlayerController.Instance != null)
            {
                PlayerController.Instance.ResetPlayer();
            }

            if (CameraManager.Instance != null)
            {
                CameraManager.Instance.ResetCamera();
            }

            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.ResetSpawnables();
            }
        }

        /// <summary>
        /// This method loads and instantiates the level defined in levelDefinition,
        /// storing a reference to its parent GameObject in levelGameObject
        /// </summary>
        /// <param name="levelDefinition">
        /// A LevelDefinition ScriptableObject that holds all information needed to 
        /// load and instantiate a level.
        /// </param>
        /// <param name="levelGameObject">
        /// A new GameObject to be created, acting as the parent for the level to be loaded
        /// </param>
        public static void LoadLevel(LevelDefinition levelDefinition, ref GameObject levelGameObject)
        {
            //if (levelDefinition == null)
            //{
            //    Debug.LogError("Invalid Level!");
            //    return;
            //}

            if (levelGameObject != null)
            {
                if (Application.isPlaying)
                {
                    Destroy(levelGameObject);
                }
                else
                {
                    DestroyImmediate(levelGameObject);
                }
            }

            levelGameObject = new GameObject("LevelManager");
            LevelManager levelManager = levelGameObject.AddComponent<LevelManager>();
            levelManager.LevelDefinition = levelDefinition;

            Transform levelParent = levelGameObject.transform;

            for (int i = 0; i < levelDefinition.Spawnables.Count; i++)
            {
                LevelDefinition.SpawnableObject spawnableObject = levelDefinition.Spawnables[i];

                if (spawnableObject.SpawnablePrefab == null)
                {
                    continue;
                }

                Vector3 position = spawnableObject.Position;
                Vector3 eulerAngles = spawnableObject.EulerAngles;
                Vector3 scale = spawnableObject.Scale;

                GameObject go = null;

                if (Application.isPlaying)
                {
                    go = GameObject.Instantiate(spawnableObject.SpawnablePrefab, position, Quaternion.Euler(eulerAngles));
                }
                else
                {
#if UNITY_EDITOR
                    go = (GameObject)PrefabUtility.InstantiatePrefab(spawnableObject.SpawnablePrefab);
                    go.transform.position = position;
                    go.transform.eulerAngles = eulerAngles;
#endif
                }

                if (go == null)
                {
                    return;
                }

                // Set Base Color
                Spawnable spawnable = go.GetComponent<Spawnable>();
                if (spawnable != null)
                {
                    //spawnable.SetBaseColor(spawnableObject.BaseColor);
                    spawnable.SetScale(scale);
                    levelManager.AddSpawnable(spawnable);
                }

                if (go != null)
                {
                    go.transform.SetParent(levelParent);
                }
            }
        }

        public void UnloadCurrentLevel()
        {
            if (m_CurrentLevelGO != null)
            {
                GameObject.Destroy(m_CurrentLevelGO);
            }

            if (m_LevelMarkersGO != null)
            {
                GameObject.Destroy(m_LevelMarkersGO);
            }

            if (m_CurrentTerrainGO != null)
            {
                GameObject.Destroy(m_CurrentTerrainGO);
            }

            m_CurrentLevel = null;
        }

        void StartGame()
        {
            ResetLevel();
            m_IsPlaying = true;
        }

        public static void PlacePostGameBoss(LevelDefinition levelDefinition, ref GameObject postGameBossObject)
        {
            if (postGameBossObject != null)
            {
                if (Application.isPlaying)
                {
                    Destroy(postGameBossObject);
                }
                else
                {
                    DestroyImmediate(postGameBossObject);
                }
            }


            postGameBossObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            postGameBossObject.transform.position = new Vector3(0, 0.5f, levelDefinition.LevelLength + 1);
            GameObject postGameTerrain = new GameObject("Post Game Terrain");

            TerrainGenerator.TerrainDimensions terrainDimensions = new TerrainGenerator.TerrainDimensions()
            {
                Width = levelDefinition.LevelWidth,
                Length = 100,
                StartBuffer = 0,
                EndBuffer = 0,
                Thickness = levelDefinition.LevelThickness + 10
            };
            TerrainGenerator.CreateTerrain(terrainDimensions, levelDefinition.TerrainMaterial, ref postGameTerrain);

            postGameTerrain.transform.position = new Vector3(0, 0, levelDefinition.LevelLength);
            postGameTerrain.AddComponent<BoxCollider>();


        }
        /// <summary>
        /// Creates and instantiates the StartPrefab and EndPrefab defined inside
        /// the levelDefinition.
        /// </summary>
        /// <param name="levelDefinition">
        /// A LevelDefinition ScriptableObject that defines the start and end prefabs.
        /// </param>
        /// <param name="levelMarkersGameObject">
        /// A new GameObject that is created to be the parent of the start and end prefabs.
        /// </param>
        /// 
        public static void PlaceLevelMarkers(LevelDefinition levelDefinition, ref GameObject levelMarkersGameObject)
        {
            if (levelMarkersGameObject != null)
            {
                if (Application.isPlaying)
                {
                    Destroy(levelMarkersGameObject);
                }
                else
                {
                    DestroyImmediate(levelMarkersGameObject);
                }
            }

            levelMarkersGameObject = new GameObject("Level Markers");

            GameObject start = levelDefinition.StartPrefab;
            GameObject end = levelDefinition.EndPrefab;

            if (start != null)
            {
                GameObject go = GameObject.Instantiate(start, new Vector3(start.transform.position.x, start.transform.position.y, 0.0f), Quaternion.identity);
                go.transform.SetParent(levelMarkersGameObject.transform);
            }

            if (end != null)
            {
                GameObject go = GameObject.Instantiate(end, new Vector3(end.transform.position.x, end.transform.position.y, levelDefinition.LevelLength), Quaternion.identity);
                go.transform.SetParent(levelMarkersGameObject.transform);
            }
        }

        /// <summary>
        /// Creates and instantiates a Terrain GameObject, built
        /// to the specifications saved in levelDefinition.
        /// </summary>
        /// <param name="levelDefinition">
        /// A LevelDefinition ScriptableObject that defines the terrain size.
        /// </param>
        /// <param name="terrainGameObject">
        /// A new GameObject that is created to hold the terrain.
        /// </param>
        public static void CreateTerrain(LevelDefinition levelDefinition, ref GameObject terrainGameObject)
        {
            TerrainGenerator.TerrainDimensions terrainDimensions = new TerrainGenerator.TerrainDimensions()
            {
                Width = levelDefinition.LevelWidth,
                Length = levelDefinition.LevelLength,
                StartBuffer = levelDefinition.LevelLengthBufferStart,
                EndBuffer = 0,
                Thickness = levelDefinition.LevelThickness + 10
            };
            TerrainGenerator.CreateTerrain(terrainDimensions, levelDefinition.TerrainMaterial, ref terrainGameObject);
        }

        private void StartPostGame()
        {
            PlayerController.Instance.StopMovingForward();
            isInPostGame = true;
            m_PostGameBoss.AddComponent<PostGameCollider>();
            m_PostGameBoss.transform.position += new Vector3(0, 1, 0);
            Rigidbody rb = m_PostGameBoss.AddComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddForce(0, 800, 800);
        }
        private void LostGame()
        {
            PlayerController.Instance.StopMovingForward();
            isInPostGame = true;
        }
        public void Win()
        {
            Debug.Log("Game is won");
            m_WinEvent.Raise();
            Whizzy.Hypercaual.UIManager.Instance.OnGameComplete();
            PlayerController.Instance.m_AutoMoveForward = false;


#if UNITY_EDITOR
            if (m_LevelEditorMode)
            {
                ResetLevel();
            }
#endif
        }



        public void Lose()
        {
            m_LoseEvent.Raise();

#if UNITY_EDITOR
            if (m_LevelEditorMode)
            {
                ResetLevel();
            }
#endif
        }
    }
}