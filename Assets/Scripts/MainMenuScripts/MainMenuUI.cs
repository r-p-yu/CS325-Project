using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public Button startButton;
    public Button settingsButton;

    public GameObject menuCanvas;
    public GameObject weaponSelectionPanel;

    public Button smgButton;
    public Button arButton;
    public Button heavyRifleButton;

    public float rotationSpeed = 10f;
    public Transform panoramaRig;

    void Start()
    {
        // Only unlock cursor if we're in the main menu scene
        if (SceneManager.GetActiveScene().name == "MainMenuScene")
        {
            UnlockCursor();
        }

        startButton.onClick.AddListener(OnStartClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);

        smgButton.onClick.AddListener(() => SelectWeapon("SMG"));
        arButton.onClick.AddListener(() => SelectWeapon("AR")); 
        heavyRifleButton.onClick.AddListener(() => SelectWeapon("Heavy Rifle"));

        weaponSelectionPanel.SetActive(false);
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
        menuCanvas.SetActive(false);
        weaponSelectionPanel.SetActive(true);
    }

    void SelectWeapon(string weaponName)
    {
        GameManager.instance.SetWeapon(weaponName);
        GameManager.instance.StartGame();
        LockCursor(); // Ensure locked before scene change
        SceneManager.LoadScene("FarmScene");
    }

    void OnSettingsClicked()
    {
        Debug.Log("Settings clicked.");
    }
}
