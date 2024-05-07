using UnityEngine;
using UnityEngine.UI;

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

    public void TurnOnButton(Button objectForTurnOn)
    {
        objectForTurnOn.interactable = true;
    }
    public void TurnOffButton(Button objectForTurnOff)
    {
        objectForTurnOff.interactable = false;
    }
}
