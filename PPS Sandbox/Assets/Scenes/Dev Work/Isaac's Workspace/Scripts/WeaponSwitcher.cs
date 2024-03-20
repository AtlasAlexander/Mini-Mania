using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] InputAction scroll;
    [SerializeField] InputAction swap;
    [SerializeField] int currentWeapon = 0;
    [SerializeField] float timeBetweenSwaps = 0.15f;

    PauseMenu pauseMenu;

    bool canSwap = true;

    private void OnEnable()
    {
        scroll.Enable();
        swap.Enable();
    }

    private void OnDisable()
    {
        scroll.Disable();
        swap.Disable();
    }

    void Start()
    {
        SetWeaponActive();
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        if (!pauseMenu.GamePaused)
        {
            int previousWeapon = currentWeapon;

            ProcessKeyInput();
            ProcessScrollWheel();
            ProcessButtonInput();

            if (previousWeapon != currentWeapon)
            {
                SetWeaponActive();
                FindObjectOfType<FmodAudioManager>().QuickPlaySound("weaponSwitch", gameObject);
            }
        }
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
    }

    private void ProcessScrollWheel()
    {
        //Input.GetAxis("Mouse ScrollWheel")
        if (scroll.ReadValue<Vector2>().y < 0)
        {
            if(currentWeapon >= transform.childCount -1)
            {
                currentWeapon = 0;
            }
            else
            {
                currentWeapon++;
            }
        }

        if (scroll.ReadValue<Vector2>().y > 0)
        {
            if (currentWeapon <= 0)
            {
                currentWeapon = transform.childCount -1;
            }
            else
            {
                currentWeapon--;
            }
        }
    }

    private void ProcessButtonInput()
    {
        if(swap.ReadValue<float>() > 0.5 && canSwap == true)
        {
            StartCoroutine(SwapWeapon());
        }
    }

    IEnumerator SwapWeapon()
    {
        canSwap = false;

        if (currentWeapon >= transform.childCount - 1)
        {
            currentWeapon = 0;
        }
        else
        {
            currentWeapon++;
        }
        yield return new WaitForSeconds(timeBetweenSwaps);
        canSwap = true;

    }

    private void SetWeaponActive()
    {
       
        int weaponIndex = 0;

        foreach(Transform weapon in transform)
        {
            if(weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
    }
}
