using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTrail : MonoBehaviour
{
    public Vector3 targetPos;

     void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, 6 * Time.deltaTime);
    }
}
