using System.Collections;
using System.Collections.Generic;
using HyperCasual.Runner;
using UnityEngine;
using UnityEngine.UIElements;

public class CrowdFormation : MonoBehaviour
{
    public static CrowdFormation Instance;
    public int number = 0;
    public List<GameObject> crowd = new List<GameObject> { };
    public GameObject character;
    public Bounds bbox;
    public int perRow = 4;
    public BoxCollider boxCollider;
    public HyperCasual.Runner.PlayerController playerController;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        GenerateCrowd();
    }
    public void GenerateCrowd()
    {
        for (int i = 0; i < crowd.Count; i++)
        {
            Destroy(crowd[i]);
        }
        crowd.Clear();
        playerController.m_Animators.Clear();
        Destroy(boxCollider);
        boxCollider = gameObject.AddComponent<BoxCollider>();
        bbox = new Bounds(transform.position, Vector3.zero);
        for (int i = 0; i < number; i++)
        {
            int rowOffset = i % perRow;
            GameObject go = GameObject.Instantiate(character, transform);
            go.transform.localPosition = new Vector3(0 + rowOffset, go.transform.position.y, -1 * Mathf.FloorToInt(i / perRow));
            go.SetActive(true);
            bbox.Encapsulate(go.GetComponentInChildren<BoxCollider>().bounds);
            boxCollider.bounds.Encapsulate(go.GetComponentInChildren<BoxCollider>().bounds);
            playerController.m_Animators.Add(go.GetComponentInChildren<Animator>());
            crowd.Add(go);
        }
        transform.localPosition = new Vector3(transform.position.x - bbox.center.x, 0, bbox.extents.z * -1);
    }
    public void AdjustCrowd(int value)
    {
        number += value;
        if(number <1)
        {
            GameManager.Instance.OnGameLost?.Invoke();
            return;
        }
        GenerateCrowd();
    }
}
