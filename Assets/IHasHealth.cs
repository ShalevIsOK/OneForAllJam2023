using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasHealth
{
    abstract void Heal(int amount);
    abstract void TakeDamage(int amount);
    abstract void Die();
}
