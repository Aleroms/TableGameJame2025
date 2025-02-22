using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFromMousePosition : MonoBehaviour
{
    [SerializeField] private Transform mouseTarget;
    [SerializeField] private BlockSpawner blockSpawner;
    [SerializeField] private GameManager gameManager; 
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
            StartCoroutine(ReleaseAndSpawnNewBlock());
        }
        if (Input.GetMouseButtonDown(1) && mouseTarget.GetChild(0) != null)
        {
            StartCoroutine(RotateBlock()); 
        }
    }

    IEnumerator ReleaseAndSpawnNewBlock()
    {
        // Let block fall and separate it from the mousePosition
        gameManager.ProgressTurn(); 
        var block = mouseTarget.GetChild(0);
        block.GetComponent<Rigidbody2D>().simulated = true;
        block.parent = null;

        yield return new WaitForSeconds(1);

        blockSpawner.SpawnBlock();
    }

    IEnumerator RotateBlock()
    {
        var block = mouseTarget.GetChild(0);
        block.GetComponent<Transform>().transform.Rotate(0, 0, 90); 
        yield return new WaitForSeconds(1);
    }
}
