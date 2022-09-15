using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CompanyName.FrameWork;
using CompanyName.Function;


public class TestInGameManager : MonoBehaviour
{
    // Player
    List<ObjectPool> playerPool = new List<ObjectPool>();

    // Enemy
    List<ObjectPool> testPool = new List<ObjectPool>();

    [SerializeField] Transform enemyWayPointRoot1P;
    [SerializeField] Transform enemyWayPointRoot2P;

    private List<Transform> enemyMoveWayPoint1P = new List<Transform>();
    private List<Transform> enemyMoveWayPoint2P = new List<Transform>();

    [HideInInspector] public List<TestCircle> enemyList = new List<TestCircle>();
    [HideInInspector] public List<TestTower> towerList = new List<TestTower>();

    void Start()
    {
        // WayPoint 
        for(int i = 0;i < enemyWayPointRoot1P.childCount;i++)
        {
            enemyMoveWayPoint1P.Add(enemyWayPointRoot1P.GetChild(i));
        }

        for (int i = 0; i < enemyWayPointRoot2P.childCount; i++)
        {
            enemyMoveWayPoint2P.Add(enemyWayPointRoot2P.GetChild(i));
        }

        testPool.Add(ModelManager.Instance.objPool[OBJ_POOL.Enemy001]);

        playerPool.Add(ModelManager.Instance.objPool[OBJ_POOL.TestCircle001]);
        playerPool.Add(ModelManager.Instance.objPool[OBJ_POOL.TestCircle002]);
        playerPool.Add(ModelManager.Instance.objPool[OBJ_POOL.TestCircle003]);
        playerPool.Add(ModelManager.Instance.objPool[OBJ_POOL.TestCircle004]);
        playerPool.Add(ModelManager.Instance.objPool[OBJ_POOL.TestCircle005]);
    }


    private void SpawnTower(Transform hit)
    {
        TestTile tile = hit.transform.GetComponent<TestTile>();

        if (tile.IsBuildTower) return;

        tile.IsBuildTower = true;

        TestTower obj = playerPool[Random.Range(0, playerPool.Count)].GetObject() as TestTower;
        obj.SetUp(this);
        obj.transform.position = hit.transform.position;

        towerList.Add(obj);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TestCircle obj = testPool[Random.Range(0, testPool.Count)].GetObject() as TestCircle;
            obj.SetUp(this,enemyMoveWayPoint1P);

            enemyList.Add(obj);
            
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            testPool[Random.Range(0, testPool.Count)].PoolObject();
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = CameraManager.Instance.hitRayCast();

            if (hit.transform.CompareTag("Tile"))
            {
                SpawnTower(hit.transform);
            }
        }
    }

}
