using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BaseGun : MonoBehaviour
{
    [Header("references")]
    public Camera fpsCam;
    [Header("bullets")]
    public GameObject bullet;

    public float shootForce, upwardForce;

    [Header("gun stats")]

    public float timeBetweenShooting, Speard, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTab;

    public bool allowButtonHold;

    int bulletsLeft, BulletsShot;

    bool shooting, readyToShoot, reloading;

    [Header("bug fixes")]
    public bool allowInvoke = true;
    

    private void Awake()
    {
        //make sure magzine's full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    private void Update()
    {
        MyInput();
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
    }
    private void Shoot()
    {
        readyToShoot = false;

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        bulletsLeft--;
        BulletsShot++;
    }
}
