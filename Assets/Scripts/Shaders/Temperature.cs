using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Temperature : MonoBehaviour
{
    public FreeFlyCamera freeCam;
    public GameObject fire;
    [SerializeField] private Material m_Temperature;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _minDistance;
    private string _distanceName = "_DistanceFactor";
    public float tempValue = 0;

    [SerializeField] private float _freezeDelay = 15f;
    [SerializeField] private float _heatDelay = 5f;
    [SerializeField] private float _returnDelay = 2f;

    private float targetValue = 0f;
    private float lerpDuration = 0f;
    private float lerpSpeed = 0f;
    private bool isLerping = false;
    private bool isBurning = false;

    void Update()
    {
        float distance = Vector3.Distance(transform.position, fire.transform.position);

        float minRange = _minDistance + 5f;
        float maxRange = _maxDistance - 5f;

        float distanceFactor = Mathf.InverseLerp(minRange, maxRange, distance);

        if (distanceFactor <= 0)
        {
            targetValue = -1f;
            lerpDuration = _freezeDelay;
        }
        else if (distanceFactor >= 1)
        {
            targetValue = 1f;
            lerpDuration = _heatDelay;
        }
        else
        {
            targetValue = 0f;
            lerpDuration = _returnDelay;
        }

        lerpSpeed = 1f / lerpDuration;

        if (!isLerping || Mathf.Abs(tempValue - targetValue) > 0.01f)
        {
            tempValue = Mathf.MoveTowards(tempValue, targetValue, lerpSpeed * Time.deltaTime);
            isLerping = true;
        }
        else
        {
            isLerping = false;
        }

        if (tempValue < -0.5f && !isBurning && distance < _minDistance || tempValue > 0.5f && !isBurning &&  distance > maxRange)
        {
            isBurning = true;
            StartCoroutine(Burn());
        }

        m_Temperature.SetFloat(_distanceName, tempValue);

        freeCam.temperatureValue = tempValue;
    }

    IEnumerator Burn()
    {
        Hurt.instance.GetHurt(Hurt.instance.hitCurve);
        yield return new WaitForSeconds(Hurt.instance.effectDuration);
        isBurning = false;
    }
}