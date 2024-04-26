using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Linq;

public class Weapon : MonoBehaviour
{
    PauseMenu pauseMenu;
    //[SerializeField] InputAction fire;
    [SerializeField] InputAction toggleTrajectory;
    PlayerControls playerControls;
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

    //RayCast
    [SerializeField] private LineRenderer line;
    [SerializeField] private RaycastHit hit;
    [SerializeField] private Transform trajectoryOrigin;
    [SerializeField] float sphereCastWidth = 0.1f;
    [SerializeField] private float raycastClusterSpread = 0.12f;

    //public float range = 50;
    public int numOfReflections = 2;
    public LayerMask mirrorLayerMask;
    [SerializeField] LayerMask sphereCastLayerMask;

    [SerializeField] private TrailRenderer BulletTrail;
    [SerializeField] private float bulletSpeed = 100f;
    [SerializeField] private bool BouncingBullets;
    [SerializeField] private float BounceDistance = 10f;

    private Ray ray;

    private Vector3 direction;

    bool canShoot = true;
    bool trajectoryOn = false;

    public FmodMusicManager[] Radios;

    private void Start()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }
    private void Awake()
    {
        playerControls = new PlayerControls();

        Radios = FindObjectsOfType<FmodMusicManager>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
        canShoot = true;
        toggleTrajectory.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
        toggleTrajectory.Disable();
    }

    void Update()
    {
        //DisplayAmmo();
        //Debug.Log(trajectoryOn);

        if (!pauseMenu.GamePaused)
        {
            if (UserInput.instance.ShootInput && canShoot == true)
            {
                StartCoroutine(Shoot());
                if (ammoType.ToString() == "Shrink")
                {
                    FindObjectOfType<FmodAudioManager>().QuickPlaySound("shootShrinkRay", gameObject);
                }
                else
                {
                    FindObjectOfType<FmodAudioManager>().QuickPlaySound("shootGrowthRay", gameObject);
                }
                
                
                //FindObjectOfType<AudioManager>().Play("shoot_shrink_ray");

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
            if (Physics.Raycast(ray.origin, ray.direction, out hit, range, mirrorLayerMask))
            {
                line.positionCount += 1;
                line.SetPosition(1, hit.point);

                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
            }
            else
            {
                line.positionCount += 1;
                line.SetPosition(line.positionCount - 1, ray.origin + (ray.direction * range));
            }
        }
       
    }

    void NormalLaser()
    {
        line.SetPosition(0, transform.position);

        if (Physics.Raycast(transform.position, transform.forward, out hit, range, mirrorLayerMask))
        {
            line.SetPosition(1, hit.point);
        }
        else
        {
            line.SetPosition(1, transform.position + (transform.forward * range));
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
            
            Vector3 rayCastOrigin = FPCamera.transform.position;
            Vector3 direction = FPCamera.transform.forward;

            PlayMuzzleFlash();
            //ProcessRaycast(rayCastOrigin, direction);

            //Define positions for Raycast cluster
            Vector3 up = FPCamera.transform.up * raycastClusterSpread;
            Vector3 down = -FPCamera.transform.up * raycastClusterSpread;
            Vector3 right = FPCamera.transform.right * raycastClusterSpread;
            Vector3 left = -FPCamera.transform.right * raycastClusterSpread;

            Vector3 upRight = (up + right).normalized * raycastClusterSpread;
            Vector3 upLeft = (up + left).normalized * raycastClusterSpread;
            Vector3 downRight = (down + right).normalized * raycastClusterSpread;
            Vector3 downLeft = (down + left).normalized * raycastClusterSpread;

            TrailRenderer trail = Instantiate(BulletTrail, trajectoryOrigin.position, Quaternion.identity);

            //Shoot cluster of raycasts
            ProcessRaycast(rayCastOrigin, FPCamera.transform.forward, false, trail);

            
            ProcessRaycast(rayCastOrigin + up, FPCamera.transform.forward, false, null);
            ProcessRaycast(rayCastOrigin + down, FPCamera.transform.forward, false, null);
            ProcessRaycast(rayCastOrigin + right, FPCamera.transform.forward, false, null);
            ProcessRaycast(rayCastOrigin + left, FPCamera.transform.forward, false, null);

            ProcessRaycast(rayCastOrigin + upRight, FPCamera.transform.forward, false, null);
            ProcessRaycast(rayCastOrigin + upLeft, FPCamera.transform.forward, false, null);
            ProcessRaycast(rayCastOrigin + downRight, FPCamera.transform.forward, false, null);
            ProcessRaycast(rayCastOrigin + downLeft, FPCamera.transform.forward, false, null);
            


            //ProcessRaycast(FPCamera.transform.position, FPCamera.transform.forward);
            //ProcessSpherecastAll(FPCamera.transform.position + (FPCamera.transform.forward * 1), FPCamera.transform.forward);
            //ammoSlot.ReduceCurrentAmmo(ammoType);

        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast(Vector3 position, Vector3 direction, bool canShootSelf, TrailRenderer trail)
    {
        RaycastHit hit;

        //TrailRenderer trail = Instantiate(BulletTrail, trajectoryOrigin.position, Quaternion.identity);
        
        if (Physics.SphereCast(position, sphereCastWidth, direction, out hit, range, sphereCastLayerMask, QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.tag == "Radio") {

                for (int i = 0; i < Radios.Length; i++)
                    Radios[i].GetComponent<FmodMusicManager>().togglePause();
                //hit.transform.GetComponent<FmodMusicManager>().togglePause(); 

            }
            StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, BounceDistance, true));

            Debug.DrawLine(position, hit.point, Color.red, 1f);
            //CreateHitImpact(hit);

            SizeChange target = hit.transform.GetComponent<SizeChange>();
            
            if (target == null && hit.collider.gameObject.tag != "Mirror")
            {
                if (RumbleManager.instance != null)
                {
                    RumbleManager.instance.RumblePulse(0.5f, 0.5f, 0.2f);
                }
                CreateHitImpact(hit);

                return;
            }
            else if (hit.collider.gameObject.tag == "Mirror")
            {
                //BouncingBullets = true;
                //trail.transform.position = hit.point;
                ReflectRay(hit.point, Vector3.Reflect(direction, hit.normal), true);
            }
            else
            {
                CreateSizeHitImpact(hit);

                //If target is not the player
                if(hit.transform.GetComponent<CharacterController>() == null)
                {
                    target.ChangeSize(ammoType /*, changeAmount*/);
                }
                else
                {
                    if(canShootSelf)
                    {
                        target.ChangeSize(ammoType /*, changeAmount*/);
                    }
                }
            }

        }
        else
        {
            StartCoroutine(SpawnTrail(trail, trajectoryOrigin.position + direction * 100, hit.normal, BounceDistance, false));
            return;
        }
    }

    private void ReflectRay(Vector3 position, Vector3 direction, bool canShootSelf)
    {
        TrailRenderer trail = Instantiate(BulletTrail, position, Quaternion.identity);
        if (Physics.SphereCast(position, sphereCastWidth, direction, out hit, range, sphereCastLayerMask, QueryTriggerInteraction.Ignore))
        {
            StartCoroutine(SpawnTrail(trail, hit.point, hit.normal, BounceDistance, true));
            Debug.DrawLine(position, hit.point, Color.red, 1f);
            //CreateHitImpact(hit);

            SizeChange target = hit.transform.GetComponent<SizeChange>();

            if (target == null && hit.collider.gameObject.tag != "Mirror")
            {
                if (RumbleManager.instance != null)
                {
                    RumbleManager.instance.RumblePulse(0.5f, 0.5f, 0.2f);
                }
                    
                CreateHitImpact(hit);

                return;
            }
            else if (hit.collider.gameObject.tag == "Mirror")
            {
                Debug.Log("Mirror");
                //BouncingBullets = true;
                ReflectRay(hit.point, Vector3.Reflect(direction, hit.normal), true);
            }
            else
            {
                CreateSizeHitImpact(hit);

                //If target is not the player
                if (hit.transform.GetComponent<CharacterController>() == null)
                {
                    target.ChangeSize(ammoType /*, changeAmount*/);
                }
                else
                {
                    if (canShootSelf)
                    {
                        target.ChangeSize(ammoType /*, changeAmount*/);
                    }
                }
            }
        }
        else
        {
            StartCoroutine(SpawnTrail(trail, trajectoryOrigin.position + direction * 100, hit.normal, BounceDistance, false));
            return;
        }
    }

    private IEnumerator SpawnTrail(TrailRenderer Trail, Vector3 HitPoint, Vector3 HitNormal, float BounceDistance, bool madeImpact)
    {
        //float time = 0f;
        if(Trail == null) { yield break; }
        Vector3 startPosition = Trail.transform.position;
        Vector3 direction = (HitPoint - Trail.transform.position).normalized;
        float distance = Vector3.Distance(Trail.transform.position, HitPoint);
        float startingDistance = distance;


        while (distance > 0)
        {
            Trail.transform.position = Vector3.Lerp(startPosition, HitPoint, 1 - (distance / startingDistance));
            distance -= Time.deltaTime * bulletSpeed;

            yield return null;
        }

        Trail.transform.position = HitPoint;

        Destroy(Trail.gameObject, Trail.time);
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

