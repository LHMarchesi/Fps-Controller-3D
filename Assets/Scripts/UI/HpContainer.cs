using UnityEngine;
using UnityEngine.UI;

public class HpContainer : MonoBehaviour
{
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fulledHeart;
    [SerializeField] private Sprite emptyHeart;
    private PlayerHealth playerHealth;

    private void Awake()
    {
            playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
    }
    void Update()
    {
        ManageHealUI();
    }

    private void ManageHealUI()
    {

        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }

        for (int i = 0; i < playerHealth.Health; i++)
        {
            hearts[i].sprite = fulledHeart;
        }
    }
}
