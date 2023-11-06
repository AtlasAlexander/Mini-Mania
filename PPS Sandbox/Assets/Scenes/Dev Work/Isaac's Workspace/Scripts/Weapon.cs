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
    [SerializeField] GameObject mirrorHitEffect;
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
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);

        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        Debug.DrawRay(FPCamera.transform.position, FPCamera.transform.forward, Color.green);

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);

            SizeChange target = hit.transform.GetComponent<SizeChange>();
            Mirror mirror = hit.transform.GetComponent<Mirror>();

            //EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null && mirror == null)
            {
                CreateHitImpact(hit);
                return;
            }
            if(target != null)
            {
                CreateSizeHitImpact(hit);
                //target.TakeDamage(damage);
                target.ChangeSize(ammoType, changeAmount);
                
            }
            if(mirror != null)
            {
                CreateMirrorHitImpact(hit);
                mirror.MirrorSizeChange(ammoType);
            }
            else 
            {
                return;
            }
            
        }
        else
        {
            return;
        }
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

    private void CreateMirrorHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(mirrorHitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 1);
    }
}
