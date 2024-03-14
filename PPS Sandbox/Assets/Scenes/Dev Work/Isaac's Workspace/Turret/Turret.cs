using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float range = 15f;
    [SerializeField] GameObject turretBigBolts;
    [SerializeField] GameObject turretSmallBolts;
    Transform target;
    SizeChange sizeChange;

    private float arrowSoundLimiter;

    private void Start()
    {
        StartCoroutine(DetectParticleEmitted());
        arrowSoundLimiter = 0.0f;
        target = FindObjectOfType<FirstPersonController>().transform;
        sizeChange = GetComponent<SizeChange>();
    }


    private void Update()
    {
        arrowSoundLimiter += Time.deltaTime;
        AimWeapon();

        if(sizeChange != null)
        {
            if(sizeChange.GetShrunkStatus())
            {
                if (turretSmallBolts != null)
                {
                    turretSmallBolts.SetActive(true);

                    if(turretBigBolts != null)
                    {
                        turretBigBolts.SetActive(false);
                    }
                }
            }

            if(!sizeChange.GetShrunkStatus())
            {
                if(turretBigBolts != null)
                {
                    turretBigBolts.SetActive(true);

                    if(turretSmallBolts != null)
                    {
                        turretSmallBolts.SetActive(false);
                    }
                }
            }
        }
        
    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);

        weapon.LookAt(target);
        
        if (targetDistance < range)
        {
           
            
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }


    private IEnumerator DetectParticleEmitted()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (projectileParticles.isPlaying)
            {
                if (projectileParticles.particleCount > 0)
                {
                    if(arrowSoundLimiter > 0.7f)
                    {
                        FindObjectOfType<FmodAudioManager>().QuickPlaySound("crossbowShoot", gameObject);
    
                        arrowSoundLimiter = 0;
                    }
                    
                }
            }
        }
    }

}
