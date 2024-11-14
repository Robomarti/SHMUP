using System;
using System.Collections;
using Menus;
using UnityEngine;

public class InputManager : MonoBehaviour {
    public static InputManager Instance = null;
    public InputState[] playerState = new InputState[2];
    public ButtonMapping[] playerButtons = new ButtonMapping[2];
    public AxisMapping[] playerAxis = new AxisMapping[2];
    public KeyButtonMapping[] playerKeyButtons = new KeyButtonMapping[2];
    public KeyAxisMapping[] playerKeyAxis = new KeyAxisMapping[2];
    
    public int[] playerController = new int[2];
    public bool[] playerUsingKeys = new bool[2];

    public const float DeadZone = 0.01f;
    
    private System.Array allKeyCodes = System.Enum.GetValues(typeof(KeyCode));

    private string[,] playerButtonNames = {
        { "J1_B1","J1_B2","J1_B3","J1_B4","J1_B5","J1_B6","J1_B7","J1_B8" },
        { "J2_B1","J2_B2","J2_B3","J2_B4","J2_B5","J2_B6","J2_B7","J2_B8" },
        { "J3_B1","J3_B2","J3_B3","J3_B4","J3_B5","J3_B6","J3_B7","J3_B8" },
        { "J4_B1","J4_B3","J4_B3","J4_B4","J4_B5","J4_B6","J4_B7","J4_B8" }
    };

    private string[,] playerAxisNames = { 
        { "J1_Horizontal","J1_Vertical" },
        { "J2_Horizontal","J2_Vertical" },
        { "J3_Horizontal","J3_Vertical" },
        { "J4_Horizontal","J4_Vertical" },
    };

    public string[] oldJoystick;

    public static string[] actionNames = {
        "Shoot", "Bomb", "Option", "Auto", "Beam", "Menu", "Extra1", "Extra2"
    };
    
    public static string[] axisNames = {
        "Left", "Right", "Up", "Down"
    };
    
    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 InputManager!");
            Destroy(this);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("InputManager Created!");
        
        // Initialize
        playerController[0] = 0;
        playerController[1] = 1;

        playerUsingKeys[0] = false;
        playerUsingKeys[1] = false;
        
        playerAxis[0] = new AxisMapping();
        playerAxis[1] = new AxisMapping();
        
        playerKeyAxis[0] = new KeyAxisMapping();
        playerKeyAxis[1] = new KeyAxisMapping();
        
        playerButtons[0] = new ButtonMapping();
        playerButtons[1] = new ButtonMapping();
        
        playerKeyButtons[0] = new KeyButtonMapping();
        playerKeyButtons[1] = new KeyButtonMapping();
        
        playerState[0] = new InputState();
        playerState[1] = new InputState();
        
        oldJoystick = Input.GetJoystickNames();

        StartCoroutine(CheckControllers());
    }
    
    private void FixedUpdate() {
        UpdatePlayerState(0);

        if (GameManager.Instance != null && GameManager.Instance.twoPlayer) {
            UpdatePlayerState(0);
        }
    }

    private IEnumerator CheckControllers() {
        while (true) {
            yield return new WaitForSecondsRealtime(1);
            
            string[] currentJoystick = Input.GetJoystickNames();

            for (int i = 0; i < currentJoystick.Length; i++) {
                if (i < oldJoystick.Length) {
                    if (currentJoystick[i] != oldJoystick[i]) {
                        if (string.IsNullOrEmpty(currentJoystick[i])) {
                            Debug.Log("Controller " + i + " disconnected.");
                            if (PlayerIsUsingController(i)) {
                                Controller.Instance.whichPlayer = i;
                                Controller.Instance.whichPlayerText.text = "Player " + (i+1) + " controller unplugged!";
                                Controller.Instance.TurnOn(null);
                            }
                        }
                        else {
                            Debug.Log("Controller " + i + " connected using: " + currentJoystick[i]);
                        }
                    }
                }
                else {
                    Debug.Log("New controller connected");
                }
            }
        }
    }

    private bool PlayerIsUsingController(int i) {
        if (playerController[0] == i) {
            return true;
        }

        if (GameManager.Instance.twoPlayer && playerController[1] == i) {
            return true;
        }
        
        return false;
    }
    
    public int DetectControllerButtonPress() {
        // Returns the controller index
        int result = -1;

        // joystick amount is 4
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 8; j++) {
                if (Input.GetButton(playerButtonNames[i, j])) return i;
            }
        }
        
        return result;
    }
    
    public int DetectButtonPress() {
        // Returns the controller index
        int result = -1;

        // joystick amount is 4
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 8; j++) {
                if (Input.GetButton(playerButtonNames[i, j])) return j;
            }
        }
        
        return result;
    }
    
    public int DetectKeyPress() {
        // Returns the controller index
        foreach (KeyCode key in allKeyCodes) {
            if (Input.GetKeyUp(key)) return ((int)key);
        }
        
        return -1;
    }

    public bool CheckForPlayerInput(int playerIndex) {
        int controller = DetectControllerButtonPress();
        if (controller > -1) {
            playerController[playerIndex] = controller;
            playerUsingKeys[playerIndex] = false;
            Debug.Log("Player " + playerIndex+" is set to controller " + controller);
            return true;
        }

        if (DetectKeyPress() > -1) {
            playerController[playerIndex] = -1;
            playerUsingKeys[playerIndex] = true;
            Debug.Log("Player " + playerIndex+" is set to keyboard ");
            return true;
        }

        return false;
    }

    private void UpdatePlayerState(int playerIndex) {
        playerState[playerIndex].Left = false;
        playerState[playerIndex].Right = false;
        playerState[playerIndex].Down = false;
        playerState[playerIndex].Up = false;
        
        playerState[playerIndex].Shoot = false;
        playerState[playerIndex].Bomb = false;
        playerState[playerIndex].Options = false;
        playerState[playerIndex].Auto = false;
        playerState[playerIndex].Beam = false;
        playerState[playerIndex].Menu = false;
        playerState[playerIndex].Extra1 = false;
        playerState[playerIndex].Extra2 = false;
        
        if (Input.GetKey(playerKeyAxis[playerIndex].Left)) { playerState[playerIndex].Left = true; }
        if (Input.GetKey(playerKeyAxis[playerIndex].Right)) { playerState[playerIndex].Right = true; }
        if (Input.GetKey(playerKeyAxis[playerIndex].Up)) { playerState[playerIndex].Up = true; }
        if (Input.GetKey(playerKeyAxis[playerIndex].Down)) { playerState[playerIndex].Down = true; }
        
        if (Input.GetKey(playerKeyButtons[playerIndex].Shoot)) { playerState[playerIndex].Shoot = true; }
        if (Input.GetKey(playerKeyButtons[playerIndex].Bomb)) { playerState[playerIndex].Bomb = true; }
        if (Input.GetKey(playerKeyButtons[playerIndex].Options)) { playerState[playerIndex].Options = true; }
        if (Input.GetKey(playerKeyButtons[playerIndex].Auto)) { playerState[playerIndex].Auto = true; }
        if (Input.GetKey(playerKeyButtons[playerIndex].Beam)) { playerState[playerIndex].Beam = true; }
        if (Input.GetKey(playerKeyButtons[playerIndex].Menu)) { playerState[playerIndex].Menu = true; }
        if (Input.GetKey(playerKeyButtons[playerIndex].Extra1)) { playerState[playerIndex].Extra1 = true; }
        if (Input.GetKey(playerKeyButtons[playerIndex].Extra2)) { playerState[playerIndex].Extra2 = true; }
        
        if (playerController[playerIndex] < 0) return;
        
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex], playerAxis[playerIndex].Horizontal]) < DeadZone) {
            playerState[playerIndex].Left = true;
        }
        
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex], playerAxis[playerIndex].Horizontal]) > DeadZone) {
            playerState[playerIndex].Right = true;
        }
        
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex], playerAxis[playerIndex].Vertical]) < DeadZone) {
            playerState[playerIndex].Down = true;
        }
        
        if (Input.GetAxisRaw(playerAxisNames[playerController[playerIndex], playerAxis[playerIndex].Vertical]) < DeadZone) {
            playerState[playerIndex].Up = true;
        }
        
        if (Input.GetButton(playerButtonNames[playerController[playerIndex], playerButtons[playerIndex].Shoot])) {
            playerState[playerIndex].Shoot = true;
        }
        
        if (Input.GetButton(playerButtonNames[playerController[playerIndex], playerButtons[playerIndex].Bomb])) {
            playerState[playerIndex].Bomb = true;
        }
        
        if (Input.GetButton(playerButtonNames[playerController[playerIndex], playerButtons[playerIndex].Options])) {
            playerState[playerIndex].Options = true;
        }
        
        if (Input.GetButton(playerButtonNames[playerController[playerIndex], playerButtons[playerIndex].Auto])) {
            playerState[playerIndex].Auto = true;
        }
        
        if (Input.GetButton(playerButtonNames[playerController[playerIndex], playerButtons[playerIndex].Beam])) {
            playerState[playerIndex].Beam = true;
        }
        
        if (Input.GetButton(playerButtonNames[playerController[playerIndex], playerButtons[playerIndex].Menu])) {
            playerState[playerIndex].Menu = true;
        }
        
        if (Input.GetButton(playerButtonNames[playerController[playerIndex], playerButtons[playerIndex].Extra1])) {
            playerState[playerIndex].Extra1 = true;
        }
        
        if (Input.GetButton(playerButtonNames[playerController[playerIndex], playerButtons[playerIndex].Extra2])) {
            playerState[playerIndex].Extra2 = true;
        }
    }

    public string GetButtonName(int playerIndex, int actionID) {
        string buttonName = "";

        switch (actionID) {
            case 0:
                buttonName = playerButtonNames[playerIndex, playerButtons[playerIndex].Shoot];
                break;
            case 1:
                buttonName = playerButtonNames[playerIndex, playerButtons[playerIndex].Bomb];
                break;
            case 2:
                buttonName = playerButtonNames[playerIndex, playerButtons[playerIndex].Options];
                break;
            case 3:
                buttonName = playerButtonNames[playerIndex, playerButtons[playerIndex].Auto];
                break;
            case 4:
                buttonName = playerButtonNames[playerIndex, playerButtons[playerIndex].Beam];
                break;
            case 5:
                buttonName = playerButtonNames[playerIndex, playerButtons[playerIndex].Menu];
                break;
            case 6:
                buttonName = playerButtonNames[playerIndex, playerButtons[playerIndex].Extra1];
                break;
            case 7:
                buttonName = playerButtonNames[playerIndex, playerButtons[playerIndex].Extra2];
                break;
        }

        char numberOfButton = buttonName[4];
        
        return "Button "+ numberOfButton;
    }
    
    public string GetKeyName(int playerIndex, int actionID) {
        KeyCode key = KeyCode.None;
        switch (actionID) {
            case 0:
                key = playerKeyButtons[playerIndex].Shoot;
                break;
            case 1:
                key = playerKeyButtons[playerIndex].Bomb;
                break;
            case 2:
                key = playerKeyButtons[playerIndex].Options;
                break;
            case 3:
                key = playerKeyButtons[playerIndex].Auto;
                break;
            case 4:
                key = playerKeyButtons[playerIndex].Beam;
                break;
            case 5:
                key = playerKeyButtons[playerIndex].Menu;
                break;
            case 6:
                key = playerKeyButtons[playerIndex].Extra1;
                break;
            case 7:
                key = playerKeyButtons[playerIndex].Extra2;
                break;
        }
        
        return key.ToString();
    }
    
    public string GetKeyAxisName(int playerIndex, int actionID) {
        KeyCode key = KeyCode.None;
        switch (actionID) {
            case 0:
                key = playerKeyAxis[playerIndex].Left;
                break;
            case 1:
                key = playerKeyAxis[playerIndex].Right;
                break;
            case 2:
                key = playerKeyAxis[playerIndex].Up;
                break;
            case 3:
                key = playerKeyAxis[playerIndex].Down;
                break;
        }
        
        return key.ToString();
    }
    
    public void BindPlayerKey(int player, int actionID, KeyCode key) {
        switch (actionID) {
           case 0:
               playerKeyButtons[player].Shoot = key;
               break;
           case 1:
               playerKeyButtons[player].Bomb = key;
               break;
           case 2:
               playerKeyButtons[player].Options = key;
               break;
           case 3:
               playerKeyButtons[player].Auto = key;
               break;
           case 4:
               playerKeyButtons[player].Beam = key;
               break;
           case 5:
               playerKeyButtons[player].Menu = key;
               break;
           case 6:
               playerKeyButtons[player].Extra1 = key;
               break;
           case 7:
               playerKeyButtons[player].Extra2 = key;
               break;
        }
    }

    public void BindPlayerAxisKey(int player, int actionID, KeyCode key) {
        switch (actionID) {
            case 0:
                playerKeyAxis[player].Left = key;
                break;
            case 1:
                playerKeyAxis[player].Right = key;
                break;
            case 2:
                playerKeyAxis[player].Up = key;
                break;
            case 3:
                playerKeyAxis[player].Down = key;
                break;
        }
    }
    
    public void BindPlayerButton(int player, int actionID, byte button) {
        switch (actionID) {
            case 0:
                playerButtons[player].Shoot = button;
                break;
            case 1:
                playerButtons[player].Bomb = button;
                break;
            case 2:
                playerButtons[player].Options = button;
                break;
            case 3:
                playerButtons[player].Auto = button;
                break;
            case 4:
                playerButtons[player].Beam = button;
                break;
            case 5:
                playerButtons[player].Menu = button;
                break;
            case 6:
                playerButtons[player].Extra1 = button;
                break;
            case 7:
                playerButtons[player].Extra2 = button;
                break;
        }
    }
}

public class InputState {
    public bool Left;
    public bool Right;
    public bool Up;
    public bool Down;

    public bool Shoot;
    public bool Bomb;
    public bool Options;
    public bool Auto;
    public bool Beam;
    public bool Menu;
    public bool Extra1;
    public bool Extra2;
}

public class ButtonMapping {
    public byte Shoot = 0;
    public byte Bomb = 1;
    public byte Options = 2;
    public byte Auto = 3;
    public byte Beam = 4;
    public byte Menu = 5;
    public byte Extra1 = 6;
    public byte Extra2 = 7;
}

public class AxisMapping {
    public byte Horizontal = 0;
    public byte Vertical = 1;
}

public class KeyButtonMapping {
    public KeyCode Shoot = KeyCode.B;
    public KeyCode Bomb = KeyCode.N;
    public KeyCode Options = KeyCode.M;
    public KeyCode Auto = KeyCode.Comma;
    public KeyCode Beam = KeyCode.Period;
    public KeyCode Menu = KeyCode.J;
    public KeyCode Extra1 = KeyCode.K;
    public KeyCode Extra2 = KeyCode.L;
}

public class KeyAxisMapping {
    public KeyCode Left = KeyCode.LeftArrow;
    public KeyCode Right = KeyCode.RightArrow;
    public KeyCode Up = KeyCode.UpArrow;
    public KeyCode Down = KeyCode.DownArrow;
}