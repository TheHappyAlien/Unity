using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour
{

    void Start()
    {
        if (gameObject.name == "Dust(Clone)")
        {
            Destroy(gameObject, 1f);
        }
    }

}
