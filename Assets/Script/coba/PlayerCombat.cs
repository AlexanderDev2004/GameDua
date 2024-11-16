using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] private EntityStats playerStats;
    [SerializeField] float attackForce;

    Rigidbody rb;

    Collider[] enemyColliders;
    [SerializeField] Transform detectionSphere;
    [SerializeField] LayerMask targetLayerMask;
    [SerializeField] float detectionRadius;

    void Start()
    {
        if (detectionSphere == null)
        {
            Debug.LogError("DetectionSphere is not assigned on: " + gameObject.name);
        }
        playerStats = GetComponent<EntityStats>();
        // detectionSphere = GetComponentInChildren<Transform>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Debug.Log("Health of "+gameObject.name + " : "+ playerStats.CurrentHealth);
        if (playerStats.CurrentHealth <= 0)
        {
            Die();
            return;
        }
        if (detectionSphere == null)
        {
            Debug.LogError("DetectionSphere lost reference during runtime on: " + gameObject.name);
        }

        enemyColliders = Physics.OverlapSphere(detectionSphere.transform.position, detectionRadius, targetLayerMask);
        Debug.Log("Detected enemies: " + enemyColliders.Length);
        if (Input.GetAxisRaw("Fire1") > 0)
        {
            Debug.Log("Tombol Fire1 dipencet");
            Attack(enemyColliders);
        }
        // todo
        // retrieve inputs
        // cases of actions
        // basic attack, heavy attack, special attack

    }

    public void GetAttacked(Vector3 hitDirection, float force, float damage)
    {
        Debug.Log($"{gameObject.name} is attacked. Damage: {damage}, Force: {force}");

        rb.AddForce(hitDirection * force, ForceMode.Impulse);

        // animator.SetTrigger("Hurt");

        playerStats.TakeDamage(damage);
        
        
    }


    public void Attack(Collision collision, Vector3 hitDirection)
    {
        // animator.SetTrigger("Attack");
        // todo
        // create attack method
        // IMPORTANT =============> PLZ SEE SOEntityStats.cs FIRST

        collision.gameObject.GetComponent<PlayerCombat>().GetAttacked(hitDirection, attackForce, playerStats.CurrentAttack);
    }

    public void Attack(Collider[] enemyColliders)
    {
        // animator.SetTrigger("Attack");
        // todo
        // create attack method
        // IMPORTANT =============> PLZ SEE SOEntityStats.cs FIRST

        foreach (var enemyCollider in enemyColliders)
        {
            Vector3 hitDirection = (enemyCollider.transform.position - transform.position).normalized;
            enemyCollider.gameObject.GetComponent<PlayerCombat>().GetAttacked(hitDirection, attackForce, playerStats.CurrentAttack);
            
        }
    }

    public void Die()
    {
        // animator.SetTrigger("Die");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length); // Hancurkan setelah animasi selesai
    }

    protected void OnDrawGizmosSelected()
    {
        if (detectionSphere == null)
            return;
        Gizmos.DrawWireSphere(detectionSphere.position, detectionRadius);
    }

}
