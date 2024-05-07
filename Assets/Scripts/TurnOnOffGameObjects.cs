using UnityEngine;

public class TurnOnOffGameObjects : MonoBehaviour
{
    public void TurnOn(GameObject objectForTurnOn)
    {
        objectForTurnOn.SetActive(true);
    }
    public void TurnOff(GameObject objectForTurnOff)
    {
        objectForTurnOff.SetActive(false);
    }
}
