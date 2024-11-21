using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : CombatEntity
{
    private float nextAttackHeavyTime;
    protected override void Update()
    {
        base.Update();

        if (Input.GetAxisRaw("Fire1") > 0)
        {
            Debug.Log("Player attacking");
            Attack(detectedColliders);
        }
        else if (Input.GetAxisRaw("Fire2") > 0)
        {
            Debug.Log("Player attacking heavy");
            AttackHeavy(detectedColliders);
        }
    }

    public override void Die()
    {
        Debug.Log("Player has died!");
        gameObject.SetActive(false); // Atau animasi kematian
    }

    public override void AttackHeavy(Collider[] targets)
    {
        // Pastikan cooldown selesai
        if (Time.time < nextAttackHeavyTime)
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
                targetCombat.GetAttacked(hitDirection, attackForce, entityStats.CurrentAttack * 2);
            }
        }

        // calculating next second of attack
        // Debug.Log($"{nextAttackTime} is updated.");
        nextAttackHeavyTime = Time.time + 1f / (entityStats.AttackSpeed / 2);

    }
}