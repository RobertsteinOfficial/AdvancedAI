using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 1;
    public float runSpeed = 3;
    public float turnSpeed = 90;
    [Header("Stats")]
    public int maxHp = 100;
    [Range(0, 1)]
    public float weatherSoundInfluence = 0.5f;


    //local
    float actualHp;
    float actualSpeed;
    Rigidbody rBody;
    bool isInside = false;

    //static
    public static PlayerController instance;
    public static Transform PlayerTransform { get { return instance.transform; } }
    public static bool IsStealth = false;
    public static float PlayerNoiseEmission // 0 - 1
    {
        get
        {
            //rapporto tra velocità attuale e massima / meteo
            float speedNoise = instance.rBody.velocity.magnitude / instance.runSpeed;
            float weatherNoise = 1;
            
            if (!instance.isInside && (int)WeatherManager.ActualWeather > 1) //or ++ > 2
            {
                //rainy or storm
                weatherNoise = (float)WeatherManager.ActualWeather - instance.weatherSoundInfluence;
            }

            return speedNoise / weatherNoise;
        }
    }

    #region Monobehaviour
    private void Awake()
    {
        if (!instance)
            instance = this;
    }

    private void OnValidate()
    {
        actualSpeed = walkSpeed;
    }

    void Start()
    {
        actualHp = maxHp;
        rBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Area myArea = new Area();
            if(AreaManager.GetArea(transform.position,out myArea))
            {
                Debug.Log(myArea.areaName);
            }
        }


        CheckStealth();
        Move();
        Turn();
    }
    #endregion

    public static void OnBuildingEnterExit(bool isEntering)
    {
        instance.isInside = isEntering;
        CameraController.PlayerInside(isEntering);
    }

    #region Internal methods

    void CheckStealth()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            actualSpeed = walkSpeed;
            IsStealth = true;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            actualSpeed = runSpeed;
            IsStealth = false;
        }
    }

    void Move()
    {
        float forward = Input.GetAxis("Vertical");
        transform.Translate(transform.forward * actualSpeed * forward * Time.deltaTime, Space.World);
    }

    void Turn()
    {
        float rotation = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, rotation * turnSpeed * Time.deltaTime);
    }

    #endregion

}
