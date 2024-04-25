using Fusion;
using Fusion.LagCompensation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPlayerController : NetworkBehaviour
{
    [SerializeField] private PlayerPool _playerPool;

    // Local Runtime references
    private ChangeDetector _changeDetector;
    private Rigidbody _rigidbody = null;
    private PlayerDataNetworked _playerDataNetworked = null;

    private List<LagCompensatedHit> _lagCompensatedHits = new List<LagCompensatedHit>();

    // Game Session SPECIFIC Settings
    public bool AcceptInput => _isAlive && Object.IsValid;

    [Networked] private NetworkBool _isAlive { get; set; }

    [Networked] private TickTimer _respawnTimer { get; set; }


    public override void Spawned()
    {
        // --- Host & Client
        // Set the local runtime references.
        _rigidbody = GetComponent<Rigidbody>();
        _playerDataNetworked = GetComponent<PlayerDataNetworked>();
        //_visualController = GetComponent<CharacterVisualController>();
        _changeDetector = GetChangeDetector(ChangeDetector.Source.SimulationState);

        // --- Host
        // The Game Session SPECIFIC settings are initialized
        if (Object.HasStateAuthority == false) return;
        _isAlive = true;

        _playerPool.RegisterPlayer(gameObject);
    }

    public override void Render()
    {
    }

    //private void ToggleVisuals(bool wasAlive, bool isAlive)
    //{
    //    // Check if the spaceship was just brought to life
    //    if (wasAlive == false && isAlive == true)
    //    {
    //        _visualController.TriggerSpawn();
    //    }
    //    // or whether it just got destroyed.
    //    else if (wasAlive == true && isAlive == false)
    //    {
    //        _visualController.TriggerDestruction();
    //    }
    //}

    public override void FixedUpdateNetwork()
    {
    }

    // Check asteroid collision using a lag compensated OverlapSphere
    //private bool HasHitAsteroid()
    //{
    //    _lagCompensatedHits.Clear();

    //    var count = Runner.LagCompensation.OverlapSphere(_rigidbody.position, _spaceshipDamageRadius,
    //        Object.InputAuthority, _lagCompensatedHits,
    //        _asteroidCollisionLayer.value);

    //    if (count <= 0) return false;

    //    _lagCompensatedHits.SortDistance();

    //    var asteroidBehaviour = _lagCompensatedHits[0].GameObject.GetComponent<AsteroidBehaviour>();
    //    if (asteroidBehaviour.IsAlive == false)
    //        return false;

    //    asteroidBehaviour.HitAsteroid(PlayerRef.None);

    //    return true;
    //}

    // Toggle the _isAlive boolean if the spaceship was hit and check whether the player has any lives left.
    // If they do, then the _respawnTimer is activated.
   
    // Resets the spaceships movement velocity
    //private void ResetShip()
    //{
    //    _rigidbody.velocity = Vector3.zero;
    //    _rigidbody.angularVelocity = Vector3.zero;
    //}
}
