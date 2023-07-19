using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockHead : MonoBehaviour
{
    public float UpSpeed;
    public float DownSpeed;
    public Transform Up;
    public Transform Down;
    BoxCollider2D coll;
    bool Chop;
    Animator anim;

    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= Up.position.y)
        {
            Chop = true;
        }
        if(transform.position.y <= Down.position.y) 
        {
            Chop = false;        
        }
        if(Chop)
        {
            transform.position = Vector2.MoveTowards(transform.position, Down.position, DownSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Up.position, UpSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")||collision.CompareTag("Ground"))
        {
            anim.SetTrigger("run");
        }
    }

}
