using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IDialogManager : MonoBehaviour
{
    public abstract void OnEnter();

    public abstract void OnPlay();
    public abstract void OnEnd();

    public abstract void OnExit();
}
