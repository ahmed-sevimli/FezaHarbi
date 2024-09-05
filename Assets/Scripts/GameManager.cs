using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int killCount;
    private int currTargetCount;
    private bool debugModeOn = false;
    private bool playerIsDead = false;

    [SerializeField]
    private Dropdown levelsDropdown;
    private int selectedLevel;
    private int lastUnlockedLevel=0;
    private Box box;
    private Character _char;

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

    public void SubscribeBoxAction(Box box)
    {
        this.box = box;
        box.OnBoxDestroy += CheckState;
    }
    
    public void SubscribeCharacterEvent(Character _char)
    {
        this._char = _char;
        _char.CharDestruction += CheckState;
    }

    void CheckState(string name, string type)
    {
        //Check for win and loss
        Debug.Log(name + " has been destroyed");
        Invoke("WinState",1f);
        Invoke("LoseState",1f);

        //Unsubscribe
        Debug.Log("Checkstate " + type);
        if(type == "Box")
        {
            box.OnBoxDestroy -= CheckState;
        }
        else if(type == "Character")
        {
            //Check if the player is dead
            if(name == "Player")
            {
                playerIsDead = true;
            }
            _char.CharDestruction -= CheckState;
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
        debugModeOn = !debugModeOn;
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

    void SpawnEnemy()
    {
        return;
    }
}
