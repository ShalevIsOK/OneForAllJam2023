using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AbilitiesManager : MonoBehaviour
{
    //needs a way to know when an ability activation is over so that you can't trigger the other during
    //maybe just do a state thing

    [SerializeField] private Ability[] abilities = new Ability[2];
    private bool abilityTriggeredThisFrame;

    private void Update()
    {
        abilityTriggeredThisFrame = false;
    }
    public void Ability1Input(InputAction.CallbackContext context)
    {
        Debug.Log("input1 recieved");
        if (!abilityTriggeredThisFrame)
        {
            abilityTriggeredThisFrame = abilities[0].TryInitiate();
        }
        else Debug.Log("blocked for frame");
    }

    public void Ability2Input(InputAction.CallbackContext context) 
    {
        Debug.Log("input2 recieved");
        if (!abilityTriggeredThisFrame)
        {
            abilityTriggeredThisFrame = abilities[1].TryInitiate();
        }
        else Debug.Log("blocked for frame");
    }

}
