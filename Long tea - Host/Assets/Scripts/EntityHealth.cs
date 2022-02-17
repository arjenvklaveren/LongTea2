using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class EntityHealth : NetworkBehaviour
{
    [SerializeField, SyncVar] public float health = 100f;
    [SerializeField] private UnityEvent OnHit;
    [SerializeField] private UnityEvent OnDeath;
	[SerializeField, SyncVar] private bool canTakeDamage = true;

    [SyncVar] protected bool isDead = false;
    protected float startHealth = 100f;

    public override void OnStartClient()
    {
        startHealth = health;
    }

    [Command(requiresAuthority = false)]
    public virtual void DealDamage(float damageAmount)
    {
        //Skip is entity is already dead
        if (isDead || !canTakeDamage) return;

        health -= damageAmount;
        UpdateHits();
    }

    [ClientRpc]
    public void UpdateHits()
    {
        if (health <= 0)
        {
            OnDeath.Invoke();
            isDead = true;
        }
        else
        {
            OnHit.Invoke();
        }
    }
}
