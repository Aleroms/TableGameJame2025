using UnityEngine;

public class Dropping : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private GameObject _gameObject;

    [SerializeField]
    private float _depth;
    [SerializeField]
    private float _spawnHeight;

    private void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = _depth;
        mousePos = _camera.ScreenToWorldPoint(mousePos);
        if (Input.GetMouseButtonDown(0))
        {
            mousePos.y = _spawnHeight;
            Debug.Log(mousePos);
            
            Instantiate(_gameObject, mousePos,Quaternion.identity);
        }
    }
}

/*
user clicks on screen
an object is dropped where the user clicked

get user's mouse position
create a ray from its origin going forward

 */