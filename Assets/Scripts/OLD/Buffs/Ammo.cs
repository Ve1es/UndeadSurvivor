using UnityEngine;

public class Ammo : Buff
{
    private float _ammo = 5;
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindEffect(other);
    }
    public override void FindEffect(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            //other.GetComponent<Health>().AddHP(_ammo);
            Runner.Despawn(Object);
        }
    }
    public override void Destroy() { }
}
