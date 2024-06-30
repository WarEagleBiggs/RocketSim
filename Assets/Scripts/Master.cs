using System;
using System.Collections;
using System.Collections.Generic;
using Pinwheel.Jupiter;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Animations;
using Random = UnityEngine.Random;

public class Master : MonoBehaviour
{
    public GameObject Rocket1;
    private Rigidbody Rocket1_rb;

    public bool CanApplyForce;
    public float UpwardThrust;
    public float BurnRate;
    public float PercentageOfFuel; 
    public float WindSpeed;

    public Vector3 wind;
    public TextMeshProUGUI Velocity;
    public TextMeshProUGUI Altidude;
    public TextMeshProUGUI PercentageText;
    public TextMeshProUGUI WindSpeedTxt;

    public ParticleSystem BOOM;
    public GameObject BOOMfx;
    public AudioSource BOOMsfx;
    public Animator TowerAnim;

    public GameObject RunBtn;
    public GameObject ResetBtn;
    public List<GameObject> ButtonsToHide;
    public AudioSource BtnClick;

    public int CurrRocket = 1;
    public int CurrFuel = 1;
    public TextMeshProUGUI CurrFuelTxt;

    public float ApexAltitude;
    public GameObject ApexLine;
    public TextMeshPro ApexTxt;
    public bool canMoveApex = true;
    public bool isRunning;
    public bool canHappenOnce = false;
    
    //sky fx
    public JDayNightCycle NightCycleSettings;
    public TextMeshProUGUI TimeTxt;
    
    void Start()
    {
        Rocket1_rb = Rocket1.GetComponent<Rigidbody>();

        NightCycleSettings.Time = Random.Range(1, 23);

        //set
        CurrRocket = Singleton.GetInstance.currRocket;
        CurrFuel = Singleton.GetInstance.currFuel;
        PercentageOfFuel = Singleton.GetInstance.currQuantity;
        
        //randomize wind
        WindSpeed = Random.Range(1, 5);
        //randomize wind direction
        wind = new Vector3(Random.Range(-1f, 1.1f), 0, Random.Range(-1f, 1.1f));

    }

    void Update()
    {
        
        //set time txt
        TimeTxt.SetText(NightCycleSettings.Time.ToString("0") + ":00 HRS");
        
        WindSpeedTxt.SetText("Wind: " + (10 * WindSpeed).ToString("0") + " m/s");
        
        //leave at apex
        if (canMoveApex)
        {
            ApexLine.transform.position = Rocket1.transform.position;
        }

        if (isRunning && Rocket1_rb.velocity.y <= 1)
        {
            //apex
            canMoveApex = false;

            ApexTxt.SetText(ApexAltitude.ToString("0"));
            if (canHappenOnce)
            {
                canHappenOnce = false;
                ApexAltitude = Rocket1.transform.position.y;
            }
        }

        
        PercentageText.SetText((PercentageOfFuel * 100).ToString("0") + "%");
        
        Rocket1_rb.AddForce(wind.normalized * WindSpeed * 2000 * Time.deltaTime);
        
        Velocity.SetText(Rocket1_rb.velocity.y.ToString("0" + " m/s"));
        

        Altidude.SetText(Rocket1.transform.position.y.ToString("0" + " m"));

        if (CurrFuel == 1)
        {
            //FUEL A
            Singleton.GetInstance.currFuel = 1;
            CurrFuelTxt.SetText("A");
            UpwardThrust = 100;
            BurnRate = 12;
        } else if (CurrFuel == 2)
        {
            //FUEL B
            Singleton.GetInstance.currFuel = 2;
            CurrFuelTxt.SetText("B");
            UpwardThrust = 300;
            BurnRate = 4;
        } else if (CurrFuel == 3)
        {
            //FUEL C
            Singleton.GetInstance.currFuel = 3;
            CurrFuelTxt.SetText("C");
            UpwardThrust = 620;
            BurnRate = 2;
        }
    }

    private void FixedUpdate()
    {
        //physics
        if (CanApplyForce)
        {
            Rocket1_rb.AddForce(new Vector3(0,  Rocket1.transform.position.y,0).normalized * UpwardThrust * PercentageOfFuel, ForceMode.Impulse);
        }
        
        
        
    }

    public void RunSim()
    {
        TowerAnim.SetBool("canRun", true);
        
        isRunning = true;
        BtnClick.Play();

        StartCoroutine(StartApexCalc());
        
        foreach (var i in ButtonsToHide)
        {
            i.SetActive(false);
        }
        
        
        
        RunBtn.SetActive(false);
        ResetBtn.SetActive(true);
        
        //apply force
        CanApplyForce = true;
        StartCoroutine(BurnFuel());

        if (PercentageOfFuel != 0)
        {
            BOOMsfx.Play();
            BOOM.Play();
            BOOMfx.SetActive(true);
        }
        
    }

    public void PauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        
    }
    public void FastForward()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 6;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ResetSim()
    {
        Time.timeScale = 1;
        BtnClick.Play();
        SceneManager.LoadScene("GameScene");
    }

    public void PercentageUp()
    {
        BtnClick.Play();
        if (PercentageOfFuel < 1)
        {
            PercentageOfFuel = PercentageOfFuel + 0.1f;
            Singleton.GetInstance.currQuantity = PercentageOfFuel;
        }
    }

    public void PercentageDown()
    {
        BtnClick.Play();
        if (PercentageOfFuel > 0)
        {
            PercentageOfFuel = PercentageOfFuel - 0.1f;
            Singleton.GetInstance.currQuantity = PercentageOfFuel;
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

    public IEnumerator StartApexCalc()
    {
        yield return new WaitForSeconds(1);
        ApexTxt.SetText(" ");
        ApexTxt.gameObject.SetActive(true);
        canMoveApex = true;
        canHappenOnce = true;
    }

}
