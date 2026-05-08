// using UnityEngine;
using UnityEngine.UI;

// public class BoardUIManager : MonoBehaviour
// {
//     public Sprite specialSprite_obs;
//     public Sprite specialSprite_star;
//     public int randomCount = 3;
//     private Image[] cells;

//     void Start()
//     {
//         cells = GetComponentsInChildren<Image>();

//         for (int i = 0; i < randomCount; i++)
//         {
//             int randIndex = Random.Range(0, cellsList.Count);

//             GameObject selectedCell = cellsList[randIndex];

//             var renderer = selectedCell.GetComponent<MeshRenderer>();
//             if (renderer != null)
//             {
//                 renderer.material.mainTexture = specialSprite_obs.texture;
//             }

//             cellsList.RemoveAt(randIndex);
//         }
//     }
// }

// using UnityEngine;
// using UnityEngine.UI;

// public class BoardUIManager : MonoBehaviour
// {
//     public Sprite specialSprite;
//     public int randomCount = 5;
//     private Image[] cells;

//     void Start()
//     {
//         // فرض می‌کنیم تمام خانه‌ها درون BoardGame قرار دارند
//         cells = GetComponentsInChildren<Image>();

//         for (int i = 0; i < randomCount; i++)
//         {
//             int randIndex = Random.Range(0, cells.Length);
//             cells[randIndex].sprite = specialSprite;
//         }
//     }
// }




using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public int randomCount = 3;
    public Sprite specialSprite_obs;
    public Sprite specialSprite_star;
    private Image[] cells;

    void Start()
    {
        cells = GetComponentsInChildren<Image>();

        List<Image> cellsList = new List<Image>(cells);

        for (int i = 0; i < randomCount; i++)
        {
            int randIndex = Random.Range(0, cellsList.Count);

            Image selectedCell = cellsList[randIndex];

            // var renderer = selectedCell.GetComponent<MeshRenderer>();
            // if (renderer != null)
            // {
            cells[randIndex].sprite = specialSprite_obs;
            // }

            cellsList.RemoveAt(randIndex);
        }
        for (int i = 0; i < randomCount; i++)
        {
            int randIndex = Random.Range(0, cellsList.Count);

            Image selectedCell = cellsList[randIndex];
            cells[randIndex].sprite = specialSprite_star;
            cellsList.RemoveAt(randIndex);

    }
}
