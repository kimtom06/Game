using UnityEngine;

public class Weapons : MonoBehaviour
{
    public int Amount;
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
}
