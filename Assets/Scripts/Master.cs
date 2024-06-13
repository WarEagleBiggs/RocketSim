using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Master : MonoBehaviour
{
    public GameObject Rocket1;
    private Rigidbody Rocket1_rb;
    public float UpwardVelocity;
    public Vector3 wind;
    public TextMeshProUGUI Velocity;
    public TextMeshProUGUI Altidude;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Rocket1_rb = Rocket1.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Rocket1_rb.AddForce(wind);
        Velocity.SetText(Rocket1_rb.velocity.magnitude.ToString("0" + " m/s"));
        Altidude.SetText(Rocket1.transform.position.y.ToString("0" + " m"));
    }

    public void RunSim()
    {
        
        Rocket1_rb.AddForce(transform.up * UpwardVelocity, ForceMode.Impulse);
    }
}
