using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance = null;

    [SerializeField] private GameObject deathPanel;

    public void ToggleDeathPanel()
    {
        deathPanel.SetActive(!deathPanel.activeSelf);
    }

    public void Respawn()
    {
        SceneManager.LoadScene("RoRLevel1");
    }

    public void Exit()
    {
        Application.Quit();
    }

    void Awake()
    {
        if (instance == null) instance = this;

        else if (instance != this) Destroy(gameObject);
    }
}
