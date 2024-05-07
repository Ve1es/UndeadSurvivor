using UnityEngine;

public class Bomb : Buff
{
    [SerializeField] private float _explosion_radius = 5;
    private const string PLAYER_TAG = "Player";
    private const string ENEMY_TAG = "Enemy";
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindEffect(other);
    }
    public override void FindEffect(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, _explosion_radius);
            foreach (Collider2D enemyCollider in enemies)
            {
                if (enemyCollider.CompareTag(ENEMY_TAG))
                {
                    enemyCollider.gameObject.GetComponent<Health>().ReduceHP(enemyCollider.gameObject.GetComponent<Health>().GetHP());
                }
                
            }
            Runner.Despawn(Object);
        }
    }
    public override void Destroy() { }
}
