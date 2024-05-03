using Fusion;
using UnityEngine;

public class PlayerDataNetworked : NetworkBehaviour
{
    [SerializeField]
    [Networked]
    public string NickName { get; private set; }
    [SerializeField]
    [Networked]
    public string PlayerInputNumber { get; private set; }
    [SerializeField]
    [Networked]
    public int playerCharacterNumber { get; private set; }

    //public override void Spawned()
    //{
    //    if (Object.HasInputAuthority)
    //    {
    //        var nickName = FindObjectOfType<PlayerData>().GetNickName();
    //        RpcSetNickName(nickName);
    //        RpcSetCharacterNumber(FindObjectOfType<PlayerData>().GetCharacter());
    //        RpcSetPlayerInputNumber(Object.InputAuthority.ToString());
    //    } 
    //}

    //[Rpc]
    //private void RpcSetNickName(string nickName)
    //{
    //    if (string.IsNullOrEmpty(nickName)) return;
    //    NickName = nickName;
    //}
    //[Rpc]
    //private void RpcSetCharacterNumber(int number)
    //{
    //    playerCharacterNumber = number;
    //}
    //[Rpc]
    //private void RpcSetPlayerInputNumber(string playerInputNumber)
    //{
    //    PlayerInputNumber = playerInputNumber;
    //}
}
