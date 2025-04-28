using UnityEngine;
using UnityEngine.UI;
public class PlayerUIManager : MonoBehaviour
{
    public Slider HealthSlider;
    public Text HealthText;
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            if (HealthSlider)
            {
                HealthSlider.value = (float)player.health.Health / player.health.MaxHealth;
            }
            if (HealthText)
            {
                HealthText.text = player.health.Health + "/" + player.health.MaxHealth;
            }
        }
    }
}
