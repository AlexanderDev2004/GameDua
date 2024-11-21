using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatEntity : MonoBehaviour
{
    [Header("General Config")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected float attackForce;
    [Header("Attack Config")]
    
    private float nextAttackTime; // next second of attack
    [SerializeField] protected Transform detectionSphere;
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField] protected float detectionRadius;

    [SerializeField] protected EntityStats entityStats;
    protected Rigidbody rb;
    protected Collider[] detectedColliders;

    protected virtual void Start()
    {
        if (entityStats == null)
        {
            Debug.LogError($"EntityStats is not assigned on: {gameObject.name}");
        }
        if (entityStats.CurrentAttack <= 0)
        {
            Debug.LogError($"EntityStats.CurrentAttack is not valid on: {gameObject.name}");
        }
        if (detectionSphere == null)
        {
            Debug.LogError("DetectionSphere is not assigned on: " + gameObject.name);
        }

        rb = GetComponent<Rigidbody>();

        
        if (entityStats.AttackSpeed <= 0)
        {
            Debug.LogError($"{gameObject.name} has invalid entityStats.AttackSpeed: {entityStats.AttackSpeed}. Setting default to 1.");
            entityStats.AttackSpeed = 1f;
        }
        nextAttackTime = 0;
    }

    protected virtual void Update()
    {
        // Debug.Log($"{gameObject.name} Cooldown: {nextAttackTime - Time.time}");
        Debug.Log($"{gameObject.name} Health: {entityStats.CurrentHealth}");


        if (entityStats.CurrentHealth <= 0)
        {
            Debug.Log($"{gameObject.name} died.");
            Die();
            return;
        }

        detectedColliders = Physics.OverlapSphere(detectionSphere.position, detectionRadius, targetLayerMask);

        Debug.Log($"Detected enemies by {gameObject.name}: {detectedColliders.Length}");
    }
    public virtual void GetAttacked(Vector3 hitDirection, float force, float damage)
    {
        Debug.Log($"{gameObject.name} is attacked. Damage: {damage}, Force: {force}");

        rb.AddForce(hitDirection * force, ForceMode.Impulse);
        entityStats.TakeDamage(damage);
    }

    public virtual void Attack(Collider[] targets)
    {
        // Pastikan cooldown selesai
        if (Time.time < nextAttackTime)
        {
            // Debug.Log($"{gameObject.name} is still in cooldown.");
            return;
        }
        
        foreach (var target in targets)
        {
            Vector3 hitDirection = (target.transform.position - transform.position).normalized;
            CombatEntity targetCombat = target.GetComponent<CombatEntity>();

            if (targetCombat != null)
            {
                targetCombat.GetAttacked(hitDirection, attackForce, entityStats.CurrentAttack);
            }
        }
        
        // calculating next second of attack
        // Debug.Log($"{nextAttackTime} is updated.");
        nextAttackTime = Time.time + 1f / entityStats.AttackSpeed;

    }

    public abstract void Die();

    protected virtual void OnDrawGizmosSelected()
    {
        if (detectionSphere == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionSphere.position, detectionRadius);
    }

    public abstract void AttackHeavy(Collider[] targets);
}