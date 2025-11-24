using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public bool isLogged = false;
    public Button loginButton, exitButton, routesButton, logOutButton;
    public GameObject errorPanel;
    public SceneChanger sceneManager;
    void Start()
    {
        errorPanel.SetActive(false);
        logOutButton.onClick.AddListener(LogOut);
        loginButton.onClick.AddListener(Login);
        exitButton.onClick.AddListener(() => Application.Quit());
        routesButton.onClick.AddListener(Routes);
        UpdateButtonStates();
    }
    void Routes()
    {
        if (isLogged)
        {
            sceneManager.ChangeScene("Rutas");
        }
        else
        {
            ShowErrorXTime();
        }
    }
    void ShowErrorXTime()
    {
        errorPanel.SetActive(true);
        CoroutineRunner.instance.StartCoroutine(HideErrorAfterDelay(2f));
    }
    System.Collections.IEnumerator HideErrorAfterDelay(float delay)
    {
        UnityEngine.Vector3 originalPosition = errorPanel.transform.position;
        for (float t = 0; t < delay; t += Time.deltaTime)
        {
            float yOffset = Mathf.Sin(t * Mathf.PI * 2) * 5f;
            errorPanel.transform.position = originalPosition + new UnityEngine.Vector3(0, yOffset, 0);
            yield return null;
        }
        errorPanel.transform.position = originalPosition;
        errorPanel.SetActive(false);
    }
    void Login()
    {
        sceneManager.ChangeScene("Login");
    }
    void LogOut()
    {
        isLogged = false;
        UpdateButtonStates();
    }
    void UpdateButtonStates()
    {
        if (isLogged)
        {
            loginButton.interactable = false;
        }
        else
        {
            loginButton.interactable = true;
        }
    }
}
