using Fusion;
using UnityEngine;

public class Health : NetworkBehaviour
{
    [Networked]
    [SerializeField]
    private float _healthPoint { get; set; }
    private float _maxHP;
    public override void Spawned()
    {
        _maxHP = _healthPoint;
    }
    public void ReduceHP(float damage)
    {
        _healthPoint -= damage;
        if (_healthPoint <= 0)
        {
            Runner.Despawn(Object);
        }
    }
    public void AddHP(float healing)
    {
        _healthPoint += healing;
        if (_healthPoint > _maxHP)
            _healthPoint = _maxHP;
    }

}
