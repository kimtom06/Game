using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int Amount;
    public int CurrentAmount;
    public bool Reroading = false;
    public void Attack()
    {
        if (!Reroading)
        {
            UseWeapon();
        }
    }
    public virtual void UseWeapon()
    {

    }
    private void Awake()
    {
        CurrentAmount = Amount;
    }
}
