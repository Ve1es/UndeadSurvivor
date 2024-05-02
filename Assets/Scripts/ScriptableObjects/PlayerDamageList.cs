using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayersDamage", menuName = "Managers/PlayersDamage")]
public class PlayerDamageList : ScriptableObject
{
    public List<string[]> playersDamage = new List<string[]>();
    public void AddString(string player, string damage)
    {
        playersDamage.Add(new string[] { player, damage });
    }
    public void ClearPool()
    {
        playersDamage.Clear();
    }
}
