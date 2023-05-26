using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserProfileManager : Singleton<UserProfileManager>
{

    public string _userName;
    public int Score;

    private void Start() {
    }
    public void SaveGameData(int score){
        Debug.Log("Test");
        foreach (var item in UserData.Instance._userData)
        {
            if (item.name == _userName)
            {
                if(score > item.score)item.score = score;
                SaveData.Instance.SaveToJeson<UserScoreData>(UserData.Instance._userData);
                Debug.Log("Saved Data");
            }
        }
    }

}
