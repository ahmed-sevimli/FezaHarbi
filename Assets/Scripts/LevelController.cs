using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    private static string[] levels = new string[8];
    public static int activeLevel;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //Initialize and set levels array
        int i;
        levels[0] = "GameOver";
        for(i=1;i<levels.Length-2;i++)
        {
            levels[i] = "Level"+ i.ToString();
        }
        levels[i] = "GameWon";
    }

    void Start()
    {
        GameManager.Instance.LevelPass += NextLevel;
        GameManager.Instance.LevelLost += LoseGame;
        GameManager.Instance.LevelSelect += SelectLevel;
        GameManager.Instance.StartTheGame += StartGame2;
        GameManager.Instance.RestartTheGame += RestartGame2;
    }

    void OnDisable()
    {
        GameManager.Instance.LevelPass -= NextLevel;
        GameManager.Instance.LevelLost -= LoseGame;
        GameManager.Instance.LevelSelect -= SelectLevel;
        GameManager.Instance.StartTheGame -= StartGame2;
        GameManager.Instance.RestartTheGame -= RestartGame2;
    }

    void LoadActiveLevel()
    {
        Debug.Log(activeLevel + " is ActiveLevelNum");
        Debug.Log(levels[activeLevel] + " is ActiveLevel");
        SceneManager.LoadScene(levels[activeLevel]);
    }

    public static string GetActiveLevel()
    {
        return levels[activeLevel];
    }

    void LoseGame()
    {
        activeLevel = 0;
		LoadActiveLevel();
    }

    void StartGame2()
    {
        Debug.Log(SceneManager.GetActiveScene().name + " is ActiveScene");
        activeLevel = 1;
		LoadActiveLevel();
    }

    void RestartGame2()
    {
        activeLevel = 3;
		LoadActiveLevel();
    }

    void NextLevel()
    {
        string tmp = SceneManager.GetActiveScene().name;
        Debug.Log("tmp = " + tmp);
        tmp = tmp.Remove(0,tmp.Length-1);
        Debug.Log("tmp = " + tmp);
        int temp = Int32.Parse(tmp);
        Debug.Log("tmp = " + temp);
        activeLevel = temp + 1;
        LoadActiveLevel();
    }

    void SelectLevel(int requestedLevel)
    {
        activeLevel = requestedLevel;
		LoadActiveLevel();
    }
}
