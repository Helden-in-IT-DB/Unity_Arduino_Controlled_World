using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private Animator gun;
    [SerializeField] private string gunFire = "fire";
    [SerializeField] private string gunidle = "idle";
    [SerializeField] private string gunwalk = "walk";
    [SerializeField] private string gunreload = "reload";
    [SerializeField] private string gunhold = "pose";

    private string currentState;

    private bool shoot;
    private bool idle;
    private bool walk;
    private bool reload;
    private bool hold;
    private float timer;
    private float maxtime = 3.5f;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = maxtime;
    }

    private void OnShoot()
    {
        shoot = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (shoot)
        {
            changeAnimationState(gunFire);

        }
        else { changeAnimationState(gunidle); }
    }
    void changeAnimationState(string Newstate)
    {
        if (currentState == Newstate) return;

        gun.Play(Newstate);

        currentState = Newstate;

    }
}
