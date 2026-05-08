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
        // int x =0;
        // // diceButton.onClick.AddListener(TossDice);
        // x = Dice.getFace();
        Dice.OnDiceStopped += MovePawn; 
    }

    // private void TossDice()
    // {
    //     Dice.TossDice();
    //     MovePawn();

    // }
    private void MovePawn()
    {
        Debug.Log("hhhhhhhhhhhh");
        int currentPos = Pawn.GetPos();
        int targetPos = currentPos + Dice.getFace();
        Debug.Log(targetPos);
        Pawn.MovePawn(Board.GetTile(targetPos), targetPos);
    }
}
