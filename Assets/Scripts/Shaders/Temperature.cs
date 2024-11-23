using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class Temperature : MonoBehaviour
{
    public GameObject fire;
    [SerializeField] private Material m_Temperature;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _minDistance;
    private string _distanceName = "_DistanceFactor";

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, fire.transform.position);

        float clampedDistance = Mathf.Clamp(distance, _minDistance, _maxDistance);

        float remappedValue = Mathf.Lerp(-1f, 1f, (clampedDistance - _minDistance) / (_maxDistance - _minDistance));

        m_Temperature.SetFloat(_distanceName, remappedValue);
    }
}
