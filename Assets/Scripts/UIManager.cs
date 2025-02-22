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
    [SerializeField] private GameObject leftScaleHighlight; 
    [SerializeField] private GameObject rightScaleHighlight;
    [SerializeField] private TextMeshProUGUI turnText; 
    private void Update()
    {
        leftScaleText.text = leftScale.currentWeight.ToString(); 
        rightScaleText.text = rightScale.currentWeight.ToString();
    }
    public void HighlightLeft()
    {
        leftScaleHighlight.SetActive(true);
        rightScaleHighlight.SetActive(false); 
    }
    public void HighlightRight()
    {
        rightScaleHighlight.SetActive(true); 
        leftScaleHighlight.SetActive(false);
    }
    public void UpdateTurn(int turn)
    {
        turnText.text = "Turn: " + turn.ToString();
    }
}
