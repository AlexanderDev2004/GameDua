using UnityEngine;

public class SlasherEnemy : EnemyCombat
{
    [Header("Slasher Enemy Settings")]
    [SerializeField] private float slashDamagePhase1 = 10f;
    [SerializeField] private float slashDamagePhase2 = 20f;
    [SerializeField] private float slashDamagePhase3 = 30f;
    private float chaseSpeed;

    protected override void Start()
    {
        base.Start();
        chaseSpeed = entityStats.Speed;
    }

    protected override void ExecutePhase1Behaviour()
    {
        Debug.Log($"{gameObject.name} is in Phase 1: Basic slashes.");
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            PerformSlashAttack(slashDamagePhase1);
        }
    }

    protected override void ExecutePhase2Behaviour()
    {
        Debug.Log($"{gameObject.name} is in Phase 2: Faster slashes.");
        chaseSpeed += 0.5f; // Meningkatkan kecepatan di fase ini
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            PerformSlashAttack(slashDamagePhase2);
        }
    }

    protected override void ExecutePhase3Behaviour()
    {
        Debug.Log($"{gameObject.name} is in Phase 3: Aggressive attack pattern.");
        chaseSpeed += 1f; // Musuh jadi lebih cepat
        attackRange += 1f; // Jarak serangan bertambah
        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            PerformSlashAttack(slashDamagePhase3);
        }
    }

    private void PerformSlashAttack(float damage)
    {
        Debug.Log($"{gameObject.name} performs a slash attack dealing {damage} damage!");
        Vector3 hitDirection = (playerTransform.position - transform.position).normalized;
        playerTransform.GetComponent<PlayerCombat>().GetAttacked(hitDirection, attackForce, damage);
    }

    protected override void OnPhaseChange(EnemyPhase newPhase)
    {
        base.OnPhaseChange(newPhase);
        if (newPhase == EnemyPhase.Phase2)
        {
            // Tambahkan efek visual seperti aura atau animasi khusus
            Debug.Log($"{gameObject.name} glows red as it becomes stronger!");
        }
        else if (newPhase == EnemyPhase.Phase3)
        {
            Debug.Log($"{gameObject.name} enters berserk mode!");
        }
    }
}