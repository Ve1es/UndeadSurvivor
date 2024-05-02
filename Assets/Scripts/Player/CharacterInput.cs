using Fusion;
using UnityEngine;


    enum CharacterButtons
    {
        Fire = 0,
    }

public struct CharacterInput : INetworkInput
{
    public float MoveHorizontalInput;
    public float MoveVerticalInput;
    public float WeaponHorizontalInput;
    public float WeaponVerticalInput;
    public bool Shoot;
    //public NetworkButtons Buttons;
}
