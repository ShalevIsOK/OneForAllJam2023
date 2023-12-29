using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Animate4DirectionsConfig", menuName = "ScriptableObjects/Animate4DirectionsConfig", order = 1)]
public class Animate4DirectionsConfig : ScriptableObject
{
    public AnimationFrames[] AnimationConfig;


    public string ReloadNewSpriteNameTarget;
    
    [ContextMenu("Reload every Sprite but from ReloadNewSpriteName")]
    public void ReloadFromDifferentSprite()
    {
        foreach (var animationFrames in AnimationConfig)
        {
            DoReloadNewSpriteName(animationFrames.Up, ReloadNewSpriteNameTarget);
            DoReloadNewSpriteName(animationFrames.Down, ReloadNewSpriteNameTarget);
            DoReloadNewSpriteName(animationFrames.Left, ReloadNewSpriteNameTarget);
        }

        void DoReloadNewSpriteName(Sprite[] spriteCollection, string newSpriteFileName = null)
        {
            for (int i = 0; i < spriteCollection.Length; i++)
            {
                var sprite = spriteCollection[i];
                var name = sprite.name;
                var splitName = name.Split('_');
                var spriteName = splitName[0];
                var spriteNumber = splitName[1];
                var childSpriteName = $"{newSpriteFileName}_{spriteNumber}";
                Debug.Log($"Replacing {name} with {childSpriteName}");
                // Runs in Editor: Use UnityEditor.AssetDatabase.FindAssets():
                Sprite[] newSprites = Resources.LoadAll<Sprite>(newSpriteFileName);
                var newSprite = Array.Find(newSprites, item => item.name == childSpriteName);
                
                spriteCollection[i] = newSprite;
            }
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
