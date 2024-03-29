using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHasHealth
{
    public int maxHealth;
    public int health;
    [SerializeField] GameObject fillRoot;

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void Heal(int amount)
    {
        health += amount;
        OnHealthChanged();
    }

    public bool IsDead()
    {
        return health <= 0;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        OnHealthChanged();
    }

    private void OnHealthChanged()
    {
        fillRoot.transform.localScale = new Vector3(Mathf.InverseLerp(0, maxHealth, health), fillRoot.transform.localScale.y, fillRoot.transform.localScale.z);
    }

    private void Update()
    {
        OnHealthChanged();
    }

}
