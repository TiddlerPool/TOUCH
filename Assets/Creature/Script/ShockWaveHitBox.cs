using System.Collections;
using UnityEngine;
using AnimationTween;

public class ShockWaveHitBox : MonoBehaviour
{
    public LineRenderer Circle;
    public Vector3 Offset;
    public float CircleScale;
    public float BlowRange;
    private float radius = 0f;
    private SphereCollider _collider;

    public float BlowDuration;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<SphereCollider>();
        StartCoroutine("Blow");
    }

    // Update is called once per frame
    void Update()
    {
        _collider.radius = radius;
        DrawCircle(Circle.positionCount, radius, radius);
    }

    void DrawCircle(int steps, float minAxis, float maxAxis)
    {
        Circle.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float zScaled = Mathf.Sin(currentRadian);

            float x = minAxis * xScaled * CircleScale + Offset.x;
            float z = maxAxis * zScaled * CircleScale + Offset.z;

            Vector3 currentPosition = new Vector3(x, Offset.y, z);

            Circle.SetPosition(currentStep, currentPosition);
        }

    }

    public IEnumerator Blow()
    {
        float timeBlow = 0;
        while(timeBlow < BlowDuration)
        {
            radius = Mathf.Lerp(radius, BlowRange, Tween.EaseOut(timeBlow / BlowDuration));
            yield return null;
            timeBlow += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
