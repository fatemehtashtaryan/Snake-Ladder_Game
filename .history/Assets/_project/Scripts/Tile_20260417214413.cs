using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] private Transform tileTransform;
    

    public Transform GetTileTransform(){
        return tileTransform;
    }

}