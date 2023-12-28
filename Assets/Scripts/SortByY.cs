using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortByY : MonoBehaviour
{
    private void Update()
    {
        // Set z position to y position
        var position = transform.position;
        position.z = position.y;
        transform.position = position;
    }
}
