using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CompanyName.Function;

public enum WeaponState
{
    SearchTarget = 0,
    AttackToTarget
}

public class TestTower : IObject
{
    MonoBehaviour playManager;
    
    // 발사체 Pool
    private ObjectPool bulletPool;

    private float attackRate = 0.5f;    // 공격 속도
    private float attackRange = 2.0f;   // 공격 범위
    private WeaponState weaponState = WeaponState.SearchTarget; // 타워 상태
    
    private Transform attackTarget = null;
    private TestCircle enemyTarget = null;

    public void SetUp(MonoBehaviour playManager)
    {
        this.playManager = playManager as TestInGameManager;
    }


    //
    public override void OnDisabled()
    {
        
    }

    public override void OnEnter()
    {
        bulletPool = GetComponent<ObjectPool>();
    }

    public override void OnExit()
    {
      
    }

    public override void OnInit()
    {

    }
}
