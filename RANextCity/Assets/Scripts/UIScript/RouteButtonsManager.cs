using UnityEngine;
using UnityEngine.UI;

public class RouteButtonsManager : MonoBehaviour
{
    public bool route1Active = false, route2Active = false, route3Active = false;
    public int selectedRoute = 0;
    public Button route1Button, route2Button, route3Button;
    public SceneChanger sceneManager;
    void Start()
    {
        route1Button.onClick.AddListener(Route1);
        route2Button.onClick.AddListener(Route2); 
        route3Button.onClick.AddListener(Route3);
    }
    void Route1()
    {
        selectedRoute = 0;
        SceneChange();
    }
    void Route2()
    {
        selectedRoute = 1;
        SceneChange();
    }
    void Route3()
    {
        selectedRoute = 2;
        SceneChange();
    }
    void SceneChange()
    {
        sceneManager.ChangeScene("Maps");
    }
    private void Update() {
        route1Button.interactable = route1Active;
        route2Button.interactable = route2Active;
        route3Button.interactable = route3Active;
    }
}
