using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class UIManager : MonoBehaviour
{
    [SerializeField] private BlockDetector leftScale; 
    [SerializeField] private BlockDetector rightScale;
    [SerializeField] private TextMeshProUGUI leftScaleText;
    [SerializeField] private TextMeshProUGUI rightScaleText;

    private void Update()
    {
        leftScaleText.text = leftScale.currentWeight.ToString(); 
        rightScaleText.text = rightScale.currentWeight.ToString();
    }
}
