using UnityEngine;

namespace Menus {
public class CraftSelect : Menu {
    public static CraftSelect Instance = null;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 CraftSelect Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("CraftSelect Menu Created!");
    }
    
    public void OnBackButton() {
        TurnOff(true);
    }
}
}
