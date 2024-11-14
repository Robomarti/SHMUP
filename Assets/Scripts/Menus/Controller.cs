using TMPro;
using UnityEngine;

namespace Menus {
public class Controller : Menu {
    public static Controller Instance;
    public int whichPlayer = 0;
    public TMP_Text whichPlayerText;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 Controller Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("Controller Menu Created!");
    }

    private void Update() {
        if (!ROOT.gameObject.activeSelf) return;
        if (InputManager.Instance.CheckForPlayerInput(whichPlayer)) {
            TurnOff();
        }
    }
}
}
