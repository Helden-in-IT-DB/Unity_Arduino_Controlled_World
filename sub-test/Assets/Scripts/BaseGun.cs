using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseGun : MonoBehaviour
{
    [Header("references")]
    public Camera fpsCam;
    public Transform attackPoint;


    [Header("bullets")]
    public GameObject bullet;

    public float shootForce, upwardForce;

    [Header("gun stats")]

    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTab;

    public bool allowButtonHold;

    int bulletsLeft, BulletsShot;

    bool shooting, readyToShoot, reloading;

    [Header("bug fixes")]
    public bool allowInvoke = true;
    
    [Header("graphics")]
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammoDisplay;
    private void Awake()
    {
        //make sure magzine's full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();

        //ammo ui for when i want
        if (ammoDisplay != null)
        {
            ammoDisplay.SetText(bulletsLeft / bulletsPerTab + " / " + magazineSize / bulletsPerTab);
        }
    }
    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            //Set bullets shot to 0
            BulletsShot = 0;

            Shoot();
        }

        //reloading
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        //autoreload
        if(readyToShoot && shooting && !reloading && bulletsLeft <= 0) Reload();
    }
    private void Shoot()
    {
        readyToShoot = false;
        
        //finds the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // just a ray through the center of the screen
        RaycastHit hit;

        //checks if ray hits something
        Vector3 targetPoint;
        if(Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else 
        {
            targetPoint = ray.GetPoint(75); // faraway point from the player for air shooting
        }

        //calculate direction from attackpoint to targetpoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //calculate spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        //calculate direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        //bullet projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);

        currentBullet.transform.forward = directionWithSpread.normalized;

        // add forces to bullets
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        //muzzle flash for when i want
        if (muzzleFlash != null)
        {
            Instantiate(muzzleFlash,attackPoint.position, Quaternion.identity);
        }


        bulletsLeft--;
        BulletsShot++;


        //invoke resetShot function (if it wasn't already)
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
        if (BulletsShot < bulletsPerTab && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShooting);
        }
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}
