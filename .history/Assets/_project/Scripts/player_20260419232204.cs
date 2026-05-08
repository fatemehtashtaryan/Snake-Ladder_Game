using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections;





public class player : MonoBehaviour
{
    [SerializeField] private RotateObject Dice;
    [SerializeField] private pawn Pawn;
    [SerializeField] private boardgame Board;
    [SerializeField] private warning txt_warning;
    public AudioSource stepAudio;
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
        StartCoroutine(FireworkShow(new Vector3(0, -514, 997)));

    }
    
    IEnumerator FireworkShow(Vector3 center)
{
    for (int i = 0; i < 6; i++)
    {
        Vector3 randomPos = center ;
        // +
        //  new Vector3(
            // Random.Range(-2f, 2f),
            // Random.Range(1f, 3f),
            // Random.Range(-2f, 2f)
        // );

        SpawnFirework(randomPos);
        yield return new WaitForSeconds(0.25f);
    }
}
public void SpawnFirework(Vector3 pos)
{
    pos.z += 997f; 
    GameObject fw = Instantiate(fireworkPrefab, new Vector3(0, -514, 997), Quaternion.identity);
    Destroy(fw, 210f);
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
