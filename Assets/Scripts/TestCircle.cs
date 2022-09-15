using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CompanyName.Function;

public class TestCircle : IObject
{
    private TestInGameManager playManager;

    private List<Transform> wayPointList = new List<Transform>();
    private int currentPoint = 0;

    private float moveSpeed = 1.0f;
    private Vector3 moveDirect = Vector3.zero;

    private Coroutine corPlayMove = null;

    public void SetUp(TestInGameManager playManager, List<Transform> wayPointList, int startPoint = 0)
    {
        OnDisabled();
        this.playManager = playManager;

        // WayPoint
        this.wayPointList = wayPointList;

        // Init
        currentPoint = startPoint;
        
        // Start Position
        transform.position = wayPointList[currentPoint].transform.position;

        if (corPlayMove == null) corPlayMove = StartCoroutine(cPlayMove());
    }

    // override 
    public override void OnDisabled()
    {
        if (corPlayMove != null) StopCoroutine(corPlayMove);
            corPlayMove = null;
    }
    public override void OnEnter()
    {
       
    }
    public override void OnExit()
    {
        
    }
    public override void OnInit()
    {
        
    }

    // Function
    private void NextMoveTo()
    {
        if (currentPoint < wayPointList.Count - 1)
        {
            transform.position = wayPointList[currentPoint].position;

            currentPoint++;
            Vector3 direction = (wayPointList[currentPoint].position - transform.position).normalized;
            moveDirect = direction;
        }
        else
        {
            PoolObject();
        }
    }

    //
    private IEnumerator cPlayMove()
    {
        NextMoveTo();

        while(true)
        {
            transform.position += moveDirect * moveSpeed * Time.deltaTime;

            if(Vector3.Distance(transform.position, wayPointList[currentPoint].position) < 0.02f * moveSpeed)
            {
                NextMoveTo();
            }
            yield return null;
        }
    }
}
