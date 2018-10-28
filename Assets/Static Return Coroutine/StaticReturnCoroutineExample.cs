using System.Collections;
using UnityEngine;

public class StaticReturnCoroutineExample : MonoBehaviour
{
    private void Start()
    {
        DoExample();
    }

    static void DoExample()
    {
        StaticReturnCoroutine.Start(StartWaitThreeSecondLog());
        StaticReturnCoroutine.Start(StartWaitTwoSecondLog());
        StaticReturnCoroutine.Start(StartWaitOneSecondLog());
    }

    static IEnumerator StartWaitThreeSecondLog()
    {
        ReturnCoroutine<string> bc = new ReturnCoroutine<string>(WaitThreeSecondLog());
        yield return bc.coroutine;
        string result = bc.result;

        Debug.Log(result);
    }

    static IEnumerator StartWaitTwoSecondLog()
    {
        ReturnCoroutine<string> bc = new ReturnCoroutine<string>(WaitTwoSecondLog());
        yield return bc.coroutine;
        string result = bc.result;

        Debug.Log(result);
    }

    static IEnumerator StartWaitOneSecondLog()
    {
        ReturnCoroutine<string> bc = new ReturnCoroutine<string>(WaitOneSecondLog());
        yield return bc.coroutine;
        string result = bc.result;

        Debug.Log(result);
    }

    static IEnumerator WaitThreeSecondLog()
    {
        yield return new WaitForSeconds(3);
        yield return "!";
    }

    static IEnumerator WaitTwoSecondLog()
    {
        yield return new WaitForSeconds(2);
        yield return "World";
    }

    static IEnumerator WaitOneSecondLog()
    {
        yield return new WaitForSeconds(1);
        yield return "Hellow";
    }
}
