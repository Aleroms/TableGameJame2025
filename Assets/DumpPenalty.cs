using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DumpPenalty : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject); 
        GameManager.Instance.penalty++;
        Debug.Log("Penalty! Current number: " + GameManager.Instance.penalty);
    }
}
