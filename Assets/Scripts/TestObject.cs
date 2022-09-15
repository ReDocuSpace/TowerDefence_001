using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CompanyName.Function;

public class TestObject : IObject
{
    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;
    private Movement2D movement2D;

    public void Setup(Transform[] waypoint)
    {
        movement2D = GetComponent<Movement2D>();

        wayPointCount = waypoint.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = waypoint;

        transform.position = waypoint[currentIndex].position;

        StartCoroutine("OnMove");

    }

    private IEnumerator OnMove()
    {
        NextMoveTo();

        while(true)
        {
            transform.Rotate(Vector3.forward * 10);

            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();

            }

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if(currentIndex < wayPointCount -1)
        {
            transform.position = wayPoints[currentIndex].position;

            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement2D.MoveTo(direction);
        }
        else
        {
            PoolObject();
        }
    }



    // Override
    public override void OnDisabled()
    {
       
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
}
