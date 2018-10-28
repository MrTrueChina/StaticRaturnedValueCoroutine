/*
 *  从协程获得返回值
 *  
 *  协程和普通方法的最直接区别就是协程的返回值必须是 IEnumrator，这导致返回值被占用了，而且协程还不能用 out 和 ref，要从协程获取返回值只能靠自写
 *  
 *  一般方法的返回值在方法的最后返回，返回的同时方法执行也就结束了，而协程的特性让我们可以多次返回，这些返回都可以用来作为返回值的通道，但为了和普通方法一致，这个脚本用了最后一个返回值
 */

using System.Collections;
using UnityEngine;

public class CoroutineWithReturnedValue<T>
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


    /*
     *  传入一个 MonoBehaviour 和一个 IEnumerator
     * 
     *  用来启动协程的 StartCoroutine 是 MonoBehaviour 的方法，所以传进一个 MonoBehaviour 来启动协程
     */
    public CoroutineWithReturnedValue(MonoBehaviour mono, IEnumerator coroutine)
    {
        _coroutine = mono.StartCoroutine(Start(coroutine));     //启动协程并保存下来
    }

    
    IEnumerator Start(IEnumerator coroutine)
    {
        while (coroutine.MoveNext())            //IEnumerator.MoveNext()：让迭代器向后前进一个元素，可以理解为前进一个 yield return，如果前进成功则返回 true，到底后不能继续前进则返回 false
            yield return coroutine.Current;     //IEnumerator.Current：返回迭代器当前元素的返回值，就是 yield return 返回的值
        _result = (T)coroutine.Current;         //
        /*
         *  逐句解释：
         *  
         *  while (coroutine.MoveNext())
         *      IEnumerator.MoveNext()：让迭代器（IEnumerator）向后前进一个元素，可以理解为前进一个 yield return，如果前进成功则返回 true，到底后不能继续前进则返回 false
         *      这样可以一直循环到迭代器迭代到最后一个元素
         *      
         *      yield return coroutine.Current;
         *          IEnumerator.Current：返回迭代器当前元素的返回值，就是 yield return 返回的值
         *          MoveNext()只负责前进，不负责处理，像是 WaitForSeconds 就不会起效，需要一个 yield return 接住返回值，交给外面的这个协程来执行
         * 
         *  _result = (T)coroutine.Current;
         *      在循环到最后一个元素之后，这个元素的返回值就是我们需要的返回值
         *      用 IEnumerator.Current 获取返回值，之后强转到泛型，保存进返回值
         */
    }
}
