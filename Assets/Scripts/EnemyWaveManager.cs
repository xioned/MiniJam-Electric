using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyWaveManager : MonoBehaviour
{
    public int currentWave;
    public List<WaveEnemies> waveEnemies = new();
    public float waveDuration = 5;
    public Transform[] spawnLocation;
    public RewardManager rewardManager;
    [Header("Debug")]
    public List<EnemySpawnDetails> selectedEnemiesForThisWave = new();
    public List<GameObject> enemiesToSpawnInThisWave = new();
    public List<Enemy> spawnedEnemies = new();
    public static EnemyWaveManager Singleton;
    private void Awake()
    {
        Singleton = this;
    }
    void Start()
    {
        GenerateWave();
    }

    public void RemoveSpawnedEnemy(Enemy enemy)
    {
        spawnedEnemies.Remove(enemy);
        if (spawnedEnemies.Count <= 0) { rewardManager.CreateReward(); }
    }

    private void SpawnWaveEnemy(float spawnInterval)
    {
        StartCoroutine(SpawnWaveEnemyRoutine(spawnInterval));
    }
    private IEnumerator SpawnWaveEnemyRoutine(float spawnInterval)
    {
        while (enemiesToSpawnInThisWave.Count > 0)
        {
            Enemy enemy = Instantiate(enemiesToSpawnInThisWave[0], spawnLocation[Random.Range(0, spawnLocation.Length)].position, Quaternion.identity).GetComponent<Enemy>();
            spawnedEnemies.Add(enemy);
            enemiesToSpawnInThisWave.RemoveAt(0);
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    public void GenerateWave()
    {
        GenerateWaveEnemies();
        float spawnInterval = waveDuration / enemiesToSpawnInThisWave.Count;
        SpawnWaveEnemy(spawnInterval);
    }

    public void GenerateWaveEnemies()
    {
        int enemyPoint = currentWave * 15;
        List<GameObject> enemiesToSpawnNewWave = new();
        while (enemyPoint > 0 || enemiesToSpawnNewWave.Count < 60)
        {
            int randEnemyId = Random.Range(0,waveEnemies[currentWave+1].Enemies.Count);

            int enemyCost = waveEnemies[currentWave + 1].Enemies[randEnemyId].Weight;
            if (enemyPoint - enemyCost >= 0)
            {
                enemiesToSpawnNewWave.Add(waveEnemies[currentWave + 1].Enemies[randEnemyId].Prefab);
                enemyPoint -= enemyCost;
            }
            else if(enemyPoint < waveEnemies[currentWave + 1].Enemies[0].Weight)
            {
                break;
            }
        }
        currentWave++;
        enemiesToSpawnInThisWave.Clear();
        enemiesToSpawnInThisWave = enemiesToSpawnNewWave;
    }
}

[System.Serializable]
public class EnemySpawnDetails
{
    public GameObject Prefab;
    public int Weight;
}
[System.Serializable]
public class WaveEnemies
{
    public List<EnemySpawnDetails> Enemies;
}