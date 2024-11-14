using UnityEngine;

namespace Menus {
public class Replays : Menu {
    public static Replays Instance = null;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 Replays Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("Replays Menu Created!");
    }
    
    public void OnBackButton() {
        TurnOff(true);
    }
}
}
