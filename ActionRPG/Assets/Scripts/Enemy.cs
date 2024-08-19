using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public Player player;
    public float attackDistance;
    public int damage;
    public int health;
    private bool isAttacking;
    private bool isDead;
    public NavMeshAgent agent;
    public Animator animator;

    private const string RUN = "IsRunning", ATTACK = "Attack", DIE = "Die";
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead) return;
        if(Vector3.Distance(transform.position, player.transform.position) <= attackDistance){
            agent.isStopped = true;
            animator.SetBool(RUN, false);
            if(!isAttacking)
                Attack();
        }  
        else{
            agent.isStopped = false;
            agent.SetDestination(player.transform.position);
            animator.SetBool(RUN, true);
        }      
    }

    void Attack(){
        isAttacking = true;
        player.TakeDamage(damage);
        animator.SetTrigger(ATTACK);
        Invoke("TryDamage", 1.3f);
        Invoke("DisableIsAttacking", 2.66f);
    }

    void TryDamage(){
        if(Vector3.Distance(transform.position, player.transform.position) <= attackDistance){
            player.TakeDamage(damage);
        }
    }

    void DisableIsAttacking(){
        isAttacking = false;
    }

    public void TakeDamage(int damage){
        health -= damage;
        if(health <= 0){
            isDead = true;
            agent.isStopped = true;
            animator.SetTrigger(DIE);
            //Disable animations;
        }
    }
    

}
