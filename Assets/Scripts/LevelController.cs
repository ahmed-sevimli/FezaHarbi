using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    public string activeLevel;
    public string nextLevel;
    public int targetCount;

    void Awake()
    {
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
        SetActiveLevel();
    }

    public string GetActiveLevel()
    {
        return activeLevel;
    }

    public void SetActiveLevel()
    {
        activeLevel = SceneManager.GetActiveScene().name;
    }

    public string GetNextLevel()
    {
        //Next Level ataması UI üzerinden yapılır
        return nextLevel;
    }

    public int GetTargetCount()
    {
        return targetCount;
    }
}