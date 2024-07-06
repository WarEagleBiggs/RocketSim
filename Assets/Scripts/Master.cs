using System;
using System.Collections;
using System.Collections.Generic;
using Pinwheel.Jupiter;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Animations;
using Random = UnityEngine.Random;
using UnityEngine.UI;

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
    public GameObject WindDirectionIndicator;
    public TextMeshProUGUI Velocity;
    public TextMeshProUGUI Altidude;
    public TextMeshProUGUI PercentageText;
    public TextMeshProUGUI WindSpeedTxt;

    public GameObject RocketExplosionObj;
    public ParticleSystem RocketExplosion1;
    public ParticleSystem RocketExplosion2;
    public bool FollowingApex;
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

    public Image PauseColor;
    public Image FastForwardColor;

    public int randomRotationDir;
    
    //objects to toggle between rockets
    public MeshCollider Rocket1_Collider;
    public MeshRenderer Rocket1_Model;
    public GameObject Rocket1_Trails;
    public GameObject Rocket1_MiniModel;
    public GameObject Rocket1_Image;

    public GameObject Rocket2_Model;
    public GameObject Rocket2_Trails;
    public GameObject Rocket2_MiniModel;
    public GameObject Rocket2_Image;

    public GameObject Rocket3_Model;
    public GameObject Rocket3_Trails;
    public GameObject Rocket3_MiniModel;
    public GameObject Rocket3_Image;
    
    
    void Start()
    {
        Rocket1_rb = Rocket1.GetComponent<Rigidbody>();

        NightCycleSettings.Time = Random.Range(1, 23);

        randomRotationDir = Random.Range(1, 3);

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
        Vector3 normalizedDirection = wind.normalized;

        Quaternion targetRotation = Quaternion.LookRotation(normalizedDirection);

        WindDirectionIndicator.transform.rotation = targetRotation;
        
        Debug.DrawRay(WindDirectionIndicator.transform.position, normalizedDirection, Color.magenta);
        
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
            FollowingApex = true;

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

        if (CurrRocket == 1)
        {
            Singleton.GetInstance.currRocket = 1;
            Rocket1_rb.mass = 130;
            
            //Rocket 1
            Rocket1_Collider.enabled = true;
            Rocket1_Model.enabled = true;
            Rocket1_Trails.SetActive(true);
            Rocket1_MiniModel.SetActive(true);
            Rocket1_Image.SetActive(true);
            
            Rocket2_Model.SetActive(false);
            Rocket2_Trails.SetActive(false);
            Rocket2_MiniModel.SetActive(false);
            Rocket2_Image.SetActive(false);
            
            Rocket3_Model.SetActive(false);
            Rocket3_Trails.SetActive(false);
            Rocket3_MiniModel.SetActive(false);
            Rocket3_Image.SetActive(false);
        } else if (CurrRocket == 2)
        {
            Singleton.GetInstance.currRocket = 2;
            Rocket1_rb.mass = 100;
            
            Rocket1_Collider.enabled = false;
            Rocket1_Model.enabled = false;
            Rocket1_Trails.SetActive(false);
            Rocket1_MiniModel.SetActive(false);
            Rocket1_Image.SetActive(false);
            
            //Rocket 2
            Rocket2_Model.SetActive(true);
            Rocket2_Trails.SetActive(true);
            Rocket2_MiniModel.SetActive(true);
            Rocket2_Image.SetActive(true);
            
            Rocket3_Model.SetActive(false);
            Rocket3_Trails.SetActive(false);
            Rocket3_MiniModel.SetActive(false);
            Rocket3_Image.SetActive(false);
        } else if (CurrRocket == 3)
        {
            Singleton.GetInstance.currRocket = 3;
            Rocket1_rb.mass = 120;
            
            Rocket1_Collider.enabled = false;
            Rocket1_Model.enabled = false;
            Rocket1_Trails.SetActive(false);
            Rocket1_MiniModel.SetActive(false);
            Rocket1_Image.SetActive(false);
            
            Rocket2_Model.SetActive(false);
            Rocket2_Trails.SetActive(false);
            Rocket2_MiniModel.SetActive(false);
            Rocket2_Image.SetActive(false);

            //rocket 3
            Rocket3_Model.SetActive(true);
            Rocket3_Trails.SetActive(true);
            Rocket3_MiniModel.SetActive(true);
            Rocket3_Image.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        //physics
        if (CanApplyForce)
        {
            Rocket1_rb.AddForce(new Vector3(0,  Rocket1.transform.position.y,0).normalized * UpwardThrust * PercentageOfFuel, ForceMode.Impulse);
            
            if (randomRotationDir == 1)
            {
                Rocket1_rb.AddTorque(transform.up * WindSpeed * 5);
            } else
            {
                Rocket1_rb.AddTorque(transform.up * WindSpeed * -5);
            }
        }
        
        
        
    }

    public void EXPLODE()
    {
        RocketExplosionObj.transform.position = Rocket1.transform.position;
        RocketExplosion1.Play();
        RocketExplosion2.Play();
        
    }

    public void RunSim()
    {
        Rocket1_rb.isKinematic = false;
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
        if (Time.timeScale == 1 || Time.timeScale == 6)
        {
            Time.timeScale = 0;
            PauseColor.color = Color.grey;
            FastForwardColor.color = Color.white;
        }
        else
        {
            Time.timeScale = 1;
            PauseColor.color = Color.white;
            FastForwardColor.color = Color.white;
        }
        
    }
    public void FastForward()
    {
        if (Time.timeScale == 1 || Time.timeScale == 0)
        {
            Time.timeScale = 6;
            PauseColor.color = Color.white;
            FastForwardColor.color = Color.grey;
        }
        else
        {
            Time.timeScale = 1;
            PauseColor.color = Color.white;
            FastForwardColor.color = Color.white;
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
