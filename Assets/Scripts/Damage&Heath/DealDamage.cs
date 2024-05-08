using Fusion;
using System;
using UnityEngine;

public class DealDamage : NetworkBehaviour
{
    private const string Non_Player_Input = "[Player:None]";
    private const float Default_Damage = 0;
    private float _damage = Default_Damage;
    private string _goalTag;
    private string _player;

    [SerializeField] private KillsList _killsList;
    [SerializeField] private PlayerDamageList _playerDamageList;

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
                if (_killsList != null && _player != Non_Player_Input)
                { 
                    _killsList.AddString(_player);
                }
            }
            if (_playerDamageList != null && _player != Non_Player_Input)
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
