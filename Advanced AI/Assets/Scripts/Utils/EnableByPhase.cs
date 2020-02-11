using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableByPhase : MonoBehaviour
{
    Light myLight;
    float myIntensity;

    private void Start()
    {
        myLight = GetComponent<Light>();
        myIntensity = myLight.intensity;
    }

    private void OnEnable()
    {
        TimeManager.OnPhaseChange += SetLight;
    }

    private void OnDisable()
    {
        TimeManager.OnPhaseChange -= SetLight;
    }

    void SetLight(DayPhase phase)
    {
        switch (phase)
        {
            case DayPhase.Night:
            case DayPhase.Evening:
            case DayPhase.Midnight:
                if(myLight.intensity != 0)
                myLight.intensity = 0;
                break;
            case DayPhase.Dawn:
            case DayPhase.Morning:
            case DayPhase.Noon:
            case DayPhase.Afternoon:
            case DayPhase.Dusk:
                if (myLight.intensity != myIntensity)
                    myLight.intensity = myIntensity;
                break;
        }
    }
}
