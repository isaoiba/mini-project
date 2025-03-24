using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializedField] private Texture2D skyboxNight;
    [SerializedField] private Texture2D skyboxSunrise;
    [SerializedField] private Texture2D skyboxDay;
    [SerializedField] private Texture2D skyboxSunset;
    
    [SerializedField] private Gradient gradientNightToSunrise;
    [SerializedField] private Gradient gradientSunriseToDay;
    [SerializedField] private Gradient gradientDayToSunset;
    [SerializedField] private Gradient gradientSunsetToNight;

    [SerializedField] private Light globalLight;

    private int minutes;
    public int Minutes
    { get { return minutes; } set { minutes = value; OnMinutesChange(value); } }

    private int hours;
    public int Hours
    { get { return hours; } set { hours = value; OnHoursChange(value); } }
    private int days;
    public int Days
    { get { return days; } set { days = value; } }

    private float tempSecond;

    private void Awake()
    {
        Time.timeScale = 4f;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        tempSecond += Time.deltaTime;
        if (tempSecond >= 1)
        {
            Minutes += 1;
            tempSecond = 0;
        }
    }

    private void OnMinutesChange(int value)
    {
        globalLight.transform.Rotate(Vector3.up, (1f/1440f)*360f, Space.World);//https://www.youtube.com/watch?v=IVW-IhFGvrE
        if (value >= 60)
        {
            Hours++;
            Minutes = 0;
        }
        if (value >= 24)
        {
            Hours = 0;
            Days++;
        }
    }

    private void OnHoursChange(int value)
    {
        if (value >= 6)
        {
            StartCoroutine(LerpSkybox(skyboxNight, skyboxDaySunrise, 10f));
            StartCoroutine(LerpLight(gradientNightToSunrise, 10f));
        }
        else if (value >= 8)
        {
            StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, 10f));
            StartCoroutine(LerpLight(gradientSunriseToDay, 10f));
        }
        else if (value >= 18)
        {
            StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, 10f));
            StartCoroutine(LerpLight(gradientDayToSunset, 10f));
        }
        else if (value >= 22)
        {
            StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, 10f));
            StartCoroutine(LerpLight(gradientSunsetToNight, 10f));
        }
    }

    private IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
    {

    }
}
