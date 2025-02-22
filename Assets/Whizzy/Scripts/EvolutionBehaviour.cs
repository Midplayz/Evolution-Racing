using HyperCasual.Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EvolutionBehaviour : MonoBehaviour
{
    public static EvolutionBehaviour instance;
    [field: Header("Evolution System")]
    [field: SerializeField] private List<EvolutionSpecificationsData> evolutionObjects;
    [field: SerializeField] private int startingEvolution = 0;
    [field: SerializeField] private GameObject playerParentGameObject;
    [field: SerializeField] private GameObject currentGameObject;
    [field: SerializeField] private int currentEvolutionLevel = 0;
    [field: SerializeField] private CrowdFormation crowdFormation;

    //[field: SerializeField] private int maxEvolutionLevel = 7000;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        crowdFormation.character = evolutionObjects[startingEvolution].EvolutionPrefab;
        currentGameObject = evolutionObjects[startingEvolution].EvolutionPrefab;
    }
    public void AdjustEvolution (int value)
    {
        currentEvolutionLevel += value;
        UpdateEvolutionCharacter();
    }
    public void MultiplyEvolution (int evoMultiplication)
    {
        currentEvolutionLevel*= evoMultiplication;
        UpdateEvolutionCharacter();
    }
    public void DivideEvolution (int evoDivide)
    {
        currentEvolutionLevel = currentEvolutionLevel/evoDivide;
        UpdateEvolutionCharacter();
    }
    public void AddToStartingEvolution (int addingAmount)
    {
        startingEvolution += addingAmount;
        UpdateEvolutionCharacter();
    }
    public void UpdateEvolutionCharacter()
    {
        for (int i = 0; i < evolutionObjects.Count; i++)
        {
            if (currentEvolutionLevel > evolutionObjects[i].MinEvolution && currentEvolutionLevel <= evolutionObjects[i].MaxEvolution)
            {
                currentGameObject = evolutionObjects[i].EvolutionPrefab;
                crowdFormation.character = evolutionObjects[i].EvolutionPrefab;
                crowdFormation.GenerateCrowd();
                PlayerController.Instance.m_SkinnedMeshRenderer = currentGameObject.GetComponentInChildren<SkinnedMeshRenderer>();
                break;
            }
        }
    }
}
