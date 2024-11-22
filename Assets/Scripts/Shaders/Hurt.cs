using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour
{
    public static Hurt instance;
    [SerializeField] private Material m_HurtMaterial;
    [SerializeField] private float _partDamage;
    private float _hurtFadeTime;
    private float _hurtAmmount;
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
        if(_hurtAmmount < 1f)
            _hurtAmmount += _partDamage;
            m_HurtMaterial.SetFloat(_intensityName, _hurtAmmount);
    }

    public void Heal()
    {
        StartCoroutine(LerpHeal());
    }

    IEnumerator LerpHeal()
    {
        Debug.Log("Start Heal");
        var startValue = m_HurtMaterial.GetFloat(_intensityName);

        var endValue = 0f;

        var t = 0f;

        while (t <= 1f)
        {
            var life = Mathf.Lerp(startValue, endValue, t);
            m_HurtMaterial.SetFloat(_intensityName, life);
            t += Time.deltaTime / 2f;
            yield return null;
        }
        _hurtAmmount = 0f;
    }
}
