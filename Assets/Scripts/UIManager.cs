using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI; 
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI penaltyText;
    [SerializeField] private TextMeshProUGUI warningText;
    [SerializeField] private TextMeshProUGUI thresholdText;
    [SerializeField] private TextMeshProUGUI weightDiffText;
    [SerializeField] private TextMeshProUGUI swapCooldownText; 
    [SerializeField] private GameObject warningPanel; 
    private void Update()
    {
        levelText.text = "Level: " + GameManager.Instance.level.ToString(); 
        penaltyText.text = "Remaining Drops: " + GameManager.Instance.penalty.ToString();
        scoreText.text = "Score: " + GameManager.Instance.score.ToString();
        warningText.text = "Warning Weight: " + GameManager.Instance.weightDiffWarningThreshold.ToString(); 
        thresholdText.text = "Game Over Weight: " + GameManager.Instance.weightDiffThreshold.ToString();
        weightDiffText.text = "CURRENT WEIGHT DIFF: " + Mathf.Abs(GameManager.Instance.weightDiff).ToString(); 
        if(GameManager.Instance.swapCooldown < GameManager.Instance.maxSwapCooldown)
        {
            swapCooldownText.text = "Swap Cooldown: " + (GameManager.Instance.maxSwapCooldown - GameManager.Instance.swapCooldown).ToString() + " turns remaining"; 
        }
        else
        {
            swapCooldownText.text = "Swap Ready! Press S to swap scales"; 
        }
        if(GameManager.Instance.warning)
        {
            EnableWarning(); 
        }
        else
        {
            DisableWarning(); 
        }
    }
    
    private void EnableWarning()
    {
        warningPanel.SetActive(true); 
    }
    private void DisableWarning()
    {
        warningPanel.SetActive(false); 
    }
}
