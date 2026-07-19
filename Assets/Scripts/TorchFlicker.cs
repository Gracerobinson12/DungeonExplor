using UnityEngine;

/// <summary>
/// Makes a point light flicker like a torch for dungeon atmosphere.
/// </summary>
public class TorchFlicker : MonoBehaviour
{
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.4f;
    public float flickerSpeed = 8f;

    Light torchLight;

    void Start()
    {
        torchLight = GetComponent<Light>();
    }

    void Update()
    {
        if (torchLight == null) return;
        float target = Mathf.Lerp(minIntensity, maxIntensity,
            Mathf.PerlinNoise(Time.time * flickerSpeed, 0f));
        torchLight.intensity = target;
    }
}
