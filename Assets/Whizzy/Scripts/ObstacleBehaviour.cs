using HyperCasual.Gameplay;
using HyperCasual.Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    EndGame,
    ChangeSpeed,
    DisableSidewaysMovement,
}
public class ObstacleBehaviour : MonoBehaviour
{
    [field:Header("Type of Obstacle")]
    [field: SerializeField] private ObstacleType obstacleType;

    [field: Header("Speed Change Obstacle (Ignore if you chose other types)")]
    [field: SerializeField] private float adjustSpeedValue = 1f;

    [field: Header("Timer For Speed and Disable Sideways Movement")]
    [field: SerializeField] private float effectDuration = 3f;

    private float timer = 0f;
    private bool effectInitiated = false;
    private bool parameterChanged = false;
    void Awake()
    {
        if(obstacleType == ObstacleType.ChangeSpeed || obstacleType == ObstacleType.DisableSidewaysMovement)
        {
            effectInitiated = false;
            parameterChanged = false;
            timer = 0f;
        }
    }
    private void Update()
    {
        if(obstacleType == ObstacleType.ChangeSpeed && effectInitiated)
        {
            ObstacleAdjustSpeed();   
        }
        if(obstacleType == ObstacleType.DisableSidewaysMovement && effectInitiated)
        {
            SidewaysMovementDisabled();
        }
    }
    private void EndGame()
    {
        GameManager.Instance.OnGameLost?.Invoke();
    }
    private void ObstacleAdjustSpeed()
    {
            timer += Time.deltaTime;
        if (timer <= effectDuration && parameterChanged == false)
            {
                PlayerController.Instance.AdjustSpeed(adjustSpeedValue);
                parameterChanged = true;
            }
            else if(timer > effectDuration) 
            {
                PlayerController.Instance.GoBackToBaseSpeed();
                effectInitiated= false;
                parameterChanged= false;
            }
    }
    private void SidewaysMovementDisabled()
    {
        timer += Time.deltaTime;
        if (timer <= effectDuration && parameterChanged == false)
        {
            InputManager.Instance.sidewaysMovementEnabled = false;
            parameterChanged = true;
        }
        else if (timer > effectDuration)
        {
            InputManager.Instance.sidewaysMovementEnabled = true;
            effectInitiated = false;
            parameterChanged = false;
        }
    }

    private void OnTriggerEnter(Collider _collider)
    {
        if (_collider.tag == "Player")
        {
            if (obstacleType == ObstacleType.EndGame)
            {
                EndGame();
                //Destroy(_collider.gameObject); //Comment Not Deleted cuz yet to decide what to do upon collision.
            }
            else if (obstacleType == ObstacleType.ChangeSpeed || obstacleType == ObstacleType.DisableSidewaysMovement)
            {
                effectInitiated = true;
            }
        }
    }
}
