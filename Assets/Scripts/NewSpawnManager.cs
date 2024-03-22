using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab1;
    [SerializeField]
    private GameObject _enemyPrefab2;
    [SerializeField]
    private GameObject _enemyContainer;
    
    [SerializeField]
    [SerializeReference]
    private GameObject[] powerups;

    private bool _stopSpawning = false;
    

    void Start()
    {
        StartCoroutine(SpawnRoutine1());
        StartCoroutine(SpawnRoutine2());
        StartCoroutine(SpawnPowerupRoutine());
    }

    void Update()
    {

    }

    IEnumerator SpawnRoutine1()
    {
        while (!_stopSpawning)
        {
            float randomY = Random.Range(5f, 10f);
            Vector3 spawnPosition = new Vector3(20.3f, randomY, 0);

            Debug.Log("Spawned at Y (SpawnManager1): " + randomY);

            GameObject newEnemy = Instantiate(_enemyPrefab1, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator SpawnRoutine2()
    {
        while (!_stopSpawning)
        {
            float randomY = Random.Range(3.6f, 6f);
            Vector3 spawnPosition = new Vector3(20.3f, randomY, 0);

            Debug.Log("Spawned at Y (SpawnManager2): " + randomY);

            GameObject newEnemy = Instantiate(_enemyPrefab2, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(3.0f);
        }
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (!_stopSpawning && powerups != null)
        {
            float randomY = Random.Range(3.6f, 10f);
            Vector3 spawnPosition = new Vector3(20.3f, randomY, 0);
            int randomPowerUp = Random.Range(0, 3);
            if (powerups[randomPowerUp] != null)
            {
                Instantiate(powerups[randomPowerUp], spawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(Random.Range(4, 8));
        }
    }

    public void OnPlayerDeath1()
    {
        _stopSpawning = true;
    }

   
}

