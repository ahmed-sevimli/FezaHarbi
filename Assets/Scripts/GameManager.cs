using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Text debugText;
    private int killCount;
    private int currTargetCount;
    private bool debugModeOn = false;
    private bool playerIsDead = false;

    [SerializeField]
    private Dropdown levelsDropdown;
    private int selectedLevel;
    private int lastUnlockedLevel=0;
    //private Box[] boxes;
    //private Character[] characters;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //Singleton mimarisi
        if (Instance != null && Instance != this)
        { 
            Destroy(this);
        }
        else 
        { 
            Instance = this; 
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
                Debug.Log("You Won!!!");
                NextLevel();
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("You Lost!!!");
                LoseLevel();
            }
        }
    }

    void CheckState(string name)
    {
        //Check for win and loss
        Debug.Log(name + " has been destroyed");
        Invoke("WinState",1f);
        Invoke("LoseState",1f);

        //Check if the player is dead
        if(name == "Player")
        {
            playerIsDead = true;
        }
    }

    public void LoadLevel(string levelToLoad)
    {
        SceneManager.LoadScene(levelToLoad);
        //reset counts
        killCount = 0;
        currTargetCount = LevelController.Instance.GetTargetCount();
    }

    public void StartGame()
    {
		//Debug.Log(LevelController.GetActiveLevel() + " is ActiveScene");
		LoadLevel("Level1");
    }

    void RestartGame()
    {
		//Debug.Log(LevelController.GetActiveLevel() + " is ActiveScene");
		LoadLevel("Level3");
    }

    void LoseLevel()
    {
        LoadLevel("GameOver");
    }

    void NextLevel()
    {
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
            Debug.Log("You Won!!!");
            LoadLevel(LevelController.Instance.GetNextLevel());
        }
        else
        {
            Debug.Log("Not a win yet");
        }
    }

    void LoseState()
    {
        if(playerIsDead)
        {
            Debug.Log("You Lost!!!");
            //Debug.Log(LevelController.GetActiveLevel() + " is ActiveScene");
		    LoseLevel();
            playerIsDead = false;
        }
    }

    void OnDisable()
    {
        //Unsubscribe
        Character.CharDestruction -= CheckState;
    }
}
