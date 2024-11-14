using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Menus {
public class ControlsOptions : Menu {
    public static ControlsOptions Instance = null;

    public Button[] player1Buttons = new Button[8];
    public Button[] player2Buttons = new Button[8];
    public Button[] player1Keys = new Button[12];
    public Button[] player2Keys = new Button[12];

    public GameObject bindingPanel;
    public TMP_Text bindingText;
    public EventSystem eventSystem;

    private bool bindingKey;
    private bool bindingAxis;
    private bool bindingButton;

    private int actionBinding = -1;
    private int playerBinding = -1;

    private bool waiting;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 ControlsOptions Menu!");
            Destroy(this);
            return;
        }
    
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("ControlsOptions Menu Created!");
    }

    private void Update() {
        if (!(bindingKey || bindingButton)) return;

        if (waiting) {
            if (Input.anyKey) return;
            if (InputManager.Instance.DetectButtonPress() > -1) return;
            
            waiting = false;
        }
        
        if (bindingKey) {
            foreach (KeyCode key in KeyCode.GetValues(typeof(KeyCode))) {
                if (!key.ToString().Contains("Joystick")) {
                    if (Input.GetKeyDown(key)) {
                        if (bindingAxis) {
                            InputManager.Instance.BindPlayerAxisKey(playerBinding, actionBinding, key);
                            bindingPanel.SetActive(false);
                
                            bindingKey = false;
                            bindingButton = false;
                            bindingAxis = false;
                
                            eventSystem.gameObject.SetActive(true);
                            UpdateButtons();
                        }
                        else {
                            InputManager.Instance.BindPlayerKey(playerBinding, actionBinding, key);
                            bindingPanel.SetActive(false);
                
                            bindingKey = false;
                            bindingButton = false;
                            bindingAxis = false;
                
                            eventSystem.gameObject.SetActive(true);
                            UpdateButtons();
                        }
                    }
                }
            }
        }
        else if (bindingButton) {
            int button = InputManager.Instance.DetectButtonPress();
            if (button > -1) {
                InputManager.Instance.BindPlayerButton(playerBinding, actionBinding, (byte)button);
                bindingPanel.SetActive(false);
                
                bindingKey = false;
                bindingButton = false;
                bindingAxis = false;
                
                eventSystem.gameObject.SetActive(true);
                UpdateButtons();
            }
        } 
    }
    
    public void OnBackButton() {
        TurnOff(true);
    }

    private void OnEnable() {
        UpdateButtons();
    }

    private void UpdateButtons() {
        // controller buttons
        for (int i = 0; i < 8; i++) {
            player1Buttons[i].GetComponentInChildren<TMP_Text>().text = InputManager.Instance.GetButtonName(0, i);
            player2Buttons[i].GetComponentInChildren<TMP_Text>().text = InputManager.Instance.GetButtonName(1, i);
        }
        
        // key buttons
        for (int i = 0; i < 8; i++) {
            player1Keys[i].GetComponentInChildren<TMP_Text>().text = InputManager.Instance.GetKeyName(0, i);
            player2Keys[i].GetComponentInChildren<TMP_Text>().text = InputManager.Instance.GetKeyName(1, i);
        }
        
        // key axis
        for (int i = 0; i < 4; i++) {
            player1Keys[i+8].GetComponentInChildren<TMP_Text>().text = InputManager.Instance.GetKeyAxisName(0, i);
            player2Keys[i+8].GetComponentInChildren<TMP_Text>().text = InputManager.Instance.GetKeyAxisName(1, i);
        }
    }

    public void BindP1Key(int actionID) {
        eventSystem.gameObject.SetActive(false);
        bindingText.text = "Press a key for player 1 " + InputManager.actionNames[actionID];
        bindingPanel.SetActive(true);
        
        bindingKey = true;
        bindingAxis = false;
        bindingButton = false;
        playerBinding = 0;
        actionBinding = actionID;
        waiting = true;
    }
    
    public void BindP1AxisKey(int actionID) {
        eventSystem.gameObject.SetActive(false);
        bindingText.text = "Press a key for player 1 " + InputManager.axisNames[actionID];
        bindingPanel.SetActive(true);
        
        bindingKey = true;
        bindingAxis = true;
        bindingButton = false;
        playerBinding = 0;
        actionBinding = actionID;
        waiting = true;
    }
    
    public void BindP1Button(int actionID) {
        eventSystem.gameObject.SetActive(false);
        bindingText.text = "Press a button for player 1 " + InputManager.actionNames[actionID];
        bindingPanel.SetActive(true);
        
        bindingKey = false;
        bindingAxis = false;
        bindingButton = true;
        playerBinding = 0;
        actionBinding = actionID;
        waiting = true;
    }
    
    public void BindP2Key(int actionID) {
        eventSystem.gameObject.SetActive(false);
        bindingText.text = "Press a key for player 2 " + InputManager.actionNames[actionID];
        bindingPanel.SetActive(true);
        
        bindingKey = true;
        bindingAxis = false;
        bindingButton = false;
        playerBinding = 1;
        actionBinding = actionID;
        waiting = true;
    }
    
    public void BindP2AxisKey(int actionID) {
        eventSystem.gameObject.SetActive(false);
        bindingText.text = "Press a key for player 2 " + InputManager.axisNames[actionID];
        bindingPanel.SetActive(true);
        
        bindingKey = true;
        bindingAxis = true;
        bindingButton = false;
        playerBinding = 1;
        actionBinding = actionID;
        waiting = true;
    }
    
    public void BindP2Button(int actionID) {
        eventSystem.gameObject.SetActive(false);
        bindingText.text = "Press a key for player 2 " + InputManager.actionNames[actionID];
        bindingPanel.SetActive(true);
        
        bindingKey = false;
        bindingAxis = false;
        bindingButton = true;
        playerBinding = 1;
        actionBinding = actionID;
        waiting = true;
    }
}
}
