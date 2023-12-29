using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Animate4Directions : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private IHasVelocity Velocity;
    [SerializeField] private Direction InitialDirection;
    [SerializeField] private string InitialAnimation;
    [SerializeField] private bool ShouldResetFrameOnDirectionChange;
    [SerializeField] private Animate4DirectionsConfig AnimationsConfig;

    [Header("Debug inspect")]
    
    // Data
    private Dictionary<string, AnimationFrames> Animations;

    // Direction
    [SerializeField] private Direction CurrentDirection;

    // Animation
    [SerializeField] public string CurrentAnimation;
    [SerializeField] public bool FinishedAnimation;
    [SerializeField] private float AnimationTimer;
    [SerializeField] private int CurrentFrame;

    private void Start()
    {
        if (Velocity == null) {
            Velocity = GetComponents<IHasVelocity>()
                .FirstOrDefault(c => (object)c != this);
            if (Velocity == null) {
                throw new Exception("There is no IHasVelocity component");
            }
        }
        if (SpriteRenderer == null) {
            SpriteRenderer = GetComponentsInChildren<SpriteRenderer>()
                .First(c => c.enabled);
            if (SpriteRenderer == null) {
                throw new Exception("There is no SpriteRenderer component");
            }
        }
        CurrentDirection = InitialDirection;
        CurrentAnimation = InitialAnimation;

        Animations = new Dictionary<string, AnimationFrames>();
        foreach (var animation in AnimationsConfig.AnimationConfig) {
            Animations.Add(animation.Name, animation);
        }
    }

    public void SetAnimation(string name)
    {
        var isDifferentAnimation = CurrentAnimation != name;
        if (isDifferentAnimation) {
            CurrentAnimation = name;
            FinishedAnimation = false;
            AnimationTimer = 0;
            CurrentFrame = 0;
        }
    }

    private void Update()
    {
        var animationExists = Animations.ContainsKey(CurrentAnimation);
        if (!animationExists) {
            return;
        }
        
        var velocity = Velocity.Velocity;
        var newDirection = GetDirection(velocity);
        if (newDirection != null) {
            var differentDirection = newDirection.Value!= CurrentDirection;
            if (differentDirection) {
                CurrentDirection = newDirection.Value;
                if (ShouldResetFrameOnDirectionChange) {
                    AnimationTimer = 0;
                    CurrentFrame = 0;
                }
            }
        }

        Animate();
    }

    private void Animate()
    {
        var animation = Animations[CurrentAnimation];
        var spritesAndIsFlipped = animation.GetSprites(CurrentDirection);
        var sprites = spritesAndIsFlipped.Item1;
        var isFlipped = spritesAndIsFlipped.Item2;

        var timePassed = Time.deltaTime;
        AnimationTimer += timePassed;
        if (AnimationTimer >= animation.FrameDuration) {
            AnimationTimer -= animation.FrameDuration;
            CurrentFrame++;
            if (CurrentFrame >= sprites.Length) {
                CurrentFrame = 0;
                FinishedAnimation = true;
            }
        }

        ApplySprite(sprites[CurrentFrame], isFlipped);
    }

    private void ApplySprite(Sprite sprite, bool isFlipped)
    {
        SpriteRenderer.sprite = sprite;
        SpriteRenderer.flipX = isFlipped;
    }

    private Direction? GetDirection(Vector3 velocity)
    {
        if (velocity.x > 0) {
            return Direction.Right;
        } else if (velocity.x < 0) {
            return Direction.Left;
        } else if (velocity.y > 0) {
            return Direction.Up;
        } else if (velocity.y < 0) {
            return Direction.Down;
        } else {
            return null;
        }
    }
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
