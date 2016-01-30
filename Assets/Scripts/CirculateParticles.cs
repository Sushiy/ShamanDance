using UnityEngine;
using System.Collections;

public class CirculateParticles : MonoBehaviour
{
    ParticleSystem particles;
	// Use this for initialization
	void Start ()
    {
        particles = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
            RingEffect();
	}

    public void RingEffect()
    {
        particles.Play();

        ParticleSystem.Particle[] p = new ParticleSystem.Particle[particles.particleCount];
        particles.GetParticles(p);
        for (int i = 0; i < particles.particleCount; ++i)
        {
            float a = Vector3.Angle(transform.right, p[i].velocity);
            a = p[i].velocity.y > 0 ? a : -a;
            p[i].rotation = a;
        }
        particles.SetParticles(p, particles.particleCount);
    }
}
