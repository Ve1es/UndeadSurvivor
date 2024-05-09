using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayersKilling", menuName = "Managers/PlayersKilling")]
public class KillsList : ScriptableObject
{
    public List<string> PlayersKills = new List<string>();
    public void AddString(string info)
    {
        PlayersKills.Add(info);
    }
    public void ClearPool()
    {
        PlayersKills.Clear();
    }
}
