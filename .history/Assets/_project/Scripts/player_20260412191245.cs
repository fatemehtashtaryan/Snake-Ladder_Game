using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Serialization;


public class player : MonoBehaviour
{
    [SerializeField] private DiceInfo Dice;
    [SerializeField] private Pawn Pawn;
    [SerializeField] private BoardGame Board;

    [SerializeField] private Button diceButton;
    
    // private void Start()
    // {
    //     int x =0;
    //     // diceButton.onClick.AddListener(TossDice);
    //     x = Dice.getFace();
    //     MovePawn
    // }

    // private void TossDice()
    // {
    //     Dice.TossDice();
    //     MovePawn();

    // }
    private void MovePawn()
    {
        int currentPos = Pawn.GetPos();
        int targetPos = currentPos + Dice.getFace();
        Pawn.MovePawn(Board.GetTile(targetPos), targetPos);
    }
}
