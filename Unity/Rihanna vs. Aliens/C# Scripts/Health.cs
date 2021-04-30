using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 200;
    [SerializeField] GameObject deathVFX = null;
    
    public void DealDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            TriggerDeathVFX();
            Destroy(gameObject);
        }
    }

    private void TriggerDeathVFX()
    {
        if (!deathVFX) { return; }
        GameObject deathVFXObject = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(deathVFXObject, 1f);
    }
}
