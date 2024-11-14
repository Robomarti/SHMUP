using UnityEngine;

namespace Menus {
public class Credits : Menu {
    public static Credits Instance = null;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 Credits Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("Credits Menu Created!");
    }
    
    public void OnBackButton() {
        TurnOff(true);
    }
}
}
