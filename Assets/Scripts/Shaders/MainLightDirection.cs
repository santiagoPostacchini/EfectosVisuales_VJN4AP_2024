using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class MainLightDirection : MonoBehaviour
{
    [SerializeField] private Material m_SkyboxMaterial;
    [SerializeField] private float _scrollAmmount;
    private string _MainLightDirName = "_MainLightDirection";
    private string _MainLightUpName = "_MainLightUp";
    private string _MainLightRightName = "_MainLightRight";
    private string _MoonPhaseName = "_MoonPhase";
    private int _PhaseDirection = 1;
    private float _PhaseValue;

    private void Start()
    {
        _PhaseValue = m_SkyboxMaterial.GetFloat(_MoonPhaseName);
    }


    private void Update()
    {

        if (Input.mouseScrollDelta == Vector2.up)
        {
            if (_PhaseValue <= 0)
                _PhaseDirection = 1;

            if (_PhaseValue >= 1)
                _PhaseDirection = -1;

            transform.Rotate(Vector3.left * _scrollAmmount * Time.deltaTime, Space.Self);

            _PhaseValue += 0.000347f * _PhaseDirection;
            m_SkyboxMaterial.SetFloat(_MoonPhaseName, _PhaseValue);
        }

        if (Input.mouseScrollDelta == Vector2.down)
        {

            if (_PhaseValue <= 0)
                _PhaseDirection = 1;

            if (_PhaseValue >= 1)
                _PhaseDirection = -1;
            transform.Rotate(Vector3.right * _scrollAmmount * Time.deltaTime, Space.Self);

            _PhaseValue -= 0.000347f * _PhaseDirection;
            m_SkyboxMaterial.SetFloat(_MoonPhaseName, _PhaseValue);
        }

        m_SkyboxMaterial.SetVector(_MainLightDirName, transform.forward);
        m_SkyboxMaterial.SetVector(_MainLightUpName, transform.up);
        m_SkyboxMaterial.SetVector(_MainLightRightName, transform.right);
    }
}
