using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
public class LightFlickering : MonoBehaviour
{
    public float minTimeBetweenFlickers = 1.0f;
    public float maxTimeBetweenFlickers = 3.0f;

    public float minFlickerDuration = 0.05f;
    public float maxFlickerDuration = 0.15f;

    private Light2D light2D;
    private float baseIntensity;

    [SerializeField] private float minIntensity = 0.5f;

    private void Start()
    {
        light2D = GetComponent<Light2D>();
        baseIntensity = light2D.intensity;
        StartCoroutine(FlickerLoop());
    }

    private IEnumerator FlickerLoop()
    {
        while (true)
        {
            float waitTime = Random.Range(minTimeBetweenFlickers, maxTimeBetweenFlickers);
            yield return new WaitForSeconds(waitTime);

            light2D.intensity = minIntensity;

            float flickerDuration = Random.Range(minFlickerDuration, maxFlickerDuration);
            yield return new WaitForSeconds(flickerDuration);

            light2D.intensity = baseIntensity;
        }
    }
}
