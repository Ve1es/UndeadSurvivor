using Fusion;
using UnityEngine;

public class Health : NetworkBehaviour
{
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
            
            _animator.SetBool("Dead", true);
            Runner.Despawn(Object);
            return true;
        }
        _animator.SetTrigger("Hit");
        return false;
        
    }
    public void AddHP(float healing)
    {
        _healthPoint += healing;
        if (_healthPoint > _maxHP)
            _healthPoint = _maxHP;
    }
}
