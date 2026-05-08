using UnityEngine;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public int randomCount = 3;
    public Sprite specialSprite_obs;
    public Sprite specialSprite_star;

    void Start()
    {
        // پیدا کردن همه خانه‌ها
        GameObject[] allCells = GameObject.FindGameObjectsWithTag("Cell");

        List<GameObject> cellsList = new List<GameObject>(allCells);

        for (int i = 0; i < randomCount; i++)
        {
            int randIndex = Random.Range(0, cellsList.Count);

            GameObject selectedCell = cellsList[randIndex];

            var renderer = selectedCell.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.mainTexture = specialSprite_obs.texture;
            }

            cellsList.RemoveAt(randIndex);
        }
        for (int i = 0; i < randomCount; i++)
        {
            int randIndex = Random.Range(0, cellsList.Count);

            GameObject selectedCell = cellsList[randIndex];

            var renderer = selectedCell.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                renderer.material.mainTexture = specialSprite_star.texture;
            }

            cellsList.RemoveAt(randIndex);
        }
    }
}
