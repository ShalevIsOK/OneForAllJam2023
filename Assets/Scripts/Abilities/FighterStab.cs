using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
        GameObject projectile = Instantiate(knifeProjectile,transform.position,quaternion.identity);
        projectile.GetComponent<StabProjectile>().Initialize(movementInput.moveType);
        Debug.Log(movementInput.moveType.ToString());
    }

    private void OnServerInitialized()
    {
        
    }

}
