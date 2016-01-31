using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    #region singleton
    // singleton
    private static LevelManager levelManager;
    public static LevelManager Instance()
    {
        if (!levelManager) {
            levelManager = FindObjectOfType(typeof(LevelManager)) as LevelManager;
            if (!levelManager) {
                Debug.LogWarning("There needs to be one active LevelManager on a GameObject in your scene.");
                return null;
            }
        }

        return levelManager;
    }
    #endregion

    [SerializeField]
    private bool loadLevels = true;
    [SerializeField]
    private string[] motivationalShouts;
    [SerializeField]
    private string[] loserShouts;

    private GameObject UI_GO;
    private Text countText;
    private GameObject endLevelPanel;


    private int letterCount = 0;
    private int maxCountForLevel = 0;
    private bool endingScreenShowing = false;
    private int currentlevel = 0;

    public int deathCount = 0;

    void Awake()
    {
        UI_GO = GameObject.FindGameObjectWithTag("UI");
        Assert.IsNotNull<GameObject>(UI_GO);
        DontDestroyOnLoad(UI_GO);

        countText = UI_GO.transform.FindChild("Briefcount").GetComponentInChildren<Text>();
        Assert.IsNotNull<Text>(countText);
        endLevelPanel = UI_GO.transform.FindChild("EndScreen").gameObject;
        Assert.IsNotNull<GameObject>(endLevelPanel);
        endLevelPanel.SetActive(false);

        DontDestroyOnLoad(gameObject);
        OnLevelWasLoaded(0);
    }

    void Update()
    {
        if (endingScreenShowing) {
            if (Input.GetKeyDown(KeyCode.F)) {
                ReloadLevel();
            }
            else if (Input.GetKeyDown(KeyCode.J)){
                LoadNextLevel();
            }
        }
    }

    void OnLevelWasLoaded(int level)
    {
        letterCount = 0;

        maxCountForLevel = 0;
        //Time.timeScale = 1f;
        GameObject[] pickups = GameObject.FindGameObjectsWithTag("Pickup");
        for (int i = 0; i < pickups.Length; ++i) {
            maxCountForLevel++;
        }
        Debug.Log("Found so many fishies: " + maxCountForLevel);

        UpdateCount();
    }

    public void Died()
    {
        deathCount++;
        if(deathCount >= 5)
        {
            LevelExited();
        }
    }

    public void LevelExited()
    {
        deathCount = 0;
        Text text = endLevelPanel.transform.GetChild(1).GetComponentInChildren<Text>();
        text.text = letterCount + " / " + maxCountForLevel;
        Text shout = endLevelPanel.transform.GetChild(1).GetChild(4).GetComponentInChildren<Text>();
        shout.text = ChooseMotivationalShout();
        endingScreenShowing = true;
        endLevelPanel.SetActive(true);
        //Time.timeScale = 0f;
    }

    string ChooseMotivationalShout()
    {
        int t = Mathf.RoundToInt(Random.Range(0, motivationalShouts.Length-1));
        return motivationalShouts[t];
    }

    string ChooseLoseShout()
    {
        int t = Mathf.RoundToInt(Random.Range(0, loserShouts.Length - 1));
        return loserShouts[t];
    }

    public void LoadNextLevel()
    {
        endLevelPanel.SetActive(false);
        endingScreenShowing = false;
        currentlevel++;
        Debug.Log("ENDING LEVEL");
        if (loadLevels)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReloadLevel()
    {
        if (currentlevel == 0) {
            Destroy(UI_GO);
            Destroy(gameObject);
        }
        else {
            endLevelPanel.SetActive(false);
            endingScreenShowing = false;
        }

        Debug.Log("RELOADING");
        if (loadLevels)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddLetters(int amount)
    {
        letterCount += amount;
        UpdateCount();
    }

    void UpdateCount()
    {
        countText.text = letterCount.ToString();
    }
}
