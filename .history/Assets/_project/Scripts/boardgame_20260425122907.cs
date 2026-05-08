using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;

public class boardgame : MonoBehaviour
{
    public int randomCount_Spacial = 3;
    public int randomCount_Element = 3;
    public Sprite specialSprite_obs;
    public Sprite specialSprite_star;

    // private Image[] cells;

    private List<int> stars;
    private List<int> obs;
    private List<(int, int)> specials;


    private HashSet<int> usedIndexes = new HashSet<int>();

    [SerializeField] private List<Tile> Tiles;
    [SerializeField] private List<Tile> orderedTile;
    [SerializeField] private List<BoardElementConfig> elements;

    [SerializeField] private GameObject SnakePrefab;
    [SerializeField] private GameObject SnakePrefab_2;
    [SerializeField] private GameObject SnakePrefab_3;
    [SerializeField] private GameObject SnakePrefab_4;
    [SerializeField] private GameObject LadderPrefab;
    [SerializeField] private GameObject specialTile;
    [SerializeField] private GameObject starTile;
    private List<int> ladders_index;

    void Start()
    {
        
        for(int i = 0; i < Tiles. Count; i++){
             Tiles[i].Initialize(i);
        }
        stars = new List<int>();
        obs = new List<int>();
        ladders_index = new List<int>();
        // specials = new List<(int, int)>();
        for(int i = 0; i < randomCount_Element; i++)
        {
            SpawnLadderRandom();
        }
        // SpawnSnakeRandom(2, 2, 5, 6, SnakePrefab);
        SpawnSnakeRandom(1, 2, 4, 4, SnakePrefab_2);
        // SpawnSnakeRandom(2, 2, 5, 6, SnakePrefab_4);
        // SpawnSnakeRandom(2, 3, 5, 11, SnakePrefab_3);


        for (int i = 0; i < randomCount_Spacial; i++)
        {
            int rand = GetUniqueRandomIndex(1, Tiles.Count,1);
            GameObject spawnStarTile = Instantiate(starTile , Tiles[rand].GetTileTransform());
            spawnStarTile.transform.localPosition = Vector3.zero;
            stars.Add(rand);
            Debug.Log(rand+"**************************");
        }
        for (int i = 0; i < randomCount_Spacial; i++)
        {
            int rand = GetUniqueRandomIndex(1, Tiles.Count, 1);
            int forwardedIndex = GetUniqueRandomIndex(1, Tiles.Count, 0);
            // specials.Add((rand, forwardedIndex));
            GameObject spawnSpecialTile = Instantiate(specialTile , Tiles[rand].GetTileTransform());
            SpecialConfig config = spawnSpecialTile.transform.GetComponent<SpecialConfig>();
            spawnSpecialTile.transform.localPosition = Vector3.zero;
            config.Initialize(rand, forwardedIndex);
            elements.Add(config);
        }
    }

    private int GetUniqueRandomIndex(int firstrange, int endrange, int type)
    {
        int rand;
        if (type==1){
            do
            {
                rand = Random.Range(firstrange, endrange);
            }
            while (usedIndexes.Contains(rand));
            usedIndexes.Add(rand);
        }
        else
        {
            rand = Random.Range(firstrange, endrange);
        }
        return rand;
    }

    public (Transform targetTransform , int targetPosition) CheckElementsHit(int index){
        foreach (  BoardElementConfig element in elements)
        {
            Debug.Log(element.getHitInd()+ " d");
            if (element.IsHitIndex(index)){
                Debug.Log("in hit"+ element.IsForwardedIndex());
                return (Tiles[element.IsForwardedIndex()].GetTileTransform(),element.IsForwardedIndex()) ;
            }
        }
        return (null, 0);
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
   
    private void SpawnSnakeRandom(int firstRange_i, int firstRange_j, int coeff_j, int minus, GameObject SnakePrefab)
    {
        int head;

        do{
            int j = GetUniqueRandomIndex(firstRange_j, 10, 0);
            int i =  GetUniqueRandomIndex(firstRange_i, 5, 0);
            head = i + j*coeff_j;
            Debug.Log(i +"dd"+j);
        }while(ladders_index.Contains(head));
        int tale = head - minus;

        Tile tailTile = orderedTile[tale];
        Tile headTile = orderedTile[head];
        // Debug.Log(tailTile.GetIndex());
        int hitIndex = headTile.GetIndex();
        int forwardedIndex = tailTile.GetIndex();
        usedIndexes.Add(hitIndex);

        GameObject SpawnSnake = Instantiate(SnakePrefab, tailTile.GetSnakeSpawnTransform());
        SnakeConfig config = SpawnSnake.transform.GetComponent<SnakeConfig>();

        config.Initialize(hitIndex, forwardedIndex);

        elements.Add(config);

    }

    private void SpawnLadderRandom()
    {
        int j = GetUniqueRandomIndex(1, 10, 0);
        int i =  GetUniqueRandomIndex(1, 6, 0);

        int head = i + j*4;
        int tale = head - 5;

        ladders_index.Add(head);
        ladders_index.Add(tale);

        Tile tailTile = orderedTile[tale];
        Tile headTile = orderedTile[head];
        Debug.Log(tailTile.GetIndex());
        int hitIndex = tailTile.GetIndex();
        int forwardedIndex = headTile.GetIndex();
        usedIndexes.Add(hitIndex);

        GameObject SpawnLadder = Instantiate(LadderPrefab, tailTile.GetLadderSpawnTransform());
        LadderConfig config = SpawnLadder.transform.GetComponent<LadderConfig>();

        config.Initialize(hitIndex, forwardedIndex);

        elements.Add(config);

    }
   


}
