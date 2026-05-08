using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [SerializeField] private Transform tileTransform;
    [SerializeField] private TMP_Text tileIndex;
    [SerializeField] private Transform spawnSnakePlace;
    [SerializeField] private Transform spawnLadderPlace;
    [SerializeField] private Image tileImage;

    private int index;

    private readonly Vector2[] hueRanges = new Vector2[]
    {
        new Vector2(0.0f, 0.05f),   // قرمز
        new Vector2(0.9f, 0.95f),   // صورتی
        new Vector2(0.55f, 0.70f),  // آبی
        new Vector2(0.12f, 0.18f),  // زرد
        new Vector2(0.25f, 0.45f),  // سبز
        new Vector2(0.75f, 0.90f),  // بنفش
        new Vector2(0.05f, 0.12f)   // نارنجی
    };

    public void Initialize(int index)
    {
        tileIndex.text = index.ToString();
        this.index = index;

        // انتخاب یک رنگ اصلی (خانواده رنگ)
        int pickedHue = Random.Range(0, hueRanges.Length);

        // انتخاب Hue تصادفی داخل بازه
        float h = Random.Range(hueRanges[pickedHue].x, hueRanges[pickedHue].y);

        // مقدار اشباع و روشنایی با کمی رندوم (طیف زیباتر)
        float s = Random.Range(0.6f, 1f);
        float v = Random.Range(0.7f, 1f);

        Color randomColor = Color.HSVToRGB(h, s, v);

        tileTransform.GetComponent<Image>().color = randomColor;
    }
    private void SetRandomColor()
    {
        if (tileImage == null)
            tileImage = GetComponent<Image>(); 

        if (tileImage != null)
        {
            Color randomColor = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                1f
            );

            tileImage.color = randomColor;
        }
    }
    public Transform GetTileTransform(){
        return tileTransform;
    }
    public int GetIndex(){
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

