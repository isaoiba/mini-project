using UnityEngine;

public class SkyboxBlender : MonoBehaviour
{
    public Material dayNightSkyboxMaterial;  // The skybox material that blends between day and night
    public float cycleSpeed = 0.5f;         // Speed of the day-night cycle
    private float timeOfDay = 0f;           // Time of day used to drive the sine function

    // The name of the shader parameter that controls the blend between day and night
    public string blendParameter = "_Blend"; 

    void Start()
    {
        // Make sure fog is enabled (optional, if you want fog to change with the skybox)
        RenderSettings.fog = true;
    }

    void Update()
    {
        // Increment time based on cycle speed
        timeOfDay += Time.deltaTime * cycleSpeed;

        // Use the sine function to smoothly oscillate between -1 and 1
        float sineValue = Mathf.Sin(timeOfDay);

        // Map the sine value from [-1, 1] to [0, 1] to blend from day to night
        float blendValue = (sineValue + 1f) / 2f;

        // Set the global shader parameter that blends the skybox
        dayNightSkyboxMaterial.SetFloat(blendParameter, blendValue);
    }
}
