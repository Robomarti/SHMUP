using Menus;
using UnityEngine;

public class GameInitializer : MonoBehaviour {
   public enum GameMode {
      INVALID,
      Menus,
      Gameplay
   }
   
   [SerializeField] private GameObject gameManagerPrefab;
   public GameMode gameMode;
   private bool menuLoaded = false;

   private void Start() {
      if (GameManager.Instance == null && gameManagerPrefab) {
         Instantiate(gameManagerPrefab);
      }
   }

   private void Update() {
      if (menuLoaded) return;
      
      switch (gameMode) {
         case GameMode.INVALID:
            break;
         case GameMode.Menus:
            MenuManager.Instance.SwitchToMainMenu();
            break;
         case GameMode.Gameplay:
            MenuManager.Instance.SwitchToGameplayMenus();
            break;
      }

      menuLoaded = true;
   }
}
