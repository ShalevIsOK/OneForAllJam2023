using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomSpawnAroundCircle : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Transform center;
    [SerializeField] private float radius;
    [SerializeField] private float spawnRate;
    [SerializeField] private bool spawnOnlyOnEdges;
    [SerializeField] private int limit;

    private float timeUntilNextSpawn;

    private GameObject[] SpawnedObjects;
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnedObjects = new GameObject[0];

        timeUntilNextSpawn = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnedObjects = SpawnedObjects.Where(x => x != null).ToArray();

        timeUntilNextSpawn -= Time.deltaTime;
        if (timeUntilNextSpawn <= 0) {
            timeUntilNextSpawn = spawnRate;
            Spawn();
        }
    }

    private void Spawn()
    {
        if (limit > 0 && SpawnedObjects.Length >= limit) {
            return;
        }
        
        var angle = UnityEngine.Random.Range(0, 360);
        var distance = UnityEngine.Random.Range(0, radius);
        if (spawnOnlyOnEdges) {
            distance = radius;
        }

        var x = center.position.x + Mathf.Cos(angle) * distance;
        var y = center.position.y + Mathf.Sin(angle) * distance;
        var position = new Vector3(x, y, 0);
        var newObject = Instantiate(prefabToSpawn, position, Quaternion.identity);
        SpawnedObjects = SpawnedObjects.Append(newObject).ToArray();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(center.position, radius);
    }
}
