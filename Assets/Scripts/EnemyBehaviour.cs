using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IHasVelocity
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Animate4Directions Animator;

    Vector3 IHasVelocity.Velocity => this.velocity;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        RunAI();

        Move();

        UpdateAnimation();
    }

    private void RunAI()
    {
        // Randomly change velocity: one of 4 directions
        var random = UnityEngine.Random.Range(0, 1000);
        if (random == 0) {
            velocity = new Vector3(speed, 0, 0);
        } else if (random == 1) {
            velocity = new Vector3(-speed, 0, 0);
        } else if (random == 2) {
            velocity = new Vector3(0, speed, 0);
        } else if (random == 3) {
            velocity = new Vector3(0, -speed, 0);
        } else if (random == 4) {
            velocity = new Vector3(0, 0, 0);
        }
    }

    private void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void UpdateAnimation()
    {
        var currentAnimation = Animator.CurrentAnimation;
        var isSpecialMove = currentAnimation == "SpecialMove";
        if (isSpecialMove) {
            
        } else {
            UpdateAnimationBasedOnVelocity();
        }
    }

    private void UpdateAnimationBasedOnVelocity()
    {
        var anyMovement = velocity.sqrMagnitude > 0;
        if (anyMovement) {
            Animator.SetAnimation("Walk");
        } else {
            Animator.SetAnimation("Idle");
        }
    }
}
