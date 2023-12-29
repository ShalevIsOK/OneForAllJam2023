using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Ability : MonoBehaviour
{
    [SerializeField] int cooldown;
    bool isEnabled = true;

    public bool TryInitiate()
    {
        if (!isEnabled) return false;
        Debug.Log("initiated");
        StartCoroutine(CooldownTimer());
        Activate();
        return true;
    }

    protected abstract void Activate();

    IEnumerator CooldownTimer()
    {
        isEnabled = false;
        float timer = 0;
        Debug.Log("Cooldown Started");
        while (timer < cooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        isEnabled = true;
        Debug.Log("Cooldown Over");
    }
}
