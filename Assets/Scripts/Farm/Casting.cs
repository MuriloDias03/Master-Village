using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casting : MonoBehaviour
{
    private bool detectingPlayer;
    private PlayerAnime playerAnime;
    private PlayerItens player;

    private int percentage = 50;    // porcentagem de chance de pescar um peixe a cada tentativa
    [SerializeField] private GameObject fishPrefab;

    void Start()
    {
        player = FindObjectOfType<PlayerItens>();
        playerAnime = player.GetComponent<PlayerAnime>();
    }

    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E))
        {
            playerAnime.OnCastingStarted();
        }
    }

    public void OnCasting()
    {
        int randomValue = Random.Range(1, 100);

        if(randomValue < percentage)
        {
            Instantiate(fishPrefab, player.transform.position + new Vector3(Random.Range(-2f, -1f), 0f, 0f), Quaternion.identity);
        }
        else
        {
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            detectingPlayer = false;
        }
    }
}
