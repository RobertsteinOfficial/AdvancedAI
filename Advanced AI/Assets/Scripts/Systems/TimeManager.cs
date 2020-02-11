using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DayPhase
{
    Night,     //01.00 - 05.00 (0.84 - 0.91)
    Dawn,      //05.00 - 07.00 (0.92 - 0.08)
    Morning,   //07.00 - 11.00 (0.09 - 0.16)
    Noon,      //11.00 - 13.00 (0.17 - 0.33)
    Afternoon, //13.00 - 17.00 (0.34 - 0.41)
    Dusk,      //17.00 - 19.00 (0.42 - 0.58)
    Evening,   //19.00 - 23.00 (0.59 - 0.66)
    Midnight   //23.00 - 01.00 (0.67 - 0.83)
}
public class TimeManager : MonoBehaviour
{
    [Header("Parameters")]
    public float rotateSpeed = 1;
    public float dayPhaseUpdateFrequency = 1;

    public static Action<DayPhase> OnPhaseChange;

    //LOCAL
    private Transform sunLight;
    public static float angle = 0;
    public static DayPhase actualPhase = DayPhase.Night;

    private void Start()
    {
        sunLight = transform.GetChild(0).transform;
        InvokeRepeating("PollTime", dayPhaseUpdateFrequency, dayPhaseUpdateFrequency);
    }

    private void Update()
    {
        transform.Rotate(transform.right, rotateSpeed * Time.deltaTime, Space.World);
    }

    void PollTime()
    {
        //incremento tempo e rotazione
        angle += rotateSpeed * dayPhaseUpdateFrequency;
        //controllo dayphase
        float n = ((Mathf.Deg2Rad * angle) / (Mathf.PI * 2)) % 1;
        DayPhase newDayPhase = TimeToPhase(n);
        //aggiorno se cambia
        if (actualPhase != newDayPhase)
        {
            actualPhase = newDayPhase;
            OnPhaseChange.Invoke(actualPhase);
        }
    }

    DayPhase TimeToPhase(float n)
    {
        if (n > 0.84f && n <= 0.91f) return DayPhase.Night;
        else if (n > 0.91f || n <= 0.08f) return DayPhase.Dawn;
        else if (n > 0.08f && n <= 0.16f) return DayPhase.Morning;
        else if (n > 0.16f && n <= 0.33f) return DayPhase.Noon;
        else if (n > 0.33f && n <= 0.41f) return DayPhase.Afternoon;
        else if (n > 0.41f && n <= 0.58f) return DayPhase.Dusk;
        else if (n > 0.58f && n <= 0.67f) return DayPhase.Evening;
        else return DayPhase.Midnight;
    }

}
