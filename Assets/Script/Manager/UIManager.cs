using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<Image> _healthImage = new List<Image>();
    [SerializeField] private TextMeshProUGUI _coinCountText;
    [SerializeField] private TextMeshProUGUI _coinScore;
    [SerializeField] private Transform GameOverScreen;
    [SerializeField] private UserProfileManager USD;
    [SerializeField] private UserData UD;
    [SerializeField] private Transform _container;
    [SerializeField] private Transform _userDataTamplate;

    [SerializeField] private int _coinCount;
    [SerializeField] private int _currentHealthCount;

    bool gameDataSentToJson;
    private void Start()
    {
        _coinCount = 0;
        _coinCountText.text = _coinCount.ToString();
        gameDataSentToJson = false;
        GameOverScreen.gameObject.SetActive(false);

        USD = FindObjectOfType<UserProfileManager>();
        UD = FindObjectOfType<UserData>();
        UD.Container = _container;
        UD.UserDataTamplate = _userDataTamplate;
    }

    public void CoinCounterUpdate()
    {
        _coinCount++;
        _coinCountText.text = _coinCount.ToString();
    }
    private void Update()
    {
        if (_currentHealthCount <= 0 && !GameManager.Instance.isDead)
        {
            _coinScore.text =  "Score : "+_coinCount.ToString();
            SetPlayerCointData();

        }
    }
    public void HealthDown()
    {
        if (GameManager.Instance.State != GameState.Shield)
        {
            if (_currentHealthCount > 0)
            {
                _currentHealthCount--;
                _healthImage[_currentHealthCount].enabled = false;
            }
        }

    }

    public int GetCurrentPlayerSaveData(string _playerName)
    {
        return PlayerPrefs.GetInt(_playerName);
    }

    public void SetPlayerCointData()
    {
        if (!GameManager.Instance.isDead && !gameDataSentToJson)
        {
            USD.SaveGameData(_coinCount);
            UD.UpdateVisual();
            GameOverScreen.gameObject.SetActive(true);
            GameManager.Instance.isDead = true;
            gameDataSentToJson = true;
        }
    }
    public void SavePlayerCoinData()
    {
        USD.SaveGameData(_coinCount);
    }
}
