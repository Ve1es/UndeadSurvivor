using UnityEngine;
using Fusion;

public class ChooseCharacter : NetworkBehaviour
{
    private const int DEFAULT_CHARACTER_NUMBER = 0;
    private int _characterNumber = DEFAULT_CHARACTER_NUMBER;
    [SerializeField]
    private GameObject[] _characterBack;
    [SerializeField]
    private CharacterSpawner _spawn;

    [Rpc]
    public void RPC_ClientSpawnCharacter(int characterNumber, PlayerRef localPlayer)
    {
        _spawn.AddPlayerCharacter(characterNumber, localPlayer);
    }
    public void SetCharacter()
    {
        
        RPC_ClientSpawnCharacter(_characterNumber, Runner.LocalPlayer);
    }

    public void ChangeCharacter(int number)
    {
        _characterNumber = number;
        for (int i = 0; i < _characterBack.Length; i++)
        {
            if (i != _characterNumber)
            {
                _characterBack[i].SetActive(false);
            }
            else
            {
                _characterBack[i].SetActive(true);
            }
        }
    }
}
