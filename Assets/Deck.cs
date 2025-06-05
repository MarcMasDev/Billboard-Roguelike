using NUnit.Framework;
using UnityEngine;


[CreateAssetMenu(fileName = "Deck", menuName = "Scriptable Objects/Deck")]
public class Deck : ScriptableObject
{
    public WhiteBall WhiteBall;
    public Ball BlackBall;
    public Ball[] balls;
}
