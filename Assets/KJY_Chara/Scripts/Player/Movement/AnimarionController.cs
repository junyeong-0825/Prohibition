using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimarionController : MonoBehaviour
{
    private Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        ChangeParamitor(PlayerInputController.instance.inputX, PlayerInputController.instance.inputY);
    }

    public void ChangeParamitor(float inputx, float inputy)
    {
        anim.SetFloat("InputX", inputx);
        anim.SetFloat("InputY", inputy);
        if (inputx != 0 || inputy != 0)
            anim.SetBool("IsMove", true);
        else
            anim.SetBool("IsMove", false);
    }
}
