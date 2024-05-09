using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPool", menuName = "Managers/PlayerPool")]
public class PlayerPool : ScriptableObject
{
    public List<GameObject> Players = new List<GameObject>();
    public List<string> PlayersInputsNumbers = new List<string>();

    public void RegisterPlayer(GameObject playerTransform)
    {
        if (!Players.Contains(playerTransform))
        {
            Players.Add(playerTransform);
        }
    }
    public void RegisterPlayerInputNumber(string playerInputNumber)
    {
        PlayersInputsNumbers.Add(playerInputNumber);
    }


    public void UnregisterPlayer(GameObject playerTransform)
    {
        Players.Remove(playerTransform);
    }

    public void ClearPool()
    {
        Players.Clear();
        PlayersInputsNumbers.Clear();
    }
}
