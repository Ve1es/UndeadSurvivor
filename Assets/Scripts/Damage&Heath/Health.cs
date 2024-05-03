using Fusion;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [SerializeField] private NetworkPrefabRef _deathPrefab;
    [SerializeField] private Animator _animator;
    [Networked]
    [SerializeField]
    private float _healthPoint { get; set; }
    private float _maxHP;

    public void SetHP(float hp)
    {
        _healthPoint = hp;
        _maxHP = hp;
    }
    public float GetHP()
    {
        return _healthPoint;
    }
    public bool ReduceHP(float damage)
    {
        _healthPoint -= damage;
        if (_healthPoint <= 0)
        {
            Runner.Spawn(_deathPrefab, transform.position, Quaternion.identity);
            Runner.Despawn(Object);
            return true;
        }
        RPC_ShowHitEffect();
        return false;
        
    }
    [Rpc]
    private void RPC_ShowHitEffect()
    {
        _animator.SetTrigger("Hit");
    }
    public void AddHP(float healing)
    {
        _healthPoint += healing;
        if (_healthPoint > _maxHP)
            _healthPoint = _maxHP;
    }
}
