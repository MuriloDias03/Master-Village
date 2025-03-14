using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    private PlayerItens player;

    private void Start()
    {
        player = FindObjectOfType<PlayerItens>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && player.fishes < player.fishesLimit)
        {
            collision.GetComponent<PlayerItens>().fishes++;
            Destroy(gameObject);
        }
    }
}
