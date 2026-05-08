using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour{
    [SerializeField] private Button START_GAME;

    void Start(){
        START_GAME.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene(){
        SceneManager.LoadScene("projectt") ;
    }

    
}



