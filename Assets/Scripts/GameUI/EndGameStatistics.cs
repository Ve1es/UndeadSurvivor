using Fusion;
using TMPro;
using UnityEngine;

public class EndGameStatistics : NetworkBehaviour
{
    private const int Player_One_Number_In_List = 0;
    private const int Player_Two_Number_In_Lis = 1;
    private const int Player_Damage_First_Field = 0;
    private const int Player_Damage_Second_Field = 1;
    [Networked] private string playerOneName { get; set; }
    [Networked] private string playerTwoName { get; set; }
    [Networked] private string playerOneKills { get; set; }
    [Networked] private string playerTwoKills { get; set; }
    [Networked] private string playerOneDamage { get; set; }
    [Networked] private string playerTwoDamage { get; set; }
    [Networked] private int playersCount { get; set; }

    [SerializeField] private Canvas _endGameStats;
    [SerializeField] private PlayerPool _playerPool;
    [SerializeField] private KillsList _killsList;
    [SerializeField] private PlayerDamageList _playerDamageList;

    [SerializeField] private TMP_Text _playerOneName;
    [SerializeField] private TMP_Text _playerOneKills;
    [SerializeField] private TMP_Text _playerOneDamage;

    [SerializeField] private TMP_Text _playerTwoName;
    [SerializeField] private TMP_Text _playerTwoKills;
    [SerializeField] private TMP_Text _playerTwoDamage;

    public void DisplayStatistic()
    {
        Rpc_DisplayStatistic();
    }
    private void FillStatistic()
    {
        if (HasStateAuthority)
        {
            playersCount = _playerPool.playersInputsNumbers.Count;
            string playerOneInput = _playerPool.playersInputsNumbers[Player_One_Number_In_List];
            playerOneName = playerOneInput;
            playerOneKills = KillCount(playerOneInput).ToString();
            playerOneDamage = DamageCount(playerOneInput).ToString();

            if (playersCount > 1)
            {
                string playerTwoInput = _playerPool.playersInputsNumbers[Player_Two_Number_In_Lis];
                playerTwoName = playerTwoInput;
                playerTwoKills = KillCount(playerTwoInput).ToString();
                playerTwoDamage = DamageCount(playerTwoInput).ToString();
            }
        }
        Rpc_FillDisplayStatistic();
    }

    private int KillCount(string playerInput)
    {
         int kills = 0;
        foreach (var kill in _killsList.playersKills)
        {
            if (kill == playerInput)
                kills++;
        }
        return kills;
    }
    private float DamageCount(string playerInput)
    {
        float damage = 0;
        foreach (string[] oneDamageTick in _playerDamageList.playersDamage)
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
        _playerOneName.text = playerOneName;
        _playerOneKills.text = playerOneKills;
        _playerOneDamage.text = playerOneDamage;

        if (playersCount > 1)
        {
            _playerTwoName.text = playerTwoName;
            _playerTwoKills.text = playerTwoKills;
            _playerTwoDamage.text = playerTwoDamage;
        }
    }
}
