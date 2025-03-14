using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Skeleton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationControl animControl;

    private Player player;
    private bool detectPlayer;

    [Header("Stats")]
    public float radius;
    public LayerMask layer;
    public float health;
    public Image healthBar;
    public float totalHealth;
    public bool isDeath;

    void Start()
    {
        health = totalHealth;
        player = FindObjectOfType<Player>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Update()
    {
        if (!isDeath && detectPlayer)
        {
            if (!player.isDeath)
            {
                agent.isStopped = false;
                agent.SetDestination(player.transform.position);

                if(Vector2.Distance(transform.position, player.transform.position) <= agent.stoppingDistance)
                {
                    animControl.PlayAnim(2);
                }
                else
                {
                    animControl.PlayAnim(1);
                }

                float posX = player.transform.position.x - transform.position.x;

                if(posX > 0)
                {
                    transform.eulerAngles = new Vector2(0, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector2(0, 180);
                }
            }
        }

    }

    private void FixedUpdate()
    {
        DetectPlayer();
    }

    public void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, radius, layer);

        if(hit != null)
        {
            detectPlayer = true;
        }
        else
        {
            detectPlayer = false;
            animControl.PlayAnim(0);
            agent.isStopped = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}