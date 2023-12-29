using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPyramid : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Sprite Unbuilt;
    [SerializeField] private Sprite Built;
    [SerializeField] private bool StartBuilt;
    [SerializeField] private float EnemyTriggerDistance;
    [SerializeField] private GameObject ExplosionPrefab;

    [Header("Debug inspect")]
    [SerializeField] private bool IsBuilt;

    // Start is called before the first frame update
    void Start()
    {
        IsBuilt = StartBuilt;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        GetComponent<SpriteRenderer>().sprite = IsBuilt ? Built : Unbuilt;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();
        if (IsBuilt) {
            var allEnemies = FindObjectsOfType<EnemyBehaviour>();
            foreach (var enemy in allEnemies) {
                var toEnemy = enemy.transform.position - transform.position;
                toEnemy.z = 0;
                var distance = toEnemy.magnitude;
                if (distance <= EnemyTriggerDistance) {
                    IsBuilt = false;
                    Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                    break;
                }
            }
        }
    }

    public void Repair()
    {
        IsBuilt = true;
    }
}
