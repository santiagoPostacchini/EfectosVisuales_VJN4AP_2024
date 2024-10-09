using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLightDirection : MonoBehaviour
{
    [SerializeField] private Material m_SkyboxMaterial;
    [SerializeField] private float _scrollAmmount;
    private string _MainLightDirName = "_MainLightDirection";
    
    private void Update()
    {
        if (Input.mouseScrollDelta == Vector2.up)
        {
            transform.Rotate(Vector3.left * _scrollAmmount * Time.deltaTime, Space.Self);
        }

        if (Input.mouseScrollDelta == Vector2.down)
        {
            transform.Rotate(Vector3.right * _scrollAmmount * Time.deltaTime, Space.Self);
        }

        m_SkyboxMaterial.SetVector(_MainLightDirName, transform.forward);
    }
}
