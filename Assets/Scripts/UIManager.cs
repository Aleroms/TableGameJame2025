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
    [SerializeField] private GameObject weightDiffLeft; 
    [SerializeField] private GameObject weightDiffRight;
    [SerializeField] private TextMeshProUGUI weightDiffLeftText;
    [SerializeField] private TextMeshProUGUI weightDiffRightText;
    [SerializeField] private Image weightDiffLeftImage; 
    [SerializeField] private Image weightDiffRightImage;
    [SerializeField] private GameObject warningPanel; 

    private void Update()
    {
        levelText.text = "Level: " + GameManager.Instance.level.ToString(); 
        scoreText.text = "Score: " + GameManager.Instance.score.ToString();
        DisplayWeightDiff(); 
        if(GameManager.Instance.gracePeriod)
        {
            warningPanel.SetActive(true); 
        }
        else
        {
            warningPanel.SetActive(false);
        }
    }

    private void DisplayWeightDiff()
    {
        if(GameManager.Instance.leftOrRight) //Right side is heavier
        {
            weightDiffRight.SetActive(true);
            weightDiffLeft.SetActive(false); 
            weightDiffRightText.text = GameManager.Instance.weightDiff.ToString(); 
            if(GameManager.Instance.warning)
            {
                weightDiffRightImage.color = Color.red; 
            }
            else
            {
                weightDiffRightImage.color = Color.white; 
            }
        }
        else //Left side is heavier
        {
            weightDiffLeft.SetActive(true);
            weightDiffRight.SetActive(false);
            weightDiffLeftText.text = GameManager.Instance.weightDiff.ToString(); 
            if(GameManager.Instance.warning)
            {
                weightDiffLeftImage.color = Color.red; 
            }
            else
            {
                weightDiffLeftImage.color = Color.white; 
            }
        }
    }
}
