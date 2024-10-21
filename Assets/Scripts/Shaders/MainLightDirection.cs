using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[ExecuteAlways]
public class MainLightDirection : MonoBehaviour
{
    [Header("SkyBox")]
    [SerializeField] private Material m_SkyboxMaterial;
    [SerializeField] private float _scrollAmmount;
    private string _MainLightDirName = "_MainLightDirection";
    private string _MainLightUpName = "_MainLightUp";
    private string _MainLightRightName = "_MainLightRight";
    private string _MoonPhaseName = "_MoonPhase";
    private int _PhaseDirection = 1;
    private float _PhaseValue;

    [Header("Fog")]
    [SerializeField] private Material m_FogMaterial;
    private string _FogAmmoutName = "_FogAmmount";
    private float _day = 0;
    private float _fadeTime = 2;
    private bool _FogState = false;

    private float _lastRotationValue;

    private void Start()
    {
        _PhaseValue = m_SkyboxMaterial.GetFloat(_MoonPhaseName);
        _lastRotationValue = transform.rotation.x;
    }


    private void Update()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            MoveLight();
            
            CheckDay();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(ToggleFog(!_FogState));
        }

        m_SkyboxMaterial.SetVector(_MainLightDirName, transform.forward);
        m_SkyboxMaterial.SetVector(_MainLightUpName, transform.up);
        m_SkyboxMaterial.SetVector(_MainLightRightName, transform.right);
    }

    void CheckDay()
    {
        if (_lastRotationValue > -0.015f && _lastRotationValue < 0.015f)
        {
            _day += Input.mouseScrollDelta.y;
            Debug.Log(_day);
            ChangeFog();
        }
        _lastRotationValue = transform.rotation.x;
    }

    void ChangeFog()
    {
        if (_day % 3 == 0)
        {
            StartCoroutine(ToggleFog(!_FogState));
        }
    }

    IEnumerator ToggleFog(bool onOff)
    {
        var startValue = m_FogMaterial.GetFloat(_FogAmmoutName);

        var t = 0f;

        var endValue = onOff ? 1f : 0f;

        while (t <= 1f)
        {
            var fog = Mathf.Lerp(startValue, endValue, t);
            m_FogMaterial.SetFloat(_FogAmmoutName, fog);
            t += Time.deltaTime / _fadeTime;
            yield return null;
        }
        _FogState = !_FogState;
    }

    void MoveLight()
    {
        if (_PhaseValue <= 0)
            _PhaseDirection = 1;
        if (_PhaseValue >= 1)
            _PhaseDirection = -1;

        var dir = new Vector2(Input.mouseScrollDelta.y, Input.mouseScrollDelta.x);

        transform.Rotate(dir * _scrollAmmount * Time.deltaTime, Space.Self);

        _PhaseValue += (0.000694f * _PhaseDirection);
        m_SkyboxMaterial.SetFloat(_MoonPhaseName, _PhaseValue);
    }
}
