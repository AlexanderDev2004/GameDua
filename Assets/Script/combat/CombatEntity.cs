using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public abstract class CombatEntity : MonoBehaviour
{
    [Header("General Config")]
    [SerializeField] protected Animator animator;
    [SerializeField] protected float attackForce;
    [Header("Attack Config")]
    private float attackSpeed; // attack per time
    private float nextAttackTime = 0; // next second of attack
    [SerializeField] protected Transform detectionSphere;
    [SerializeField] protected LayerMask targetLayerMask;
    [SerializeField] protected float detectionRadius;

    protected EntityStats entityStats;
    protected Rigidbody rb;
    protected Collider[] detectedColliders;

    protected virtual void Start()
    {
        if (detectionSphere == null)
        {
            Debug.LogError("DetectionSphere is not assigned on: " + gameObject.name);
        }

        entityStats = GetComponent<EntityStats>();
        rb = GetComponent<Rigidbody>();

        attackSpeed = entityStats.AttackSpeed;
    }

    protected virtual void Update()
    {

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
            Debug.Log($"{gameObject.name} is still in cooldown.");
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
        nextAttackTime = Time.time + 1f / attackSpeed;

    }

    public abstract void Die();

    protected virtual void OnDrawGizmosSelected()
    {
        if (detectionSphere == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(detectionSphere.position, detectionRadius);
    }
}