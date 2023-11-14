using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject duckPrefab;
    public GameObject fastduckPrefab;
    public float spawnInterval = 3.0f;
    public AudioClip spawnSound;

    private AudioSource audioSource;
    private int ducksOnScreen = 0;
    private bool canSpawn = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("SpawnDuck", 6.0f, spawnInterval);
    }

    void SpawnDuck()
    {
        if (canSpawn && ducksOnScreen < 1)
        {
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject duckPrefabToSpawn = Random.Range(0, 10) < 8 ? duckPrefab : fastduckPrefab;
            GameObject duckInstance = Instantiate(duckPrefabToSpawn, randomSpawnPoint.position, Quaternion.identity);
            ducksOnScreen++;

            if (spawnSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(spawnSound);
            }
        }
    }

    public void DuckShot()
    {
        ducksOnScreen--;
    }

    public void StopSpawning()
    {
        canSpawn = false;
    }

    public void StartSpawning()
    {
        canSpawn = true;
    }
}