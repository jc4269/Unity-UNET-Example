using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        //Debug.Log("Hit");
        if (health != null)
        {
            health.TakeDamage(10);
        }
        Destroy(gameObject);
    }


}
