using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusItemPosition : MonoBehaviour
{
    [SerializeField] GameObject statusItem;
    private void FixedUpdate()
    {
        ChangeTransform(PlayerInputController.instance.inputX, PlayerInputController.instance.inputY);
    }

    void ChangeTransform(float inputX, float inputY)
    {
        if(inputX == 0 && inputY == 0)
        {
            statusItem.SetActive(true);
            statusItem.transform.localPosition = new Vector3(-0.28f,transform.localPosition.y, 0);
        }
        else if(inputX == 1)
        {
            statusItem.SetActive(true);
            statusItem.transform.localPosition = new Vector3(0f, transform.localPosition.y, 0);
        }
        else if(inputX == -1)
        {
            statusItem.SetActive(false);
        }
        else if(inputY > 0)
        {
            statusItem.SetActive(true);
            statusItem.transform.localPosition = new Vector3(0.28f, transform.localPosition.y, 0);
        }
        else if (inputY < 0)
        {
            statusItem.SetActive(true);
            statusItem.transform.localPosition = new Vector3(-0.28f, transform.localPosition.y, 0);
        }
    }
}
