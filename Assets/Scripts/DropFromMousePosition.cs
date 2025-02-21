using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFromMousePosition : MonoBehaviour
{
    [SerializeField] private Transform mouseTarget;

    // Start is called before the first frame update
    void Start()
    {
        if (mouseTarget.GetChild(0) != null)
        {
            mouseTarget.GetChild(0).GetComponent<Rigidbody2D>().simulated = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && mouseTarget.GetChild(0) != null)
        {
            // Let block fall and separate it from the mousePosition
            var block = mouseTarget.GetChild(0);
            block.GetComponent<Rigidbody2D>().simulated = true;
            block.parent = null;
            
        }
    }
}
