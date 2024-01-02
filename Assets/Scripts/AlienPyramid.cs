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
    [SerializeField] private float growSpeed;

    [Header("Debug inspect")]
    [SerializeField] private bool IsBuilt;
    [SerializeField] private float prefabScale;

    // Start is called before the first frame update
    void Start()
    {
        prefabScale = transform.localScale.x;
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
            // grow over time
            var currentScale = transform.localScale.x;
            var newScale = currentScale + Time.deltaTime * growSpeed;
            newScale = Mathf.Clamp(newScale, 0, prefabScale);
            transform.localScale = Vector3.one * newScale;
        }
        if (GetIsBuiltAndReady()) {
            var allEnemies = FindObjectsOfType<EnemyBehaviour>();
            foreach (var enemy in allEnemies) {
                var toEnemy = enemy.transform.position - transform.position;
                toEnemy.z = 0;
                var distance = toEnemy.magnitude;
                if (distance <= EnemyTriggerDistance) {
                    IsBuilt = false;
                    // Reset scale
                    transform.localScale = Vector3.one * prefabScale;
                     
                    Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
                    break;
                }
            }
        }
    }

    public void Repair()
    {
        IsBuilt = true;
        // start small
        transform.localScale = Vector3.one * prefabScale * 0.5f;
    }

    public bool GetIsBuilt()
    {
        return IsBuilt;
    }

    public bool GetIsBuiltAndReady()
    {
        return IsBuilt && transform.localScale.x >= prefabScale;
    }
}
