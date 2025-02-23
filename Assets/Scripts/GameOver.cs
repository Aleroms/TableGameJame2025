using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreTurns;

    private void Start()
    {
        int turns = FindObjectOfType<AudioManager>().turns;
        highScoreTurns.text = turns + " turns!"; 
    }
}
