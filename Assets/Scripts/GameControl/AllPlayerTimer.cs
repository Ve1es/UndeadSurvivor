using Fusion;
using TMPro;
using UnityEngine;

public class AllPlayerTimer : NetworkBehaviour
{
    [SerializeField] private TMP_Text _gameRoundTimer;
    public override void Spawned()
    {
        Runner.SetIsSimulated(Object, true);
    }
    public void ChangeTimerUI(string time)
    {
        _gameRoundTimer.text = time;
    }
}
