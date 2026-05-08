using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private TMP_Text tileIndex;
    [SerializeField] private Transform spawnSnakePlace;
    [SerializeField] private Transform spawnLadderPlace;
    [SerializeField] private Image tileImage;

    private int index;
    private RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Initialize(int index)
    {
        tileIndex.text = index.ToString();
        this.index = index;
        SetRandomColor();
    }

    private void SetRandomColor()
    {
        if (tileImage == null)
            tileImage = GetComponent<Image>();

        if (tileImage != null)
        {
            Color randomColor = new Color(
                Random.Range(0f,1f),
                Random.Range(0f,1f),
                Random.Range(0f,1f),
                1f
            );

            tileImage.color = randomColor;
        }
    }

    public void SetTileIndexColor(Color c)
    {
        tileIndex.color = c;
    }

    public RectTransform GetTileTransform()
    {
        return rect;
    }

    public int GetIndex()
    {
        return index;
    }

    public Transform GetSnakeSpawnTransform()
    {
        return spawnSnakePlace;
    }

    public Transform GetLadderSpawnTransform()
    {
        return spawnLadderPlace;
    }
}
