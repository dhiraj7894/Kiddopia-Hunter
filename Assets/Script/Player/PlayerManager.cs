using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerManager : MonoBehaviour
{
    public ParticleSystem ShieldParticle;
    public UIManager _uiManager;
    public Collider2D Shield;

    private void OnEnable()
    {
        GameManager.OnIdleState += OnIdleStateStart;
        GameManager.OnShieldState += OnShieldStateStart;
    }
    private void OnDisable()
    {
        GameManager.OnIdleState -= OnIdleStateStart;
        GameManager.OnShieldState -= OnShieldStateStart;
    }


    public void OnIdleStateStart(object sender, EventArgs arg)
    {
        Shield.enabled = false;
        ShieldParticle.Stop();
    }
    public void OnShieldStateStart(object sender, EventArgs arg)
    {
        Shield.enabled = true;
        ShieldParticle.Play();
        Invoke("SetIdleState", GameManager.Instance.ShieldTime);
    }

    public void SetIdleState()
    {
        GameManager.Instance.ChangeState(GameState.Idle);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("coin"))
        {
            _uiManager.CoinCounterUpdate();
        }


        if (other.gameObject.CompareTag("boost"))
        {
            GameManager.Instance.ChangeState(GameState.Boosted);
        }


        if (other.gameObject.CompareTag("shield"))
        {
            GameManager.Instance.ChangeState(GameState.Shield);
        }


        if (other.gameObject.CompareTag("meteor"))
        {
            _uiManager.HealthDown();
        }

        
        if (other.gameObject.CompareTag("space"))
        {
            _uiManager.HealthDown();
        }

        other.gameObject.GetComponent<ItemMover>().SendBackToItemSpwannerList();
    }
}
