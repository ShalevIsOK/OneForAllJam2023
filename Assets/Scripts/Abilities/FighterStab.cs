using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterStab : Ability
{
    [SerializeField] GameObject knifeProjectile;
    KeyboardMovementInput movementInput;

    private void Start()
    {
        movementInput = GetComponent<KeyboardMovementInput>();
    }
    protected override void Activate()
    {
        Instantiate(knifeProjectile,transform);
    }

}
