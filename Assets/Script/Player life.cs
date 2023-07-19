
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Playerlife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    Vector2 CheckpointPos;
    private SpriteRenderer spriteRenderer;
    audiomanager audiomanager;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audiomanager>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        CheckpointPos = transform.position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("trap") || collision.GetComponent<Enemy>())
        {
            audiomanager.PlaySFX(audiomanager.death);
            StartCoroutine(Respawn(1f));
        }
    }

    public void UpdateCheckpoint (Vector2 pos)
    {
        CheckpointPos = pos;
    }

    IEnumerator Respawn(float duration)
    {
        rb.simulated = false;
        rb.velocity = new Vector2(0, 0);
        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask; 
        yield return new WaitForSeconds(duration);
        spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
        transform.position = CheckpointPos;
        rb.simulated = true;
    }
}
