using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float Range;
    [SerializeField] private float CheckDelay;
    [SerializeField] private LayerMask PlayerLayer;
    private float CheckTimer;
    private Vector3 Destination;

    private bool Attacking;

    private Vector3[] Directions = new Vector3[4];

    private Animator Anim;
    private void OnEnable()
    {
        Stop();
    }

    private void Start()
    {
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(Attacking)
        {
            transform.Translate(Destination * Time.deltaTime * Speed);
        }
        else
        {
            CheckTimer += Time.deltaTime;
            if(CheckTimer> CheckDelay )
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirection();

        for(int i = 0;i< Directions.Length; i++)
        {
            Debug.DrawRay(transform.position, Directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Directions[i], Range, PlayerLayer);

            if(hit.collider !=null&& !Attacking)
            {
                Attacking = true;
                Destination= Directions[i];
                CheckTimer = 0;
            }
        }
    }

    private void CalculateDirection()
    {    
        Directions[0] = transform.right * Range;
        Directions[1] = -transform.right * Range;
        Directions[2] = transform.up * Range;
        Directions[3] = -transform.up * Range;
    }

    private void Stop()
    {
        Destination = transform.position;
        Attacking= false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            Stop();
        }
    }

}
