using Fusion;
using UnityEngine;

public class KillsCounter : NetworkBehaviour
{
    [Networked] public int playerKills { get; set; }
    private int listLength=0;
    private string _player;
    [SerializeField] private KillsList _killList;

    public void Update()
    {
        //_dealDamage.OnKillListUpdated += UpdateKills;
        if (_killList != null && listLength != _killList.playersKills.Count)
        {
            listLength = _killList.playersKills.Count;
            if (_killList.playersKills[listLength - 1] == _player)
                playerKills++;
        }
    }
    //[Rpc]
    //public void RPC_KillsChange(bool isRun)
    //{
    //    _anim.SetBool("Run", isRun);
    //}

    public override void Spawned()
    {
        _player = Object.InputAuthority.ToString();
        
    }
}
