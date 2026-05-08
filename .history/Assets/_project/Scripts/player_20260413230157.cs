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
        Debug.Log(string.Join("| ", star));
        Dice.OnDiceStopped += MovePawn; 
        txt_warning.ShowText_score(score);
    }
    

    
    private void MovePawn()
    {
        
        int currentPos = Pawn.GetPos();
        int targetPos = currentPos + Dice.getFace();
        int target_reassign = targetPos;
        if (target_reassign % 10 == 5){target_reassign=target_reassign + 4;Debug.Log("4444 " +target_reassign);}
        else if (target_reassign % 10 == 6){target_reassign=target_reassign + 3;Debug.Log("3333 " +target_reassign);}
        else if (target_reassign % 10 == 9){target_reassign=target_reassign -4;Debug.Log("--4444 " +target_reassign);}
        else if (target_reassign % 10 == 8){target_reassign=target_reassign-3;Debug.Log("++4444 " +target_reassign);}

        if(obs.Contains(target_reassign)){
            txt_warning.ShowText("Boom! You’re sent back to start!!!", 8f, Color.red);
            targetPos = 0;
            Debug.Log("fffff");
        }
        if(star.Contains(target_reassign)){
            score+=1;
            txt_warning.ShowText("Awesome! Your score just went up!!!", 8f, Color.green);
            txt_warning.ShowText_score(score);
            
        }

        // if(star.Contains(targetPos)){
        //     score
        // }
        // Debug.Log(currentPos);
        // Debug.Log(target_reassign);
        Pawn.MovePawn(Board.GetTile(targetPos), targetPos);

        
    }
}
