using Fusion;
using UnityEngine;

public class KillsCounter : NetworkBehaviour
{
    public int playerKills=0;
    private int listLength=0;
    private string _player;
    [SerializeField] private KillsList _killList;

    public void Update()
    {
        if (_killList != null && listLength != _killList.playersKills.Count)
        { 
            listLength = _killList.playersKills.Count;
            if (_killList.playersKills[listLength - 1] == _player)
                playerKills++;
        }
    }
    public override void Spawned()
    {
        _player = Object.InputAuthority.ToString();
    }
}
