using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CompanyName.Function;

public class TestGameManager : MonoBehaviour
{
    [SerializeField] IObject playerObj;

    [HideInInspector] ObjectPool playerObjPool;

    private void Awake()
    {
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerObjPool.GetObject();
        }

    }

}
