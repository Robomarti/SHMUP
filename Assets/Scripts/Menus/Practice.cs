using UnityEngine;

namespace Menus {
public class Practice : Menu {
    public static Practice Instance = null;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 Practice Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("Practice Menu Created!");
    }
    
    public void OnStageModeButton() {
        TurnOff();
        PracticeStage.Instance.TurnOn(this);
    }
    
    public void OnArenaModeButton() {
        TurnOff();
        PracticeArena.Instance.TurnOn(this);
    }
    
    public void OnBackButton() {
        TurnOff(true);
    }
}
}
