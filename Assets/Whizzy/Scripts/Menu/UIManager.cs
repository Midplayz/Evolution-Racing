using HyperCasual.Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Whizzy.Hypercaual
{

    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;
        [SerializeField]
        private GameObject _mainUIMenu;
        [SerializeField]
        private GameObject _postGameUIMenu;


        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            TogglePanel(_mainUIMenu.gameObject.name);
            GameManager.Instance.OnGameMenuLoaded += () =>
            {
                //
                //TogglePanel(_mainUIMenu.gameObject.name);
            };
            GameManager.Instance.OnGameWon += () =>
            {
                TogglePanel(_postGameUIMenu.gameObject.name);
            };
            GameManager.Instance.OnGameLost += () =>
            {
                TogglePanel(_postGameUIMenu.gameObject.name);
            };

        }

        private void TogglePanel(string name)
        {
            _mainUIMenu.SetActive(_mainUIMenu.name.Equals(name));
            _postGameUIMenu.SetActive(_postGameUIMenu.name.Equals(name));

        }

        public void OnGameEnter()
        {
            _mainUIMenu.SetActive(false);
            _postGameUIMenu.SetActive(false);
        }
        public void OnGameComplete()
        {
            _mainUIMenu.SetActive(false);
            _postGameUIMenu.SetActive(true);
        }
    }

}