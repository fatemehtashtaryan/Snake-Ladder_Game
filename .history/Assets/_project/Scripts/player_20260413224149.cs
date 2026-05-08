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
        Debug.Log(string.Join(", ", obs));
        Dice.OnDiceStopped += MovePawn; 
        txt_warning.ShowText_score(score);
    }
    

    
    private void MovePawn()
    {
        
        int currentPos = Pawn.GetPos();
        int targetPos = currentPos + Dice.getFace();
        int target_reassign = targetPos;
        if (target_reassign % 10 == 5){target_reassign+=4;}
        if (target_reassign % 10 == 6){target_reassign+=3;}
        if (target_reassign % 10 == 9){target_reassign-=4;}
        if (target_reassign % 10 == 8){target_reassign-=3;}
        if(obs.Contains(target_reassign)){
            txt_warning.ShowText("Boom! You’re sent back to start!!!");
            targetPos = 0;
        }
        if(star.Contains(targetPos)){
            score+=1;
            txt_warning.ShowText("Awesome! Your score just went up!!!");
            
        }

        // if(star.Contains(targetPos)){
        //     score
        // }
        // Debug.Log(currentPos);
        // Debug.Log(targetPos);
        Pawn.MovePawn(Board.GetTile(targetPos), targetPos);
        
    }
}
