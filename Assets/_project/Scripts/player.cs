using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    [SerializeField] private RotateObject Dice;
    [SerializeField] private pawn Pawn;
    [SerializeField] private boardgame Board;
    [SerializeField] private warning txt_warning;
    [SerializeField] private GameObject diceRenderer;
    [SerializeField] private TMP_Text score_txt;
    [SerializeField] private Button Home_Button;

    public GameObject fireworkPrefab;

    private int score;
    List<int> star;
    List<int> obs;
    
    private void Start()
    {
        score = 0;
        star = Board.get_star();
        Dice.OnDiceStopped += MovePawn; 
        score_txt.text = score.ToString();
        Home_Button.onClick.AddListener(BackHome);

    }

    public void Activate(bool active)
    {
         diceRenderer.SetActive(active);
    }

    private void BackHome()
    {
        SceneManager.LoadScene("Home") ;
    }

    
    private void MovePawn()
    {
        StartCoroutine(MovePawnSteps());
    }

    IEnumerator MovePawnSteps()
    {
        int current = Pawn.GetPos();
        int steps = Dice.getFace();

        for (int i = 0; i < steps; i++)
        {
            int nextIndex = current + 1;

            RectTransform nextTile = Board.GetTile(nextIndex);
            Pawn.MoveStep(nextTile, nextIndex);

            yield return new WaitForSeconds(0.35f);

            current = nextIndex;
        }
        // yield return StartCoroutine(checkTile_situation(current));
        bool somethingHappened = true;
        while (somethingHappened)
        {
            somethingHappened = false;
            yield return StartCoroutine(checkTile_situation(Pawn.GetPos()));

            yield return new WaitForSeconds(0.3f);
            if (CheckForHit())
            {
                somethingHappened = true;
                yield return new WaitForSeconds(0.4f);
            }
        }
        // yield return StartCoroutine(checkTile_situation(Pawn.GetPos()));
        GameManager.Instance.NextTurn(); 
        
    }


    private IEnumerator checkTile_situation(int targetPos)
    {
        int finalPos = targetPos;
        if (star == null) {star= Board.get_star();}  
        
        if (star.Contains(targetPos))
        {
            score += 1;
            txt_warning.ShowText("Awesome! Your score just went up!!!", 5f, Color.green);
            score_txt.text = score.ToString();
        }
        if (finalPos != targetPos)
        {
            RectTransform jumpTile = Board.GetTile(finalPos);
            Pawn.MoveStep(jumpTile, finalPos);

            yield return new WaitForSeconds(0.4f);
        }
        bool hasAlreadyReturned = false;

        foreach (var p in GameManager.Instance.players)
        {
            if (p.Pawn == this.Pawn)
                continue;
            else if (!hasAlreadyReturned && p.Pawn.GetPos() == this.Pawn.GetPos())
            {
                Debug.Log("mmmmmmmmmmmmmmmmm");

                hasAlreadyReturned = true;   // اولین و آخرین بار

                p.Pawn.ReturnToStart();
                yield break;
            }

        }
    }

    private bool CheckForHit()
    {
        int currentPawnPosition = Pawn.GetPos();
        var res = Board.CheckElementsHit(currentPawnPosition);

        if (res.targetTransform != null)
        {
            if(res.targetPosition>currentPawnPosition)
            {
                txt_warning.ShowText("Forward leap! Enjoy the advantage!", 5f, Color.green);
            }
            else
            {
                txt_warning.ShowText("Oops… You fell behind!", 5f, Color.red);
            }
            if(res.blackHole)
            {
                AudioManager.Instance.Play_BlackHole_Audio();
                Pawn.MoveStep_blackHole(res.targetTransform, res.targetPosition);
            }
            
            else
            {
                Pawn.MoveStep(res.targetTransform, res.targetPosition);
            }
            return true;
        }
        return false;
    }
    
    public bool reachedLastTile(){
        Debug.Log(Pawn.GetPos() + "fff" + Board.getBoardLength());
        return Pawn.GetPos()>=Board.getBoardLength()-1;
    }

}
