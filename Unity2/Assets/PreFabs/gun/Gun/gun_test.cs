using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class gun_test : MonoBehaviour
{
    [SerializeField] private float HoldCords_x;
    [SerializeField] private float HoldCords_y;
    [SerializeField] private float HoldCords_z;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator arms; 
    [SerializeField] private string includeArms = "_gun-arm";
    [SerializeField] private String[] animationNames;
    private bool shooting;
    private bool reloading;
    // Start is called before the first frame update
    void Start()
    {
        animator.Play("pose");           
        
        arms = transform.parent.parent.Find("arms3").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.localPosition = new Vector3(HoldCords_x,HoldCords_y,HoldCords_z);

    }

    private void OnShoot()
    {
       if (!shooting && !reloading)
       {
        Debug.Log("hewo");
        Invoke("animeReset",.5f);
        shooting = true;
        animator.Play(animationNames[0]);
        arms.Play(animationNames[0] + includeArms);
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
        arms.Play("pose" + includeArms);
    }
    private void OnReload()
    {
        reloading = true;
        Invoke("animeReset", 2.5f);
        animator.Play("reload");
        arms.Play("reload" + includeArms);
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
