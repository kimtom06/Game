using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool CanReciveMoveInput = true;
    public FirstPersonController fpsc;
    [HideInInspector] public HealthModule health;
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
        if (health.Alive)
        {
            fpsc.lockCursor = useCross;
            fpsc.cameraCanMove = useCross;
            if (CanReciveMoveInput && Input.GetAxis("Fire1") == 1)
            {
                PlayerWeapon[WeaponIndex].Attack();
            }

            float scrollInput = Input.mouseScrollDelta.y;
            if (scrollInput > 0)
            {
                WeaponIndex = Mathf.Min(WeaponIndex + 1, PlayerWeapon.Count - 1);
            }
            else if (scrollInput < 0)
            {
                WeaponIndex = Mathf.Max(WeaponIndex - 1, 0);
            }
            for(int i=0; i< PlayerWeapon.Count; i++)
            {
                if(i != WeaponIndex)
                {
                    if (PlayerWeapon[i])
                    {
                        PlayerWeapon[i].gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (PlayerWeapon[i])
                    {
                        PlayerWeapon[i].gameObject.SetActive(true);
                    }
                }
            }
        }
        else
        {
            fpsc.lockCursor = false;
            fpsc.cameraCanMove = false;
        }
    }
}