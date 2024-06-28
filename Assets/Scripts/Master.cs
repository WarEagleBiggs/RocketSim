using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Master : MonoBehaviour
{
    public GameObject Rocket1;
    private Rigidbody Rocket1_rb;

    public bool CanApplyForce;
    public float UpwardThrust;
    public float BurnRate;
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

    public int CurrRocket = 1;
    public int CurrFuel = 1;
    public TextMeshProUGUI CurrFuelTxt;

    public float ApexAltitude;
    public GameObject ApexLine;
    public bool canMoveApex = true;
    public bool isRunning;
    
    void Start()
    {
        Rocket1_rb = Rocket1.GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        //leave at apex
        if (canMoveApex)
        {
            ApexLine.transform.position = Rocket1.transform.position;
        }

        if (isRunning && Rocket1_rb.velocity.z == 1)
        {
            Debug.Log("Apex");
            canMoveApex = false;
        }

        
        PercentageText.SetText((PercentageOfFuel * 100).ToString("0") + "%");
        Rocket1_rb.AddForce(wind.normalized * WindSpeed * Time.deltaTime);
        Velocity.SetText(Rocket1_rb.velocity.magnitude.ToString("0" + " m/s"));
        Altidude.SetText(Rocket1.transform.position.y.ToString("0" + " m"));

        if (CurrFuel == 1)
        {
            //FUEL A
            CurrFuelTxt.SetText("A");
            UpwardThrust = 100;
            BurnRate = 12;
        } else if (CurrFuel == 2)
        {
            //FUEL B
            CurrFuelTxt.SetText("B");
            UpwardThrust = 300;
            BurnRate = 4;
        } else if (CurrFuel == 3)
        {
            //FUEL C
            CurrFuelTxt.SetText("C");
            UpwardThrust = 640;
            BurnRate = 2;
        }
    }

    private void FixedUpdate()
    {
        //physics
        if (CanApplyForce)
        {
            Rocket1_rb.AddForce(transform.up * UpwardThrust * PercentageOfFuel, ForceMode.Impulse);
        }
        
    }

    public void RunSim()
    {
        isRunning = true;
        BtnClick.Play();
        foreach (var i in ButtonsToHide)
        {
            i.SetActive(false);
        }
        BOOMsfx.Play();
        RunBtn.SetActive(false);
        ResetBtn.SetActive(true);
        //apply force
        CanApplyForce = true;
        StartCoroutine(BurnFuel());
        
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
        if (CurrRocket < 3)
        {
            CurrRocket++;
        }
        
    }
    public void RocketLeft()
    {
        BtnClick.Play();
        if (CurrRocket > 1)
        {
            CurrRocket--;
        }
    }
    
    public void FuelRight()
    {
        BtnClick.Play();
        if (CurrFuel < 3)
        {
            CurrFuel++;
        }
    }
    public void FuelLeft()
    {
        BtnClick.Play();
        if (CurrFuel > 1)
        {
            CurrFuel--;
        }
    }

    public IEnumerator BurnFuel()
    {
        yield return new WaitForSeconds(BurnRate);
        CanApplyForce = false;
        BOOMfx.SetActive(false);
    }
}
