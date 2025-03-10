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
    [SerializeField] public Transform leftScaleboundaryLeft;
    [SerializeField] public Transform leftScaleboundaryRight;
    [SerializeField] public Transform rightScaleboundaryLeft;
    [SerializeField] public Transform rightScaleboundaryRight;
    [SerializeField] private LineRenderer lineRenderer; 
    // Update is called once per frame
    private void Start()
    {
        boundaryLeft = leftScaleboundaryLeft.transform; 
        boundaryRight = leftScaleboundaryRight.transform;
    }
    void Update()
    {
        // Position of object to be dropped based off of the player's mouse
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -(_camera.transform.position.z));
        mousePos = _camera.ScreenToWorldPoint(mousePos);
        mousePos.y = 40;
        if (mousePos.x < boundaryLeft.position.x)
        {
            mousePos.x = boundaryLeft.position.x;
        }
        else if (mousePos.x > boundaryRight.position.x)
        {
            mousePos.x = boundaryRight.position.x;
        }
        mousePos.z = 0;

        mouseTarget.position = mousePos;
        Ray ray = new Ray(mousePos, Vector2.down);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction); 
        if(hit)
        {
            lineRenderer.SetPosition(0, mousePos);
            lineRenderer.SetPosition(1, hit.point); 
        }
    }
}
