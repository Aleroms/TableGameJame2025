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

    public void TintLeft()
    {
        leftScaleHighlight.GetComponent<Image>().color = new Color32(180, 0, 0, 100); 
    }

    public void TintRight()
    {
        rightScaleHighlight.GetComponent<Image>().color = new Color32(180, 0, 0, 100);
    }

    public void RestoreLeft()
    {
        leftScaleHighlight.GetComponent<Image>().color = new Color32(43, 43, 43, 100);
    }

    public void RestoreRight()
    {
        leftScaleHighlight.GetComponent<Image>().color = new Color32(43, 43, 43, 100);
    }
}
