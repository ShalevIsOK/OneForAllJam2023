using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;

    [SerializeField] public int score = 0;

    public void AddScore(int amount)
    {
        score += amount;
        textMesh.text = score.ToString();
    }
}
