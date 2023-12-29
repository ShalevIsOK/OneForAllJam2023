using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnAroundCircle : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private Transform center;
    [SerializeField] private float radius;
    [SerializeField] private float spawnRate;

    private float timeUntilNextSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        timeUntilNextSpawn = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        timeUntilNextSpawn -= Time.deltaTime;
        if (timeUntilNextSpawn <= 0) {
            timeUntilNextSpawn = spawnRate;
            Spawn();
        }
    }

    private void Spawn()
    {
        var angle = UnityEngine.Random.Range(0, 360);
        var x = center.position.x + radius * Mathf.Cos(angle);
        var y = center.position.y + radius * Mathf.Sin(angle);
        var position = new Vector3(x, y, 0);
        Instantiate(prefabToSpawn, position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(center.position, radius);
    }
}
