using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyCombat : CombatEntity
{
    public override void Die()
    {
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject); // Enemy dihancurkan setelah mati
    }

    // Tambahkan logika AI untuk enemy
    protected override void Update()
    {
        base.Update();

        // Logika AI untuk menyerang player
        if (detectedColliders.Length > 0)
        {
            Debug.Log($"{gameObject.name} attacking player");
            Attack(detectedColliders);
        }
    }
}
