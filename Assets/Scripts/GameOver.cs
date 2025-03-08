using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highScoreTurns;

    private void Start()
    {
        highScoreTurns.text = "Final Score: " + GameManager.Instance.score.ToString(); 
    }
}
