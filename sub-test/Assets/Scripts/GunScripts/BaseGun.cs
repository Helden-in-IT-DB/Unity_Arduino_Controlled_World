using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using TMPro;

/// Thanks for downloading my projectile gun script! :D
/// Feel free to use it in any project you like!
/// 
/// The code is fully commented but if you still have any questions
/// don't hesitate to write a yt comment
/// or use the #coding-problems channel of my discord server
/// 
/// Dave
public class BaseGun : MonoBehaviour
{
    //bullet 
    public GameObject bullet;

    //bullet force
    public float shootForce, upwardForce;

    //Gun stats
    public float timeBetweenShooting, spread, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    //Recoil
    private Rigidbody playerRb;
    public float recoilForce;

    //bools
    bool shooting, readyToShoot, reloading;

    //Reference
    private Camera fpsCam;
    private Transform camHolder;
    public Transform attackPoint;

    //Graphics
    public GameObject muzzleFlash;
    public TextMeshProUGUI ammunitionDisplay;

    //bug fixing :D
    public bool allowInvoke = true;

    [Header("animator/ cords")]
    [SerializeField] private float HoldCords_x;
    [SerializeField] private float HoldCords_y;
    [SerializeField] private float HoldCords_z;
    [SerializeField] private Animator animatorGun;
    [SerializeField] private Animator animatorArms; 
    [SerializeField] private string includeArms = "_gun-arm";
    [SerializeField] private string[] animationNames;
    private bool animate;
    private float idleCounter;
    private void Awake()
    {
        //make sure magazine is full
        bulletsLeft = magazineSize;
        readyToShoot = true;
        Debug.Log("picked up");
    }
    private void Update()
    {
        MyInput();
        if (playerRb == null || playerRb != transform.parent.parent.parent.GetComponent<Rigidbody>())
        {
            //Debug.Log("changing playerRb and fpscam values");
            //sets up the parent and playerRB values
            camHolder = transform.parent.parent;  
            fpsCam = camHolder.GetComponent<Camera>();
            playerRb = transform.parent.parent.parent.GetComponent<Rigidbody>();
            animatorGun =transform.GetComponent<Animator>();
            animatorArms = transform.parent.parent.Find("arms3").GetComponent<Animator>();
            if (animatorArms != null && animatorGun != null)
            {
                animate = true;
            }
        }
        //Set ammo display, if it exists :D
        if (ammunitionDisplay != null) 
        {
            ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        }
        /*
        if (animatorGun.GetCurrentAnimatorStateInfo(0).IsName(animationNames[0]) && idleCounter < 2f)
        {
            idleCounter += Time.deltaTime;
        }
        else if (idleCounter >= 2f)
        {
            AnimateThings(1);
            idleCounter = 0f;
        }
        */
    }
    private void MyInput()
    {
        //Check if allowed to hold down button and take corresponding input
        //if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        //else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reloading 
        //if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading) Reload();
        //Reload automatically when trying to shoot without ammo
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0) OnReload();

        //Shooting
        if (readyToShoot && allowButtonHold && shooting && !reloading && bulletsLeft > 0)
        {
            //Set bullets shot to 0
            bulletsShot = 0;

            Shooting();
        }
    }
    private void OnShoot()
    {
        if (!shooting)
        {
            shooting = true;
            if (readyToShoot && !reloading && bulletsLeft > 0)
            {
                //Set bullets shot to 0
                bulletsShot = 0;

                Shooting();
            }
        }
        else if (shooting)
        {
            shooting = false;
        }
    }

    private void Shooting()
    {
        readyToShoot = false;
        AnimateThings(3);

        //Find the exact hit position using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); //Just a ray through the middle of your current view
        RaycastHit hit;

        //check if ray hits something
        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(75); //Just a point far away from the player

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);

        //Calculate new direction with spread
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0); //Just add spread to last direction

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        //Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;

        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        //Instantiate muzzle flash, if you have one
        if (muzzleFlash != null)
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);

        bulletsLeft--;
        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;

            //Add recoil to player (should only be called once)
            playerRb.AddForce(-directionWithSpread.normalized * recoilForce, ForceMode.Impulse);
        }

        //if more than one bulletsPerTap make sure to repeat shoot function
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
            Invoke("Shooting", timeBetweenShots);
    }
    private void ResetShot()
    {
        //Allow shooting and invoking again
        AnimateThings(1);
        readyToShoot = true;
        allowInvoke = true;
    }

    private void OnReload()
    {
        reloading = true;
        AnimateThings(4);
        Invoke("ReloadFinished", reloadTime); //Invoke ReloadFinished function with your reloadTime as delay
    }
    private void ReloadFinished()
    {
        //Fill magazine
        AnimateThings(1);
        bulletsLeft = magazineSize;
        reloading = false;
    }

        public void DisableAmmoText()
    {
        ammunitionDisplay.text = null;
    }
    //sets the animation of whatever animation is specified
    private void AnimateThings (int specificAnimation)
    {
        if (animate)
        {
            animatorGun.Play(animationNames[specificAnimation]);
            animatorArms.Play(animationNames[specificAnimation] + includeArms);
        }
    }
    public void ShutdownAnimations ()
    {
        playerRb = null;
        if (animate)
        {
            animatorGun.Play(animationNames[0]);
            animatorArms.Play(animationNames[0] + includeArms);
        }
        
    }
}
