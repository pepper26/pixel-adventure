
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int Cherries = 0;
    [SerializeField] private Text FruitsText;
    audiomanager audiomanager;

    private void Awake()
    {
        audiomanager = GameObject.FindGameObjectWithTag("Audio").GetComponent<audiomanager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Fruits")){
            audiomanager.PlaySFX(audiomanager.Collect);
            Destroy(collision.gameObject);
            Cherries++;
            FruitsText.text = "Fruits: " + Cherries;
        }
    }
}
