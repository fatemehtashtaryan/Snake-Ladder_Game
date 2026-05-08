using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.Collections.Generic;


public class player : MonoBehaviour
{
    [SerializeField] private RotateObject Dice;
    [SerializeField] private pawn Pawn;
    [SerializeField] private boardgame Board;
    [SerializeField] private warning txt_warning;

    private int score;
    List<int> star;
    List<int> obs;
    
    // [SerializeField] private Button diceButton;
    
    private void Start()
    {
        score = 0;
        star = Board.get_star();
        obs = Board.get_obs();
        Debug.Log(obs);
        Dice.OnDiceStopped += MovePawn; 
    }
    

    
    private void MovePawn()
    {
        
        int currentPos = Pawn.GetPos();
        int targetPos = currentPos + Dice.getFace();
        if(obs.Contains(targetPos)){
            txt_warning.ShowText("Boom! You’re sent back to start!!!");
            targetPos = 0;
        }
        // if(star.Contains(targetPos)){
        //     score
        // }
        // Debug.Log(currentPos);
        // Debug.Log(targetPos);
        Pawn.MovePawn(Board.GetTile(targetPos), targetPos);
        
    }
}
