using UnityEngine;

namespace Menus {
public class Pause : Menu {
    public static Pause Instance = null;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 Pause Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("Pause Menu Created!");
    }
}
}
