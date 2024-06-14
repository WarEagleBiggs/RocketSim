using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Master : MonoBehaviour
{
    public GameObject Rocket1;
    private Rigidbody Rocket1_rb;
    public float UpwardVelocity;
    public Vector3 wind;
    public TextMeshProUGUI Velocity;
    public TextMeshProUGUI Altidude;
    public ParticleSystem BOOM;
    public GameObject BOOMfx;

    public GameObject RunBtn;
    public GameObject ResetBtn;
    
    public float WindSpeed = 4000;

    public TMP_InputField ThrustInput;

    
    
    // Start is called before the first frame update
    void Start()
    {
        Rocket1_rb = Rocket1.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpwardVelocity = Single.Parse(ThrustInput.text);
        
        Rocket1_rb.AddForce(wind.normalized * WindSpeed * Time.deltaTime);
        Velocity.SetText(Rocket1_rb.velocity.magnitude.ToString("0" + " m/s"));
        Altidude.SetText(Rocket1.transform.position.y.ToString("0" + " m"));
    }

    public void RunSim()
    {
        RunBtn.SetActive(false);
        ResetBtn.SetActive(true);
        Rocket1_rb.AddForce(transform.up * UpwardVelocity, ForceMode.Impulse);
        BOOM.Play();
        BOOMfx.SetActive(true);
    }

    public void ResetSim()
    {
        SceneManager.LoadScene("GameScene");
    }
}
