using UnityEngine;

public class DiceSetup : MonoBehaviour
{
    [SerializeField] private MeshRenderer diceRenderer;

    public void Setup(Material material)
    {
        diceRenderer.material = material;
    }
}
