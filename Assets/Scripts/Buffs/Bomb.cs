using UnityEngine;

public class Bomb : Buff
{
    [SerializeField] private float _explosion_radius;
    [SerializeField] private KillsList _killsList;
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindEffect(other);
    }
    public override void FindEffect(Collider2D other)
    {
        if(other.TryGetComponent(out CharacterPlayerController player))
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _explosion_radius);
            foreach (Collider2D enemyCollider in enemies)
            {
                if(enemyCollider.TryGetComponent(out Enemy enemy))
                {
                    enemyCollider.gameObject.GetComponent<Health>().ReduceHP(enemyCollider.gameObject.GetComponent<Health>().GetHP());
                    _killsList.AddString(other.GetComponent<CharacterPlayerController>().CharacterInput);
                }
                
            }
            Runner.Despawn(Object);
        }
    }
    public override void Destroy() { }
}
