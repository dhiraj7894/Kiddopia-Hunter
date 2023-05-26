using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartButton : MonoBehaviour
{
    public TMP_InputField UserName;
    public void StartGame()
    {
        if (string.IsNullOrEmpty(UserName.text)) return;

        if (UserData.Instance._userData.Count <= 0)
        {
            UserData.Instance._userData.Add(new UserScoreData(UserName.text, 0));
            SaveData.Instance.SaveToJeson<UserScoreData>(UserData.Instance._userData);
        }

        foreach (UserScoreData item in UserData.Instance._userData.ToArray())
        {
            if (item.name != UserName.text)
            {
                UserData.Instance._userData.Add(new UserScoreData(UserName.text, 0));
                SaveData.Instance.SaveToJeson<UserScoreData>(UserData.Instance._userData);
                Debug.Log("Saved Data");
            }
        }
    }
    public void StartLevel()
    {
        if (string.IsNullOrEmpty(UserName.text)) return;
        UserProfileManager.Instance._userName = UserName.text;
        SceneManager.LoadScene(1);
    }
}
