using UnityEngine;
using Fusion;

public class PlayerUIController : NetworkBehaviour
{
    private const float Zero_Hp = 0;

    private PlayerUIChanger UICanvas;
    private float _currentHp;
    private float _currentKills;
    private int _currentAmmo;

    [SerializeField] private Health _hp;
    [SerializeField] private WeaponController _ammo;
    [SerializeField] private KillsCounter _kills;
    

    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            UICanvas = FindObjectOfType<PlayerUIChanger>();
            _currentKills = _kills.PlayerKills;
        }
    }

    public void Update()
    {
        if (_hp.GetHP() != _currentHp)
        {
            _currentHp = _hp.GetHP();
            if (UICanvas != null)
                UICanvas.ChangeHP(_currentHp);
        }
        if (_ammo.GetAmmo() != _currentAmmo)
        {
            _currentAmmo = _ammo.GetAmmo();
            if (UICanvas != null)
                UICanvas.ChangeAmmo(_currentAmmo);
        }
        if (_kills.PlayerKills != _currentKills)
        {
            _currentKills = _kills.PlayerKills;
            if (UICanvas != null)
                UICanvas.ChangeKills(_currentKills);
        }

    }
    public override void Despawned(NetworkRunner runner, bool hasState)
    {
        UICanvas.ChangeHP(Zero_Hp);
    }
}
