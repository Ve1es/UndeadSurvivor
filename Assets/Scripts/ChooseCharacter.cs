using UnityEngine;
using Fusion;
using UnityEngine.UI;

public class ChooseCharacter : NetworkBehaviour
{
    private const int Default_Character_Number = 0;
    private int _characterNumber = Default_Character_Number;
    [SerializeField] private Image[] _characterBack;
    [SerializeField] private Button[] _characterChooseButtons;
    [SerializeField] private CharacterSpawner _spawn;

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
                _characterBack[i].gameObject.SetActive(false);
            }
            else
            {
                _characterBack[i].gameObject.SetActive(true);
            }
        }
    }

    public void CloseAllNonChooseCharacters()
    {
        for (int i = 0; i < _characterChooseButtons.Length; i++)
        {
            if (i != _characterNumber)
            {
                _characterBack[i].gameObject.SetActive(false);
                _characterChooseButtons[i].gameObject.SetActive(false);
            }
        }
    }
}
