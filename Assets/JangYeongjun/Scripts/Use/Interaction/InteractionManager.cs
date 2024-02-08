using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionManager : MonoBehaviour
{
    #region Singleton
    public static InteractionManager interactionInstance;

    private void Awake()
    {
        if (interactionInstance == null)
        {
            interactionInstance = this;
        }
        else if (interactionInstance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    [SerializeField] GameObject[] panels;
    [SerializeField] GameObject[] Machines;
    private int storeDistinguishValue;
    public PlayerStatus playerStatus;
    private void Start()
    {
        for(int i = 0; i < panels.Length; i++) 
        {
            panels[i].gameObject.SetActive(false);
        }
        playerStatus = FindObjectOfType<PlayerStatus>();
    }
    public void SetValue(int value)
    {
        storeDistinguishValue = value;
    }


    void OnInteraction()
    {
        if (storeDistinguishValue < 100)
        {
            if (storeDistinguishValue > 0 && storeDistinguishValue < 50)
            {
                panels[storeDistinguishValue - 1].gameObject.SetActive(true);
            }
            else
            {
                playerStatus.IsServed(storeDistinguishValue - 50);
            }
        }
    }
}
