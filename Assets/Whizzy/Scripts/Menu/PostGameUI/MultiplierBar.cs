using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplierBar : MonoBehaviour
{
    [SerializeField] private RectTransform image;
    [SerializeField] private RectTransform pointer;
    [SerializeField] private float speed = 100;
    [SerializeField] private float speedMultiplier = 1;
    [SerializeField] PostGamePanelHandler postGamePanelHandler;
    [SerializeField] public TextMeshProUGUI _bonusCoinsValue;
    private int _finalCoins;
    float maxDistance;
    Vector3 temp;
    private bool keepPointerMoving;
    private float _twentyPercentValue;
    private float _thirtyPercentValue;
    public OnSliderStopped onSliderStopped;
    public delegate float OnSliderStopped(float value);
    private void OnEnable()
    {
        maxDistance = image.rect.width;
        _twentyPercentValue = maxDistance * 0.2f;
        _thirtyPercentValue = maxDistance * 0.3f;
        pointer.anchoredPosition = new Vector3(0, 17f, 0);
        keepPointerMoving = true;
    }
    public float GetSliderNormalizedValue()
    {
        return (pointer.anchoredPosition.x / maxDistance);
    }
    public float GetSliderValue()
    {

        return pointer.anchoredPosition.x;
    }
    void Update()
    {
        if (keepPointerMoving)
        {
            temp = pointer.anchoredPosition;
            temp.x += Time.deltaTime * speed * speedMultiplier;
            if (temp.x > maxDistance)
            {
                temp.x = maxDistance;
                speed *= -1;
            }
            else if (temp.x < 0)
            {
                temp.x = 0;
                speed *= -1;
            }
            pointer.anchoredPosition = temp;
            CoinValueChange();
        }
    }
    public void StopSlider()
    {
        keepPointerMoving = false;
        CurrencyDataHandler.instance.AddCoins(_finalCoins);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //if(pointer.anchoredPosition.x>=0f && pointer.anchoredPosition.x< _twentyPercentValue)
        //{
        //    Debug.Log("5x");
        //}
        //else if(pointer.anchoredPosition.x >= _twentyPercentValue && pointer.anchoredPosition.x < (_twentyPercentValue+_thirtyPercentValue))
        //{
        //    Debug.Log("4x");
        //}
        //else if (pointer.anchoredPosition.x >= (_twentyPercentValue + _thirtyPercentValue) && pointer.anchoredPosition.x < (_twentyPercentValue + (_thirtyPercentValue*2)))
        //{
        //    Debug.Log("3x");
        //}
        //else
        //{
        //    Debug.Log("2x");
        //}
    }
    public void CoinValueChange()
    {
        if (pointer.anchoredPosition.x >= 0f && pointer.anchoredPosition.x < _twentyPercentValue)
        {
            _bonusCoinsValue.text = (postGamePanelHandler._winCoins * 5).ToString();
            _finalCoins = postGamePanelHandler._winCoins * 5;
        }
        else if (pointer.anchoredPosition.x >= _twentyPercentValue && pointer.anchoredPosition.x < (_twentyPercentValue + _thirtyPercentValue))
        {
            _bonusCoinsValue.text = (postGamePanelHandler._winCoins * 4).ToString();
            _finalCoins = postGamePanelHandler._winCoins * 4;
        }
        else if (pointer.anchoredPosition.x >= (_twentyPercentValue + _thirtyPercentValue) && pointer.anchoredPosition.x < (_twentyPercentValue + (_thirtyPercentValue * 2)))
        {
            _bonusCoinsValue.text = (postGamePanelHandler._winCoins * 3).ToString();
            _finalCoins = postGamePanelHandler._winCoins * 3;
        }
        else
        {
            _bonusCoinsValue.text = (postGamePanelHandler._winCoins * 2).ToString();
            _finalCoins = postGamePanelHandler._winCoins * 2;
        }
    }
}
