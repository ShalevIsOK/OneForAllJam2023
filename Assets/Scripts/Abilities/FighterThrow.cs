using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterThrow : Ability
{
    [SerializeField] GameObject fleshProjectile;
    KeyboardMovementInput movementInput;

    private void Start()
    {
        movementInput = GetComponent<KeyboardMovementInput>();
    }
    protected override void Activate()
    {
        Debug.Log("Throw your flesh you sexy alien");
        GameObject projectile = Instantiate(fleshProjectile,transform.position, Quaternion.identity);
        projectile.GetComponent<StabProjectile>().Initialize(movementInput.moveType);
        Debug.Log(movementInput.moveType.ToString());
    }

    private void OnServerInitialized()
    {
        
    }

}
