using UnityEngine;

public class InputConnectionControll
{
    private IInputStrategy _inputStrategy;
    public IInputStrategy ChooseStrategy()
    {
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            _inputStrategy = new MobileInputStrategy();
        }
        else
        {
            _inputStrategy = new PCInputStrategy();
        }
        return _inputStrategy;
    }
}
