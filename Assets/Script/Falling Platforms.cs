using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 InitialPosition;
    bool PlatformMovingBack;
    [SerializeField] private float PlatformBack;
    audiomanager audiomanager;
    // Start is called before the first frame update

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audiomanager>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        InitialPosition = transform.position;

    }

    private void Update()
    {
        if(PlatformMovingBack)
        {
            transform.position = Vector2.MoveTowards(transform.position, InitialPosition, 20f * Time.deltaTime);
        }
        if(transform.position.y == InitialPosition.y)
        {
            PlatformMovingBack = false;
        }
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && !PlatformMovingBack)
        {
            audiomanager.PlaySFX(audiomanager.fallplaform);
            Invoke("DropPlastform", 0.5f);
            //Destroy(gameObject, 1f);  
        }
    }
    void DropPlastform()
    {
        rb.isKinematic = false;
        Invoke("GetPlatformBack", PlatformBack);
    }

    void GetPlatformBack()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        PlatformMovingBack = true;
    }

}
