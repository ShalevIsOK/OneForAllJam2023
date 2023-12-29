using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StabProjectile : MonoBehaviour
{
    [SerializeField] float duration;
    [SerializeField] float distance = 2f;
    [SerializeField] float stayBeforeDestroyDuration = 0.5f;
    private Vector2 target;
    private Vector2 startLocation;
    float timeElapsed = 0f;

    public void Initialize(MoveStates direction)
    {
        Debug.Log("init started");

        startLocation = transform.position;
        switch (direction)
        {
            case MoveStates.Up:
                target = startLocation + new Vector2 (0, distance);
                return;
            case MoveStates.Down:
                target = startLocation + new Vector2 (0, -distance);
                return;
            case MoveStates.Left:
                target = startLocation + new Vector2(-distance,0);
                transform.Rotate(0, 0, 90);
                return;
            case MoveStates.Right:
                target = startLocation + new Vector2(distance, 0);
                transform.Rotate(0, 0, 90);
                return;
        }
    }

    private void Update()
    {
        Debug.Log("update");
        if (timeElapsed < duration)
        {
            Debug.Log("lerp started");
            Vector2 newlocation;
            float progress = timeElapsed/duration;
            newlocation.x = Mathf.Lerp(startLocation.x, target.x, progress);
            newlocation.y = Mathf.Lerp(startLocation.y, target.y, progress);
            transform.position = newlocation;
            timeElapsed += Time.deltaTime;
        }
        else if (timeElapsed-duration <= stayBeforeDestroyDuration)
        {
            transform.position = target;
            timeElapsed += Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
