using UnityEngine;
using TMPro;

public class Tile : MonoBehaviour
{
    [SerializeField] private Transform tileTransform;
    [SerializeField] private TMP_Text tileIndex;

    public Transform GetTileTransform(){
        return tileTransform;
    }

}