using UnityEngine;

namespace Menus {
public class AudioOptions : Menu {
    public static AudioOptions Instance = null;
    
    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 AudioOptions Menu!");
            Destroy(this);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("AudioOptions Menu Created!");
    }
    
    public void OnBackButton() {
        TurnOff(true);
    }
}
}