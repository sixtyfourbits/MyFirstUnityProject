using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public static WaveSpawner waveSpawner;


    public enum SpawnState
    {
        SPAWN,
        WAIT,
        COUNTDOWN,
    }

    [System.Serializable]

    public struct wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public wave[] waves;
    int curWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves;
    float waveCountDown;

    public SpawnState state;

    [SerializeField] List<Transform> remainingEnemies;

    [SerializeField] bool waveEnd;


    void Start()
    {
        WaveSpawner.waveSpawner = this;
        StartCoroutine(CountDownAndSpawnCoroutine());
        remainingEnemies = new List<Transform>();
    }

    void Update()
    {

    }

    IEnumerator CountDownAndSpawnCoroutine()
    {
        state = SpawnState.COUNTDOWN;
        waveCountDown = timeBetweenWaves;
        yield return new WaitForSeconds(waveCountDown);
        state = SpawnState.SPAWN;
        waveEnd = false;
        wave tmpWave = waves[curWave];
        Transform enemy = Instantiate(tmpWave.enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
        remainingEnemies.Add(enemy);
        for (int i = 1; i < tmpWave.count; i++)
        {
            yield return new WaitForSeconds(1 / tmpWave.rate);
            enemy = Instantiate(tmpWave.enemy, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            remainingEnemies.Add(enemy);
        }
        waveEnd = true;

        state = SpawnState.WAIT;
    }

    public void WaveEndCheck(Transform enemy)
    {
        remainingEnemies.Remove(enemy);
        if (waveEnd && remainingEnemies.Count == 0)
        {
            curWave++;
            if (curWave < waves.Length) StartCoroutine(CountDownAndSpawnCoroutine());
            else Debug.Log("끝");
        }
    }
}
