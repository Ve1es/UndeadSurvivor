using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerData : MonoBehaviour
{
    private const int DEFAULT_CHARACTER_NUMBER = 0;
    private string _nickName = null;
    [SerializeField]
    private int _playerCharacterNumber = DEFAULT_CHARACTER_NUMBER;

    private void Start()
    {
        var count = FindObjectsOfType<PlayerData>().Length;
        if (count > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SetNickName(string nickName)
    {
        _nickName = nickName;
    }

    public string GetNickName()
    {
        if (string.IsNullOrWhiteSpace(_nickName))
        {
            _nickName = GetRandomNickName();
        }

        return _nickName;
    }
    public void SetCharacter(int number)
    {
        _playerCharacterNumber = number;
    }

    public int GetCharacter()
    {
        return _playerCharacterNumber;
    }

    public static string GetRandomNickName()
    {
        var rngPlayerNumber = Random.Range(0, 9999);
        return $"Player {rngPlayerNumber.ToString("0000")}";
    }
}
