using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] private Transform tileTransform;
    [SerializeField] private TMP_Text tileIndex;
    [SerializeField] private Transform ElementPlace;

    private int index;

    public void Initialize(int index){
        tileIndex. text = index. ToString() ;
        this.index = index;
    }
    public Transform GetTileTransform(){
        return tileTransform;
    }
    public int GetIndex(){
        return index;
    }
    public Transform GetElementSpawnTransform()
    {
        return ElementPlace;
    }
        

}

