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
    public int killCount;
    public int currTargetCount;
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
            //Level Control
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

            //Enemy Control
            if (Input.GetKeyUp(KeyCode.Alpha3))
            {
                Enemy.enemyCanShoot = !Enemy.enemyCanShoot;
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
        Invoke("GetCurrTargetCount",0.2f);
    }

    public void GetCurrTargetCount()
    {
        //reset killCount and get new targetCount
        killCount = 0;
        currTargetCount = LevelController.Instance.GetTargetCount();
        Debug.Log("curr target count is " + currTargetCount);
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
        Debug.Log("You Lost!!!");
        //Debug.Log(LevelController.GetActiveLevel() + " is ActiveScene");
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

    void OnDisable()
    {
        //Unsubscribe
        Character.CharDestruction -= CheckState;
    }
}
