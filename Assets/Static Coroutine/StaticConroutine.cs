/*
 *  协程必须要让动态的实例来启动，而静态方法无法通过实例调用，这就导致静态方法无法启动协程
 *  
 *  要让静态方法启动协程最简单的方法是把协程发给一个动态的对象来执行
 */

using System.Collections;
using UnityEngine;

public class StaticConroutine : MonoBehaviour
{
    static StaticConroutine instance
    {
        get { return GetInstance(); }
    }
    static StaticConroutine _instance;
    static StaticConroutine GetInstance()
    {
        if (_instance != null)
            return _instance;

        _instance = new GameObject("Static Conroutine Instance").AddComponent<StaticConroutine>();
        return _instance;
    }


    public static void Start(IEnumerator coroutine)     //静态方法通过调用这个方法来执行协程，原理就是把协程传给动态实例，让他来执行
    {
        instance.StartCoroutine(coroutine);
    }


    private void OnDestroy()
    {
        StopAllCoroutines();            //执行协程的实力在场景中存在，如果实例被销毁的话则停止所有协程的进行，一般来说会发生在场景切换的时候
    }
}
