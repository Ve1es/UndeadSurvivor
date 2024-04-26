using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon Data", order = 2)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string _weaponName;
    [SerializeField]
    private float _timeBetweenShots;
    [SerializeField]
    private float _damage;
    [SerializeField]
    private int _ammo;
    [SerializeField]
    private float _range;
    [SerializeField]
    private int _bulletPerShot;

    public string WeaponName
    {
        get
        {
            return _weaponName;
        }
    }
    public float TimeBetweenShots
    {
        get
        {
            return _timeBetweenShots;
        }
    }
    public float Damage
    {
        get
        {
            return _damage;
        }
    }
    public float Range
    {
        get
        {
            return _range;
        }
    }
    public int Ammo
    {
        get
        {
            return _ammo;
        }
    }
    public int BulletPerShot
    {
        get
        {
            return _bulletPerShot;
        }
    }
}
