using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] InputAction fire;
    [SerializeField] InputAction toggleTrajectory;
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

    [SerializeField] private LineRenderer line;
    [SerializeField] private RaycastHit hit;
    [SerializeField] private Transform trajectoryOrigin;
    [SerializeField] float sphereCastWidth = 0.1f;

    public float defaultLength = 50;
    public int numOfReflections = 2;
    public LayerMask layerMask;

    private Ray ray;

    private Vector3 direction;

    bool canShoot = true;
    bool trajectoryOn = false;

    private void OnEnable()
    {
        fire.Enable();
        canShoot = true;
        toggleTrajectory.Enable();
    }

    private void OnDisable()
    {
        fire.Disable();
        toggleTrajectory.Disable();
    }

    void Update()
    {
        //DisplayAmmo();
        Debug.Log(trajectoryOn);

        if (fire.ReadValue<float>() > 0.5 && canShoot == true)
        {
            StartCoroutine(Shoot());

            FindObjectOfType<AudioManager>().Play("shoot_shrink_ray");

        }

        ReflectLaser();

        var wasPressed = toggleTrajectory.triggered;
        if (wasPressed)
        {
            trajectoryOn = !trajectoryOn;
        }

        if (trajectoryOn)
        {
            turnTrajectoryOn();
        }
        else line.enabled = false;

        
    }

    void turnTrajectoryOn()
    {
        line.enabled = true;
    }

    void ReflectLaser()
    {
        ray = new Ray(FPCamera.transform.position, FPCamera.transform.forward);

        line.positionCount = 1;
        line.SetPosition(0, trajectoryOrigin.position);

        for (int i = 0; i < numOfReflections; i++)
        {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, defaultLength, layerMask))
            {
                line.positionCount += 1;
                line.SetPosition(1, hit.point);

                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
            }
            else
            {
                line.positionCount += 1;
                line.SetPosition(line.positionCount - 1, ray.origin + (ray.direction * defaultLength));
            }
        }
       
    }

    void NormalLaser()
    {
        line.SetPosition(0, transform.position);

        if (Physics.Raycast(transform.position, transform.forward, out hit, defaultLength, layerMask))
        {
            line.SetPosition(1, hit.point);
        }
        else
        {
            line.SetPosition(1, transform.position + (transform.forward * defaultLength));
        }
    }

    /*private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }*/

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            ProcessRaycast(FPCamera.transform.position + (FPCamera.transform.forward * 1), FPCamera.transform.forward);
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
        if (Physics.SphereCast(position, sphereCastWidth, direction, out hit, range))
        {
            Debug.DrawLine(position, hit.point, Color.red, 1f);
            CreateHitImpact(hit);

            SizeChange target = hit.transform.GetComponent<SizeChange>();

            //EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null)
            {
                CreateHitImpact(hit);

                if (hit.collider.gameObject.tag == "Mirror")
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
                target.ChangeSize(ammoType /*, changeAmount*/);
                
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

