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
        SpawnNPC();
        yield return new WaitForSeconds(3);
    }

    public GameObject SpawnFromPool(int index)
    {
        GameObject select = null;
        foreach (GameObject pool in pools[index])
        {
            if (!pool.activeSelf)
            {
                select = pool;
                select.SetActive(true);
                StartCoroutine(DeactivateTrapAfterDelay(select, 2f));
                break;
            }
        }
        if (!select)
        {
            select = Instantiate(prefeb[index], transform);
            pools[index].Add(select);
            StartCoroutine(DeactivateTrapAfterDelay(select, 2f));
        }

        return select;
    }
    void SpawnNPC()
    {
        int randomPrefeb = Random.Range(0, prefeb.Length);
        GameObject NPC = SpawnFromPool(randomPrefeb);
        NPC.transform.position = spawnPosition.position;
    }
    IEnumerator DeactivateTrapAfterDelay(GameObject select, float delay)
    {

        yield return new WaitForSeconds(delay);

        select.SetActive(false);
    }
}
