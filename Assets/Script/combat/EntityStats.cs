using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EntityStats : MonoBehaviour
{
    [SerializeField] private SOEntityStats statsTemplate;
    public float CurrentHealth{get;private set;}
    public float BaseHealth{get; private set;}
    public float Speed{get; private set;}
    public float CurrentAttack{get; private set;}
    public float AttackSpeed{get; private set;}
    public float AttackForce{get; private set;}

    private void Start()
    {
        // value that can change at runtime should be initialized in this class
        CurrentHealth = statsTemplate.BaseHealth;
        BaseHealth = statsTemplate.BaseHealth;
        Speed = statsTemplate.BaseSpeed;

        CurrentAttack = statsTemplate.BaseAttack;
        AttackSpeed = statsTemplate.AttackSpeed;
        AttackForce = statsTemplate.AttackForce;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Taking damage: " + damage + ", Current health: " + CurrentHealth);

        float finalDamage = statsTemplate.CalculateDamage(damage);
        CurrentHealth -= finalDamage;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);
        Debug.Log($"Took {finalDamage} damage. Current Health: {CurrentHealth}");
    }

    public void Heal(float amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Min(CurrentHealth, statsTemplate.BaseHealth);
        Debug.Log($"Healed {amount}. Current Health: {CurrentHealth}");
    }

    public void ModifyAttack(float amount) // for buff or debuff | put negative amount for debuff
    {
        CurrentAttack += amount;
        Debug.Log($"Modified Attack by {amount}. Current Attack: {CurrentAttack}");
    }
}
