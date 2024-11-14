using UnityEngine;

namespace Menus {
public class Options : Menu {
    public static Options Instance = null;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 Options Menu!");
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("Options Menu Created!");
    }
    
    public void OnGraphicsButton() {
        TurnOff();
        GraphicsOptions.Instance.TurnOn(this);
    }
    
    public void OnAudioButton() {
        TurnOff();
        AudioOptions.Instance.TurnOn(this);
    }
    
    public void OnControlsButton() {
        TurnOff();
        ControlsOptions.Instance.TurnOn(this);
    }
    
    public void OnBackButton() {
        TurnOff(true);
    }
}
}
