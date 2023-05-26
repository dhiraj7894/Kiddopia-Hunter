using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UserScoreData
{
    public string name;
    public int score;

    public UserScoreData(string _name, int _score)
    {
        name = _name;
        score = _score;
    }
}

public class UserData : Singleton<UserData>
{
    public Transform Container;
    public Transform UserDataTamplate;

    public List<UserScoreData> _userData = new List<UserScoreData>();

    private void Start()
    {

        if (!GameObject.Find("Scroll View").activeSelf)
        {
            Container = GameObject.Find("Scroll View").transform.GetChild(0).transform;
            if (Container) UserDataTamplate = Container.GetChild(0).transform;
        }
        else
        {
            Container = GameObject.Find("Scroll View").transform.GetChild(0).transform;
            if (Container) UserDataTamplate = Container.GetChild(0).transform;
        }




        _userData = SaveData.ReadListFromJSON<UserScoreData>();
        UserDataTamplate.gameObject.SetActive(false);
        UpdateVisual();
    }
    public void UpdateVisual()
    {
        foreach (Transform item in Container)
        {
            if (item == UserDataTamplate)
                continue;
            Destroy(item.gameObject);
        }

        foreach (var item in _userData)
        {
            Transform user = Instantiate(UserDataTamplate, Container);
            user.gameObject.SetActive(true);
            user.GetComponent<UserDataButton>().SetUserData(item.name, item.score);
        }
    }
}
