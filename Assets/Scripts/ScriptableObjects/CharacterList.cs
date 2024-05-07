using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterList", menuName = "Character List", order = 1)]
public class CharacterList : ScriptableObject
{
    [SerializeField] private Sprite[] characterSprites;
    [SerializeField] private RuntimeAnimatorController[] characterAnimators;

    public Sprite GetSprite(int characterNumber)
    {
        return characterSprites[characterNumber];
    }
    public RuntimeAnimatorController GetAnimator(int characterNumber)
    {
        return characterAnimators[characterNumber];
    }
}
