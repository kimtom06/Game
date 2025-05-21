using UnityEngine;
using UnityEngine.UI;
public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;
    public bool isOnWindows = false;
    public Slider HealthSlider;
    public Text HealthText;
    public Text WeaponUsageText;
    public Player player;
    public GameObject DeathScreen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        player.useCross = !isOnWindows;
        if (player)
        {
            if (WeaponUsageText)
            {
                WeaponUsageText.text = player.PlayerWeapon[player.WeaponIndex].CurrentAmount.ToString();
            }
            if (HealthSlider)
            {
                HealthSlider.value = (float)player.health.Health / player.health.MaxHealth;
            }
            if (HealthText)
            {
                HealthText.text = player.health.Health + "/" + player.health.MaxHealth;
            }
        }
        if (!player.health.Alive)
        {
            DeathScreen.gameObject.SetActive(true);
        }
    }
}
