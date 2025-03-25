using System;
using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Texture2D skyboxNight;
    [SerializeField] private Texture2D skyboxSunrise;
    [SerializeField] private Texture2D skyboxDay;
    [SerializeField] private Texture2D skyboxSunset;
    
    [SerializeField] private Gradient gradientNightToSunrise;
    [SerializeField] private Gradient gradientSunriseToDay;
    [SerializeField] private Gradient gradientDayToSunset;
    [SerializeField] private Gradient gradientSunsetToNight;

    [SerializeField] private Light globalLight;

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

    private int hours;
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
        Time.timeScale = 4f; // Speed up time progression for testing
        // Create a new Material for the skybox
        skyboxMaterial = new Material(Shader.Find("Skybox/Procedural"));
        RenderSettings.skybox = skyboxMaterial; // Assign the material to RenderSettings.skybox
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initial Skybox setup if needed
        RenderSettings.skybox = skyboxMaterial;
        globalLight.color = gradientNightToSunrise.Evaluate(0); // Start with night light color
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
        globalLight.transform.Rotate(Vector3.up, (1f / 1440f) * 360f, Space.World); // Rotate the light source each minute
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
        if (value >= 6 && value < 8)
        {
            StartCoroutine(LerpSkybox(skyboxNight, skyboxSunrise, 10f));
            StartCoroutine(LerpLight(gradientNightToSunrise, 10f));
        }
        else if (value >= 8 && value < 18)
        {
            StartCoroutine(LerpSkybox(skyboxSunrise, skyboxDay, 10f));
            StartCoroutine(LerpLight(gradientSunriseToDay, 10f));
        }
        else if (value >= 18 && value < 22)
        {
            StartCoroutine(LerpSkybox(skyboxDay, skyboxSunset, 10f));
            StartCoroutine(LerpLight(gradientDayToSunset, 10f));
        }
        else if (value >= 22 || value < 6)
        {
            StartCoroutine(LerpSkybox(skyboxSunset, skyboxNight, 10f));
            StartCoroutine(LerpLight(gradientSunsetToNight, 10f));
        }
    }

    private IEnumerator LerpSkybox(Texture2D a, Texture2D b, float time)
    {
        // Set initial skybox texture
        skyboxMaterial.SetTexture("_MainTex", a);

        // Lerp the blend between skyboxes over time
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            skyboxMaterial.SetFloat("_Blend", i / time);
            yield return null;
        }

        // Set the final skybox texture
        skyboxMaterial.SetTexture("_MainTex", b);
    }

    private IEnumerator LerpLight(Gradient lightGradient, float time)
    {
        for (float i = 0; i < time; i += Time.deltaTime)
        {
            globalLight.color = lightGradient.Evaluate(i / time);  // Interpolate light color based on the gradient
            RenderSettings.fogColor = globalLight.color;  // Change fog color to match light color
            yield return null;
        }
    }
}
