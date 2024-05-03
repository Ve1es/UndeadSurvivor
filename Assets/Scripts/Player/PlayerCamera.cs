using Fusion;
using UnityEngine;

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
    public void ChangeCamera()
    {
        if (_playerPool.players != null)
        {
           // camera.GetComponent<TopDownCamera>().target = _playerPool.players[ANOTHER_PLAYER_NUMBER_IN_LIST].transform;
        }
    }
}
