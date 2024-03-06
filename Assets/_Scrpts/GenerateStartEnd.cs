using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateStartEnd : MonoBehaviour
{
    public GameObject EndPoint;
    public float distance = 30;

    private void Awake()
    {
        float x = Random.Range(-1, 1f);
        float y = Random.Range(-1, 1f);

        while (x == 0 || y == 0)
        {
            x = Random.Range(-1, 1f);
            y = Random.Range(-1, 1f);
        }


        Vector3 end = new Vector3(x, y, 0).normalized * distance;
        EndPoint.transform.position = end;
    }
}
