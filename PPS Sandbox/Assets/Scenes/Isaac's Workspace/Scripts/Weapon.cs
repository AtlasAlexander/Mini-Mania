using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] InputAction fire;
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    //[SerializeField] float damage = 30f;
    [SerializeField, Range(0.1f, 0.5f)] float changeAmount;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] GameObject sizeHitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText;

    bool canShoot = true;

    private void OnEnable()
    {
        fire.Enable();
        canShoot = true;
    }

    private void OnDisable()
    {
        fire.Disable();
    }

    void Update()
    {
        DisplayAmmo();

        if (fire.ReadValue<float>() > 0.5 && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast(FPCamera.transform.position, FPCamera.transform.forward);
            ammoSlot.ReduceCurrentAmmo(ammoType);

        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast(Vector3 position, Vector3 direction)
    {
        RaycastHit hit;
        if(Physics.Raycast(position, direction, out hit, range))
        {
            Debug.DrawLine(position, hit.point, Color.red, 1f);
            CreateHitImpact(hit);

            SizeChange target = hit.transform.GetComponent<SizeChange>();

            //EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                CreateHitImpact(hit);

                if(hit.collider.gameObject.tag == "Mirror")
                {
                    Debug.Log("Mirror");
                    ReflectRay(hit.point, Vector3.Reflect(direction, hit.normal));
                    
                }

                return;
            }
            else
            {
                CreateSizeHitImpact(hit);
                //target.TakeDamage(damage);
                target.ChangeSize(ammoType, changeAmount);
                
            }
            
        }
        else
        {
            return;
        }
    }

    private void ReflectRay(Vector3 position, Vector3 direction)
    {
        ProcessRaycast(position, direction);
    }



    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1);
    }
    private void CreateSizeHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(sizeHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1);
    }
}
