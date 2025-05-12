using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts;

    void Update()
    {
        int health = GameManager.instance.playerHealth;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = (i < health);
        }
    }
}
