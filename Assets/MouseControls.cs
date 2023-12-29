using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseControls : MonoBehaviour, IHasVelocity
{
    [SerializeField] Vector2 target;
    Vector2 mousePos;
    [SerializeField] private float speed = 1f;
    Rigidbody2D rb2d;
    [SerializeField] float moveToTargetThreshold = 1f;

    public Vector3 Velocity => rb2d.velocity;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Vector2 v2position = new Vector2 (transform.position.x, transform.position.y);
        var toTarget = target - v2position;
        if (toTarget.magnitude>moveToTargetThreshold)
        {
            Vector2 movementVec = toTarget.normalized * speed;
            rb2d.velocity = movementVec;
        }
        else rb2d.velocity = Vector2.zero;
    }

    public void MousePosition (InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();

    }
   public void MoveInput (InputAction.CallbackContext context)
    {
        if (context.started)
        {
            target = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.Log(target);
        }
    }
}
