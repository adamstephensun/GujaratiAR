using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightConfig : MonoBehaviour
{
    [SerializeField]
    private Renderer renderer;
    [SerializeField]
    private Light light;

    private void Start()
    {
        renderer.material.color = Color.white;  //Set default light colour to white
        renderer.material.SetColor("_EmissionColor", Color.white);
        renderer.material.EnableKeyword("_EMISSION");
    }

    public void UpdateColAndIntensity(Color col, float intensity)
    {
        renderer.material.color = col;
        renderer.material.SetColor("_EmissionColor", col);
        renderer.material.EnableKeyword("_EMMISSION");
        light.color = col;
        light.intensity = intensity;
    }
}
