using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterList", menuName = "Character List", order = 1)]
public class CharacterList : ScriptableObject
{
    [SerializeField] private Sprite[] _characterSprites;
    [SerializeField] private RuntimeAnimatorController[] _characterAnimators;

    public Sprite GetSprite(int characterNumber)
    {
        return _characterSprites[characterNumber];
    }
    public RuntimeAnimatorController GetAnimator(int characterNumber)
    {
        return _characterAnimators[characterNumber];
    }
}
