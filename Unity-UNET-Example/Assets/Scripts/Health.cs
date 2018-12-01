using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public int MaxHealth = 100;
    [SyncVar (hook = "OnChangeHealth")] public int CurrentHealth;

    public RectTransform HealthBar;


    public void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            return;
        }

        CurrentHealth -= amount;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = MaxHealth;
            
            RpcRespawn();
        }

        HealthBar.sizeDelta = new Vector2(CurrentHealth, HealthBar.sizeDelta.y);
    }

    void OnChangeHealth(int health)
    {
        HealthBar.sizeDelta = new Vector2(health, HealthBar.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            transform.position = Vector3.zero;
        }
    }
}
