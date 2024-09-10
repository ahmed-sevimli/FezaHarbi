using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text debugText;
    public int killCount;
    public int currTargetCount;
    private bool debugModeOn = true;
    private bool playerIsDead = false;
    public bool lastLevelWon = false;
    public bool lastLevelLost = false;

    [SerializeField]
    private Dropdown levelsDropdown;
    private int selectedLevel;
    private int lastUnlockedLevel=0;
    //private Box[] boxes;
    //private Character[] characters;

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
    }

    void Start()
    {
        //Subscribe
        Character.CharDestruction += CheckState;
    }


    void Update()
    {
        if(debugModeOn)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                NextLevel();
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                LoseLevel();
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

    public void StartGame()
    {
		//Debug.Log(LevelController.GetActiveLevel() + " is ActiveScene");
		LoadLevel("Level1");
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

    void SelectLevel()
    {
        //Code for dropdown
        //requestedLevel = dropdown.choice;
        
        //unlock all levels
        if(debugModeOn)
        {
            lastUnlockedLevel = 8;
        }

        //try and load selected level
        if(selectedLevel <= lastUnlockedLevel)
        {
            LoadLevel("Level" + selectedLevel);
        }
    }

    public void ChangeDebugMode()
    {
        debugModeOn =! debugModeOn;
        if(debugModeOn)
        {
            debugText.text = "Debug Mode: On";
        }
        else
        {
            debugText.text = "Debug Mode: Off";
        }
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
}
