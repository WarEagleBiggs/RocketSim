using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    public GameObject Rocket1;

    private Rigidbody Rocket1_rb;

    public float UpwardVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        Rocket1_rb = Rocket1.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunSim()
    {
        Rocket1_rb.AddForce(transform.up * UpwardVelocity, ForceMode.Impulse);
    }
}
