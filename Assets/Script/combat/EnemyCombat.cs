using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public abstract class EnemyCombat : CombatEntity
{
    protected enum EnemyPhase {Phase1, Phase2, Phase3}
    protected EnemyPhase currentPhase;

    [Header("Enemy Config")]
    [SerializeField] protected Transform playerTransform;
    [SerializeField] protected float attackRange;

    [SerializeField] protected float phase2Threshold = 0.6f;
    [SerializeField] protected float phase3Threshold = 0.3f;

    protected override void Start()
    {
        base.Start();
        currentPhase = EnemyPhase.Phase1;
    }

    private void CheckPhaseChange()
    {
        float healthPercentage = entityStats.CurrentHealth / entityStats.BaseHealth;
        if (healthPercentage <= phase3Threshold && currentPhase != EnemyPhase.Phase3)
        {
            currentPhase = EnemyPhase.Phase3;
            OnPhaseChange(EnemyPhase.Phase3);
        } else if (healthPercentage <= phase2Threshold && currentPhase != EnemyPhase.Phase2)
        {
            currentPhase = EnemyPhase.Phase2;
            OnPhaseChange(EnemyPhase.Phase2);
        }
    }

    protected virtual void OnPhaseChange(EnemyPhase newPhase)
    {
        Debug.Log($"{gameObject.name} hase entered {newPhase}");
        // implement efect that happens on phase change here
    }

    public override void Die()
    {
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject); // Enemy dihancurkan setelah mati
    }

    // Tambahkan logika AI untuk enemy
    protected virtual void Update()
    {
        base.Update();
        CheckPhaseChange();

        // Pengejaran
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * entityStats.Speed;
        }

        switch (currentPhase)
        {
            case EnemyPhase.Phase1:
                ExecutePhase1Behaviour();
                break;
            case EnemyPhase.Phase2:
                ExecutePhase2Behaviour();
                break;
            case EnemyPhase.Phase3:
                ExecutePhase3Behaviour();
                break;
        }

    }


    protected abstract void ExecutePhase1Behaviour();
    protected abstract void ExecutePhase2Behaviour();
    protected abstract void ExecutePhase3Behaviour();
}
