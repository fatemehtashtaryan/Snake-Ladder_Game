using TMPro;
using UnityEngine;

public class warning : MonoBehaviour
{
    public TMP_Text alert;
    public TMP_Text score_txt;

    public void ShowText(string message, float duration = 8f)
    {
        StartCoroutine(ShowTextCoroutine(message, duration));
    }

    private System.Collections.IEnumerator ShowTextCoroutine(string message, float duration, Color color)
    {
        alert.gameObject.SetActive(true);
        alert.text = message;
        alert.color = color;

        yield return new WaitForSeconds(duration);

        alert.gameObject.SetActive(false);
    }
    private System.Collections.IEnumerator ShowTextCoroutine(int score)
    {
        score_txt.text = "YOUR SCORE = "+score;
        alert.color = color;

        yield return new WaitForSeconds(duration);

        alert.gameObject.SetActive(false);
    }
}
