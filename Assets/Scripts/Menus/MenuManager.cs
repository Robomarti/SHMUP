using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menus {
public class MenuManager : MonoBehaviour {
    public static MenuManager Instance;
    [SerializeField] private Menu activeMenu;

    private void Start() {
        if (Instance) {
            Debug.LogError("Trying to instantiate more than 1 MenuManager!");
            Destroy(this);
            return;
        }
    
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log("MenuManager Created!");
    }

    public void SwitchToGameplayMenus() {
        SceneManager.LoadScene("AudioOptions", LoadSceneMode.Additive);
        SceneManager.LoadScene("ControlsOptions", LoadSceneMode.Additive);
        SceneManager.LoadScene("GraphicsOptions", LoadSceneMode.Additive);
        SceneManager.LoadScene("Options", LoadSceneMode.Additive);
        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
        SceneManager.LoadScene("YesNo", LoadSceneMode.Additive);
    }

    public void SwitchToMainMenu() {
        SceneManager.LoadScene("ControlsOptions", LoadSceneMode.Additive);
        SceneManager.LoadScene("CraftSelect", LoadSceneMode.Additive);
        SceneManager.LoadScene("Credits", LoadSceneMode.Additive);
        SceneManager.LoadScene("Main", LoadSceneMode.Additive);
        SceneManager.LoadScene("Medals", LoadSceneMode.Additive);
        SceneManager.LoadScene("Options", LoadSceneMode.Additive);
        SceneManager.LoadScene("Play", LoadSceneMode.Additive);
        SceneManager.LoadScene("Practice", LoadSceneMode.Additive);
        SceneManager.LoadScene("PracticeArena", LoadSceneMode.Additive);
        SceneManager.LoadScene("PracticeStage", LoadSceneMode.Additive);
        SceneManager.LoadScene("Replays", LoadSceneMode.Additive);
        SceneManager.LoadScene("Scores", LoadSceneMode.Additive);
        SceneManager.LoadScene("TitleScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("YesNo", LoadSceneMode.Additive);
    }
}
}

