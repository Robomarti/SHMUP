using UnityEngine;

namespace Menus {
public class Main : Menu {
    public static Main Instance;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 Main Menu!");
            Destroy(this);
            return;
        }
      
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("Main Menu Created!");
    }

    public void OnPlayButton() {
        TurnOff();
        Play.Instance.TurnOn(this);
    }
    
    public void OnPracticeButton() {
        TurnOff();
        Practice.Instance.TurnOn(this);
    }
    
    public void OnOptionsButton() {
        TurnOff();
        Options.Instance.TurnOn(this);
    }
    
    public void OnScoresButton() {
        TurnOff();
        Scores.Instance.TurnOn(this);
    }
    
    public void OnMedalsButton() {
        TurnOff();
        Medals.Instance.TurnOn(this);
    }
    
    public void OnReplaysButton() {
        TurnOff();
        Replays.Instance.TurnOn(this);
    }
    
    public void OnCreditsButton() {
        TurnOff();
        Credits.Instance.TurnOn(this);
    }
    
    public void OnQuitButton() {
        TurnOff();
        YesNo.Instance.TurnOn(this);
    }
}
}