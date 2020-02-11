using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public enum Weather
{
    Clear,
    Foggy,
    Rainy,
    Stormy
}

public struct WeatherSetup
{
    public float rainIntensity;
    public float fogIntensity;
}

public class WeatherManager : MonoBehaviour
{
    public AnimationCurve fogProbability;
    public AnimationCurve rainProbability;

    public WeatherSetup WeatherStatus
    {
        get
        {
            WeatherSetup setup = new WeatherSetup();
            switch (ActualWeather)
            {
                case Weather.Clear:
                    setup.rainIntensity = 0;
                    setup.fogIntensity = 0;
                    break;
                case Weather.Foggy:
                    setup.rainIntensity = 0;
                    setup.fogIntensity = 0.25f;
                    break;
                case Weather.Rainy:
                    setup.rainIntensity = 1;
                    setup.fogIntensity = 0.05f;
                    break;
                case Weather.Stormy:
                    setup.rainIntensity = 3;
                    setup.fogIntensity = 0.1f;
                    break;
            }
            return setup;
        }
    }

    //Local
    public static Weather ActualWeather;
    ParticleSystem rainParticle;

    private void OnEnable()
    {
        TimeManager.OnPhaseChange += DayPhaseChanged;
    }

    void Start()
    {
        rainParticle = transform.GetChild(0).GetComponent<ParticleSystem>();
    }

    private void OnDisable()
    {
        TimeManager.OnPhaseChange -= DayPhaseChanged;
    }

    void DayPhaseChanged(DayPhase phase)
    {
        float timeOfDay = ((Mathf.Deg2Rad * TimeManager.angle) / (Mathf.PI * 2)) % 1;
        float fogProb = fogProbability.Evaluate(timeOfDay);
        float rainProb = rainProbability.Evaluate(timeOfDay);

        if (fogProb < 0.4f && rainProb < 0.4f)
            ActualWeather = Weather.Clear;
        else if (fogProb > rainProb && fogProb > 0.7f)
            ActualWeather = Weather.Foggy;
        else if (fogProb < rainProb && rainProb > 0.7f)
            ActualWeather = Weather.Stormy;
        else
            ActualWeather = Weather.Rainy;

        UpdateWeather();
    }

    private void UpdateWeather()
    {
        WeatherSetup setup = WeatherStatus;
        //rain
        EmissionModule emissionModule = rainParticle.emission;
        emissionModule.rateOverTime = setup.rainIntensity * 500;
        //fog
        RenderSettings.fogDensity = setup.fogIntensity;
    }
}
