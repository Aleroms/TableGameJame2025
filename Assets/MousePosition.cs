using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform mouseTarget;
    [SerializeField] private Transform boundaryLeft;
    [SerializeField] private Transform boundaryRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Position of object to be dropped based off of the player's mouse
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -(_camera.transform.position.z));
        mousePos = _camera.ScreenToWorldPoint(mousePos);
        mousePos.y = 0;
        if (mousePos.x < boundaryLeft.position.x)
        {
            mousePos.x = boundaryLeft.position.x;
        }
        else if (mousePos.x > boundaryRight.position.x)
        {
            mousePos.x = boundaryRight.position.x;
        }
        mousePos.z = 0;

        Debug.Log(mousePos);
        mouseTarget.position = mousePos;
    }
}
