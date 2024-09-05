using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int kill_count;
    private bool debugModeOn = false;

    [SerializeField]
    private Dropdown levelsDropdown;
    private int selectedLevel;
    private Box box;
    private Character _char;

    public delegate void LevelSelector(int requestedLevel);
    public event LevelSelector LevelSelect;
    public delegate void ManageGame();
    public event ManageGame LevelPass;
    public event ManageGame LevelLost;
    public event ManageGame StartTheGame;
    public event ManageGame RestartTheGame;

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

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        if(debugModeOn)
        {
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                Debug.Log("You Won!!!");
                LevelPass();
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                Debug.Log("You Lost!!!");
                LevelLost();
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
            _char.CharDestruction -= CheckState;
        }
    }

    public void StartGame()
    {
		if(StartTheGame != null)
        {
            StartTheGame();
        }
    }

    void RestartGame()
    {
		if(RestartTheGame != null)
        {
            RestartTheGame();
        }
        else
        {
            Debug.Log("RestartGame is NULL!!!!");
        }
    }

    public void ChangeDebugMode()
    {
        debugModeOn = !debugModeOn;
    }

    public void GoToSelectedLevel()
    {
        LevelSelect(selectedLevel);
    }

    int GetEnemyCount()
    {
        return 0;
    }
    
    void WinState()
    {
        Debug.Log("WinState Check");
        if(String.Equals(SceneManager.GetActiveScene().name,"Level1")
        || String.Equals(SceneManager.GetActiveScene().name,"Level2"))
        {
            if(!GameObject.FindWithTag("Cubes"))
            {
                Debug.Log("You Won!!!");
                if(LevelPass != null)
                {
                    LevelPass();
                }
            }
        }
        else if(!GameObject.FindWithTag("Enemies"))
        {
            Debug.Log("You Won!!!");
            if(LevelPass != null)
            {
                Debug.Log("Not null");
                LevelPass();
            }
        }
        else
        {
            Debug.Log("No cube, enemies still at large.");
        }
    }

    void LoseState()
    {
        if(!GameObject.FindWithTag("Player"))
        {
            Debug.Log("You Lost!!!");
            if(LevelLost != null)
            {
                LevelLost();
            }
        }
    }

    void SpawnEnemy()
    {
        return;
    }
}
