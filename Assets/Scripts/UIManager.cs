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
    private void Update()
    {
        levelText.text = "Level: " + GameManager.Instance.level.ToString(); 
        scoreText.text = "Score: " + GameManager.Instance.score.ToString();
        
    }

}
