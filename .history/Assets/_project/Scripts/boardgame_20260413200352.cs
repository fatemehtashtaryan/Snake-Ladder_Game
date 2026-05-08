
using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class boardgame : MonoBehaviour
{
    public int randomCount = 3;
    public Sprite specialSprite_obs;
    public Sprite specialSprite_star;
    private Image[] cells;
    private List<int> stars;
    private List<int> obs;
    [SerializeField] private List<Transform> Tiles;

    void Start()
    {
        cells = GetComponentsInChildren<Image>();
        stars = new List<int>();
        obs = new List<int>();

        List<Image> cellsList = new List<Image>(cells);

        for (int i = 0; i < randomCount; i++)
        {
            int randIndex = Random.Range(0, cellsList.Count);
            Image selectedCell = cellsList[randIndex];
            cells[randIndex].sprite = specialSprite_obs;
            cellsList.RemoveAt(randIndex);
            obs.Add(randIndex);

        }
        for (int i = 0; i < randomCount; i++)
        {
            int randIndex = Random.Range(0, cellsList.Count);

            Image selectedCell = cellsList[randIndex];
            cells[randIndex].sprite = specialSprite_star;
            cellsList.RemoveAt(randIndex);
            stars.Add(randIndex);
        }
    }
    
    public Transform GetTile(int Index){
        Debug.Log("Index: " + Index + ", List Count: " + Tiles.Count);
        return Tiles[Index];
    }
    public List<int> get_obs(){
        return obs;
    }
    public List<int> get_star(){
        return stars;
    }
}
