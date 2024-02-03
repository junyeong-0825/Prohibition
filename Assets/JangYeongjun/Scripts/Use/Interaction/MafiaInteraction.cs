using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MafiaInteraction : MonoBehaviour
{
    #region �̱���
    public static MafiaInteraction Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    #endregion
    [SerializeField] GameObject[] panels;
    private int storeDistinguishValue = 100;
    private void Start()
    {
        for(int i = 0; i < panels.Length; i++) 
        {
            panels[i].gameObject.SetActive(false);
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
            panels[storeDistinguishValue].gameObject.SetActive(true);
        }
    }
}
