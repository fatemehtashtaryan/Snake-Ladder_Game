using UnityEngine;


public class Tile : MonoBehaviour
{
    [SerializeField] private Transform tileTransform;
    [SerializeField] private Image tileImage;

    public Transform GetTileTransform(){
        return tileTransform;
    }

    public void SetSprite(Sprite sprite)
    {
        tileImage.sprite = sprite;
    }

}