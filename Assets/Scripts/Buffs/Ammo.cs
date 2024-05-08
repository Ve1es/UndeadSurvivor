using UnityEngine;

public class Ammo : Buff
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindEffect(other);
    }
    public override void FindEffect(Collider2D other) 
    {
        if (other.TryGetComponent(out CharacterPlayerController player))
        {
            player.GetComponent<WeaponController>().AddAmmo();
            Runner.Despawn(Object);
        }
    }
    public override void Destroy() { }
}
