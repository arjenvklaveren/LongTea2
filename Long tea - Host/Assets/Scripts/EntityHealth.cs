using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

public class EntityHealth : NetworkBehaviour
{
    [SyncVar(hook = "OnHealthChanged")] public int health = 100;
    [SyncVar] public bool canTakeDamage = true;

    [Header("Hit and Death events")]
    [SerializeField] private UnityEvent OnHitLocal;
    [SerializeField] private UnityEvent OnHitOther;
    [SerializeField] private UnityEvent OnDeathLocal;
    [SerializeField] private UnityEvent OnDeathOther;

    [SyncVar] public bool isDead = false;


    [ClientRpc]
    public virtual void RPCSetHealth()
    {
        //OnHealthChanged();
    }

    public void DealDamage(int damageAmount)
    {
        if (!isServer || isDead || !canTakeDamage)
        {
            return;
        }

        health -= damageAmount;

        if(health <= 0)
        {
            isDead = true;
        }

        if (isServerOnly)
        {
            UpdateHitsServer();
        }

        RPCSetHealth();        
    }

    public void OnHealthChanged(int oldHealth, int newHealth)
    {
        Debug.Log($"Health is {health}");
        if (health <= 0)
        {            
            if (isLocalPlayer)
            {
                OnDeathLocal.Invoke();
            }
            else
            {
                OnDeathOther.Invoke();
            }
        }
        else
        {
            if (isLocalPlayer)
            {
                OnHitLocal.Invoke();
            }
            else
            {
                OnHitOther.Invoke();
            }
        }
    }

    public void UpdateHitsServer()
    {
        if (health <= 0)
        {
            OnDeathOther.Invoke();
        }
        else
        {
            OnHitOther.Invoke();
        }
    }
}
