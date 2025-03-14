using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimumHeight : MonoBehaviour
{
    public bool TooTall = false; 
    private void OnTriggerStay2D(Collider2D collision)
    {
        TooTall = true; 
    }
}
