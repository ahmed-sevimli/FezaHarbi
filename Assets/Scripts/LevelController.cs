using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public static string activeLevel;
    public static string nextLevel;
    public static int targetCount;

    void Start()
    {
        SetActiveLevel();
    }

    public static string GetActiveLevel()
    {
        return activeLevel;
    }

    public void SetActiveLevel()
    {
        activeLevel = SceneManager.GetActiveScene().name;
    }

    public static string GetNextLevel()
    {
        //Next Level ataması UI üzerinden yapılır
        return nextLevel;
    }

    public static int GetTargetCount()
    {
        return targetCount;
    }
}