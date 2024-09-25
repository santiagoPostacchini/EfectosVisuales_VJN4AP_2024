using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassInteraction : MonoBehaviour
{
    [Header("RENDERING")]
    [SerializeField] private string _sqrDistName = "_SqrDistance";

    private float _sqrDist = 0.0f;

    private Renderer _renderer;
    private Material _mat;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();

        _mat = _renderer.material;
    }

    private void Update()
    {
        _sqrDist = Vector3.SqrMagnitude(transform.position - Player.instance.transform.position);

        Debug.Log(_sqrDist);

        _mat.SetFloat(_sqrDistName, _sqrDist);
    }
}
