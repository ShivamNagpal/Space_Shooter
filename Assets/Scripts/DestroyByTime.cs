using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float lifetime;

    private void Start()
    {
        Destroy(this.gameObject, lifetime);
    }

}
