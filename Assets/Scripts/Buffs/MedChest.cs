using UnityEngine;

public class MedChest : Buff
{
    [SerializeField]
    private float _healing = 5;
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindEffect(other);
    }
    public override void FindEffect(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Health>().AddHP(_healing);
            Runner.Despawn(Object);
        }
    }
    public override void Destroy() { }
}
