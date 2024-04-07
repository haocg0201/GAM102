using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject scoreBoard;
    public GameObject scoreBoardDead;

    [SerializeField] private int score = 0;
    [SerializeField] private int highScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreDeadText;
    public TextMeshProUGUI highScoreText;
    

    private void Start()
    {
        LoadGame();
        SetTextScore();
    }
    public void SaveGame()
    {
        string encrypt = Extension.Encrypt(highScore.ToString(),"12345678Ehehe");
        PlayerPrefs.SetString("score", encrypt);
    }
    public void LoadGame()
    {
        string decrypt = PlayerPrefs.GetString("score");
        if (!string.IsNullOrEmpty(decrypt))
        {
            highScore = int.Parse(Extension.Decrypt(decrypt, "12345678Ehehe"));
            
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

    public void PlayerDead()
    {
        if (score > highScore)
        {
            highScore = score;
            SaveGame();
        }
        scoreBoard.SetActive(false); // ẩn bảng ở trên bên trái
        scoreBoardDead.SetActive(true); // hiện bảng end game ở giữa

        scoreDeadText.text = "Score: " + score.ToString("n0");
        highScoreText.text = "Score: " + highScore.ToString("n0");
    }

    public void BtnPlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    public void BtnHome()
    {
        SceneManager.LoadScene(0);
    }

    public int GetScore()
    {
        return score;
    }

    public void SubtractScore()
    {
        score--;
    }
}
