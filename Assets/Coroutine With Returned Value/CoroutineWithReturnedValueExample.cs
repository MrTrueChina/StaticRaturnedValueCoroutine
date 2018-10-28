using System.Collections;
using UnityEngine;

public class CoroutineWithReturnedValueExample : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DoStartCoroutine());
    }

    //使用方法在这
    IEnumerator DoStartCoroutine()
    {
        CoroutineWithReturnedValue<string> instance = new CoroutineWithReturnedValue<string>(this, WaitTwoSeconedLog());    //首先创建对象，传入一个 MonoBehaviour 和一个协程，自己就是 MonoBehaviour 直接传进去了
        yield return instance.coroutine;        //等待创建的对象的协程执行完毕，注意是直接 yield return，因为协程在创建对象时就已经开始执行了，不需要再启动
        string result = instance.result;        //等执行完毕后从对象里取出返回值


        Debug.Log(result);
    }

    IEnumerator WaitTwoSeconedLog()
    {
        for (int i = 0; i < 10; i++)
            yield return new WaitForSeconds(0.2f);
        yield return "Hellow World!";
    }
}
