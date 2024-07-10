using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    [Header("Amounts")]

    private int woodAmount = 6;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;
    [SerializeField] private float timeAmount;
    

    [Header("Components")]

    [SerializeField] private GameObject houseColl;
    [SerializeField] private SpriteRenderer houseSprite;
    [SerializeField] private Transform point;
    
    private bool detectingPlayer;
    private Player player;
    private PlayerAnime playerAnim;
    private PlayerItens playerItens;
    
    private float timeCount;
    private bool isBeging;

    void Start()
    {
        player = FindObjectOfType<Player>();
        playerAnim = player.GetComponent<PlayerAnime>();
        playerItens = player.GetComponent<PlayerItens>();
    }

    void Update()
    {
        if (detectingPlayer && Input.GetKeyDown(KeyCode.E) && playerItens.TotalWood >= woodAmount)
        {
            // construção da casa iniciada
            isBeging = true;
            playerAnim.OnHammeringStarted();
            houseSprite.color = startColor;
            player.transform.position = point.position;
            player.isPaused = true;
            player.transform.eulerAngles = new Vector2(0, 0);
            playerItens.TotalWood -= woodAmount;
        }

        if (isBeging)
        {
            timeCount += Time.deltaTime;

            if(timeCount >= timeAmount)
            {
                playerAnim.OnHammeringEnded();
                houseSprite.color = endColor;
                player.isPaused = false;
                houseColl.SetActive(true);
            }
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