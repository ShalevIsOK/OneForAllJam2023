using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IHasVelocity
{
    [Header("Config")]
    [SerializeField] private float speed;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private Animate4Directions Animator;

    [Header("Debug inspect")]

    [SerializeField] private Transform target;
    [SerializeField] private float runAICoolDown;

    Vector3 IHasVelocity.Velocity => this.velocity;

    // Start is called before the first frame update
    void Start()
    {
        target = LookForATarget();
    }

    private Transform LookForATarget()
    {
        // Choose as target one of the heroes in the scene randomly
        var heroes = FindObjectsOfType<HeroUnit>();
        if (heroes.Length == 0) {
            return null;
        }
        return heroes[UnityEngine.Random.Range(0, heroes.Length)].transform;
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
        runAICoolDown -= Time.deltaTime;
        if (runAICoolDown > 0) {
            return;
        }
        runAICoolDown = 0.1f;

        // Randomly change velocity: one of 4 directions
        var random = UnityEngine.Random.Range(0, 100);
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
        } else if (random == 5) {
            Animator.SetAnimation("Attack");
        } else if (random >= 6 && random <= 20) {
            velocity = VelocityTowardsTarget();
        }
    }

    private Vector3 VelocityTowardsTarget()
    {
        if (target == null) {
            target = LookForATarget();
            if (target == null) {
                target = Camera.main.transform;
            }
        }
        
        // Either x only or y only movement
        var toTarget = target.position - transform.position;
        var moreHorizontal = Math.Abs(toTarget.x) > Math.Abs(toTarget.y);
        if (moreHorizontal) {
            return new Vector3(toTarget.x > 0 ? speed : -speed, 0, 0);
        } else {
            return new Vector3(0, toTarget.y > 0 ? speed : -speed, 0);
        }
    }

    private void Move()
    {
        transform.position += velocity * Time.deltaTime;
    }

    private void UpdateAnimation()
    {
        var currentAnimation = Animator.CurrentAnimation;

        var isAttacking = currentAnimation == "Attack";

        var isSpecialMove = isAttacking;
        if (isSpecialMove) {
            // Can't move while punching or making special moves
            velocity = Vector3.zero;
            if (Animator.FinishedAnimation) {
                Animator.SetAnimation("Idle");
            }
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
