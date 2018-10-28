/*
 *  把静态调用协程和协程返回写到一个脚本里...
 *  
 *  第一次有了静态调用协程的类
 *  有了能从协程获得返回值的类
 *  这两个类写在一起
 *  怎么感觉这么的没技术呢
 *  
 *  就是给协程返回类加了一个构造方法，在不传 MonoBehaviour 的时候走静态调用
 */

using System.Collections;
using UnityEngine;

public class StaticReturnCoroutine : MonoBehaviour
{
    static StaticReturnCoroutine instance
    {
        get
        {
            if (_instance != null)
                return _instance;

            _instance = new GameObject("Static Return Coroutine Instance").AddComponent<StaticReturnCoroutine>();
            return _instance;
        }
    }
    static StaticReturnCoroutine _instance;


    public static Coroutine Start(IEnumerator coroutine)
    {
        return instance.StartCoroutine(coroutine);
    }


    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}


public class ReturnCoroutine<T>
{
    public Coroutine coroutine
    {
        get { return _coroutine; }
    }
    Coroutine _coroutine;

    public T result
    {
        get { return _result; }
    }
    T _result;


    //就在这加了这么一个构造方法，不传 mono 就直接走静态，别的改变一点没有
    public ReturnCoroutine(IEnumerator coroutine)
    {
        _coroutine = StaticReturnCoroutine.Start(Start(coroutine));
    }
    public ReturnCoroutine(MonoBehaviour mono, IEnumerator coroutine)
    {
        _coroutine = mono.StartCoroutine(Start(coroutine));
    }


    IEnumerator Start(IEnumerator coroutine)
    {
        while (coroutine.MoveNext())
            yield return coroutine.Current;
        _result = (T)coroutine.Current;
    }
}