using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapon Data", order = 2)]
public class WeaponData : ScriptableObject
{
    [SerializeField]
    private string _weaponName;
    [SerializeField]
    private float _fireRate;
    [SerializeField]
    private float _damage;

    public string WeaponName
    {
        get
        {
            return _weaponName;
        }
    }
    public float FireRate
    {
        get
        {
            return _fireRate;
        }
    }
    public float Damage
    {
        get
        {
            return _damage;
        }
    }
}
