using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabProjectile : MonoBehaviour
{
    public MoveStates direction;
    [SerializeField] float speed = 1f;
    [SerializeField] float distance = 2f;
    private Vector2 target;

    private void Start()
    {
        switch (direction)
        {
            case MoveStates.Up:
                target = new Vector2 (0, distance);
                return;
            case MoveStates.Down:
                target = new Vector2 (0, -distance);
                return;
            case MoveStates.Left:
                target = new Vector2(-distance,0);
                return;
            case MoveStates.Right:
                target = new Vector2(distance,0);
                return;
        }
    }
}
