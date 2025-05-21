using UnityEngine;

public class SimpleGunScript : Weapons
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 5f;
    private float nextTimeToFire = 0.6f;
    public Camera mainCamera; // Reference to your main camera

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    public override void UseWeapon()
    {
        if (!isAIUsing)
        {
            if (Time.time >= nextTimeToFire && CurrentAmount > 0 && !Reroading)
            {
                Shoot();
                nextTimeToFire = Time.time + (1f / fireRate);
                CurrentAmount--;
            }
        }
        else
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if (!isAIUsing)
        {
            Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0);
            Ray ray = mainCamera.ScreenPointToRay(screenCenter);
            Vector3 shootDirection = ray.direction;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));
            //Debug.Log("Shot fired! Ammo left: " + CurrentAmount);
        }
        else
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }
    }

    public void Reload(int reloadAmount)
    {
        if (CurrentAmount <= 0 && !Reroading)
        {
            Reroading = true;
           // Debug.Log("Reloading...");
            Invoke("FinishReload", 2f);
        }
    }

    void FinishReload()
    {
        CurrentAmount = Amount;
        Reroading = false;
       // Debug.Log("Reload complete!");
    }
}