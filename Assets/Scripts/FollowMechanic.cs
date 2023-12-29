using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMechanic : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float HitRadius = 2f;
    [SerializeField] private float FollowSpeedMin = 1f;
    [SerializeField] private float FollowSpeedMax = 2f;

    [Header("Debug inspect")]
    [SerializeField] private bool IsFollowing = false;

    [SerializeField] public float FollowSpeed;

    // Start is called before the first frame update
    void Start()
    {
        FollowSpeed = Random.Range(FollowSpeedMin, FollowSpeedMax);
    }

    // Update is called once per frame
    void Update()
    {
        var mechanic = FindObjectOfType<Mechanic>();
        if (mechanic == null) {
            return;
        }

        if (!IsFollowing) {

            var toMechanic = mechanic.transform.position - transform.position;
            toMechanic.z = 0;
            var distance = toMechanic.magnitude;
            if (distance <= HitRadius) {
                IsFollowing = true;
                mechanic.AddCogFollower(gameObject);
                AudioPlayer.Inst.PlayCogCollect();
            }
        }

        if (IsFollowing) {
            var maxDistanceDelta = FollowSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, mechanic.transform.position, maxDistanceDelta);
        }
    }
}
