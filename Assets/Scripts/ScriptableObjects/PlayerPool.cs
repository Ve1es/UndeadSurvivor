using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerPool", menuName = "Managers/PlayerPool")]
public class PlayerPool : ScriptableObject
{
    public List<GameObject> players = new List<GameObject>();
    public List<string> playersInputsNumbers = new List<string>();

    public void RegisterPlayer(GameObject playerTransform)
    {
        if (!players.Contains(playerTransform))
        {
            players.Add(playerTransform);
        }
    }
    public void RegisterPlayerInputNumber(string playerInputNumber)
    {
        playersInputsNumbers.Add(playerInputNumber);
    }


    public void UnregisterPlayer(GameObject playerTransform)
    {
        players.Remove(playerTransform);
    }

    public void ClearPool()
    {
        players.Clear();
        playersInputsNumbers.Clear();
    }
}
