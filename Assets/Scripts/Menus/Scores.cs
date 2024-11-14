using UnityEngine;

namespace Menus {
public class Scores : Menu {
    public static Scores Instance = null;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 Scores Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("Scores Menu Created!");
    }
    
    public void OnBackButton() {
        TurnOff(true);
    }
}
}
