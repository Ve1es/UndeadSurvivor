using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Fusion;

public class JoystickMove : NetworkBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private Image _joystick;
    private Image _joystickButton;
    private Vector2 _inputVector;

    private void Start()
    {
        _joystick = GetComponent<Image>();
        _joystickButton = transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystick.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = pos.x / _joystick.rectTransform.sizeDelta.x;
            pos.y = pos.y / _joystick.rectTransform.sizeDelta.x;
        }

        _inputVector = new Vector2(pos.x * 2 - 1, pos.y * 2 - 1);
        _inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;

        _joystickButton.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_joystick.rectTransform.sizeDelta.x / 2), _inputVector.y * (_joystick.rectTransform.sizeDelta.y / 2));
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
}
