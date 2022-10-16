using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private Button[] menuButton;

    [Header("Story")]
    [SerializeField] private TextMeshProUGUI textTMP;
    [SerializeField] private Image imageThumb;
    [SerializeField] private string[] textDesc;
    [SerializeField] private Sprite[] imageTex;

    private int index;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        anim.Play("idleStartMenu");
    }

    public void OnClickStartMenu()
    {
        StartCoroutine(Transition("trasitionMainMenu", "idleMainMenu"));
    }

    public void OnClickPlayButton()
    {
        StartCoroutine(Transition("transitionToStory", "idleStory"));
        textTMP.text = textDesc[0];
        imageThumb.sprite = imageTex[0];
    }

    public void OnClickLeaderboardButton()
    {
        StartCoroutine(Transition("transitionLeaderboard", "idleLeaderboard"));
    }

    public void OnClickBackFromLeaderboard()
    {
        StartCoroutine(Transition("transitionBackLeaderboard", "idleMainMenu"));
    }

    public void OnClickStepStory()
    {
        if(index < textDesc.Length - 1)
        {
            index++;
            StartCoroutine(TransitionStory("transitionPerStory", "idleStory", index));
            Debug.Log("p");
        }
        else
        {
            StartCoroutine(FadeToPlay("fadeToPlay"));
        }
    }

    private IEnumerator Transition(string transition, string idle)
    {
        anim.Play(transition);
        for (int i = 0; i < menuButton.Length; i++)
        {
            menuButton[i].interactable = false;
        }
        yield return new WaitForSecondsRealtime(1f);
        anim.Play(idle);
        for (int i = 0; i < menuButton.Length; i++)
        {
            menuButton[i].interactable = true;
        }
    }

    private IEnumerator TransitionStory(string transition, string idle, int value)
    {
        anim.Play(transition);
        for (int i = 0; i < menuButton.Length; i++)
        {
            menuButton[i].interactable = false;
        }
        yield return new WaitForSecondsRealtime(0.5f);

        textTMP.text = textDesc[value];
        imageThumb.sprite = imageTex[value];

        yield return new WaitForSecondsRealtime(0.5f);
        anim.Play(idle);
        for (int i = 0; i < menuButton.Length; i++)
        {
            menuButton[i].interactable = true;
        }
    }

    private IEnumerator FadeToPlay(string transition)
    {
        anim.Play(transition);
        for (int i = 0; i < menuButton.Length; i++)
        {
            menuButton[i].interactable = false;
        }
        yield return new WaitForSecondsRealtime(1.2f);

        SceneManager.LoadScene(1);
    }
}
