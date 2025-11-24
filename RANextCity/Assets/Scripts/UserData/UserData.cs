using UnityEngine;
using UnityEngine.SceneManagement;
public class UserData : MonoBehaviour
{
    public bool route1Unlocked = false, route2Unlocked = false, route3Unlocked = false ;
    public int selectedRoute = 0;
    public string userName = "Unlogged User";
    private string password = "";
    private ButtonManager buttonManager;
    private RouteButtonsManager routeButtonManager;
    private RouteList routeList;
    private LoginData loginData;
    private string scene;
    void Update()
    {
        scene = SceneManager.GetActiveScene().name;
        switch (scene)
        {
            case "MenuESP":
                Menu();
                break;
            case "Rutas":
                RouteSelection();
                break;
            case "Login":
                Login();
                break;
            case "Maps":
                MapLoad();
                break;
        }
    }
    void Menu()
    {
        if (buttonManager == null)
        {
            buttonManager = FindFirstObjectByType<ButtonManager>();
        }
        buttonManager.isLogged = userName != "Unlogged User";
    }
    void RouteSelection()
    {
        if (routeButtonManager == null)
        {
            routeButtonManager = FindFirstObjectByType<RouteButtonsManager>();
        }
        routeButtonManager.route1Active = route1Unlocked;
        routeButtonManager.route2Active = route2Unlocked;
        routeButtonManager.route3Active = route3Unlocked;
        selectedRoute = routeButtonManager.selectedRoute;
    }
    void Login()
    {
        if (loginData == null)
        {
            loginData = FindFirstObjectByType<LoginData>();
        }
        userName = loginData.userName;
        password = loginData.password;
        route1Unlocked = loginData.route1;
        route2Unlocked = loginData.route2;
        route3Unlocked = loginData.route3;
    }
    void MapLoad()
    {
        if (routeList == null)
        {
            routeList = FindFirstObjectByType<RouteList>();
        }
        routeList.selectedRouteIndex = selectedRoute;
    }
}
