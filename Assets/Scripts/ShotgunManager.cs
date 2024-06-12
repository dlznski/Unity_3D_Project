using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunManager : MonoBehaviour
{
    public Animator animator;
    public AudioSource shotgunSound;

    public GameObject bullet;

    public float shootForce, upwardForce;

    public float timeBetweenShooting, spread, timeBeetweenshots;
    public int bulletsPerTap;

    bool shooting, readyToShoot;

    private CustomBullet customBullet;
    public Camera MainCamera;
    public Transform attackPoint;

    public bool allowInvoke = true;
    public InGameMenu inGameMenu;

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
        if(Input.GetKeyDown(KeyCode.Mouse0) && readyToShoot && inGameMenu.paused == false)
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        readyToShoot = false;
        animator.SetTrigger("Fire");
        shotgunSound.Play();

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

        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;

        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(MainCamera.transform.up * upwardForce, ForceMode.Impulse);

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
