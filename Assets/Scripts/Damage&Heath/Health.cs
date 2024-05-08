using Fusion;
using UnityEngine;
using System.Collections;

public class Health : NetworkBehaviour
{
    private const float Decrease_Anim_Time = 3;
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int Hit = Animator.StringToHash("Hit");

    private float _maxHP;

    [SerializeField] private NetworkPrefabRef _deathPrefab;
    [SerializeField] private Animator _animator;
    [Networked]
    [SerializeField] 
    private float _healthPoint { get; set; }

    

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
        StartCoroutine(PlayFirstThenSecond());
    }
    IEnumerator PlayFirstThenSecond()
    {
        _animator.SetTrigger(Hit);
        _animator.SetBool(Run, false);
        yield return new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length/Decrease_Anim_Time);
        _animator.SetBool(Run, true);
    }
    public void AddHP(float healing)
    {
        _healthPoint += healing;
        if (_healthPoint > _maxHP)
            _healthPoint = _maxHP;
    }
}
