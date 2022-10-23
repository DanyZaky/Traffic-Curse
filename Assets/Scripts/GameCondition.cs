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
    public float timeLeftCounter;
    
    public Image fillTimeLeft;
    public TextMeshProUGUI scoreText, finalScoreText, usernameText;
    public GameObject winPanel;

    public bool isGameOver;
    private bool isFirst, isSecond, isThird, isFourth, isFifth, isSixth;
    [HideInInspector] public int childPickCount, childCalmed;

    
    public TextMeshProUGUI happyKids, sadKids, happyDriver, distractedDriver, timeSpentDistractedDriver;
    public TMP_InputField inputField;
    public Button menu, newDay;
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
        timeLeftCounter = timeLeft;
        winPanel.SetActive(false);
    }

    private void Update()
    {
        timeLeftCounter -= 1f * Time.deltaTime;
        fillTimeLeft.fillAmount = 1 - (timeLeftCounter / timeLeft);

        if(timeLeftCounter <= 0 && !isGameOver)
        {
            isGameOver = true;
            CalculateScore();
            fillTimeLeft.fillAmount = timeLeftCounter / timeLeft;
            winPanel.SetActive(true);
            finalScoreText.SetText(totalScore.ToString("0"));
            Time.timeScale = 0;
        }

        GainExp();

        if (inputField.text.Length >= 5)
        {
            menu.interactable = true;
            newDay.interactable = true;
        }
        else
        {
            menu.interactable = false;
            newDay.interactable = false;
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

        happyKids.text = countHappyKid.ToString("n0");
        sadKids.text = countSadKid.ToString("n0");
        happyDriver.text = countMobileLolos.ToString("n0");
        distractedDriver.text = countMobileKeganggu.ToString("n0");
        timeSpentDistractedDriver.text = timeSpentMobilKeganggu.ToString("n0");

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

    public void Home()
    {
        Time.timeScale = 1;
        timeLeftCounter = 0;
        GameManager.Instance.lastUsername = usernameText.text;
        GameManager.Instance.lastScore = int.Parse(finalScoreText.text);
        GameManager.Instance.RequestPostAccountAndPutScore();
        SceneManager.LoadScene("Main Menu");
    }

    private void GainExp()
    {
        if (timeLeftCounter < timeLeft * 0.8f && !isFirst)
        {
            isFirst = true;
            PlayerTechTreeSkillManager.Instance.skillPoint++;
            PlayerTechTreeSkillManager.Instance.gainedSkillpoint++;
        }

        if (timeLeftCounter < timeLeft * 0.5f && !isSecond)
        {
            isSecond = true;
            PlayerTechTreeSkillManager.Instance.skillPoint++;
            PlayerTechTreeSkillManager.Instance.gainedSkillpoint++;
        }

        if (childPickCount == 1 && !isThird)
        {
            isThird = true;
            PlayerTechTreeSkillManager.Instance.skillPoint++;
            PlayerTechTreeSkillManager.Instance.gainedSkillpoint++;
        }

        if (childPickCount == 5 && !isFourth)
        {
            isFourth = true;
            PlayerTechTreeSkillManager.Instance.skillPoint++;
            PlayerTechTreeSkillManager.Instance.gainedSkillpoint++;
        }

        if (countMobileKeganggu == 2 && !isFifth)
        {
            isFifth = true;
            PlayerTechTreeSkillManager.Instance.skillPoint++;
            PlayerTechTreeSkillManager.Instance.gainedSkillpoint++;
        }

        if (childCalmed == 3 && !isSixth)
        {
            isSixth = true;
            PlayerTechTreeSkillManager.Instance.skillPoint++;
            PlayerTechTreeSkillManager.Instance.gainedSkillpoint++;
        }
    }
}
