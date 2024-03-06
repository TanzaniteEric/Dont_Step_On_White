using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler ObjectInstance;

    [Header("Enemy01 Pool")]
    public List<GameObject> Enemy01Pool;
    public GameObject Enemy01toPool;
    public int Enemy01Amount = 20;

    [Header("Enemy02 Pool")]
    public List<GameObject> Enemy02Pool;
    public GameObject Enemy02toPool;
    public int Enemy02Amount = 20;

    [Header("Enemy03 Pool")]
    public List<GameObject> Enemy03Pool;
    public GameObject Enemy03toPool;
    public int Enemy03Amount = 20;

    private void Awake()
    {
        ObjectInstance = this;

        GenerateObject(Enemy01Pool, Enemy01toPool, Enemy01Amount);
        GenerateObject(Enemy02Pool, Enemy02toPool, Enemy02Amount);
        GenerateObject(Enemy03Pool, Enemy03toPool, Enemy03Amount);
    }

    void Start()
    {
        /*GenerateObject(Enemy01Pool, Enemy01toPool, Enemy01Amount);
        GenerateObject(Enemy02Pool, Enemy02toPool, Enemy02Amount);
        GenerateObject(Enemy03Pool, Enemy03toPool, Enemy03Amount);*/
    }


    private void GenerateObject(List<GameObject> enemyPool, GameObject enemytoPool, int enemyAmount)
    {
        //enemyPool = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < enemyAmount; i++)
        {
            tmp = Instantiate(enemytoPool);
            enemyPool.Add(tmp);
            tmp.SetActive(false);
        }
    }

    public GameObject GetEnemy01()
    {
        for (int i = 0; i < Enemy01Amount; i++)
        {
            if (!Enemy01Pool[i].activeInHierarchy)
            {
                return Enemy01Pool[i];
            }
        }

        return null;
    }
    public GameObject GetEnemy02()
    {
        for (int i = 0; i < Enemy02Amount; i++)
        {
            if (!Enemy02Pool[i].activeInHierarchy)
            {
                return Enemy02Pool[i];
            }
        }

        return null;
    }
    public GameObject GetEnemy03()
    {
        for (int i = 0; i < Enemy03Amount; i++)
        {
            if (!Enemy03Pool[i].activeInHierarchy)
            {
                return Enemy03Pool[i];
            }
        }

        return null;
    }
}
