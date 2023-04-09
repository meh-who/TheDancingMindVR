using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyParticleCollision : MonoBehaviour
{
    ParticleSystem particleSystem;
    List<ParticleCollisionEvent> particleCollisionEvents = new List<ParticleCollisionEvent>();

    public GameObject instantiateOnParticleCollision;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, particleCollisionEvents);
        for (int i = 0; i < numCollisionEvents; i++)
        {
            Instantiate(instantiateOnParticleCollision, particleCollisionEvents[i].intersection, Quaternion.identity);
        }

    }
}
