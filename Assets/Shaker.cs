using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 
public class Shaker : MonoBehaviour
{
    private Transform shakeContainer;

    [SerializeField] private float shakeStrength;
    [SerializeField] private int shakeVibrato;
    [SerializeField] private float shakeRandom;
    [SerializeField] private ShakeRandomnessMode shakeRandomnessMode;

    private void Start()
    {
        shakeContainer = this.GetComponent<Transform>(); 
    }
    public void Shake()
    {
        shakeContainer.DOMoveX(shakeStrength, 0.5f).SetLoops(10, LoopType.Yoyo);
    }
}
