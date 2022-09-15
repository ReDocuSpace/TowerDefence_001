using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class WaitMouseDown : CustomYieldInstruction
{
    public override bool keepWaiting
    {
        get
        {
            return !Input.GetMouseButtonDown(1);
        }
    }

    public WaitMouseDown()
    {
        Debug.Log("Waiting for Mouse right button down");
    }
}
class WaitKeyDown : CustomYieldInstruction
{
    KeyCode keyCode;
    public override bool keepWaiting
    {
        get
        {
            return !Input.GetKeyDown(keyCode);
        }
    }

    public WaitKeyDown(KeyCode keyCode)
    {
        this.keyCode = keyCode;

        Debug.Log("Waiting for Mouse right button down");
    }
}

internal static class YieldInstructionCache
{
    public static readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    public static readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
    public static readonly Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();

    public static readonly Dictionary<float, WaitForSeconds> waitForKeyCode = new Dictionary<float, WaitForSeconds>();


    public static WaitForSeconds WaitForSecond(float seconds)
    {
        WaitForSeconds wfs;

        if (!waitForSeconds.TryGetValue(seconds, out wfs))
            waitForSeconds.Add(seconds, wfs = new WaitForSeconds(seconds));
        return wfs;
    }


    // Experimental
    public static WaitMouseDown WaitForMouseDown()
    {
        WaitMouseDown mouseDown = new WaitMouseDown();

        return mouseDown;
    }
    public static WaitKeyDown WaitForKeyDown(KeyCode code)
    {
        WaitKeyDown keyDown = new WaitKeyDown(code);

        return keyDown;
    }
}