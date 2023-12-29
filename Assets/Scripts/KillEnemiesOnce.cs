using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemiesOnce : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        var allEnemies = FindObjectsOfType<EnemyBehaviour>();
        foreach (var enemy in allEnemies) {
            var toEnemy = enemy.transform.position - transform.position;
            toEnemy.z = 0;
            var distance = toEnemy.magnitude;
            if (distance <= radius) {
                Destroy(enemy.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
