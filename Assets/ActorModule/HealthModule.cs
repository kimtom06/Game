using UnityEngine;

public class HealthModule : MonoBehaviour
{
    public bool ShowHealthbar = false;
    [HideInInspector]public bool Alive = true;
    public int MaxHealth = 30;
    public int Health;

    private void Start()
    {
        Alive = true;
        Health = MaxHealth;
    }
    public bool Damage(int dam)
    {
        print("nah");
        Health = Health - dam;
        Mathf.Clamp(Health, 0, MaxHealth);
        Alive = (Health != 0);
        return !Alive;
    }
}
