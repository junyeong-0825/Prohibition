using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 경찰이 정찰 지점에 도착했을 때에 
public class SearchTrigger : MonoBehaviour
{
    private bool isSearchBegin;
    public bool IsSearchBegin { get { return isSearchBegin; } }
    private bool isTrigger;

    private void Awake()
    {
        isSearchBegin = false;
        isTrigger = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Police"))
        {
            isTrigger = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isTrigger && other.CompareTag("Police"))
        {
            isSearchBegin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Police"))
        {
            isTrigger = false;
            isSearchBegin = false;
        }
    }


    //void Start()
    //{
        
    //}


    //void Update()
    //{
        
    //}
}
