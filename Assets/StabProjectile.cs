using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StabProjectile : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float duration;
    [SerializeField] float distance = 2f;
    [SerializeField] float stayBeforeDestroyDuration = 0.5f;
    // [SerializeField] float damage = 1f;
    [SerializeField] float collisionRadius = 0;
    [SerializeField] GameObject spawnOnHit;
    [SerializeField] bool healProjectile;
    [SerializeField] float rotateSpeed;

    [Header("Debug inspect")]
    private GameObject shooter;
    private Vector2 target;
    private Vector2 startLocation;
    float timeElapsed = 0f;

    public void Initialize(GameObject shooter, MoveStates direction)
    {
        Debug.Log("init started");

        this.shooter = shooter;

        startLocation = transform.position;
        switch (direction)
        {
            case MoveStates.Up:
                target = startLocation + new Vector2 (0, distance);
                return;
            case MoveStates.Down:
                target = startLocation + new Vector2 (0, -distance);
                return;
            case MoveStates.Left:
                target = startLocation + new Vector2(-distance,0);
                transform.Rotate(0, 0, 90);
                return;
            case MoveStates.Right:
                target = startLocation + new Vector2(distance, 0);
                transform.Rotate(0, 0, 90);
                return;
        }
    }

    private void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);

        Debug.Log("update");
        if (timeElapsed < duration)
        {
            Debug.Log("lerp started");
            Vector2 newlocation;
            float progress = timeElapsed / duration;
            newlocation.x = Mathf.Lerp(startLocation.x, target.x, progress);
            newlocation.y = Mathf.Lerp(startLocation.y, target.y, progress);
            transform.position = newlocation;
            timeElapsed += Time.deltaTime;
        }
        else if (timeElapsed - duration <= stayBeforeDestroyDuration)
        {
            transform.position = target;
            timeElapsed += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        if (healProjectile) {
            HealHitHeroes();
        } else {
            KillHitEnemies();
        }

    }

    private void KillHitEnemies()
    {
        Vector2 hitPosition = transform.position;

        var allEnemies = FindObjectsOfType<EnemyBehaviour>();
        var anythingHit = false;
        foreach (var enemy in allEnemies)
        {
            if (Vector2.Distance(enemy.transform.position, transform.position) < collisionRadius)
            {
                if (enemy != null) {
                    Destroy(enemy.gameObject);
                    var ScoreUI = FindObjectOfType<ScoreUI>();
                    ScoreUI.AddScore(1);
                }
                AudioPlayer.Inst.PlayAstronautDie();
                anythingHit = true;
                hitPosition = enemy.transform.position;
            }
        }

        if (anythingHit)
        {
            if (spawnOnHit != null)
            {
                Instantiate(spawnOnHit, hitPosition, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }

    private void HealHitHeroes()
    {
        Vector2 hitPosition = transform.position;

        var allOtherHeroes = FindObjectsOfType<HeroUnit>().Where(hero => hero.gameObject != shooter);
        
        var anythingHit = false;
        foreach (var hero in allOtherHeroes)
        {
            if (Vector2.Distance(hero.transform.position, transform.position) < collisionRadius)
            {
                hero.GetComponentInChildren<IHasHealth>().Heal(5);
                AudioPlayer.Inst.PlayAlienHeal();
                anythingHit = true;
                hitPosition = hero.transform.position;
            }
        }

        if (anythingHit)
        {
            if (spawnOnHit != null)
            {
                Instantiate(spawnOnHit, hitPosition, Quaternion.identity);
            }
            Destroy(this.gameObject);
        }
    }
}
