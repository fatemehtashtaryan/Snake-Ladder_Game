using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] private Transform tileTransform;
    [SerializeField] private TMP_Text tileIndex;

    public void Initialize(int index){
        tileIndex. text = index. ToString() ;
    }
    public Transform GetTileTransform(){
        return tileTransform;
    }

}