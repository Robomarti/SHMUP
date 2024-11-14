using UnityEngine;

namespace Menus {
public class TitleScreen : Menu {
    public static TitleScreen Instance = null;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 TitleScreen Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("TitleScreen Menu Created!");
    }

    private void Update() {
        if (!ROOT.gameObject.activeSelf) return;
        if (InputManager.Instance.CheckForPlayerInput(0)) {
            TurnOff();
            Main.Instance.TurnOn(this);
        }
    }
}
}
