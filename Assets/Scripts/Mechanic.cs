using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Mechanic : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float RepairRadius = 1.5f;
    
    private HashSet<GameObject> cogFollowers = new HashSet<GameObject>();

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cogFollowers = cogFollowers.Where(cog => cog != null).ToHashSet();
        
        var anyCog = cogFollowers.FirstOrDefault();

        if (anyCog != null) {
            MaybeRepairPyramid(anyCog);
        }
    }

    private void MaybeRepairPyramid(GameObject anyCog)
    {
        var allPyramids = FindObjectsOfType<AlienPyramid>();
        foreach (var pyramid in allPyramids) {
            if (pyramid.GetIsBuilt()) {
                continue;
            }
            var toPyramid = pyramid.transform.position - transform.position;
            toPyramid.z = 0;
            var distance = toPyramid.magnitude;
            if (distance <= RepairRadius) {
                pyramid.Repair();
                AudioPlayer.Inst.PlayPyramidBuild();
                Destroy(anyCog);
                return;
            }
        }
    }

    public void AddCogFollower(GameObject gameObject)
    {
        cogFollowers.Add(gameObject);
    }
}
