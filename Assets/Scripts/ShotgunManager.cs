using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunManager : MonoBehaviour
{
    public Animator animator;
    public AudioSource shotgunSound;

    public GameObject bullet;

    public float shootForce, upwardForce;

    public float timeBetweenShooting, spread, timeBetweenShots;
    public int bulletsPerTap = 2;

    private bool readyToShoot;

    public Camera MainCamera;
    public Transform attackPoint;
    public Transform attackPoint2;

    private InGameMenu inGameMenu;

    private bool allowInvoke = true;

    private void Start()
    {
        inGameMenu = FindObjectOfType<InGameMenu>();
    }

    public void Awake()
    {
        readyToShoot = true;
    }

    private void Update()
    {
        WeaponBehaviour();
    }

    private void WeaponBehaviour()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && readyToShoot && !inGameMenu.paused)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        readyToShoot = false;
        animator.SetTrigger("Fire");
        shotgunSound.Play();

        for (int i = 0; i < bulletsPerTap; i++)
        {
            Transform currentAttackPoint = (i == 0) ? attackPoint : attackPoint2;

            Ray ray = MainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(75);
            }

            Vector3 directionWithoutSpread = targetPoint - currentAttackPoint.position;

            float x = Random.Range(-spread, spread);
            float y = Random.Range(-spread, spread);

            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

            GameObject currentBullet = Instantiate(bullet, currentAttackPoint.position, Quaternion.identity);
            currentBullet.transform.forward = directionWithSpread.normalized;

            Rigidbody bulletRb = currentBullet.GetComponent<Rigidbody>();
            if (bulletRb != null)
            {
                bulletRb.AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
                bulletRb.AddForce(MainCamera.transform.up * upwardForce, ForceMode.Impulse);
            }
            else
            {
                Debug.LogError("Rigidbody component is missing on the bullet prefab.");
            }
        }

        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }
}
