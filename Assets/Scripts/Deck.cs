using NUnit.Framework;
using UnityEngine;


[CreateAssetMenu(fileName = "Deck", menuName = "Scriptable Objects/Deck")]
public class Deck : ScriptableObject
{
    public Ball[] balls;

    public Difficulty difficulty;
}
