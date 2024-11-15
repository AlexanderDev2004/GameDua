using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private SOEntityStats playerStats;

    void Start()
    {
        playerStats = GetComponent<SOEntityStats>();
    }

    void Update()
    {
        // todo
        // retrieve inputs
        // cases of actions
        // basic attack, heavy attack, special attack
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
        // todo
        // create attack method
        // IMPORTANT =============> PLZ SEE SOEntityStats.cs FIRST

    }

}
