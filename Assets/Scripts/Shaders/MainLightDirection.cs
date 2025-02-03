using System;
using System.Collections;
using UnityEngine;

public class MainLightDirection : MonoBehaviour
{
    [Header("SkyBox")]
    [SerializeField] private Material m_SkyboxMaterial;
    [SerializeField] private float _scrollAmmount;
    [SerializeField] private float _rotVelocity;
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

    Action _skyMovementFunction;

    private void Start()
    {
        _skyMovementFunction = Rotation;
        _PhaseValue = m_SkyboxMaterial.GetFloat(_MoonPhaseName);
        _lastRotationValue = transform.rotation.x;
    }


    private void Update()
    {
        _skyMovementFunction();

        m_SkyboxMaterial.SetVector(_MainLightDirName, transform.forward);
        m_SkyboxMaterial.SetVector(_MainLightUpName, transform.up);
        m_SkyboxMaterial.SetVector(_MainLightRightName, transform.right);
    }

    void CheckDay(float rv = 0.00015f)
    {
        if (_lastRotationValue > -rv && _lastRotationValue < rv)
        {
            var sign = _lastRotationValue < 0 ? -1 : 1;
            _day += 1 * sign;
            //Debug.Log(_day);
            ChangeFog();
        }
        _lastRotationValue = transform.rotation.x;
    }

    void Rotation()
    {
        transform.Rotate(transform.right * _rotVelocity * Time.deltaTime, Space.Self);

        _PhaseValue += (_PhaseDirection * Time.deltaTime);

        CheckDay();

        if (Input.GetKeyDown(KeyCode.I))
            _skyMovementFunction = Inputs;
    }

    void Inputs()
    {
        if (Input.mouseScrollDelta != Vector2.zero)
        {
            MoveLight();

            CheckDay(0.015f);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(ToggleFog(!_FogState));
        }

        if (Input.GetKeyDown(KeyCode.I))
            _skyMovementFunction = Rotation;
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
