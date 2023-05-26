using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public enum GameState
{
    Idle = 0,
    Boosted = 1,
    Shield = 2
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameState _state;
    public float BoostTime = 1;
    public float ShieldTime = 1;

    public bool isDead = false;

    private void Start()
    {
        ChangeState(GameState.Idle);
    }

    public void ChangeState(GameState state)
    {
        _state = state;
        switch (_state)
        {
            case GameState.Idle:
                OnIdleState?.Invoke(this, EventArgs.Empty);
                break;

            case GameState.Boosted:
                OnBoostState?.Invoke(this, EventArgs.Empty);
                break;

            case GameState.Shield:
                OnShieldState?.Invoke(this, EventArgs.Empty);
                break;

        }
    }

    public GameState State
    {
        get { return _state; }
        private set { }

    }
    public static event EventHandler OnIdleState;
    public static event EventHandler OnBoostState;
    public static event EventHandler OnShieldState;

    public void LoadScene(){
        FindObjectOfType<UIManager>().SavePlayerCoinData();
        SceneManager.LoadScene(0);
    }

}
