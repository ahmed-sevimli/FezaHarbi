using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject PauseMenu;
    public int killCount;
    public int currTargetCount;
    public bool debugModeOn = true;
    private bool playerIsDead = false;
    public bool lastLevelWon = false;
    public bool lastLevelLost = false;

    void Awake()
    {
        //Singleton mimarisi
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //Close Pause Menu
        PauseMenu.SetActive(false);
    }

    void Start()
    {
        //Subscribe
        Character.CharDestruction += CheckState;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().name != "MainMenu")
        {
            if(debugModeOn)
            {
                //Level Control
                if(Input.GetKeyDown(KeyCode.Alpha1))
                {
                    NextLevel();
                }
                else if(Input.GetKeyDown(KeyCode.Alpha2))
                {
                    LoseLevel();
                }

                //Enemy Control
                if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    Enemy.enemyCanShoot = !Enemy.enemyCanShoot;
                }
            }

            //Pause Control
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                PauseOrResumeGame();
            }
        }
    }

    void CheckState(string name)
    {
        //raise kill count
        killCount++;
        Debug.Log(name + " has been destroyed");
        //Check if the player is dead
        if(name == "Player")
        {
            LoseLevel();
        }
        else
        {
            //Check for win
            WinState();
        }
    }

    public void LoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
        //reset current kills and get new targetCount
        killCount = 0;
        Invoke("GetCurrTargetCount",0.2f);
    }

    public void GetCurrTargetCount()
    {
        currTargetCount = LevelController.Instance.GetTargetCount();
        Debug.Log("curr target count is " + currTargetCount);
    }

    void LoseLevel()
    {
        lastLevelLost = true;
        lastLevelWon = false;
        PlayerPrefs.SetString("lastLevelLost", "true");
        Debug.Log("You Lost!!!");
        Debug.Log("Here is the lastevelLost state: " + lastLevelLost);
        //Debug.Log(LevelController.GetActiveLevel() + " is ActiveScene");
        LoadLevel("MainMenu");
    }

    void NextLevel()
    {
        Debug.Log("You Won!!!");
        lastLevelWon = true;
        LoadLevel(LevelController.Instance.GetNextLevel());
    }

    public void GoToMainMenu()
    {
        //Close Pause Screen and Return
        lastLevelWon = false;
        lastLevelLost = false;
        PauseOrResumeGame();
        LoadLevel("MainMenu");
    }
    
    void WinState()
    {
        Debug.Log("WinState Check");
        if(killCount >= currTargetCount)
        {
            NextLevel();
        }
        else
        {
            //Debug.Log("Not a win yet");
        }
    }

    void OnDisable()
    {
        //Unsubscribe
        Character.CharDestruction -= CheckState;
    }

    public void PauseOrResumeGame()
    {
        Time.timeScale = 1 - Time.timeScale;
        PauseMenu.SetActive(!PauseMenu.activeInHierarchy);
    }
}
