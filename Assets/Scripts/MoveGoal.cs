using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGoal : MonoBehaviour
{
    private Vector3 newPos;
    // Start is called before the first frame update
    void Start()
    {
        newPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit)){
                newPos = hit.point;
                transform.position = newPos;
            }
        }
    }
}
