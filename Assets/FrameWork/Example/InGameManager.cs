using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CompanyName.FrameWork;

public class InGameManager : IPlayManager
{
    protected override void OnEnter()
    {

    }

    protected override void OnExit()
    {
    
    }

    protected override IEnumerator _cGameFlow()
    {

        while(true)
        {

            yield return null;
        }
        //yield return YieldInstructionCache.WaitForSecond(1.0f);  
    }
}
