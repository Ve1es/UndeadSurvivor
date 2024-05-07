using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Fusion;


public class JoystickWeapon : NetworkBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Image _joystick;
    private Image _joystickButton;
    private Vector2 _inputVector;
    [SerializeField] private float maxDistance = 100f;

    private void Start()
    {
        _joystick = GetComponent<Image>();
        _joystickButton = transform.GetChild(0).GetComponent<Image>();
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = (eventData.position - (Vector2)_joystick.transform.position);
        direction = Vector2.ClampMagnitude(direction, maxDistance);
        _joystickButton.rectTransform.anchoredPosition = direction;
        _inputVector = direction.normalized;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        _joystickButton.rectTransform.anchoredPosition = Vector2.zero;
    }
    public float Horizontal()
    {
        if (_inputVector.x != 0)
            return _inputVector.x;
        else
            return Input.GetAxisRaw("Horizontal");
    }
    public float Vertical()
    {
        if (_inputVector.y != 0)
            return _inputVector.y;
        else
            return Input.GetAxisRaw("Vertical");
    }
    public bool Shoot()
    {
        if (_inputVector.y != 0|| _inputVector.x != 0)
            return true;
        else
            return false;
    }
}
