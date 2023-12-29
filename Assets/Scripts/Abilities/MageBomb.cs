using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MageBomb : Ability
{
    [SerializeField] GameObject projectilePrefab;
    KeyboardMovementInput movementInput;

    private void Start()
    {
        movementInput = GetComponent<KeyboardMovementInput>();
    }
    protected override void Activate()
    {
        GameObject projectile = Instantiate(projectilePrefab,transform.position, Quaternion.identity);
        projectile.GetComponent<StabProjectile>().Initialize(gameObject, movementInput.moveType);
        Debug.Log(movementInput.moveType.ToString());
        AudioPlayer.Inst.PlayAlienRange();
    }

    private void OnServerInitialized()
    {
        
    }

}
