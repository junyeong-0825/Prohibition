using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class MaybeNPC : MonoBehaviour
{
    #region Fields
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform[] movingPoint;
    [SerializeField] Transform endPoint;
    [SerializeField] public GameObject[] prefeb;
    [SerializeField] public List<GameObject>[] pools;
    bool upperLeftFirstTable = false;
    bool upperRightFirstTable = false;
    bool DownLeftFirstTable = false;
    bool DownRightFirstTable = false;
    int customerCount = 0;
    int upperLeftCount = 0;
    int upperRightCount = 0;
    int downCount = 0;
    int downLeftCount = 0;
    int downRightCount = 0;
    #endregion
    #region ObjectPool
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
        while (true)
        {
            int spawndelay = Random.Range(5, 11);
            SpawnFromPool();
            yield return new WaitForSeconds(spawndelay);
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
                select.transform.position = spawnPoint.position;
                StartCoroutine(EnterDecision(select));
                break;
            }
        }
        if (!select)
        {
            select = Instantiate(prefeb[index], transform);
            select.transform.position = spawnPoint.position;
            pools[index].Add(select);
            StartCoroutine(EnterDecision(select));
        }

        return select;
    }
    #endregion
    #region �̵��ϴ� ����
    // �߰�: �� ������Ʈ�� �̵� ���¸� �����ϴ� ����ü
    private struct MoveState
    {
        public bool isMoving;
        public Vector3 targetPosition;
        public float moveSpeed;
    }

    private Dictionary<GameObject, MoveState> moveStates = new Dictionary<GameObject, MoveState>();

    private void Update()
    {
        foreach (var kvp in moveStates)
        {
            if (kvp.Value.isMoving)
            {
                GameObject obj = kvp.Key;
                MoveState state = kvp.Value;
                obj.transform.position = Vector3.MoveTowards(obj.transform.position, state.targetPosition, state.moveSpeed * Time.deltaTime);

                if (obj.transform.position == state.targetPosition)
                {
                    // ��ǥ�� �������� ���� ����
                    state.isMoving = false;
                    moveStates[obj] = state;
                }
            }
        }
    }

    // EnterDecision, CheckCustomerCount ���� �޼ҵ忡���� �̵� ���� ����
    IEnumerator EnterDecision(GameObject select)
    {
        // �̵� ���� ����
        moveStates[select] = new MoveState
        {
            isMoving = true,
            targetPosition = movingPoint[0].position,
            moveSpeed = 5.0f // �̵� �ӵ�
        };

        System.Random rand = new System.Random();
        bool firstDecision = rand.Next(2) == 1;

        if (firstDecision)
        {
            yield return StartCoroutine(CheckCustomerCount(select));
        }
        else 
        {
            moveStates[select] = new MoveState
            {
                isMoving = true,
                targetPosition = endPoint.position,
                moveSpeed = 5.0f // �̵� �ӵ�
            };
            select.SetActive(false);
        }
    }

    IEnumerator CheckCustomerCount(GameObject select)
    {
        moveStates[select] = new MoveState
        {
            isMoving = true,
            targetPosition = movingPoint[1].position,
            moveSpeed = 5.0f // �̵� �ӵ�
        };

        if (customerCount < 8)
        {
            customerCount++;
            yield return StartCoroutine(FirstJunction(select));
        }
        else
        {
            moveStates[select] = new MoveState
            {
                isMoving = true,
                targetPosition = endPoint.position,
                moveSpeed = 5.0f // �̵� �ӵ�
            };
            select.SetActive(false);
        }
    }
    IEnumerator FirstJunction(GameObject select)
    {
        moveStates[select] = new MoveState
        {
            isMoving = true,
            targetPosition = movingPoint[2].position,
            moveSpeed = 5.0f // �̵� �ӵ�
        };
        int secondDecision = Random.Range(0, 3);
        if (secondDecision == 0)
        {
            yield return StartCoroutine(CheckUpperLeft(select));
        }
        else if (secondDecision == 1)
        {
            yield return StartCoroutine(CheckUpperRight(select));
        }
        else if (secondDecision == 2)
        {
            yield return StartCoroutine(CheckDown(select));
        }
    }
    IEnumerator CheckUpperLeft(GameObject select)
    {

        if (upperLeftCount < 2)
        {
            upperLeftCount++;
            yield return StartCoroutine(GoUpperLeft(select));
        }
        else
        {
            yield return StartCoroutine(CheckUpperRight(select));
        }
    }
    IEnumerator GoUpperLeft(GameObject select)
    {
        if (upperLeftFirstTable == false)
        {
            moveStates[select] = new MoveState
            {
                isMoving = true,
                targetPosition = movingPoint[3].position,
                moveSpeed = 5.0f // �̵� �ӵ�
            };
            upperLeftFirstTable = true;
        }
        else
        {
            moveStates[select] = new MoveState
            {
                isMoving = true,
                targetPosition = movingPoint[4].position,
                moveSpeed = 5.0f // �̵� �ӵ�
            };
        }
        yield return null;
    }
    IEnumerator CheckUpperRight(GameObject select)
    {

        if (upperRightCount < 2)
        {
            upperRightCount++;
            yield return StartCoroutine(GoUpperRight(select));
        }
        else
        {
            yield return StartCoroutine(CheckDown(select));
        }
    }
    IEnumerator GoUpperRight(GameObject select)
    {

        if (upperRightFirstTable == false)
        {
            moveStates[select] = new MoveState
            {
                isMoving = true,
                targetPosition = movingPoint[5].position,
                moveSpeed = 5.0f // �̵� �ӵ�
            };
            upperRightFirstTable = true;
        }
        else
        {
            moveStates[select] = new MoveState
            {
                isMoving = true,
                targetPosition = movingPoint[6].position,
                moveSpeed = 5.0f // �̵� �ӵ�
            };
        }
        yield return null;
    }
    IEnumerator CheckDown(GameObject select)
    {

        if (downCount < 4)
        {
            downCount++;
            yield return StartCoroutine(GoDown(select));
        }
        else
        {
            yield return StartCoroutine(CheckUpperLeft(select));
        }
    }
    IEnumerator GoDown(GameObject select)
    {
        moveStates[select] = new MoveState
        {
            isMoving = true,
            targetPosition = movingPoint[7].position,
            moveSpeed = 5.0f // �̵� �ӵ�
        };
        yield return StartCoroutine(SecondJunction(select));
    }
    IEnumerator SecondJunction(GameObject select)
    {
        int thirdDecision = Random.Range(0, 2);
        if(thirdDecision == 0)
        {
            yield return StartCoroutine(CheckDownLeft(select));
        }
        else 
        {
            yield return StartCoroutine(CheckDownRight(select));
        }
    }
    IEnumerator CheckDownLeft(GameObject select)
    {

        if(downLeftCount < 2) 
        {
            downLeftCount++;
            yield return StartCoroutine(GoDownLeft(select));
        }
        else
        {
            yield return StartCoroutine(GoDownRight(select));
        }
    }
    IEnumerator GoDownLeft(GameObject select)
    {

        if (DownLeftFirstTable == false)
        {
            moveStates[select] = new MoveState
            {
                isMoving = true,
                targetPosition = movingPoint[8].position,
                moveSpeed = 5.0f // �̵� �ӵ�
            };
            DownLeftFirstTable = true;
        }
        else
        {
            moveStates[select] = new MoveState
            {
                isMoving = true,
                targetPosition = movingPoint[9].position,
                moveSpeed = 5.0f // �̵� �ӵ�
            };
        }
        yield return null;
    }
    IEnumerator CheckDownRight(GameObject select)
    {

        if (downRightCount < 2)
        {
            downRightCount++;
            yield return StartCoroutine(GoDownRight(select));
        }
        else
        {
            yield return StartCoroutine(GoDownLeft(select));
        }
    }
    IEnumerator GoDownRight(GameObject select)
    {

        if (DownRightFirstTable == false)
        {
            moveStates[select] = new MoveState
            {
                isMoving = true,
                targetPosition = movingPoint[10].position,
                moveSpeed = 5.0f // �̵� �ӵ�
            };
            DownRightFirstTable = true;
        }
        else
        {
            moveStates[select] = new MoveState
            {
                isMoving = true,
                targetPosition = movingPoint[11].position,
                moveSpeed = 5.0f // �̵� �ӵ�
            };
        }
        yield return null;
    }
    #endregion
}
