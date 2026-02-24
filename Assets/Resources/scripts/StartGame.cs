using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class StartGame : MonoBehaviour
{
    public Characters currChar;
    
    public float survivalTimeInMinutes;

    public WavesSpawnScript WavesScript;
    public TilemapGeneration WorldGenScript;
    private void Start()
    {
        survivalTimeInMinutes = PlayerPrefs.GetFloat("SelectedMode");
        currChar = (Characters) PlayerPrefs.GetInt("SelectedCharacter");
        Instantiate(Resources.Load("prefab/player/" + currChar, typeof(GameObject)));

        WavesScript.TargetSurvivalTimeInitial(survivalTimeInMinutes * 60);

        WorldGenScript.currentLevelTitle = (Levels)PlayerPrefs.GetInt("SelectedStage");
    }
}
