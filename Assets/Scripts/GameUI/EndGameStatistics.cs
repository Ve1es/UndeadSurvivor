using Fusion;
using TMPro;
using UnityEngine;

public class EndGameStatistics : NetworkBehaviour
{
    private const int Player_One_Number_In_List = 0;
    private const int Player_Two_Number_In_Lis = 1;
    private const int Player_Damage_First_Field = 0;
    private const int Player_Damage_Second_Field = 1;
    [Networked] private string _playerOneName { get; set; }
    [Networked] private string _playerTwoName { get; set; }
    [Networked] private string _playerOneKills { get; set; }
    [Networked] private string _playerTwoKills { get; set; }
    [Networked] private string _playerOneDamage { get; set; }
    [Networked] private string _playerTwoDamage { get; set; }
    [Networked] private int _playersCount { get; set; }

    [SerializeField] private Canvas _endGameStats;
    [SerializeField] private PlayerPool _playerPool;
    [SerializeField] private KillsList _killsList;
    [SerializeField] private PlayerDamageList _playerDamageList;

    [SerializeField] private TMP_Text _playerOneNameT;
    [SerializeField] private TMP_Text _playerOneKillsT;
    [SerializeField] private TMP_Text _playerOneDamageT;

    [SerializeField] private TMP_Text _playerTwoNameT;
    [SerializeField] private TMP_Text _playerTwoKillsT;
    [SerializeField] private TMP_Text _playerTwoDamageT;

    public void DisplayStatistic()
    {
        Rpc_DisplayStatistic();
    }
    private void FillStatistic()
    {
        if (HasStateAuthority)
        {
            _playersCount = _playerPool.PlayersInputsNumbers.Count;
            string playerOneInput = _playerPool.PlayersInputsNumbers[Player_One_Number_In_List];
            _playerOneName = playerOneInput;
            _playerOneKills = KillCount(playerOneInput).ToString();
            _playerOneDamage = DamageCount(playerOneInput).ToString();

            if (_playersCount > 1)
            {
                string playerTwoInput = _playerPool.PlayersInputsNumbers[Player_Two_Number_In_Lis];
                _playerTwoName = playerTwoInput;
                _playerTwoKills = KillCount(playerTwoInput).ToString();
                _playerTwoDamage = DamageCount(playerTwoInput).ToString();
            }
        }
        Rpc_FillDisplayStatistic();
    }

    private int KillCount(string playerInput)
    {
         int kills = 0;
        foreach (var kill in _killsList.PlayersKills)
        {
            if (kill == playerInput)
                kills++;
        }
        return kills;
    }
    private float DamageCount(string playerInput)
    {
        float damage = 0;
        foreach (string[] oneDamageTick in _playerDamageList.PlayersDamage)
        {
            if (oneDamageTick[Player_Damage_First_Field] == playerInput)
                damage += float.Parse(oneDamageTick[Player_Damage_Second_Field]);
        }
        return damage;
    }
    [Rpc]
    private void Rpc_DisplayStatistic()
    {
        FillStatistic();
    }
    [Rpc]
    private void Rpc_FillDisplayStatistic()
    {
        _playerOneNameT.text = _playerOneName;
        _playerOneKillsT.text = _playerOneKills;
        _playerOneDamageT.text = _playerOneDamage;

        if (_playersCount > 1)
        {
            _playerTwoNameT.text = _playerTwoName;
            _playerTwoKillsT.text = _playerTwoKills;
            _playerTwoDamageT.text = _playerTwoDamage;
        }
    }
}
