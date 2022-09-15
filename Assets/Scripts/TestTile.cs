using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTile : MonoBehaviour
{
    public bool IsBuildTower { set; get; }

    private void Awake()
    {
        IsBuildTower = false;
    }

}
