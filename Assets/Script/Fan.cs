using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    audiomanager audiomanager;

    private void Awake()
    {
        audiomanager=GameObject.FindGameObjectWithTag("Audio").GetComponent<audiomanager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audiomanager.PlaySFX(audiomanager.fallplaform);
        }
    }

}
