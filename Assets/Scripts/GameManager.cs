using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance = null;

    public bool twoPlayer = false;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 GameManager!");
            Destroy(this);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("GameManager Created!");
    }
}
