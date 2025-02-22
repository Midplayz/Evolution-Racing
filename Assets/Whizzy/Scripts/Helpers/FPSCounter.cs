using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [Header("FPS Counter")]
    public bool Enable_FPS = true;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Image _background;
    private void Awake()
    {
        _text= GetComponentInChildren<TextMeshProUGUI>();
        _background = GetComponentInChildren<Image>();
    }
    void Update()
    {
        if (Enable_FPS)
        {
            _background.gameObject.SetActive(true);
            _text.gameObject.SetActive(true);
            _text.text = "FPS: " + Mathf.Floor(1 / Time.deltaTime);
        }
        else
        {
            _background.gameObject.SetActive(false);
            _text.gameObject.SetActive(false);
        }
    }
}
