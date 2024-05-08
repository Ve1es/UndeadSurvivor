using UnityEngine;

public class MedChest : Buff
{
    [SerializeField] private float _healing;
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindEffect(other);
    }
    public override void FindEffect(Collider2D other) 
    {
        if (other.TryGetComponent(out CharacterPlayerController player))
        {
            player.GetComponent<Health>().AddHP(_healing);
            Runner.Despawn(Object);
        }
    }
    public override void Destroy() { }
}
