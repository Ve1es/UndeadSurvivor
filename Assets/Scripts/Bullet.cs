using Fusion;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    private float _speed;
    private Vector3 _flightDirection;
    private const float DEFAULT_FLIGHT_DISTANCE = 5;
    private const float START_FLIGHT_DISTANCE = 0;
    private const float DEFAULT_SPEED = 4;
    private Vector3 _initialPosition;
    private float _distanceTravelled;
    private float _maxDistance;

    public void Init(Vector3 flightDirection, float flightDistance = DEFAULT_FLIGHT_DISTANCE, float speed = DEFAULT_SPEED)
    {
        _speed = speed;
       _flightDirection = flightDirection;
        _initialPosition = transform.position;
        _maxDistance = flightDistance;
        _distanceTravelled = START_FLIGHT_DISTANCE;
    }
    public void Init(float flightDistance = DEFAULT_FLIGHT_DISTANCE, float speed = DEFAULT_SPEED)
    {
        _speed = speed;
        _flightDirection = transform.right;
        _initialPosition = transform.position;
        _maxDistance = flightDistance;
        _distanceTravelled = START_FLIGHT_DISTANCE;
    }

    public override void FixedUpdateNetwork()
    {
        if (_distanceTravelled > _maxDistance)
            Runner.Despawn(Object);
        else
        {
            transform.position += _speed * _flightDirection * Runner.DeltaTime;
            _distanceTravelled = Vector3.Distance(_initialPosition, transform.position);
        }
    }

}
