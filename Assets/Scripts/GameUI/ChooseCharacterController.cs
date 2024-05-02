using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacterController : MonoBehaviour
{
    private const int DEFAULT_CHARACTER_NUMBER = 0;
    private int _characterNumber = DEFAULT_CHARACTER_NUMBER;
    [SerializeField]
    private GameObject _chooseCharacterPanel;
    [SerializeField]
    private GameObject[] _characterBack;
    [SerializeField] private PlayerData _playerDataPrefab;
    //[SerializeField]
    //private GameObject _characterBackTwo;
    //[SerializeField]
    //private GameObject _characterBackThree;
    //[SerializeField]
    //private GameObject _characterBackFour;

    public void ChangeCharacter(int number)
    {
        _characterNumber = number;
        for(int i=0; i< _characterBack.Length; i++)
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
    public void ChooseCharacter()
    {
        _playerDataPrefab.SetCharacter(_characterNumber);
    }
    public void CloseChoosePanel()
    {
        _chooseCharacterPanel.SetActive(false);
    }
}
