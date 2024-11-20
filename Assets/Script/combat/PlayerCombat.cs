using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : CombatEntity
{
    protected override void Update()
    {
        base.Update();

        if (Input.GetAxisRaw("Fire1") > 0)
        {
            Debug.Log("Player attacking");
            Attack(detectedColliders);
        }
    }

    public override void Die()
    {
        Debug.Log("Player has died!");
        gameObject.SetActive(false); // Atau animasi kematian
    }
}