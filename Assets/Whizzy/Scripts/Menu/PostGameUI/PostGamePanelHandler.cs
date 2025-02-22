using HyperCasual.Runner;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostGamePanelHandler : MonoBehaviour
{
    [Header("Post Game")]
    [field: SerializeField] private List<GameObject> _resultLabel;
    [field: SerializeField] private List<TextMeshProUGUI> _playerNames;
    [field: SerializeField] private TextMeshProUGUI _finalCoinsText;
    [field: SerializeField] private int _minWinCoins = 5;
    [field: SerializeField] private int _maxWinCoins = 500;
    [field: SerializeField] public int _winCoins { get; private set; }
    private void Start()
    {
        GameManager.Instance.OnGameWon += () =>
        {
            ScoreBoardGenerator();
            GetWinningRewards();
            _resultLabel[0].SetActive(true);
            _resultLabel[1].SetActive(false);
        };
        GameManager.Instance.OnGameLost += () =>
        {
            ScoreBoardGenerator();
            _resultLabel[1].SetActive(true);
            _resultLabel[0].SetActive(false);
        };
    }
    private void ScoreBoardGenerator()
    {

    }
    private void GetWinningRewards()
    {
        _winCoins = Random.Range(_minWinCoins, _maxWinCoins);
        _finalCoinsText.text = "I AM FINE WITH " + _winCoins + " COINS...";
    }
    public void ResetScene()
    {
        CurrencyDataHandler.instance.AddCoins(_winCoins);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
