using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public List<Weapons> PlayerWeapon = new List<Weapons>();
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
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
        if (Input.GetAxis("Fire1") == 1)
        {
            print("Fire!");
        }
    }
}
