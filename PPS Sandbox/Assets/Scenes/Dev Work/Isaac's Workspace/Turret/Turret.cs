using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float range = 15f;
    [SerializeField] GameObject turretBigBolts;
    [SerializeField] GameObject turretSmallBolts;
    Transform target;
    SizeChange sizeChange;

    [SerializeField] Transform lineOrigin;
    LineRenderer lineRenderer;

    private void Start()
    {
        target = FindObjectOfType<FirstPersonController>().transform;
        sizeChange = GetComponent<SizeChange>();
        lineRenderer = GetComponent<LineRenderer>();
    }


    private void Update()
    {
        AimWeapon();

        HandleSizeChange();

        Ray lineRay = new Ray(lineOrigin.position, lineOrigin.forward);
        RaycastHit hit;
        lineRenderer.SetPosition(0, lineOrigin.position);
        if(Physics.Raycast(lineRay, out hit, range))
        {
            lineRenderer.SetPosition(1, hit.point);
        }

    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);

        weapon.LookAt(target);

        if (targetDistance < range)
        {
            Attack(true);
            lineRenderer.enabled = true;
        }
        else
        {
            Attack(false);
            lineRenderer.enabled = false;
        }
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }

    void HandleSizeChange()
    {
        if (sizeChange != null)
        {
            if (sizeChange.GetShrunkStatus())
            {
                if (turretSmallBolts != null)
                {
                    turretSmallBolts.SetActive(true);

                    if (turretBigBolts != null)
                    {
                        turretBigBolts.SetActive(false);
                    }
                }
            }

            if (!sizeChange.GetShrunkStatus())
            {
                if (turretBigBolts != null)
                {
                    turretBigBolts.SetActive(true);

                    if (turretSmallBolts != null)
                    {
                        turretSmallBolts.SetActive(false);
                    }
                }
            }
        }
    }
}
