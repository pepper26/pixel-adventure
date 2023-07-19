using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private Animator anim;
    public bool Touch = false;
    Playerlife playerlife;
    private BoxCollider2D coll;
    public AudioSource audioSource;
    public Transform RespawnPoint;
    audiomanager audiomanager;
    private void Awake()
    {
        playerlife = GameObject.FindGameObjectWithTag("Player").GetComponent<Playerlife>();
        audiomanager=GameObject.FindGameObjectWithTag("Audio").GetComponent<audiomanager>();
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag ("Player"))
        {
            playerlife.UpdateCheckpoint(RespawnPoint.position);
            Touch = true;
            anim.SetTrigger("touch");
            anim.SetBool("Touch", true);
            audiomanager.PlaySFX(audiomanager.CheckPoint);
            coll.enabled = false;

        }
    }
}
