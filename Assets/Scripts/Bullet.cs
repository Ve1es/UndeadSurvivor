using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Bullet : NetworkBehaviour
{
    //private Vector3 _previousPosition;
    //private float _totalDistanceFly;

    //public float speed = 10f;
    //public float flightRange=20;

    //void Start()
    //{
    //    _previousPosition = transform.position;
    //    _totalDistanceFly = 0f;
    //}
    //public override void FixedUpdateNetwork()
    //{
    //    DestroyAfterFlightMaxDistance();
    //    transform.position += speed * transform.forward * Runner.DeltaTime;
    //}
    //public void DestroyAfterFlightMaxDistance()
    //{
    //    float distanceThisFrame = Vector3.Distance(transform.position, _previousPosition);
    //    _totalDistanceFly += distanceThisFrame;
    //    _previousPosition = transform.position;
    //    if(_totalDistanceFly> flightRange)
    //        Runner.Despawn(Object);
    //}


    [Networked] private TickTimer life { get; set; }

    public void Init()
    {
        life = TickTimer.CreateFromSeconds(Runner, 5.0f);
    }

    public override void FixedUpdateNetwork()
    {
        if (life.Expired(Runner))
            Runner.Despawn(Object);
        else
            transform.position +=  2 * transform.right * Runner.DeltaTime;
    }
}
