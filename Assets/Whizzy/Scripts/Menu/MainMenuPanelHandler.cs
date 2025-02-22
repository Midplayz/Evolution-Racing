using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainMenuPanelHandler : MonoBehaviour
{
    private void StartGame()
    {
        HyperCasual.Runner.PlayerController.Instance.m_AutoMoveForward = true;
        Whizzy.Hypercaual.UIManager.Instance.OnGameEnter();
    }
}
