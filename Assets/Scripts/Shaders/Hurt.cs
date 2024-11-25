using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    public static Hurt instance;
    [SerializeField] private Material m_HurtMaterial;
    public float effectDuration;
    [SerializeField] private AnimationCurve _hitCurve;
    private string _intensityName = "_Intensity";


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        m_HurtMaterial.SetFloat(_intensityName, 0f);
    }

    public void GetHurt()
    {
        //if(_hurtAmmount < 1f)
        //    _hurtAmmount += _partDamage;
        //    m_HurtMaterial.SetFloat(_intensityName, _hurtAmmount);
        StartCoroutine(LerpHurt());
    }

    IEnumerator LerpHurt()
    {
        var t = 0f;

        while (t <= 1f)
        {
            var life = _hitCurve.Evaluate(t);
            m_HurtMaterial.SetFloat(_intensityName, life);
            t += Time.deltaTime / effectDuration;
            yield return null;
        }
    }
}
