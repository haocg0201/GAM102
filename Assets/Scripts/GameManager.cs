using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int score;
    public TextMeshProUGUI scoreText;

    private void Start()
    {
        LoadGame();
        SetTextScore();
    }
    public void SaveGame()
    {
        string encrypt = Extension.Encrypt(score.ToString(),"12345678Ehehe");
        PlayerPrefs.SetString("score", encrypt);
    }
    public void LoadGame()
    {
        string decrypt = PlayerPrefs.GetString("score");
        if (decrypt != null)
        {
            score = int.Parse(Extension.Decrypt(decrypt, "12345678Ehehe"));
        }
        
    }
    public void SetTextScore()
    {
        scoreText.text = "Score: " + score.ToString("n0");
    }

    public void AddScore()
    {
        score++;
    }
}
