using Fusion;
using UnityEngine;

public class KillsCounter : NetworkBehaviour
{
    private int _listLength=0;
    private string _player;

    [SerializeField] private KillsList _killList;
    [Networked] public int PlayerKills { get; set; }

    public void Update()
    {
        if (_killList != null && _listLength != _killList.playersKills.Count)
        {

            for (int i = _listLength; i < _killList.playersKills.Count; i++)
            {
                if (_killList.playersKills[i] == _player)
                    PlayerKills++;
            }

            _listLength = _killList.playersKills.Count;
        }
    }

    public override void Spawned()
    {
        _player = Object.InputAuthority.ToString(); 
    }
}
