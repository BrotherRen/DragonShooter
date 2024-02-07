using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab1;
    [SerializeField] private GameObject _enemyPrefab2;
    [SerializeField] private GameObject _enemyContainer;

    private bool _stopSpawning1 = false;
    private bool _stopSpawning2 = false;

    void Start()
    {
        StartCoroutine(SpawnRoutine1());
        StartCoroutine(SpawnRoutine2());
    }

    void Update()
    {

    }

    IEnumerator SpawnRoutine1()
    {
        while (!_stopSpawning1)
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
        while (!_stopSpawning2)
        {
            float randomY = Random.Range(3.6f, 6f);
            Vector3 spawnPosition = new Vector3(20.3f, randomY, 0);

            Debug.Log("Spawned at Y (SpawnManager2): " + randomY);

            GameObject newEnemy = Instantiate(_enemyPrefab2, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(3.0f);
        }
    }

    public void OnPlayerDeath1()
    {
        _stopSpawning1 = true;
    }

    public void OnPlayerDeath2()
    {
        _stopSpawning2 = true;
    }
}

