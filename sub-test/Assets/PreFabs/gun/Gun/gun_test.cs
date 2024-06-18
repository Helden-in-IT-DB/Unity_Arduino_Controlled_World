using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class gun_test : MonoBehaviour
{
    [SerializeField] private float HoldCords_x;
    [SerializeField] private float HoldCords_y;
    [SerializeField] private float HoldCords_z;
    [SerializeField] private Animator animator;

    private bool shooting;
    private bool reloading;
    // Start is called before the first frame update
    void Start()
    {
        animator.Play("pose");
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(HoldCords_x,HoldCords_y,HoldCords_z);

    }

    private void OnShoot()
    {
       if (!shooting && !reloading)
       {
        Debug.Log("hewo");
        Invoke("animeReset",.5f);
        shooting = true;
        animator.Play("fire");
        Debug.Log("");
       }
    }
    private void OnMove()
    {

    }
    private void animeReset()
    {
        shooting = false;
        reloading = false;
        animator.Play("pose");
    }
    private void OnReload()
    {
        reloading = true;
        Invoke("animeReset", 2.5f);
        animator.Play("reload");
    }
    /*
    private IEnumerator Pplay()
    {
        float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSecondsRealtime(animationLength);
        animator.Play("walk");
    }
    */
}
