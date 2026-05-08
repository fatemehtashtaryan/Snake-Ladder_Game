using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Serialization;


public class player : MonoBehaviour
{
    [SerializeField] private RotateObject Dice;
    [SerializeField] private pawn Pawn;
    [SerializeField] private boardgame Board;

    // [SerializeField] private Button diceButton;
    
    private void Start()
    {
        Dice.OnDiceStopped += MovePawn; 
    }

    
    private void MovePawn()
    {
        // Debug.Log("hhhhhhhhhhhh");
        int currentPos = Pawn.GetPos();
        int targetPos = currentPos + Dice.getFace();
        Debug.Log(currentPos);
        Debug.Log(Dice.getFace());
        Pawn.MovePawn(Board.GetTile(targetPos), targetPos);
    }
}
