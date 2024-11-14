using UnityEngine;

namespace Menus {
    public class PracticeArena : Menu {
        public static PracticeArena Instance = null;

        private void Start() {
            if (Instance) {
                Debug.LogError("Trying to instantiate more than 1 PracticeArena Menu!");
                Destroy(this);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("PracticeArena Menu Created!");
        }
    }
}
