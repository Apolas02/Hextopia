using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    public Animator ani;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // run-walk-sprint animation
        if (Input.GetKey(KeyCode.W))
        {
            // walk animations
            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    walk_jump();
                }
                else
                {
                    walk();
                }
            }

            // sprint animations
            else if (Input.GetKey(KeyCode.LeftShift))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    sprint_jump();
                }
                else
                {
                    sprint();
                }
            }

            // run animations
            else if (Input.GetKey(KeyCode.Space))
            {
                run_jump();
            }
            else
            {
                run();
            }
        }

        // reverse walk animations
        else if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                walk_reverse_jump();
            }
            else
            {
                walk_reverse();
            }
        }

        // idle animations
        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                jump();
            }
            else
            {
                idle();
            }
        }
    }


    // idle animations
    void idle()
    {
        ani.SetBool("Idle", true);
        ani.SetBool("Walk", false);
        ani.SetBool("Run", false);
        ani.SetBool("Sprint", false);
        ani.SetBool("Walk_Reverse", false);
        ani.SetBool("Jump", false);
    }
    void jump()
    {
        ani.SetBool("Jump", true);
        ani.SetBool("Walk_Jump", false);
        ani.SetBool("Run_Jump", false);
        ani.SetBool("Sprint_Jump", false);
    }


    // walk animations
    void walk()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", true);
        ani.SetBool("Run", false);
        ani.SetBool("Sprint", false);
        ani.SetBool("Walk_Jump", false);
    }
    void walk_jump()
    {
        ani.SetBool("Jump", false);
        ani.SetBool("Walk_Jump", true);
        ani.SetBool("Run_Jump", false);
        ani.SetBool("Sprint_Jump", false);
    }


    // run antimations
    void run()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", false);
        ani.SetBool("Run", true);
        ani.SetBool("Sprint", false);
        ani.SetBool("Run_Jump", false);
    }
    void run_jump()
    {
        ani.SetBool("Jump", false);
        ani.SetBool("Walk_Jump", false);
        ani.SetBool("Run_Jump", true);
        ani.SetBool("Sprint_Jump", false);
    }


    // sprint animations
    void sprint()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk", false);
        ani.SetBool("Run", false);
        ani.SetBool("Sprint", true);
        ani.SetBool("Sprint_Jump", false);
    }
    void sprint_jump()
    {
        ani.SetBool("Jump", false);
        ani.SetBool("Walk_Jump", false);
        ani.SetBool("Run_Jump", false);
        ani.SetBool("Sprint_Jump", true);
    }


    // reverse walk animations
    void walk_reverse()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk_Reverse", true);
        ani.SetBool("Walk_Reverse_Jump", false);
    }
    void walk_reverse_jump()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Walk_Reverse", false);
        ani.SetBool("Walk_Reverse_Jump", true);
    }
}
