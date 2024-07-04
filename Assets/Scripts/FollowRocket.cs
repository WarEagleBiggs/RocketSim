using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRocket : MonoBehaviour
{
    public GameObject target;
    public float lagRate;

    void Update()
    {
        this.transform.position = new Vector3(target.transform.position.x,
            target.transform.position.y + 75,
            target.transform.position.z - 75);

        transform.RotateAround(target.transform.position, Vector3.up, 1);
        
    }
}
