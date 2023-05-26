using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpwanner : MonoBehaviour
{
    public ItemSpwanner itemSpwanner;
    [SerializeField] private int _minSpwanCount;
    [SerializeField] private int _maxSpwanCount;
    [SerializeField] private int _minDelayTime;
    [SerializeField] private int _maxDelayTime;
    [SerializeField] private int _spwanSpeed;
    
    
    float spwanTime;

    int currentCount;
    float currentDelayTime;

    private void Start()
    {
        currentCount = Random.Range(_minSpwanCount, _maxSpwanCount);
        currentDelayTime = Random.Range(_minDelayTime, _maxDelayTime);
        spwanTime = 0.1f;
    }
    private void Update()
    {
        CoinSpwannerMethod();
    }
    public void CoinSpwannerMethod()
    {
        float speed = 5f;

        if (currentDelayTime > 0)
            currentDelayTime -= Time.deltaTime;
        if (currentDelayTime <= 0)
        {
            if (spwanTime > 0) spwanTime -= Time.deltaTime * speed;
            if (spwanTime <= 0)
            {
                itemSpwanner.GetIteFromList(1, transform.position);
                currentCount -= 1;
                spwanTime = 1;
            }
            if (currentCount <= 0)
            {
                currentCount = Random.Range(_minSpwanCount, _maxSpwanCount);
                currentDelayTime = Random.Range(_minDelayTime, _maxDelayTime);
                spwanTime = .1f;
                ChangePositionOfSpwanner();
            }
        }

    }
    public void ChangePositionOfSpwanner()
    {
        if (transform.position.x == 0)
        {
            int i = Random.Range(0, 2);
            if (i == 0) transform.position = new Vector3(-2, transform.position.y, transform.position.z);
            if (i == 1) transform.position = new Vector3(2, transform.position.y, transform.position.z);
        }
        else if (transform.position.x == -2)
        {

            int i = Random.Range(0, 2);
            if (i == 0) transform.position = new Vector3(0, transform.position.y, transform.position.z);
            if (i == 1) transform.position = new Vector3(2, transform.position.y, transform.position.z);
        }
        else if (transform.position.x == 2)
        {
            int i = Random.Range(0, 2);
            if (i == 0) transform.position = new Vector3(0, transform.position.y, transform.position.z);
            if (i == 1) transform.position = new Vector3(-2, transform.position.y, transform.position.z);
        }
    }
}

