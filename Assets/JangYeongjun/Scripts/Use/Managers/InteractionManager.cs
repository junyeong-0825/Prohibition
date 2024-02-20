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
    private int storeDistinguishValue;
    [SerializeField] PlayerStatus playerStatus;
    [SerializeField] GameObject alcoholPanel;
    private void Start()
    {
        Dictionary<string, GameObject>[] storePanels = new Dictionary<string, GameObject>[panels.Length];
        for(int i = 0; i < panels.Length; i++) 
        {
            panels[i].gameObject.SetActive(false);
            storePanels[i].Add(panels[i].name, panels[i]);
        }
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
            else if(storeDistinguishValue == 50)
            {
                playerStatus.IsServed("Food");
            }
            else if(storeDistinguishValue == 51)
            {
                alcoholPanel.SetActive(true);
            }

        }
    }
}
