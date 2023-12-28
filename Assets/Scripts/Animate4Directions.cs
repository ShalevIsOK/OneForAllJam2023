using System;
using System.Collections.Generic;
using UnityEngine;

public class Animate4Directions : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] public Direction InitialDirection;
    [SerializeField] public string InitialAnimation;
    [SerializeField] public bool ShouldResetFrameOnDirectionChange;
    [SerializeField] private AnimationFrames[] AnimationsConfig;

    // Data
    private Dictionary<string, AnimationFrames> Animations;

    // Direction
    private Direction CurrentDirection;

    // Animation
    public string CurrentAnimation { get; private set; }
    private bool FinishedAnimation;
    private float AnimationTimer;
    private int CurrentFrame;

    private void Start()
    {
        CurrentDirection = InitialDirection;
        CurrentAnimation = InitialAnimation;

        Animations = new Dictionary<string, AnimationFrames>();
        foreach (var animation in AnimationsConfig) {
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
        
        var velocity = GetComponent<IHasVelocity>().Velocity;
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

[Serializable]
public struct AnimationFrames
{
    public string Name;
    public float FrameDuration;
    public Sprite[] Up;
    public Sprite[] Down;
    public Sprite[] Left;
    // Right is the same as Left, but flipped

    public (Sprite[], bool) GetSprites(Direction direction)
    {
        switch (direction) {
            case Direction.Up:
                return (Up, false);
            case Direction.Down:
                return (Down, false);
            case Direction.Left:
                return (Left, false);
            case Direction.Right:
                return (Left, true);
            default:
                throw new Exception("Invalid direction");
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
