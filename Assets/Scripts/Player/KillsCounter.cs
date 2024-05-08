using Fusion;
using UnityEngine;

public class KillsCounter : NetworkBehaviour
{
    private int listLength=0;
    private string _player;
    [SerializeField] private KillsList _killList;
    [Networked] public int playerKills { get; set; }

    public void Update()
    {
        if (_killList != null && listLength != _killList.playersKills.Count)
        {

            for (int i = listLength; i < _killList.playersKills.Count; i++)
            {
                if (_killList.playersKills[i] == _player)
                    playerKills++;
            }

            listLength = _killList.playersKills.Count;
        }
    }

    public override void Spawned()
    {
        _player = Object.InputAuthority.ToString(); 
    }
}
