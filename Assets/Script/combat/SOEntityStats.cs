using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "ScriptableObjects/EntityStats")]
public class SOEntityStats : ScriptableObject
{
    // ================= PROPERTIES =================
    [SerializeField] private float baseHealth = 10;
    [SerializeField] private float baseSpeed = 10;
    [SerializeField] private float baseDefense = 10;
    [Header("Attack Configs")]
    [SerializeField] private float baseAttack = 10;
    [SerializeField] private float attackSpeed = 10;
    [SerializeField] private float attackForce = 10;

// ============ GETTERS & SETTERS =================
    public float BaseHealth => baseHealth;
    public float BaseSpeed => baseSpeed;
    public float BaseAttack => baseAttack;
    public float BaseDefense => baseDefense;
    public float AttackSpeed => attackSpeed;
    public float AttackForce => attackForce;

    // ==================== METHODS =================
    public float CalculateDamage(float rawDamage)
    {
        return Mathf.Max(0, rawDamage - baseDefense);
    }
}
