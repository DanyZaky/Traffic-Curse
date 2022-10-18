using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    public static MainMenuController Instance { get; internal set; }

    private Animator anim;
    [SerializeField] private Button[] menuButton;
    public Transform entryParent;
    public GameObject entryPrefab;

    [Header("Story")]
    [SerializeField] private TextMeshProUGUI textTMP;
    [SerializeField] private Image imageThumb;
    [SerializeField] private string[] textDesc;
    [SerializeField] private Sprite[] imageTex;

    private int index;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    void Start()
    {
        foreach (Transform item in entryParent.transform)
        {
            Destroy(item.gameObject);
        }

        anim = gameObject.GetComponent<Animator>();
        anim.Play("idleStartMenu");
        StartCoroutine(GameManager.Instance.RequestGetScore());
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

    private List<LeaderboardScoreEntry> SetupEntry()
    {
        GameManager bc = GameManager.Instance;
        List<LeaderboardScoreEntry> entryList = new List<LeaderboardScoreEntry>();

        for (int i = 0; i < bc.userScoreDB.results.Count; i++)
        {
            entryList.Add(Instantiate(entryPrefab, entryParent).GetComponent<LeaderboardScoreEntry>());
        }

        return entryList;
    }

    private void DataEntry(List<LeaderboardScoreEntry> entryList)
    {
        GameManager bc = GameManager.Instance;
        for (int i = 0; i < bc.userScoreDB.results.Count; i++)
        {
            if (string.IsNullOrEmpty(bc.userScoreDB.results[i].username)) entryList[i].nickname.text = i + 1 + ". " + "<No Name>";
            else entryList[i].nickname.text = i + 1 + ". " + bc.userScoreDB.results[i].username;

            entryList[i].score.text = bc.userScoreDB.results[i].score.ToString();
        }
    }

    private int SortByScore(GameManager.UserScore p1, GameManager.UserScore p2)
    {
        return p2.score.CompareTo(p1.score);
    }

    public void LeaderboardGetScore()
    {
        List<LeaderboardScoreEntry> entryList = SetupEntry();
        GameManager.Instance.userScoreDB.results.Sort(SortByScore);
        DataEntry(entryList);
    }
}
