using UnityEngine;

public class SceneChanger : MonoBehaviour{
    [SerializeField] private Button button;

    void Start(){
        button.onClick.AddListener(ChangeScene);
    }

    private void ChangeScene(){
        SceneManager.LoadScene("TargetScene") ;
    }

    
}



