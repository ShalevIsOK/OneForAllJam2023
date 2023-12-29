using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IHasHealth
{
    [SerializeField] GameObject deathParticles; 
    public void Die()
    {
        Instantiate(deathParticles, new Vector3 (transform.position.x, transform.position.y,0), Quaternion.identity);
        Destroy(this.gameObject);

    }

    public void Heal(int amount)
    {
        return;
    }

    public bool IsDead()
    {
        return false;
    }

    public void TakeDamage(int amount)
    {
        Die();
    }
}
