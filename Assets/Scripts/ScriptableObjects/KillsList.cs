using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayersKilling", menuName = "Managers/PlayersKilling")]
public class KillsList : ScriptableObject
{
    public List<string> playersKills = new List<string>();
    public void AddString(string info)
    {
        playersKills.Add(info);
    }
}
