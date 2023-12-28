using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardMovementInput : MonoBehaviour, IHasVelocity
{
    private MoveStates moveType;
    [SerializeField] private float speed = 1f;
    [SerializeField] private PlayerInput playerInput;
    private Rigidbody2D rb2d;

    Vector3 IHasVelocity.Velocity => rb2d.velocity;


    private bool up = false;
    private bool down = false;
    private bool left = false;
    private bool right = false;
    

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetMoveType();
        SetMoveVector();

    }

    private void SetMoveType()
    {
        //check current axis first, if none defaults to vertical
        if (moveType == MoveStates.Up || moveType == MoveStates.Down || moveType == MoveStates.none)
        {
            if (up != down)
            {
                if (up) { moveType = MoveStates.Up; return; }
                else { moveType = MoveStates.Down; return; }
            }
            if (left != right)
            {
                if (left) { moveType = MoveStates.Left; return; }
                else { moveType = MoveStates.Right; return; }
            }
            
        }
        else
        {
            if (left != right)
            {
                if (left) { moveType = MoveStates.Left; return; }
                else { moveType = MoveStates.Right; return; }
            }
            if (up != down)
            {
                if (up) { moveType = MoveStates.Up; return; }
                else { moveType = MoveStates.Down; return; }
            }
        }
    }

    private void SetMoveVector()
    {
        switch (moveType)
        {
            case MoveStates.Left:
                rb2d.velocity = new Vector2(-speed, 0);
                if (transform.position.x <= ScreenBounds.LeftEdge) rb2d.velocity = Vector2.zero; 
                return;
               
            case MoveStates.Right:
                if (transform.position.x >= ScreenBounds.RightEdge) rb2d.velocity = Vector3.zero;
                else rb2d.velocity = new Vector3(speed, 0);
                return;

            case MoveStates.Up:
                rb2d.velocity = new Vector2(0, speed);
                if (transform.position.y >= ScreenBounds.TopEdge) rb2d.velocity = Vector3.zero;
                return;

            case MoveStates.Down:
                rb2d.velocity = new Vector2(0, -speed);
                if (transform.position.y <= ScreenBounds.BottomEdge) rb2d.velocity = Vector3.zero;
                return;
        }
    }

    public void OnUpMovement(InputAction.CallbackContext context)
    {
        if (context.started) { up = true; }
        if (context.canceled) { up = false; }
    }

    public void OnDownMovement(InputAction.CallbackContext context)
    {
        if (context.started) { down = true; }
        if (context.canceled) { down = false; }
    }

    public void OnLeftMovement(InputAction.CallbackContext context)
    {
        if (context.started) { left = true; }
        if (context.canceled) { left = false; }
    }

    public void OnRightMovement(InputAction.CallbackContext context)
    {
        if (context.started) { right = true; }
        if (context.canceled) { right = false; }
    }
}

enum MoveStates
{
    none, Up, Down, Left, Right
}
