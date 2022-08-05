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

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    private float waveCountDown = 0f;

    private float searchCountdown=1f;
    private SpawnStates state = SpawnStates.COUNTING;

    void Start()
    {
        waveCountDown = timeBetweenWaves;
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("No sapwn points reference");
        }
    }

    void Update()
    {

        if (state == SpawnStates.WAITING)
        {
            if (EnemyIsAlive()== false)
            {
                WaveCopleted();
            }
            else
            {
                
                return;
            }
                
                
        }

        if (waveCountDown <= 0)
        {
            if (state != SpawnStates.SPAWING)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
           
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
    }



    void WaveCopleted()
    {
        Debug.Log("Wave Completed");
        state = SpawnStates.COUNTING;
        waveCountDown = timeBetweenWaves;

        if (nextWave + 1 > waves.Length - 1)
        {
            nextWave = 0;
            Debug.Log("All waves complete"); // Poner el loop o fin de la partida
        }
        else
        {
            nextWave++;
        }
       
    }
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null) return false;
        }
        
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave:" + _wave.name);
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
        

        Transform _sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(_enemy, _sp.position,_sp.rotation);
    }
}