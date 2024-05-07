using UnityEngine;
using Fusion;

public class PlayerUIController : NetworkBehaviour
{
    private PlayerUIChanger UICanvas;
    [SerializeField] private Health _hp;
    [SerializeField] private WeaponController _ammo;
    [SerializeField] private KillsCounter _kills;
    private float _currentHp;
    private int _currentAmmo;
    private float _currentKills;

    public override void Spawned()
    {
        if (HasInputAuthority)
        {
            UICanvas = FindObjectOfType<PlayerUIChanger>();
            _currentKills = _kills.playerKills;
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
        if (_kills.playerKills != _currentKills)
        {
            _currentKills = _kills.playerKills;
            if (UICanvas != null)
                UICanvas.ChangeKills(_currentKills);
        }

    }
}
