using Codice.CM.Common;
using HyperCasual.Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    HorizontalMoving,
    VerticalMoving,
    Rotating
}
public class MovingObstaclesBehaviour : MonoBehaviour
{
    [field:Header("Dynamic Obstacles")]
    [SerializeField] private MovementType movementType;
    [SerializeField] private float movementSpeed;
    [field: Header("Vertical Movement Variables (Ignore if you chose Horizontal)")]
    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;

    [field: Header("Rotating Obstacles")]
    [field: SerializeField] private Vector3 rotationAxisAndSpeed = Vector3.zero;

    private Vector3 dir;
    private float levelWidth;
    private void OnEnable()
    {
        if (movementType == MovementType.HorizontalMoving)
        {
            levelWidth = (DynamicLevelGenerator.Instance.currentLevelDef.LevelWidth / 2) - gameObject.transform.localScale.x / 2;
        }
    }
    private void Awake()
    {
        if(movementType == MovementType.HorizontalMoving)
        {
            transform.position = new Vector3(0f, transform.position.y, transform.position.z);
            dir = Vector3.left;
        }
        else if(movementType == MovementType.VerticalMoving)
        {
            minHeight = transform.position.y;
            dir = Vector3.up;
        }
    }
    private void Update()
    {
        if(movementType == MovementType.HorizontalMoving)
        {
            HorizontalMovement();
        }
        else if (movementType == MovementType.VerticalMoving)
        {
            VerticalMovemement();
        }
        else if (movementType == MovementType.Rotating)
        {
            RotateObstacle();
        }
    }
    private void HorizontalMovement()
    {
        transform.Translate(dir * movementSpeed * Time.deltaTime);
        if (transform.position.x <= -levelWidth)
        {
            dir = Vector3.left;
        }
        else if (transform.position.x >= levelWidth)
        {
            dir = Vector3.right;
        }
    }
    private void VerticalMovemement()
    {
        transform.Translate(dir * movementSpeed * Time.deltaTime);

        if (transform.position.y >= maxHeight)
        {
            dir = Vector3.down;
        }
        else if (transform.position.y <= minHeight)
        {
            dir = Vector3.up;
        }
    }
    
    private void RotateObstacle()
    {
        transform.Rotate(rotationAxisAndSpeed * Time.deltaTime);
    }
}
