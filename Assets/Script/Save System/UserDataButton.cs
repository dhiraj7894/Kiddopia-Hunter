using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UserDataButton : MonoBehaviour
{
    public TextMeshProUGUI UserName;
    public TextMeshProUGUI UserScore;

    public void SetUserData(string name, int score){
        UserName.text = name;
        UserScore.text = score.ToString();
    }

    public void PlayGame(){
        UserProfileManager.Instance._userName = UserName.text;
        SceneManager.LoadScene(1);
    }
}
