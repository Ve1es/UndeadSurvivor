using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartLevelUIChange : MonoBehaviour
{
    [SerializeField] private ChooseCharacter _preGame;

    [SerializeField] private TMP_Text _kills;
    [SerializeField] private TMP_Text _hp;
    [SerializeField] private TMP_Text _ammo;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private JoystickMove _joystickMove;
    [SerializeField] private JoystickWeapon _joystickWeapon;
    [SerializeField] private Image _joysticMoveButton;
    [SerializeField] private Image _joysticWeaponButton;

    public void Changeinterface()
    {
        _preGame.gameObject.SetActive(false);
        _kills.gameObject.SetActive(true);
        _hp.gameObject.SetActive(true);
        _ammo.gameObject.SetActive(true);
        _timer.gameObject.SetActive(true);
        _joystickMove.gameObject.SetActive(true);
        _joystickWeapon.gameObject.SetActive(true);
        _joysticMoveButton.gameObject.SetActive(true);
        _joysticWeaponButton.gameObject.SetActive(true);
    }
}
