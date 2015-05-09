using UnityEngine;
using System.Collections;

public class DuckSpawner : MonoBehaviour 
{
    public GameObject Duck;

    public float spawnTime = 2;
    public float spawnTimeDeviation = 1;
    public float spawnDistanceFromPlayer = 2;

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
        float angle = Random.value * (2 * Mathf.PI);
        float x = Mathf.Cos(angle) * spawnDistanceFromPlayer;
	    float z = Mathf.Sin(angle) * spawnDistanceFromPlayer;
        Vector3 spawnPosition = new Vector3(x, 0, z);
        GameObject GO = Instantiate(Duck, spawnPosition, Quaternion.identity) as GameObject;
        GO.transform.LookAt(Camera.main.transform);
    }
}
