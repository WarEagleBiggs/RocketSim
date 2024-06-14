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
    
    public float UpwardVelocity; //max achieveable velocity for fuel type
    public float PercentageOfFuel; 
    public float WindSpeed = 4000;

    public Vector3 wind;
    public TextMeshProUGUI Velocity;
    public TextMeshProUGUI Altidude;
    public TextMeshProUGUI PercentageText;

    public ParticleSystem BOOM;
    public GameObject BOOMfx;
    public AudioSource BOOMsfx;

    public GameObject RunBtn;
    public GameObject ResetBtn;
    public List<GameObject> ButtonsToHide;
    public AudioSource BtnClick;
    
    
    
    


    // Start is called before the first frame update
    void Start()
    {
        Rocket1_rb = Rocket1.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        PercentageText.SetText((PercentageOfFuel * 100).ToString("0") + "%");
        Rocket1_rb.AddForce(wind.normalized * WindSpeed * Time.deltaTime);
        Velocity.SetText(Rocket1_rb.velocity.magnitude.ToString("0" + " m/s"));
        Altidude.SetText(Rocket1.transform.position.y.ToString("0" + " m"));
    }

    public void RunSim()
    {
        BtnClick.Play();
        foreach (var i in ButtonsToHide)
        {
            i.SetActive(false);
        }
        BOOMsfx.Play();
        RunBtn.SetActive(false);
        ResetBtn.SetActive(true);
        Rocket1_rb.AddForce(transform.up * UpwardVelocity * PercentageOfFuel, ForceMode.Impulse);
        BOOM.Play();
        BOOMfx.SetActive(true);
    }

    public void ResetSim()
    {
        BtnClick.Play();
        SceneManager.LoadScene("GameScene");
    }

    public void PercentageUp()
    {
        BtnClick.Play();
        if (PercentageOfFuel < 1)
        {
            PercentageOfFuel = PercentageOfFuel + 0.1f;
        }
    }

    public void PercentageDown()
    {
        BtnClick.Play();
        if (PercentageOfFuel > 0)
        {
            PercentageOfFuel = PercentageOfFuel - 0.1f;
        }
    }

    public void RocketRight()
    {
        BtnClick.Play();
    }
    public void RocketLeft()
    {
        BtnClick.Play();
    }
    
    public void FuelRight()
    {
        BtnClick.Play();
    }
    public void FuelLeft()
    {
        BtnClick.Play();
    }
}
