using Fusion;
using TMPro;
using UnityEngine;

public class EndGameStatistics : NetworkBehaviour
{
    private const int PLAYER_ONE_NUMBER_IN_LIST = 0;
    private const int PLAYER_TWO_NUMBER_IN_LIST = 1;
    private const int PLAYER_DAMAGE_FIRST_FIELD = 0;
    private const int PLAYER_DAMAGE_SECOND_FIELD = 1;
    [Networked] private string playerOneName { get; set; }
    [Networked] private string playerTwoName { get; set; }
    [Networked] private string playerOneKills { get; set; }
    [Networked] private string playerTwoKills { get; set; }
    [Networked] private string playerOneDamage { get; set; }
    [Networked] private string playerTwoDamage { get; set; }
    [Networked] private int playersCount { get; set; }
    [SerializeField] private GameObject _endGameStats;
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
            string playerOneInput = _playerPool.playersInputsNumbers[PLAYER_ONE_NUMBER_IN_LIST];
            playerOneName = playerOneInput;
            playerOneKills = KillCount(playerOneInput).ToString();
            playerOneDamage = DamageCount(playerOneInput).ToString();

            if (playersCount > 1)
            {
                string playerTwoInput = _playerPool.playersInputsNumbers[PLAYER_TWO_NUMBER_IN_LIST];
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
            if (oneDamageTick[PLAYER_DAMAGE_FIRST_FIELD] == playerInput)
                damage += float.Parse(oneDamageTick[PLAYER_DAMAGE_SECOND_FIELD]);
        }
        return damage;
    }
    [Rpc]
    private void Rpc_DisplayStatistic()
    {
        _endGameStats.SetActive(true);
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
