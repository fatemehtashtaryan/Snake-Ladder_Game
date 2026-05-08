using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Serialization;


public class player : MonoBehaviour
{
    [SerializeField] private RotateObject Dice;
    [SerializeField] private pawn Pawn;
    [SerializeField] private boardgame Board;
    [SerializeField] private warning txt_warning;
    private int score;
    public TMP_Text warning;

    // [SerializeField] private Button diceButton;
    
    private void Start()
    {
        score = 0;
        Dice.OnDiceStopped += MovePawn; 
    }
    

    
    private void MovePawn()
    {
        List<int> star = Board.get_star();
        List<int> obs = Board.get_obs();
        int currentPos = Pawn.GetPos();
        int targetPos = currentPos + Dice.getFace();
        if(targetPos in star){
            txt_warning.ShowText("Boom! You’re sent back to start!!!");
        }
        // Debug.Log(currentPos);
        Debug.Log(targetPos);
        Pawn.MovePawn(Board.GetTile(targetPos), targetPos);
        
    }
}
