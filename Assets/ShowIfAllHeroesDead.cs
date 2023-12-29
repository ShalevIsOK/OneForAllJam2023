using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowIfAllHeroesDead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var renderer = GetComponent<Renderer>();

        var allHeroes = FindObjectsOfType<HeroUnit>();
        var heroCount = allHeroes.Length;
        var allDead = heroCount == 0;

        renderer.enabled = allDead;
    }
}
