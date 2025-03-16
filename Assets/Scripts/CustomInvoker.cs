using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System.Collections.Generic;

public class CustomInvoker : MonoBehaviour
{
    private static List<CustomInvoker> activeInvokes = new List<CustomInvoker>();

    private Session session;

    private UnityEvent invokeEvent;

    private UnityAction call;
    private float timeSinceInvoked = 0;
    private float delay = 0;

    private bool running = false;

    void Awake()
    {
        invokeEvent = new UnityEvent();
        session = Session.Instance;
    }

    void Update()
    {
        if (!running) return;

        timeSinceInvoked += Time.deltaTime * session.gameSpeed;

        if(timeSinceInvoked >= delay)
        {
            invokeEvent.Invoke();
            DestroyThis();
        }
    }

    private void _Invoke(UnityAction call, float delay)
    {
        this.call = call;
        invokeEvent.AddListener(call);
        this.delay = delay;
        running = true;
    }

    private void DestroyThis()
    {
        running = false;

        invokeEvent.RemoveAllListeners();
        activeInvokes.Remove(this);
        Destroy(gameObject);
    }

    public static void Invoke(UnityAction call, float delay)
    {
        GameObject gameObject = new GameObject();
        CustomInvoker customInvoker = gameObject.AddComponent<CustomInvoker>();

        activeInvokes.Add(customInvoker);
        customInvoker._Invoke(call, delay);
    }

    public static void CancelInvoke(UnityAction call)
    {
        for (int i = 0; i < activeInvokes.Count; i++)
        {
            if (activeInvokes[i].call == call)
            {
                activeInvokes[i].DestroyThis();
                return;
            }
        }
    }
}