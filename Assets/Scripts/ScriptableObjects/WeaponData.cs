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
    private float _flightDistance;
    [SerializeField]
    private int _bulletPerShot;
    [SerializeField]
    private Sprite _weaponSprite;
    [SerializeField]
    private float _spread;
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
    public float FlightDistance
    {
        get
        {
            return _flightDistance;
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
    public Sprite WeaponSprite
    {
        get
        {
            return _weaponSprite;
        }
    }
    public float Spread
    {
        get
        {
            return _spread;
        }
    }
}
