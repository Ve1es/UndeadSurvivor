using System;
using System.Collections.Generic;
using Fusion;
using Fusion.Sockets;
using UnityEngine;

public class LocalInputPoller : MonoBehaviour, INetworkRunnerCallbacks
{
    private const string AXIS_HORIZONTAL = "Horizontal";
    private const string AXIS_VERTICAL = "Vertical";
    private JoystickMove _joystickMove;
    private JoystickWeapon _joystickWeapon;

    public void ConnectInputSystem(JoystickMove joysticMove, JoystickWeapon joystickWeapon)
    {
        _joystickMove = joysticMove;
        _joystickWeapon = joystickWeapon;
    }

    public void OnInput(NetworkRunner runner, NetworkInput input)
    {
        CharacterInput localInput = new CharacterInput();

        localInput.MoveHorizontalInput = Input.GetAxis(AXIS_HORIZONTAL);
        localInput.MoveVerticalInput = Input.GetAxis(AXIS_VERTICAL);
        if (_joystickMove != null)
        {
            localInput.MoveHorizontalInput = _joystickMove.Horizontal();
            localInput.MoveVerticalInput = _joystickMove.Vertical();
        }
        if(_joystickWeapon != null)
        {
            localInput.Shoot = _joystickWeapon.Shoot();
            localInput.WeaponHorizontalInput = _joystickWeapon.Horizontal();
            localInput.WeaponVerticalInput = _joystickWeapon.Vertical();
        }

        input.Set(localInput);
    }
    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }
    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player)
    {
    }
    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
    }
    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
    }
    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input)
    {
    }
    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason)
    {
    }
    public void OnConnectedToServer(NetworkRunner runner)
    {
    }
    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason)
    {
    }
    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request,
        byte[] token)
    {
    }
    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason)
    {
    }
    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message)
    {
    }
    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList)
    {
    }
    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data)
    {
    }
    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken)
    {
    }
    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data)
    {
    }
    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress)
    {
    }
    public void OnSceneLoadDone(NetworkRunner runner)
    {
    }
    public void OnSceneLoadStart(NetworkRunner runner)
    {
    }
}
