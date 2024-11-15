using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityStats", menuName = "ScriptableObjects/EntityStats")]
public class SOEntityStats : ScriptableObject
{
    // ================= PROPERTIES =================
    [SerializeField] private float health = 0;
    [SerializeField] private float speed = 0;
    [SerializeField] private float attack = 0;
    [SerializeField] private float defense = 0;

// ============ GETTERS & SETTERS =================
    public float Health
    {
        get { return health; }
        set { health = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float Attack
    {
        get { return attack; }
        set { attack = value; }
    }

    public float Defense
    {
        get { return defense; }
        set { defense = value; }
    }

    // ==================== METHODS =================
    public void TakeDamage(float damageTaken)
    {
        health = health-=damageTaken;
    }

    public void HealUp(float healAmount)
    {
        health = health+=healAmount;
    }
}
