using UnityEngine;

namespace Menus {
public class PracticeStage : Menu {
    public static PracticeStage Instance = null;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 PracticeStage Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("PracticeStage Menu Created!");
    }
}
}
