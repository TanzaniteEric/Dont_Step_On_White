using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemy : MonoBehaviour
{
    public int EnemyGenerateRateOverTime = 2;
    public int EnemyAmountToGenerateEveryTime = 2;
    private Transform playerPos;
    public endpoint end;
    private PlayerFarestPos endDetect;

    private void Awake()
    {
        playerPos = FindObjectOfType<PlayerController>().transform;
        endDetect = FindObjectOfType<PlayerFarestPos>();
    }

    private void Start()
    {
        StartCoroutine(CreateEnemy());
    }

    private IEnumerator CreateEnemy()
    {
        if (!endDetect.gameEnd)
        {
            for (int j = 0; j < EnemyAmountToGenerateEveryTime; j++)
            {
                CreateOneEnemy();
            }

            yield return new WaitForSeconds(EnemyGenerateRateOverTime);

            StartCoroutine(CreateEnemy());
        }
    }

    private void CreateOneEnemy()
    {
        float x = Random.Range(-1, 1f);
        float y = Random.Range(-1, 1f);

        Vector3 rotateVector = new Vector3(x, y, 0).normalized;
        Vector3 endVector = (end.transform.position - playerPos.position).normalized * 0.4f;

        rotateVector = (rotateVector + endVector).normalized * 10;

        int i = Random.Range(0, 3);

        if (i == 0)
        {
            GameObject tmp = ObjectPooler.ObjectInstance.GetEnemy01();
            tmp.SetActive(true);
            tmp.transform.position = playerPos.position + rotateVector;
        }
        else if (i == 1)
        {
            GameObject tmp = ObjectPooler.ObjectInstance.GetEnemy02();
            tmp.SetActive(true);
            tmp.transform.position = playerPos.position + rotateVector;
        }
        else
        {
            GameObject tmp = ObjectPooler.ObjectInstance.GetEnemy03();
            tmp.SetActive(true);
            tmp.transform.position = playerPos.position + rotateVector;
        }
    }
}
