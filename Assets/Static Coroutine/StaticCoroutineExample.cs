using System.Collections;
using UnityEngine;

public class StaticCoroutineExample : MonoBehaviour
{
    private void Start()
    {
        DoStartCoroutine();
    }

    static void DoStartCoroutine()
    {
        StaticConroutine.Start(LogTime());          //使用很简单，通过类名调用，穿进去协程就行
    }

    static IEnumerator LogTime()
    {
        while (Time.time < 10)
        {
            Debug.Log(Time.time);
            yield return null;
        }
    }
}
