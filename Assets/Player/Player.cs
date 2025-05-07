using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool CanReciveMoveInput = true;
    public FirstPersonController fpsc;
    [HideInInspector]public HealthModule health;
    public List<Weapons> PlayerWeapon = new List<Weapons>();
    public int WeaponIndex = 0;
    public bool useCross = false;
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        health = gameObject.GetComponent<HealthModule>();
        if (PlayerWeapon.Count != 4)
        {
            for (int i = 0; i < 4 - PlayerWeapon.Count; i++)
            {
                PlayerWeapon.Add(null);
            }

        }
    }
    private void Update()
    {
        
        fpsc.lockCursor = useCross;
        fpsc.cameraCanMove = useCross;
        if (CanReciveMoveInput && Input.GetAxis("Fire1") == 1)
        {
            PlayerWeapon[WeaponIndex].Attack();
        }
    }
}
