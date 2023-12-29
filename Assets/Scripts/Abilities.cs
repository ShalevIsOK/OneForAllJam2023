using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Abilities : MonoBehaviour
{
    [SerializeField] int AbilitACooldown;
    bool isEnabled;

    public void OnTriggered (InputAction.CallbackContext context)
    {
        if (!isEnabled) return;
        
        StartCoroutine(CooldownTimer());
        Activate();
        
    }

    protected abstract void Activate();

    IEnumerator CooldownTimer()
    {
        isEnabled = false;
        float timer = 0;
        while (timer< AbilitACooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        isEnabled = true;
    }
}
