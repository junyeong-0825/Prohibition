using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MaybeNPC : MonoBehaviour
{
    public Transform spawnPosition;
    public Transform[] movePosition;
    public GameObject[] prefeb;
    public List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefeb.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }
    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while(true)
        {
            SpawnFromPool();
            yield return new WaitForSeconds(3);
        }
    }

    public GameObject SpawnFromPool()
    {
        int index = Random.Range(0, prefeb.Length);
        GameObject select = null;
        foreach (GameObject pool in pools[index])
        {
            if (!pool.activeSelf)
            {
                select = pool;
                select.SetActive(true);
                select.transform.position = spawnPosition.position;
                StartCoroutine(DeactivateTrapAfterDelay(select));
                break;
            }
        }
        if (!select)
        {
            select = Instantiate(prefeb[index], transform);
            select.transform.position = spawnPosition.position;
            pools[index].Add(select);
            StartCoroutine(DeactivateTrapAfterDelay(select));
        }

        return select;
    }
    IEnumerator DeactivateTrapAfterDelay(GameObject select)
    {

        yield return new WaitForSeconds(2.0f);

        select.SetActive(false);
    }
}
