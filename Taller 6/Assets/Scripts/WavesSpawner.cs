using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesSpawner : MonoBehaviour
{

    public enum SpawnStates { SPAWING, WAITING, COUNTING };

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public Wave[] waves;
    private int nextWave = 0;

    public float timeBetweenWaves = 5f;
    public float waveCountDown = 0f;

    private SpawnStates state = SpawnStates.COUNTING;

    void Start()
    {
        waveCountDown = timeBetweenWaves;
    }

    void Update()
    {

        if (state == SpawnStates.WAITING)
        {
            // Chech enemys still alive
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnStates.SPAWING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
            else
            {
                waveCountDown -= Time.deltaTime;
            }
        }
    }

    bool EnemyIsAlive()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy") == null) return false;
        else return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnStates.SPAWING;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnStates.WAITING;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy" + _enemy.name);
    }
}