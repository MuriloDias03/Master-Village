﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] private float treeHelth;
    [SerializeField] private Animator anim;

    [SerializeField] private GameObject woodPrefab;
    [SerializeField] private int totalWood;
    [SerializeField] private ParticleSystem leafs;

    private bool isCut;

    public void OnHit()
    {
        treeHelth--;
        anim.SetTrigger("IsHit");

        leafs.Play();

        if(treeHelth <= 0)
        {
            for (int i = 0; i < totalWood; i++)
            {
                Instantiate(woodPrefab, transform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f), transform.rotation);

            }
            anim.SetTrigger("Cut");

            isCut = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Axe") && !isCut)
        {
            OnHit();
        }
    }
}
