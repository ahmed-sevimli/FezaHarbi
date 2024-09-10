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
        SetActiveLevel();
        
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

    public string GetActiveLevel()
    {
        //Debug.Log("Someone is getting " + activeLevel);
        return activeLevel;
    }

    public void SetActiveLevel()
    {
        activeLevel = SceneManager.GetActiveScene().name;
        //Debug.Log("Active Level is " + activeLevel);
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