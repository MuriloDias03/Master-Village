using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotFarm : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip holeSFX;
    [SerializeField] private AudioClip carrotSFX;

    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteRender;
    [SerializeField] private Sprite hole;
    [SerializeField] private Sprite carrot;

    [Header("Settings")]
    [SerializeField] int digAmount; // quantidade de "escavação"
    [SerializeField] private float waterAmount; // total de água para nascer uma cenoura 
    
    [SerializeField] private bool detecting;
    private bool isPlayer;  // fica verdadeiro quando o player está encostando
    
    private int initialDigAmount;
    private float currentWater;
    
    private bool dugHole;
    private bool plantedCarrot;

    PlayerItens playerItens;

    private void Start()
    {
        playerItens = FindObjectOfType<PlayerItens>();
        initialDigAmount = digAmount;
    }

    private void Update()
    {
        if (dugHole)
        {
            if (detecting)
            {
                currentWater += 0.01f;
            }

            if (currentWater >= waterAmount && !plantedCarrot)
            {
                audioSource.PlayOneShot(holeSFX);
                spriteRender.sprite = carrot;

                plantedCarrot = true;
            }

            if (Input.GetKeyDown(KeyCode.E) && plantedCarrot && isPlayer && playerItens.carrots < playerItens.carrotLimit)
            {
                audioSource.PlayOneShot(carrotSFX);
                spriteRender.sprite = hole;
                playerItens.carrots++;
                currentWater = 0f;
            }
        }
    }

    public void OnHit()
    {
        digAmount--;

        if (digAmount <= initialDigAmount / 2)
        {
            spriteRender.sprite = hole;
            dugHole = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Dig"))
        {
            OnHit();
        }

        if (collision.CompareTag("Water"))
        {
            detecting = true;
        }

        if (collision.CompareTag("Player"))
        {
            isPlayer = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Water"))
        {
            detecting = false;
        }

        if (collision.CompareTag("Player"))
        {
            isPlayer = false;
        }
    }
}