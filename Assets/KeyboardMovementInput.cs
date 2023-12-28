using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardMovementInput : MonoBehaviour
{
    private MoveStates moveType;
    [SerializeField] private float speed = 1f;
    [SerializeField] private PlayerInput playerInput;
    private Rigidbody2D rigidbody2;
    

    private void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        SetMoveVector();
    }

    private void SetMoveVector()
    {
        switch (moveType)
        {
            case MoveStates.Up:
                if(transform.position.y >= ScreenBounds.TopEdge)
                {
                    rigidbody2.velocity = Vector2.zero; return;
                }
                rigidbody2.velocity = new Vector2(0, speed);
                return;

            case MoveStates.Down:
                if (transform.position.y <= ScreenBounds.BottomEdge)
                {
                    rigidbody2.velocity = Vector2.zero; return;
                }
                rigidbody2.velocity = new Vector2(0, -speed);
                return;

            case MoveStates.Left:
                if (transform.position.x <= ScreenBounds.LeftEdge)
                {
                    rigidbody2.velocity = Vector2.zero; return;
                }
                rigidbody2.velocity = new Vector2(-speed, 0);
                return;

            case MoveStates.Right:
                if (transform.position.x >= ScreenBounds.RightEdge)
                {
                    rigidbody2.velocity = Vector2.zero; return;
                }
                rigidbody2.velocity = new Vector2(speed, 0);
                return;

        }
    }

    public void OnUpMovement(InputAction.CallbackContext context)
    {
        moveType = MoveStates.Up;
    }

    public void OnDownMovement(InputAction.CallbackContext context)
    {
        moveType = MoveStates.Down;
    }

    public void OnLeftMovement(InputAction.CallbackContext context)
    {
        moveType = MoveStates.Left;
    }

    public void OnRightMovement(InputAction.CallbackContext context)
    {
        moveType= MoveStates.Right;
    }
}

enum MoveStates
{
    none, Up, Down, Left, Right
}