using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class WavesSpawnScript : MonoBehaviour
{
    [SerializeField] private List<Wave> waves;

    public int numberOfCurrentWave { get; private set; }

    [SerializeField] private float spawnInterval;

    [SerializeField] private int maxEnemies;
    [SerializeField] private int spawnPackMin;
    [SerializeField] private int spawnPackMax;
    
    [SerializeField] private bool localMaxSpawn;
    
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI stageText;
    
    private float _wavesDuration;
    private float _spawnCooldown;
    
    private float _gameDuration;
    
    [HideInInspector] public int counterEnemies;
    
    private Transform _targetTransform;
    private float _cameraSize;
    
    private int survivalTime;
    private float targetSurvivalTime;

    private Menu menu;
    
    private void Start()
    {
        if (targetSurvivalTime == 300f)
        {
            _wavesDuration = 300 / waves.Count;
        }
        else
        {
            _wavesDuration = 120;
        }
        _targetTransform = GameObject.FindWithTag("Player").transform;
        _cameraSize = GameObject.FindWithTag("MainCamera").GetComponent<Camera>().orthographicSize;
        menu = GameObject.Find("Menu").GetComponent<Menu>();

        survivalTime = (int)targetSurvivalTime;
        
        numberOfCurrentWave = 0;
        
        if (!localMaxSpawn)
        {
            maxEnemies = waves[numberOfCurrentWave].maxEnemies;
        }

        StartCoroutine(RaceTimer());
        StartCoroutine(intervalSpawnEnemies());
        StartCoroutine(waveSwitch());

        waveRedraw();
    }

    private void Update()
    {
        if(targetSurvivalTime != 0 && survivalTime <= 0 && !menu.paused)
        {
            GameWin();
        }
    }

    private IEnumerator intervalSpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (counterEnemies < maxEnemies)
            {
                SpawnEnemies();
            }
        }
    }
    
    private IEnumerator waveSwitch()
    {
        while (numberOfCurrentWave < waves.Count-1)
        {
            yield return new WaitForSeconds(_wavesDuration);

            numberOfCurrentWave++;
            
            if (!localMaxSpawn)
            {
                maxEnemies = waves[numberOfCurrentWave].maxEnemies;
            }

            waveRedraw();
        }
    }

    private void SpawnEnemies()
    {
        int packSize = Random.Range(spawnPackMin, spawnPackMax);
        for (int packEnemiesCounter = 0; packEnemiesCounter < packSize; packEnemiesCounter++)
        {
            float angleSpawn = Random.Range(0f, 360f);
            Vector2 spawnPos = new Vector2(
                _targetTransform.position.x + (_cameraSize * 2.5f) *math.cos(angleSpawn),
                _targetTransform.position.y + (_cameraSize * 2.5f) *math.sin(angleSpawn)
            );

            GameObject randomEnemy = waves[numberOfCurrentWave]
                .enemies[Random.Range(0, waves[numberOfCurrentWave].enemies.Count)];
            
            Instantiate(randomEnemy, spawnPos, Quaternion.identity);
            counterEnemies++;
        }
    }

    public void TargetSurvivalTimeInitial(float timeOfRace)
    {
        targetSurvivalTime = timeOfRace;
    }

    private void GameWin()
    {
        StopAllCoroutines();
        GameObject.FindWithTag("Player").SetActive(false);
        menu.GameWin();
    }

    private IEnumerator RaceTimer()
    {
        while (true)
        {
            if(targetSurvivalTime == 0f)
            {
                survivalTime++;
            }
            else
            {
                survivalTime--;
            }
            TimerUpdate(survivalTime/60, survivalTime%60);
            yield return new WaitForSeconds(1f);
        }
    }

    private void TimerUpdate(int minutes, int seconds)
    {
        timerText.text = minutes + ":" + seconds;
    }

    private void waveRedraw()
    {
        stageText.text = "Stage " + (numberOfCurrentWave+1);
    }
}
