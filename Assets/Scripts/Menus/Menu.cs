using UnityEngine;
using UnityEngine.EventSystems;

namespace Menus {
public class Menu : MonoBehaviour {
    public GameObject ROOT;
    public Menu previousMenu;
    private GameObject previousItem;

    public virtual void TurnOn(Menu previous) {
        if (ROOT) {
            if (previous != null) {
                previousMenu = previous;
            }
            ROOT.SetActive(true);
            if (previousItem != null) {
                EventSystem.current.SetSelectedGameObject(previousItem);
            }
        }
        else {
            Debug.LogError("ROOT object not set.");
        }
    }

    public virtual void TurnOff(bool returnToPrevious = false) {
        if (ROOT) {
            if (EventSystem.current) {
                previousItem = EventSystem.current.currentSelectedGameObject;
            }
            ROOT.SetActive(false);
            if (returnToPrevious && previousMenu) {
                previousMenu.TurnOn(null);
            }
        }
        else {
            Debug.LogError("ROOT object not set.");
        }
    }
}
}
