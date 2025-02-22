using HyperCasual.Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Camera Transition For Player Changer")]

    [Header("Camera Zoom")]
    [SerializeField] private float _gameZoom = 100f;
    [SerializeField] private float _mainMenuZoom = 20f;

    private void Start()
    {
        GameManager.Instance.OnPlayerClickedOnScreen += () => { ToggleCamera(); };
        GameManager.Instance.OnGameLost += () => { ToggleCamera(); };
        GameManager.Instance.OnGameWon += () => { ToggleCamera(); };
    }
    public void ToggleCamera()
    {

    }
    public void ZoomIntoPlayer()
    {



    }
}
