using Fusion;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class DealDamage : NetworkBehaviour
{
    private float _damage = 5;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Health>().ReduceHP(_damage);
            Runner.Despawn(Object);
        }
        
    }
}
