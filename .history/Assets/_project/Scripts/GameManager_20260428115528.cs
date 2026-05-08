using NUnit.Framework;
using System. Collections. Generic;
using TMPro;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public List<player> players;
    [SerializeField] private TMP_Text turnText;
    [SerializeField] private Color[] playerColors; 
    // [SerializeField] private GameOverPanel WinBoard;
    public static GameManager Instance;
    private int turnIndex;
    

    

    private void Start()
    {
        Instance = this;
        turnIndex = 0;
        ActivePlayerTurn();
    }
    

    public void NextTurn(){
        // checkGameOver();
        
        turnIndex++;
        turnIndex%=players.Count;
        ActivePlayerTurn();

    }

    
    private void ActivePlayerTurn()
    {   
        for(int i = 0; i < players. Count; i++){
            if(i == turnIndex) players[i].Activate(true);
            else players[i].Activate(false);
        }

        
        // turnText. SetText($"Turn : {turnIndex +1}");
        Tile[] tiles = Object.FindObjectsByType<Tile>(FindObjectsSortMode.None);
        foreach (Tile t in tiles)
        {
            t.SetTileIndexColor(playerColors[turnIndex]);
        }
    }
    // private void checkGameOver(){
    //     if(players[turnIndex].reachedLastTile()){
    //         WinBoard.gameObject.SetActive(true);
    //         WinBoard.SetGameOverText(turnIndex+1);
    //     }
    // }
    
}


