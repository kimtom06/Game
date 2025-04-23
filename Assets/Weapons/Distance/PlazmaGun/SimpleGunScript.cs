using UnityEngine;

public class SimpleGunScript : Weapons
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextTimeToFire = 0f;

    public override void UseWeapon()
    {
        if (Time.time >= nextTimeToFire && Amount > 0 && !Reroading)
        {
            Shoot();
            nextTimeToFire = Time.time + 1f / fireRate;
            Amount--;
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("Shot fired! Ammo left: " + Amount);
    }

    public void Reload(int reloadAmount)
    {
        if (Amount <= 0 && !Reroading)
        {
            Reroading = true;
            Debug.Log("Reloading...");
            Invoke("FinishReload", 2f);
        }
    }

    void FinishReload()
    {
        Amount = 30;
        Reroading = false;
        Debug.Log("Reload complete!");
    }
}
