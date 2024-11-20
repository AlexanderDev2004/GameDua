using UnityEngine;

public class SlasherEnemy : EnemyCombat
{
    [Header("Slasher Enemy Settings")]
    [SerializeField] private float attackPhase2Multiplier = 2f;
    [SerializeField] private float attackPhase3Multiplier = 3f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();

        // Fase logika berdasarkan kesehatan
        if (entityStats.CurrentHealth > 50)
        {
            ExecutePhase1Behaviour();
        }
        else if (entityStats.CurrentHealth > 20)
        {
            ExecutePhase2Behaviour();
        }
        else
        {
            ExecutePhase3Behaviour();
        }
    }

    protected override void ExecutePhase1Behaviour()
    {
        Debug.Log($"{gameObject.name} is in Phase 1: Basic slashes.");
        if (detectedColliders.Length > 0)
        {
            Attack(detectedColliders); // Gunakan logika dari CombatEntity
        }
    }

    protected override void ExecutePhase2Behaviour()
    {
        Debug.Log($"{gameObject.name} is in Phase 2: Faster slashes.");
        entityStats.ModifySpeed(0.5f); // Perbarui stat langsung
        if (detectedColliders.Length > 0)
        {
            Attack(detectedColliders);
        }
    }

    protected override void ExecutePhase3Behaviour()
    {
        Debug.Log($"{gameObject.name} is in Phase 3: Aggressive attack pattern.");
        entityStats.ModifySpeed(1f); 
        attackRange += 1f; 
        if (detectedColliders.Length > 0)
        {
            Attack(detectedColliders);
        }
    }

    protected override void OnPhaseChange(EnemyPhase newPhase)
    {
        base.OnPhaseChange(newPhase);
        if (newPhase == EnemyPhase.Phase2)
        {
            Debug.Log($"{gameObject.name} glows red!");
        }
        else if (newPhase == EnemyPhase.Phase3)
        {
            Debug.Log($"{gameObject.name} enters berserk mode!");
        }
    }


    public override void Die()
    {
        Debug.Log($"{gameObject.name} has been defeated.");
        Destroy(gameObject);
    }
}
