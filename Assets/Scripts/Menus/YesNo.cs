using UnityEngine;

namespace Menus {
public class YesNo : Menu {
    public static YesNo Instance = null;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 YesNo Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("YesNo Menu Created!");
    }
    
    public void OnNoButton() {
        // Go back
        TurnOff(true);
    }
}
}
