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
    
    // �߻�ü Pool
    private ObjectPool bulletPool;

    private float attackRate = 0.5f;    // ���� �ӵ�
    private float attackRange = 2.0f;   // ���� ����
    private WeaponState weaponState = WeaponState.SearchTarget; // Ÿ�� ����
    
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
