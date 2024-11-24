using System.Collections;
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

    [SerializeField] private float _freezeDelay = 15f; // Time to lerp to -1
    [SerializeField] private float _heatDelay = 5f; // Time to lerp to 1
    [SerializeField] private float _returnDelay = 2f; // Time to lerp to 0

    private float targetValue = 0f; // Target value to lerp towards
    private float lerpDuration = 0f; // Duration to lerp
    private float lerpSpeed = 0f; // Speed of lerping (time per unit value)
    private bool isLerping = false;

    void Update()
    {
        // Calculate the distance between the camera and the fire
        float distance = Vector3.Distance(transform.position, fire.transform.position);

        // Adjust the range: 3 units before and after the min/max distances
        float minRange = _minDistance + 3f; // 3 units past minDistance
        float maxRange = _maxDistance - 3f; // 3 units before maxDistance

        // Map the distance into the adjusted range: minRange to maxRange
        float distanceFactor = Mathf.InverseLerp(minRange, maxRange, distance);

        // Adjust the target value based on the distance
        if (distanceFactor <= 0)
        {
            targetValue = -1f; // Lerp to -1 if in min range
            lerpDuration = _freezeDelay; // Set the time to lerp to -1
        }
        else if (distanceFactor >= 1)
        {
            targetValue = 1f; // Lerp to 1 if in max range
            lerpDuration = _heatDelay; // Set the time to lerp to 1
        }
        else
        {
            targetValue = 0f; // Lerp to 0 if in between
            lerpDuration = _returnDelay; // Set the time to lerp to 0
        }

        // Calculate lerp speed based on lerp duration
        lerpSpeed = 1f / lerpDuration;

        // Lerp towards the target value using the lerp speed, based on elapsed time
        if (!isLerping || Mathf.Abs(tempValue - targetValue) > 0.01f)
        {
            // Smoothly move the tempValue towards the target value
            tempValue = Mathf.MoveTowards(tempValue, targetValue, lerpSpeed * Time.deltaTime);
            isLerping = true;
        }
        else
        {
            // Once we reach the target value, stop lerping
            isLerping = false;
        }

        // Apply the current value to the shader (m_Temperature)
        m_Temperature.SetFloat(_distanceName, tempValue);

        // Apply the current value to the camera's temperature value (freeCam)
        freeCam.temperatureValue = tempValue;
    }
}