using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienPyramid : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Sprite Unbuilt;
    [SerializeField] private Sprite Built;
    [SerializeField] private bool StartBuilt;

    [Header("Debug inspect")]
    [SerializeField] private bool IsBuilt;

    // Start is called before the first frame update
    void Start()
    {
        IsBuilt = StartBuilt;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        GetComponent<SpriteRenderer>().sprite = IsBuilt ? Built : Unbuilt;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
