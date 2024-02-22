using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchingCCTV : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    
    // 처음에 버튼을 누르면 FirstOnCCTV 파라미터가 True로 변경
    // 그 다음부터 버튼을 누르면 OffCCTV 파라미터가 true가 되면 화면이 내려가고 false가 되면 다시 올라옴
    public void OnCam(InputValue value)
    {
        if(value.isPressed == false)
        {
            return;
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("CCTV_Idle"))
        {
            anim.SetBool("FirstOnCCTV", true);
            anim.SetBool("OffCCTV", false);
            Debug.Log("CCTV 첫가동");
        }
        else
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("CCTV_On"))
            {
                anim.SetBool("OffCCTV", true);
                Debug.Log("CCTV 종료");
            }
            else
            {
                anim.SetBool("OffCCTV", false);
                Debug.Log("CCTV 재가동");
            }
        }

    }
}
