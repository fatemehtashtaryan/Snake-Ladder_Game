using UnityEngine;
using UnityEngine.UI;

public class BoardUIManager : MonoBehaviour
{
    public Sprite specialSprite;
    public int randomCount = 5;
    private Image[] cells;

    void Start()
    {
        cells = GetComponentsInChildren<Image>();

        for (int i = 0; i < randomCount; i++)
        {
            int randIndex = Random.Range(0, cells.Length);
            cells[randIndex].sprite = specialSprite;
        }
    }
}
