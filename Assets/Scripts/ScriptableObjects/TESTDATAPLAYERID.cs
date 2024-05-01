using Fusion;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerID", menuName = "Managers/PlayerID")]
public class TESTDATAPLAYERID : ScriptableObject
{
    public List<string> players = new List<string>();

    public void AddString(string info)
    {
        players.Add(info);
    }

}
