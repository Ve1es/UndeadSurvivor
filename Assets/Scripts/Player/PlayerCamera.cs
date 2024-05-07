using Fusion;
using UnityEngine;
using UnityEngine.LowLevel;

public class PlayerCamera : NetworkBehaviour
{
    private const int ANOTHER_PLAYER_NUMBER_IN_LIST = 0;
    [SerializeField] private PlayerPool _playerPool;
    private GameObject camera;

    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera");         
            camera.GetComponent<TopDownCamera>().target = gameObject.transform;

        }
    }
    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        if (_playerPool.players.Count > 0)
        {
            for (int i = 0; i < _playerPool.players.Count; i++)
            {
                if (_playerPool.players[i] != gameObject)
                    camera.GetComponent<TopDownCamera>().target = _playerPool.players[i].transform;
            }
        }
    }
}
