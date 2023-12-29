using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedByEnemy : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private int hitRadius = 1;
    [SerializeField] private int damage = 1;
    [SerializeField] private float coolDown = 1f;

    [Header("Debug inspect")]
    [SerializeField] private float coolDownRemaining = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var touchingEnemy = false;

        var allEnemies = FindObjectsOfType<EnemyBehaviour>();
        foreach (var enemy in allEnemies) {
            if (IsTouching(enemy)) {
                touchingEnemy = true;
                break;
            }
        }

        if (touchingEnemy) {
            coolDownRemaining -= Time.deltaTime;
            if (coolDownRemaining <= 0) {
                coolDownRemaining = coolDown;
                GetComponentInChildren<IHasHealth>().TakeDamage(damage);
                if (GetComponentInChildren<IHasHealth>().IsDead()) {
                    Destroy(gameObject);
                }
            }
        } else {
            coolDownRemaining = 0;
        }
    }

    private bool IsTouching(EnemyBehaviour enemy)
    {
        var distance = Vector3.Distance(transform.position, enemy.transform.position);
        return distance <= hitRadius;
    }
}
