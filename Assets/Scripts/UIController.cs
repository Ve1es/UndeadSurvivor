using Fusion;
using TMPro;
using UnityEngine;

public class UIController : NetworkBehaviour
{
    [SerializeField] private TMP_Text _hp;
    [SerializeField] private TMP_Text _ammo;
    [SerializeField] private TMP_Text _kills;

    public void ChangeHP(float currentHp)
    {
        _hp.text = "HP "+ currentHp.ToString();
    }
    public void ChangeAmmo(float currentAmmo)
    {
        _ammo.text = "Ammo "+currentAmmo.ToString();
    }
    public void ChangeKills(float currentKills)
    {
        _kills.text = "Kills "+currentKills.ToString();
    }
}
