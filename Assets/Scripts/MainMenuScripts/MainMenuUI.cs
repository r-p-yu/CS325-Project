using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Button startButton;
    public Button exitButton;

    public GameObject menuCanvas;
    public float rotationSpeed = 10f;
    public Transform panoramaRig;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            UnlockCursor();
        }

        startButton.onClick.AddListener(OnStartClicked);
        exitButton.onClick.AddListener(OnExitClicked);
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (panoramaRig != null)
            panoramaRig.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    void OnStartClicked()
    {
        LockCursor();
        GameManager.instance.StartGame(); // Optional, based on your setup
        SceneManager.LoadScene("FarmScene");
    }

    void OnExitClicked()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
