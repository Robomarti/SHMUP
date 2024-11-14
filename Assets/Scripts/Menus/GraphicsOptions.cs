using UnityEngine;

namespace Menus {
public class GraphicsOptions : Menu {
    public static GraphicsOptions Instance = null;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 GraphicsOptions Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("GraphicsOptions Menu Created!");
    }
    
    public void OnApplyButton() {
    }
    
    public void OnBackButton() {
        TurnOff(true);
    }
}
}
