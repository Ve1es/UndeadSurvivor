using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVisibleObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _turnOnObject;
    [SerializeField]
    private GameObject _turnOffObject;

    public void ChangeVisible()
    {
        _turnOnObject.SetActive(true);
        _turnOffObject.SetActive(false);
    }
}
