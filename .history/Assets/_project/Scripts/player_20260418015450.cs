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
    public GameObject fireworkPrefab;

    private int score;
    List<int> star;
    List<int> obs;
    
    // [SerializeField] private Button diceButton;
    
    private void Start()
    {
        score = 0;
        star = Board.get_star();
        Debug.Log(string.Join("| ", star));
        Dice.OnDiceStopped += MovePawn; 
        txt_warning.ShowText_score(score);
    }
    
    public void SpawnFirework(Vector3 pos)
{
    GameObject fw = Instantiate(fireworkPrefab, pos, Quaternion.identity);
    Destroy(fw, 2f);
}
    
    private void MovePawn()
    {
        
        int currentPos = Pawn.GetPos();
        int targetPos = currentPos + Dice.getFace();
        targetPos = checkTile_situation(targetPos);
        Pawn.MovePawn(Board.GetTile(targetPos), targetPos);
        if(targetPos != Pawn.GetPos()){
            targetPos = checkTile_situation(targetPos);
        }
        if(Pawn.GetPos() != targetPos){
            MovePawn();
        }
        Debug.Log(targetPos + " ffffffffffffffffffffffffffffffffffffff");
        
    }

    private int checkTile_situation(int targetPos){
        int specialTile = Board.Is_Specials(targetPos);
        Debug.Log(targetPos + " ^ "+ specialTile);
        if(specialTile != -1){
            if(specialTile>targetPos){
                txt_warning.ShowText("Forward leap! Enjoy the advantage!", 5f, Color.green);
            }
            if(specialTile<targetPos){
                txt_warning.ShowText("Oops… You fell behind!", 5f, Color.red);
            }
            targetPos = specialTile;
            Debug.Log(targetPos + " kkkkkkkkkkkkkkkkkkkkkkkkkkkkkkk");
        }
        else if(star.Contains(targetPos)){
            score+=1;
            txt_warning.ShowText("Awesome! Your score just went up!!!", 5f, Color.green);
            txt_warning.ShowText_score(score);
        }
        return targetPos;
    }
}
