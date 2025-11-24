using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginData : MonoBehaviour
{
    public string userName = "Unlogged User";
    public string password = "";
    public TMP_InputField userNameInput, passwordInput;
    public bool route1 = false, route2 = false, route3 = false;
    public Button loginButton;
    void Start()
    {
        loginButton.onClick.AddListener(LogIn);
    }
    void Update()
    {
        userName = userNameInput.text;
        password = passwordInput.text;
    }
    void LogIn()
    {
        if (userName == "TestUser" && password == "N3xtCity2025")
        {
            route1 = true;
            route2 = true;
            route3 = true;
            SceneManager.LoadScene("MenuESP");
        }

        // FireBase authentication logic to be implemented here in the future
    }

}
