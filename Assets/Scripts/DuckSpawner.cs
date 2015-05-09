using UnityEngine;
using System.Collections;

public class DuckSpawner : MonoBehaviour 
{
    public GameObject Duck;

    public float spawnTime = 2;
    public float spawnTimeDeviation = 1;
    public float spawnDistanceFromPlayer = 4;

    private float spawnTimer = 0;

	void Start () 
    {
        spawnTimer = GetSpawnTime();
	}
	
	void Update () 
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            SpawnDuck();
            spawnTimer = GetSpawnTime();
        }
	}

    private float GetSpawnTime()
    {
        return spawnTimer = spawnTime - (0.5f * spawnTimeDeviation) + (Random.value * spawnTimeDeviation);
    }

    private void SpawnDuck()
    {
        Vector3 spawnPosition = new Vector3(0, 0, -2);
        Instantiate(Duck, spawnPosition, Quaternion.identity);
    }
}
