using UnityEngine;

namespace Menus {
public class Play : Menu {
    public static Play Instance = null;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 Play Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("Play Menu Created!");
    }

    public void OnNormalButton() {
        TurnOff();
        CraftSelect.Instance.TurnOn(this);
    }

    public void OnBullHellButton() {
        TurnOff();
        CraftSelect.Instance.TurnOn(this);
    }

    public void OnBackButton() {
        TurnOff(true);
    }
}
}
