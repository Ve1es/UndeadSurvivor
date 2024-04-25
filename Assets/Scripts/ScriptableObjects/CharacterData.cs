using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data", order = 1)]
public class CharacterData : ScriptableObject
{
    [SerializeField]
    private Sprite _characterSprite;
}
