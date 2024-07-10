using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask playerLayer;

    [SerializeField] private ParticleSystem criticP;
    
    private PlayerAnime player;
    private Animator anim;
    private Skeleton skeleton;
    private Player jogador;

    private void Start()
    {
        anim = GetComponent<Animator>();
        jogador = FindObjectOfType<Player>();
        player = FindObjectOfType<PlayerAnime>();
        skeleton = GetComponentInParent<Skeleton>();
    }

    public void PlayAnim(int value)
    {
        anim.SetInteger("transition", value);
    }

    public void Attack()
    {
            if (!skeleton.isDeath && !jogador.isDeath)
            {
                Collider2D hit = Physics2D.OverlapCircle(attackPoint.position, radius, playerLayer);

                if(hit != null)
                {
                    player.OnHit();
                }
            }
    }

    private int axeCritic = 10;
    private int swordCritc = 20;
    private int critic;

    public void OnHit(int damage)
    {
        if(skeleton.health <= 4)
        {
            skeleton.isDeath = true;
            anim.SetTrigger("death");
            skeleton.healthBar.fillAmount = 0f;

            Destroy(skeleton.gameObject, 1.5f);
        }
        else
        {
            critic = Random.Range(0, 100);
            anim.SetTrigger("hit");

            if(damage == 4)
            {
                if(critic <= 20)
                {
                    criticP.Play();
                    skeleton.health -= damage * 2;
                }
                else
                {
                    skeleton.health -= damage;
                }
            }
            else
            {
                if(critic <= 10)
                {
                    criticP.Play();
                    skeleton.health -= damage * 2;
                }
                else
                {
                    skeleton.health -= damage;
                }
            }

            skeleton.healthBar.fillAmount = skeleton.health / skeleton.totalHealth;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, radius);
    }
}