using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private Transform attackPointSword;
    [SerializeField] private Transform attackPointAxe;
    [SerializeField] private float radiusSword;
    [SerializeField] private float radiusAxe;
    [SerializeField] private LayerMask enemyLayer;

    private Player player;
    private Animator anim;

    private Casting cast;

    private bool isHitting;
    private float recoveryTime = 1f;
    private float timeCount;

    private int axeDamage = 6;
    private int swordDamage = 4;

    void Start()
    {
        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
        cast = FindObjectOfType<Casting>();
    }

    void Update()
    {
        OnMove();
        OnRun();

        if (isHitting) 
        { 
            timeCount += Time.deltaTime;
            if(timeCount >= recoveryTime)
            {
                isHitting = false;
                timeCount = 0f;
            }
        }
    }

    #region Movement
    
    void OnMove()
    {
        if (player.direction.sqrMagnitude > 0)
        {
            if (player.isRolling)
            {
                if (!anim.GetCurrentAnimatorStateInfo(0).IsName("roll"))
                { 
                    anim.SetTrigger("isroll");
                }
            }
            else
            {
                anim.SetInteger("transition", 1);
            }

        }
        else
        {
            anim.SetInteger("transition", 0);
        }

        if (player.direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (player.direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }

        if (player.isCutting)
        {
            anim.SetInteger("transition", 3);
        }

        if (player.isDigging)
        {
            anim.SetInteger("transition", 4);
        }

        if (player.isWatering)
        {
            anim.SetInteger("transition", 5);
        }

        if (player.isAttack)
        {
            anim.SetInteger("transition", 6);
        }
    }

    public void OnHammeringStarted()
    {
        anim.SetBool("hammering", true);
    }

    public void OnHammeringEnded()
    {
        anim.SetBool("hammering", false);
    }

    void OnRun()
    {
        if (player.isRunning && player.direction.sqrMagnitude > 0)
        {
            anim.SetInteger("transition", 2);
        }
    }

    #endregion


    #region Attack

    public void OnAttackSword()
    {
        Collider2D hitSword = Physics2D.OverlapCircle(attackPointSword.position, radiusSword, enemyLayer);

        if (hitSword != null)
        {
            hitSword.GetComponentInChildren<AnimationControl>().OnHit(swordDamage);
        }
    }

    public void OnAttackAxe()
    {
        Collider2D hitAxe = Physics2D.OverlapCircle(attackPointAxe.position, radiusAxe, enemyLayer);

        if (hitAxe != null)
        {
            hitAxe.GetComponentInChildren<AnimationControl>().OnHit(axeDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPointAxe.position, radiusAxe);
        Gizmos.DrawWireSphere(attackPointSword.position, radiusSword);
    }

    #endregion

    // é chamado quando o jogador o botão de ação no lago
    public void OnCastingStarted()
    {
        anim.SetTrigger("iscasting");
        player.isPaused = true;
    }

    // quando termina de executar a animação de pescaria
    public void OnCastingEnded()
    {
        cast.OnCasting();
        player.isPaused = false;
    }

    public void OnHit()
    {
        if (!isHitting)
        {
            if(player.health <= 4)
            {
                player.isDeath = true;
                anim.SetTrigger("death");
                player.healthBar.fillAmount = 0f;

                Destroy(player.gameObject, 2.1f);
            }
            else
            {
                anim.SetTrigger("hit");
                isHitting = true;
                player.health -= 4;
                player.healthBar.fillAmount = player.health / player.totalHealth;
            }
        }
    }
}