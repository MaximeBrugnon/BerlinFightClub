using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustCloud : MonoBehaviour
{
    private float lifeTime = 0.5f;
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }
}
