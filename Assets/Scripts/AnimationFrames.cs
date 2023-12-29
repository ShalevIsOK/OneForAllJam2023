using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Animate4DirectionsConfig", menuName = "ScriptableObjects/Animate4DirectionsConfig", order = 1)]
public class Animate4DirectionsConfig : ScriptableObject
{
    public AnimationFrames[] AnimationConfig;
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
