using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    //wave UI
    public int WaveNumber => waveNumber;
    public float TimeBetweenWaves => timeBetweenWaves;
    [SerializeField] GameObject waveUI;

    
    [SerializeField] GameObject[] enemyPrefabs;
    [SerializeField] float timeBetweenSpawns = 1f;
    [SerializeField] int minEnemyAmount = 4;
    [SerializeField] int maxEnemyAmount = 10;

    int waveNumber = 1;
    int enemyAmount;

    //cur enemies
    List<GameObject> enemyList;

    //repeat spawn
    WaitForSeconds waitTimeBetweenSpawns;
    WaitUntil waitUntileNoEnemy;
    WaitForSeconds waitTimeBetweenWaves;
    [SerializeField] bool spawnEnemy = true;

    [SerializeField] float timeBetweenWaves = 1f;

    //wait for spawns;
    protected override void Awake(){
        base.Awake();
        enemyList = new List<GameObject>();
        waitTimeBetweenSpawns = new WaitForSeconds(timeBetweenSpawns);
        waitTimeBetweenWaves = new WaitForSeconds(timeBetweenWaves);
        waitUntileNoEnemy = new WaitUntil(() => enemyList.Count == 0);
    }

    // bool NoEnemy(){
    //     return enemyList.Count == 0;
    // }

    IEnumerator Start(){
        //when there are no enemies
        while(spawnEnemy){
            yield return waitUntileNoEnemy;

            //wave UI
            waveUI.SetActive(true);

            yield return waitTimeBetweenWaves;

            waveUI.SetActive(false);
            yield return StartCoroutine(nameof(RandomlySpawnCoroutine));
        }
    }

    //random spawn enemy

    IEnumerator RandomlySpawnCoroutine(){
        enemyAmount = Mathf.Clamp(enemyAmount, minEnemyAmount + waveNumber / 3, maxEnemyAmount);

        //repeat respawn

        for(int i = 0; i<enemyAmount; i++){
            //get a random enemy
            var enemy = enemyPrefabs[Random.Range(0,enemyPrefabs.Length)];
            enemyList.Add(PoolManager.Release(enemy));
        
            yield return waitTimeBetweenSpawns;
        }

        waveNumber++;
    }

    public void RemoveFromList(GameObject enemy){
        enemyList.Remove(enemy);
    }
}
