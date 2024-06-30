using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Clamp : MonoBehaviour
{
    public bool canExecute;
    public GameObject refernce;
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(refernce.transform.position, transform.TransformDirection(Vector3.back), out hit) && canExecute)
        {
            Debug.DrawRay(refernce.transform.position, transform.TransformDirection(Vector3.back) * hit.distance, Color.yellow);
            refernce.transform.position = hit.point;
            transform.position = new Vector3(transform.position.x,
                hit.point.y,
                transform.position.z);
        }
    }
}
