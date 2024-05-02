using Fusion;
using System;
using UnityEngine;

public class DealDamage : NetworkBehaviour
{
    private static float DEFAULTDAMAGE = 0;
    private float _damage = DEFAULTDAMAGE;
    private string _goalTag;
    private string _player;
    [SerializeField]
    private KillsList _killsList;
    public Action OnKillListUpdated;

    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    public void SetGoalTag(string goalTag)
    {
        _goalTag = goalTag;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(_goalTag))
        {
            if (other.GetComponent<Health>().ReduceHP(_damage))
            {
                if (_killsList != null && _player != "[Player:None]")
                { 
                    _killsList.AddString(_player);
                }
            }
            Runner.Despawn(Object);
        }
    }

    public override void Spawned()
    {
        _player = Object.InputAuthority.ToString();
    }
}
