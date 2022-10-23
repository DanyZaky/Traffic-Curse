using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameCondition : MonoBehaviour
{
    public static GameCondition Instance { get; internal set; }
        
    public float timeLeft;
    private float timeLeftCounter;
    
    public Image fillTimeLeft;
    public TextMeshProUGUI scoreText, finalScoreText, usernameText;
    public GameObject winPanel;

    public bool isGameOver;

    [Header("Scoring")]
    public float totalScore;
    public int countHappyKid, countSadKid, countMobileKeganggu, countMobileLolos;
    public float timeSpentMobilKeganggu;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

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

        if(timeLeftCounter >= timeLeft && !isGameOver)
        {
            isGameOver = true;
            CalculateScore();
            fillTimeLeft.fillAmount = timeLeftCounter / timeLeft;
            winPanel.SetActive(true);
            finalScoreText.SetText(totalScore.ToString("0"));
            Time.timeScale = 0;
        }

        
    }

    private void CalculateScore()
    {
        foreach (var item in GameObject.FindGameObjectsWithTag("Grab"))
        {
            if (item.GetComponent<AnakSDHandler>().isSad)
            {
                countSadKid++;
            }
            else
            {
                countHappyKid++;
            }
            
        }

        totalScore += countHappyKid * 8;
        totalScore -= countSadKid * 6;
        totalScore += countMobileLolos * 50;
        totalScore -= countMobileKeganggu * 12;
        totalScore -= timeSpentMobilKeganggu * 1.2f;

        scoreText.SetText(totalScore.ToString("0"));
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
