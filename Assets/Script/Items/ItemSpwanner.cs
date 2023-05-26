using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum ItemList
{
    Boost = 0,
    Coin = 1,
    Metaor = 2,
    Shield = 3,
    SpaceShip = 4
}

[System.Serializable]
public class Item
{
    public List<Transform> _item = new List<Transform>();
}

public class ItemSpwanner : MonoBehaviour
{
    [SerializeField] ItemList _currentItem;
    public List<Item> _item = new List<Item>();

    [SerializeField] private Transform _boostItemList;
    [SerializeField] private Transform _coinItemList;
    [SerializeField] private Transform _meteorItemList;
    [SerializeField] private Transform _shieldItemList;
    [SerializeField] private Transform _spaceshipItemList;
    [SerializeField] private SpriteMover SP_1;
    [SerializeField] private SpriteMover SP_2;

    [SerializeField] private Transform _itemSpwannedConainer;

    [SerializeField] private float _itemSwitchTime = 5;
    [SerializeField] private float _positionSwitchRate = 0.45f;
    [SerializeField] private float _ItemIdleSpeed;
    [SerializeField] private float _ItemBoostSpeed;

    public float _currentItemSpeed;

    private void Start()
    {
        GetItemInList();
        InvokeRepeating("SpwanRandomObstacle", .1f, _itemSwitchTime);
        InvokeRepeating("ChangePositionOfSpwanner", .1f, _positionSwitchRate);
        _currentItemSpeed = _ItemIdleSpeed;
    }

    private void OnEnable()
    {
        GameManager.OnIdleState += OnIdleStateStart;
        GameManager.OnBoostState += OnBoostStateStart;
    }
    private void OnDisable()
    {
        GameManager.OnIdleState -= OnIdleStateStart;
        GameManager.OnBoostState -= OnBoostStateStart;
    }
    private void OnBoostStateStart(object sender, EventArgs e)
    {
        _currentItemSpeed = _ItemBoostSpeed;
        SP_1._currentScrollSpeed = -0.5f;
        SP_2._currentScrollSpeed = -01f;
        Invoke("SetIdleState", GameManager.Instance.BoostTime);
    }

    public void SetIdleState()
    {
        GameManager.Instance.ChangeState(GameState.Idle);
    }

    private void OnIdleStateStart(object sender, EventArgs e)
    {
        _currentItemSpeed = _ItemIdleSpeed;
        SP_1._currentScrollSpeed = -0.1f;
        SP_2._currentScrollSpeed = -0.3f;
    }

    public void GetItemInList()
    {
        foreach (Transform item in _boostItemList)
        {
            if (!_item[0]._item.Contains(item))
            {
                _item[0]._item.Add(item);
                item.GetComponent<ItemMover>().ItemId = 0;
                item.GetComponent<ItemMover>().itemSpwanner = this;
                item.gameObject.SetActive(false); ;
            }
        }
        foreach (Transform item in _coinItemList)
        {
            if (!_item[1]._item.Contains(item))
            {
                _item[1]._item.Add(item);
                item.GetComponent<ItemMover>().ItemId = 1;
                item.GetComponent<ItemMover>().itemSpwanner = this;
                item.gameObject.SetActive(false);
            }
        }
        foreach (Transform item in _meteorItemList)
        {
            if (!_item[2]._item.Contains(item))
            {
                _item[2]._item.Add(item);
                item.GetComponent<ItemMover>().ItemId = 2;
                item.GetComponent<ItemMover>().itemSpwanner = this;
                item.gameObject.SetActive(false);
            }
        }
        foreach (Transform item in _shieldItemList)
        {
            if (!_item[3]._item.Contains(item))
            {
                _item[3]._item.Add(item);
                item.GetComponent<ItemMover>().ItemId = 3;
                item.GetComponent<ItemMover>().itemSpwanner = this;
                item.gameObject.SetActive(false);
            }
        }
        foreach (Transform item in _spaceshipItemList)
        {
            if (!_item[4]._item.Contains(item))
            {
                _item[4]._item.Add(item);
                item.GetComponent<ItemMover>().ItemId = 4;
                item.GetComponent<ItemMover>().itemSpwanner = this;
                item.gameObject.SetActive(false);
            }
        }
    }

    float _itemSwitchCurrentTime = 0;

    public void SpwanRandomObstacle()
    {
        _currentItem = (ItemList)UnityEngine.Random.Range(0, 5);
        switch (_currentItem)
        {
            case ItemList.Boost:
                GetIteFromList(0, transform.position);
                break;

            case ItemList.Coin:
                GetIteFromList(1, transform.position);
                break;

            case ItemList.Metaor:
                GetIteFromList(2, transform.position);
                break;

            case ItemList.Shield:
                GetIteFromList(3, transform.position);
                break;

            case ItemList.SpaceShip:
                GetIteFromList(4, transform.position);
                break;
        }


    }

    public void ChangePositionOfSpwanner()
    {
        if (transform.position.x == 0)
        {
            int i = UnityEngine.Random.Range(0, 2);
            if (i == 0) transform.position = new Vector3(-2, transform.position.y, transform.position.z);
            if (i == 1) transform.position = new Vector3(2, transform.position.y, transform.position.z);
        }
        else if (transform.position.x == -2)
        {

            int i = UnityEngine.Random.Range(0, 2);
            if (i == 0) transform.position = new Vector3(0, transform.position.y, transform.position.z);
            if (i == 1) transform.position = new Vector3(2, transform.position.y, transform.position.z);
        }
        else if (transform.position.x == 2)
        {
            int i = UnityEngine.Random.Range(0, 2);
            if (i == 0) transform.position = new Vector3(0, transform.position.y, transform.position.z);
            if (i == 1) transform.position = new Vector3(-2, transform.position.y, transform.position.z);
        }
    }

    public void GetIteFromList(int ID, Vector3 pos)
    {
        if (_item[ID]._item.Count > 0 && !GameManager.Instance.isDead)
        {
            Transform getItem = _item[ID]._item[0];
            _item[ID]._item.Remove(_item[ID]._item[0]);
            getItem.parent = _itemSpwannedConainer;
            getItem.GetComponent<ItemMover>().MoveSpeed = _currentItemSpeed;
            getItem.localPosition = new Vector3(pos.x, 0, 0);
            getItem.gameObject.SetActive(true);
        }
    }

    public void SetParant(Transform t, int ID)
    {

        switch (ID)
        {
            case 0:
                t.parent = _boostItemList;
                break;

            case 1:
                t.parent = _coinItemList;
                break;

            case 2:
                t.parent = _meteorItemList;
                break;

            case 3:
                t.parent = _shieldItemList;
                break;

            case 4:
                t.parent = _spaceshipItemList;
                break;

        }

    }
}
