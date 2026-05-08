using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class boardgame : MonoBehaviour
{
    public int randomCount = 5;
    public Sprite specialSprite_obs;
    public Sprite specialSprite_star;

    // private Image[] cells;

    private List<int> stars;
    private List<int> obs;
    private List<(int, int)> specials;

    private HashSet<int> usedIndexes = new HashSet<int>();
    [SerializeField] private List<Tile> Tiles;
    [SerializeField] private GameObject specialTile;

    void Start()
    {
        // cells = GetComponentsInChildren<Image>();

        stars = new List<int>();
        obs = new List<int>();
        specials = new List<(int, int)>();

        // for (int i = 0; i < randomCount; i++)
        // {
        //     int rand = GetUniqueRandomIndex();

        //     cells[rand].sprite = specialSprite_obs;
        //     obs.Add(rand);
        // }
        for (int i = 0; i < randomCount; i++)
        {
            int rand = GetUniqueRandomIndex();

            // Tiles[rand].sprite = specialSprite_star;
            Tiles[rand].SetSprite(specialSprite_star);

            stars.Add(rand);
        }
        for (int i = 0; i < randomCount; i++)
        {
            int rand = GetUniqueRandomIndex();
            int forwardedIndex = Random.Range(1, Tiles.Count);
            specials.Add((rand, forwardedIndex));
            // Debug.Log(cells.Length + " ffff");
            GameObject spawnSpecialTile = Instantiate(specialTile , Tiles[rand].GetTileTransform());
            spawnSpecialTile.transform.localPosition = Vector3.zero;
            
            }
            Debug.Log(string.Join("| ", specials.Item1));
    }

    private int GetUniqueRandomIndex()
    {
        int rand;
        do
        {
            rand = Random.Range(1, Tiles.Count);
        }
        while (usedIndexes.Contains(rand));

        usedIndexes.Add(rand);
        return rand;
    }
    
    public Transform GetTile(int Index){
        // Debug.Log("Index: " + Index + ", List Count: " + Tiles.Count);
        return Tiles[Index].GetTileTransform();
    }

    public List<int> get_obs(){
        return obs;
    }
    public List<int> get_star(){
        return stars;
    }
    public int Is_Specials(int index){
        for (int i = 0; i<specials.Count; i++){
            if(index == specials[i].Item1){
                return specials[i].Item2;
            }
        }
        return -1;
    }
}
