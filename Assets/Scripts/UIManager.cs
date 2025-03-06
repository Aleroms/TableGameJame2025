using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI; 
public class UIManager : MonoBehaviour
{
    [SerializeField] private BlockDetector leftScale; 
    [SerializeField] private BlockDetector rightScale;
    [SerializeField] private TextMeshPro leftScaleText;
    [SerializeField] private TextMeshPro rightScaleText;
    [SerializeField] private TextMeshProUGUI turnText; 
    private void Update()
    {
        leftScaleText.text = leftScale.currentWeight.ToString(); 
        rightScaleText.text = rightScale.currentWeight.ToString();
        turnText.text = GameManager.Instance.turn.ToString(); 
    }
    
}
