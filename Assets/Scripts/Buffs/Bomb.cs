using UnityEngine;

public class Bomb : Buff
{
    private const string Player_Tag = "Player";
    private const string Enemy_Tag = "Enemy";
    [SerializeField] private float _explosion_radius = 5;
    [SerializeField] private KillsList _killsList;
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindEffect(other);
    }
    public override void FindEffect(Collider2D other)
    {
        if (other.CompareTag(Player_Tag))
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _explosion_radius);
            foreach (Collider2D enemyCollider in enemies)
            {
                if (enemyCollider.CompareTag(Enemy_Tag))
                {
                    enemyCollider.gameObject.GetComponent<Health>().ReduceHP(enemyCollider.gameObject.GetComponent<Health>().GetHP());
                    _killsList.AddString(other.GetComponent<CharacterPlayerController>()._characterInput);
                }
                
            }
            Runner.Despawn(Object);
        }
    }
    public override void Destroy() { }
}
