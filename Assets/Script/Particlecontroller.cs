using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Particlecontroller : MonoBehaviour
{
    [SerializeField] ParticleSystem MovementParticle;

    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;

    [SerializeField] ParticleSystem FallParticle;

    [SerializeField] Rigidbody2D rb;

    float counter;

    bool IsOnGround;

    private void Update()
    {
        counter += Time.deltaTime;
        if( IsOnGround && Mathf.Abs(rb.velocity.x)>occurAfterVelocity )
        {
            if(counter > dustFormationPeriod)
            {
                MovementParticle.Play();
                counter = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            FallParticle.Play();
            IsOnGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            IsOnGround = false;
        }
    }

}
