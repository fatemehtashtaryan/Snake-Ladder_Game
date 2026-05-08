using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class boardgame : MonoBehaviour
{
    public int randomCount = 5;
    public Sprite specialSprite_obs;
    public Sprite specialSprite_star;

    private Image[] cells;

    private List<int> stars;
    private List<int> obs;

    private HashSet<int> usedIndexes = new HashSet<int>();

    void Start()
    {
        cells = GetComponentsInChildren<Image>();

        stars = new List<int>();
        obs = new List<int>();

        for (int i = 0; i < randomCount; i++)
        {
            int rand = GetUniqueRandomIndex();

            cells[rand].sprite = specialSprite_obs;
            obs.Add(rand);
        }
        for (int i = 0; i < randomCount; i++)
        {
            int rand = GetUniqueRandomIndex();

            cells[rand].sprite = specialSprite_star;
            stars.Add(rand);
        }
    }

    private int GetUniqueRandomIndex()
    {
        int rand;
        do
        {
            rand = Random.Range(1, cells.Length);
        }
        while (usedIndexes.Contains(rand));

        usedIndexes.Add(rand);
        return rand;
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
