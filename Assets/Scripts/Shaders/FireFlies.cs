using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFlies : MonoBehaviour
{
    private ParticleSystem _particleSystem = new ParticleSystem();
    List<ParticleCollisionEvent> particleCollisionEvents = new List<ParticleCollisionEvent>();
    public AnimationCurve curve;

    public GameObject instantiateOnParticleCollision;

    void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = _particleSystem.GetCollisionEvents(other, particleCollisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            Hurt.instance.GetHurt(curve);
            Instantiate(instantiateOnParticleCollision, particleCollisionEvents[i].intersection, Quaternion.identity);
        }
    }
}

