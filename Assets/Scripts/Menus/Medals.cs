using UnityEngine;

namespace Menus {
public class Medals : Menu {
    public static Medals Instance = null;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 Medals Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("Medals Menu Created!");
    }
    
    public void OnBackButton() {
        TurnOff(true);
    }
}
}
