using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DayNightLight : MonoBehaviour
{
    public Light2D nightLight;

    public float dayInnerRadius = 2f;
    public float dayOuterRadius = 15f;

    public float nightInnerRadius = 0.3f;
    public float nightOuterRadius = 5f;

    public float cycleDuration = 5f;
    public float smoothTime = 0.5f;

    private float timer;
    private bool isNight;

    private float innerVelocity;
    private float outerVelocity;

    void Start()
    {
        nightLight.pointLightInnerRadius = dayInnerRadius;
        nightLight.pointLightOuterRadius = dayOuterRadius;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= cycleDuration)
        {
            isNight = !isNight;
            timer = 0f;
        }

        float targetInner = isNight ? nightInnerRadius : dayInnerRadius;
        float targetOuter = isNight ? nightOuterRadius : dayOuterRadius;

        nightLight.pointLightInnerRadius = Mathf.SmoothDamp(
            nightLight.pointLightInnerRadius,
            targetInner,
            ref innerVelocity,
            smoothTime
        );

        nightLight.pointLightOuterRadius = Mathf.SmoothDamp(
            nightLight.pointLightOuterRadius,
            targetOuter,
            ref outerVelocity,
            smoothTime
        );
    }
}