using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
using UnityEngine.UI;
using DG.Tweening;

public class EntityHealth : NetworkBehaviour
{
    [SyncVar(hook = "OnHealthChanged")] public int health = 100;
    [SyncVar] public bool canTakeDamage = true;
    [SerializeField] private float healthbarMoveSpeed = 0.5f;
    [SerializeField] private GameObject killedByLabel;

    [Header("Hit and Death events")]
    [SerializeField] private UnityEvent OnHitLocal;
    [SerializeField] private UnityEvent OnHitOther;
    [SerializeField] private UnityEvent OnDeathLocal;
    [SerializeField] private UnityEvent OnDeathOther;

    [SyncVar] public bool isDead = false;

    private Image healthBar;
    private int startValue;
    private RoomPlayerUI lastHitBy;
    private RoomPlayerUI ownRoomPlayerUI;

    private void Start()
    {
        if (isLocalPlayer || isServer)
        {
            startValue = health;

            RoomPlayerUI[] playerUI = GameObject.FindObjectsOfType<RoomPlayerUI>();
            if (hasAuthority)
            {
                foreach (RoomPlayerUI playerUIInstance in playerUI)
                {
                    if (playerUIInstance.hasAuthority)
                    {
                        ownRoomPlayerUI = playerUIInstance;
                        break;
                    }
                }
            }

            if (!killedByLabel) killedByLabel = GameObject.FindGameObjectWithTag("KilledLabel");
        }
    }


    [ClientRpc]
    public virtual void RPCSetHealth()
    {
        //OnHealthChanged();
    }

    public void CoupleHealthbar(Image healthBarReference)
    {
        healthBar = healthBarReference;
    }

    public void SetLastHitBy(RoomPlayerUI lastHit)
    {
        lastHitBy = lastHit;
    }

    public void UpdateHealthBar()
    {
        if (isLocalPlayer && healthBar)
        {
            healthBar.DOComplete();
            healthBar.DOFillAmount((float)health / (float)startValue, healthbarMoveSpeed);
        }
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

    [TargetRpc]
    public void TargetShowKilled(NetworkConnection target, string ownPlayerName)
    {
        killedByLabel.GetComponent<FadeUI>().FadeIn(1f);
        killedByLabel.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = $"You have killed '" + ownPlayerName + "'";
        if(killedByLabel.TryGetComponent(out EventAfterTime eventAfterTime))
        {
            eventAfterTime.StartTimedEvent();
        }
    }

    [Command]
    public void ShowOpponentKill()
    {
        NetworkIdentity opponentIdentity = lastHitBy.netIdentity;
        TargetShowKilled(opponentIdentity.connectionToClient, ownRoomPlayerUI.playerName);
    }

    public void OnHealthChanged(int oldHealth, int newHealth)
    {
        if (health <= 0)
        {            
            if (isLocalPlayer)
            {
                OnDeathLocal.Invoke();
                UpdateHealthBar();
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
                UpdateHealthBar();
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
