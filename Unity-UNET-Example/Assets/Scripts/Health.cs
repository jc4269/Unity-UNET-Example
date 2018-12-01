using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

    public int MaxHealth = 100;
    [SyncVar (hook = "OnChangeHealth")] public int CurrentHealth;

    public RectTransform HealthBar;
    public bool DestroyOnDeath;

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
            if (DestroyOnDeath) // for enemies or those that dont respawn
            {
                Destroy(gameObject);
            }
            else // for players or those that do respawn.
            {
                CurrentHealth = MaxHealth;

                RpcRespawn();
            }


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
