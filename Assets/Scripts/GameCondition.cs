using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameCondition : MonoBehaviour
{
    public float score;
    
    public float timeLeft;
    private float timeLeftCounter;
    
    public Image fillTimeLeft;
    public TextMeshProUGUI scoreText, finalScoreText, usernameText;
    public GameObject winPanel;

    private void Start()
    {
        fillTimeLeft.fillAmount = 0;
        timeLeftCounter = 0;
        winPanel.SetActive(false);
    }

    private void Update()
    {
        timeLeftCounter += 1f * Time.deltaTime;
        fillTimeLeft.fillAmount = timeLeftCounter / timeLeft;

        if(timeLeftCounter >= timeLeft)
        {
            fillTimeLeft.fillAmount = timeLeftCounter / timeLeft;
            winPanel.SetActive(true);
            finalScoreText.SetText(score.ToString("0"));
            Time.timeScale = 0;
        }

        Score();
    }

    private void Score()
    {
        score += 2f * Time.deltaTime;

        scoreText.SetText(score.ToString("0"));
    }

    public void Restart()
    {
        Time.timeScale = 1;
        timeLeftCounter = 0;
        GameManager.Instance.lastUsername = usernameText.text;
        GameManager.Instance.lastScore = int.Parse(finalScoreText.text);
        GameManager.Instance.RequestPostAccountAndPutScore();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
