using System;
using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Cubemap skyboxNight;
    [SerializeField] private Cubemap skyboxDay;
    
    [SerializeField] private Light globalLight;
    [SerializeField] private Color nightColor = Color.blue;
    [SerializeField] private Color dayColor = Color.yellow;

    private int minutes;
    public int Minutes
    { 
        get { return minutes; } 
        set 
        { 
            minutes = value; 
            OnMinutesChange(value); 
        } 
    }

    private int hours = 6; // Start at 6 AM
    public int Hours
    { 
        get { return hours; } 
        set 
        { 
            hours = value; 
            OnHoursChange(value); 
        } 
    }
    
    private int days;
    public int Days
    { 
        get { return days; } 
        set { days = value; } 
    }

    private float tempSecond;
    private Material skyboxMaterial;

    private void Awake()
    {
        Time.timeScale = 1f;
        skyboxMaterial = new Material(Shader.Find("Skybox/Cubemap"));
        RenderSettings.skybox = skyboxMaterial;
    }

    void Start()
    {
        SetSkyboxCubemap(skyboxDay);
        globalLight.color = dayColor;
    }

    void Update()
    {
        tempSecond += Time.deltaTime * 10f;
        if (tempSecond >= 1)
        {
            Minutes += 1;
            tempSecond = 0;
        }
    }

    private void OnMinutesChange(int value)
    {
        globalLight.transform.Rotate(Vector3.up, (1f / 1440f) * 360f, Space.World);
        if (value >= 60)
        {
            Hours++;
            Minutes = 0;
        }
        if (Hours >= 24)
        {
            Hours = 0;
            Days++;
        }
    }

    private void OnHoursChange(int value)
    {
        if (value == 6)
        {
            StartCoroutine(LerpSkybox(skyboxNight, skyboxDay, 10f));
            StartCoroutine(LerpLight(nightColor, dayColor, 10f));
        }
        else if (value == 18)
        {
            StartCoroutine(LerpSkybox(skyboxDay, skyboxNight, 10f));
            StartCoroutine(LerpLight(dayColor, nightColor, 10f));
        }
    }

    private void SetSkyboxCubemap(Cubemap cubemap)
    {
        skyboxMaterial.SetTexture("_Tex", cubemap);
    }

    private IEnumerator LerpSkybox(Cubemap a, Cubemap b, float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            float t = elapsedTime / time;
            skyboxMaterial.SetTexture("_Tex", t < 0.5f ? a : b);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        SetSkyboxCubemap(b);
    }

    private IEnumerator LerpLight(Color startColor, Color endColor, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            float t = i / time;
            globalLight.color = Color.Lerp(startColor, endColor, t);
            RenderSettings.fogColor = globalLight.color;
            yield return null;
        }
    }
}
