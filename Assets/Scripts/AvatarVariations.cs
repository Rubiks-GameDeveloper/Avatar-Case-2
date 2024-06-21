using System.Collections.Generic;
using GigaChatAdapter.Completions;
using UnityEngine;

public enum AvatarType
{
    Student,
    Hockey,
    Baker,
    NatureLover,
    Skier,
    Worker
}
[CreateAssetMenu(fileName = "Add variation", menuName = "Avatar/Create new variant")]
public class AvatarVariations : ScriptableObject
{
    public string startPrompt;
    public AvatarType avatarType;
    public List<GigaChatMessage> history;
}
