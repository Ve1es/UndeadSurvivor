using Fusion;
using System;
using UnityEngine;

public class DealDamage : NetworkBehaviour
{
    private const string NON_PLAYER_INPUT = "[Player:None]";
    private static float DEFAULTDAMAGE = 0;
    private float _damage = DEFAULTDAMAGE;
    private string _goalTag;
    private string _player;
    [SerializeField]
    private KillsList _killsList;
    [SerializeField]
    private PlayerDamageList _playerDamageList;
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
                if (_killsList != null && _player != NON_PLAYER_INPUT)
                { 
                    _killsList.AddString(_player);
                }
            }
            if (_playerDamageList != null && _player != NON_PLAYER_INPUT)
            {
                _playerDamageList.AddString(_player, _damage.ToString());
            }
            Runner.Despawn(Object);
        }
    }

    public override void Spawned()
    {
        _player = Object.InputAuthority.ToString();
    }
}
