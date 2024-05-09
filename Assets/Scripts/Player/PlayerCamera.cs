using Fusion;
using UnityEngine;

public class PlayerCamera : NetworkBehaviour
{
    [SerializeField] private PlayerPool _playerPool;
    private Camera _camera;

    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            _camera = Camera.main;      
            _camera.GetComponent<TopDownCamera>().Target = gameObject.transform;

        }
    }
    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        if (_playerPool.Players.Count > 0)
        {
            for (int i = 0; i < _playerPool.Players.Count; i++)
            {
                if (_playerPool.Players[i] != gameObject)
                    _camera.GetComponent<TopDownCamera>().Target = _playerPool.Players[i].transform;
            }
        }
    }
}
