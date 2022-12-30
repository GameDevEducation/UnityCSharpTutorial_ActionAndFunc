using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoLogic : MonoBehaviour
{
    public enum EMovementMode
    {
        Bounce,
        Rotate,
        BounceAndRotate
    }

    System.Action DemoAction1;
    System.Func<float> DemoFunc1;
    System.Func<int, int, float> DemoFunc2;

    Dictionary<EMovementMode, System.Action<float>> MovementFns;

    public EMovementMode MovementMode = EMovementMode.Bounce;

    Vector3 InitialPosition;

    // Start is called before the first frame update
    void Start()
    {
        InitialPosition = transform.position;

        MovementFns = new Dictionary<EMovementMode, System.Action<float>>();
        MovementFns[EMovementMode.Bounce] = Move_Bounce;
        MovementFns[EMovementMode.Rotate] = Move_Rotate;
        MovementFns[EMovementMode.BounceAndRotate] = (float deltaTime) =>
        {
            transform.position = InitialPosition + Vector3.up * Mathf.Sin(Time.time * Mathf.PI);
            transform.Rotate(0f, 0f, 30 * deltaTime);
        };

        DemoAction1 = DemoFunction1;
        DemoAction1.Invoke();

        DemoFunc1 = DemoFunction2;
        Debug.Log(DemoFunc1.Invoke());

        DemoFunc2 = DemoFunction3;
        Debug.Log(DemoFunc2.Invoke(1, 100));
    }

    // Update is called once per frame
    void Update()
    {
        if (MovementFns.ContainsKey(MovementMode) && MovementFns[MovementMode] != null)
            MovementFns[MovementMode].Invoke(Time.deltaTime);
    }

    void DemoFunction1()
    {
        Debug.Log("Demo Function 1");
    }

    float DemoFunction2()
    {
        return Random.Range(1, 12);
    }

    float DemoFunction3(int minValue, int maxValue)
    {
        return Random.Range(minValue, maxValue);
    }

    void Move_Bounce(float deltaTime)
    {
        transform.position = InitialPosition + Vector3.up * Mathf.Sin(Time.time * Mathf.PI);
    }

    void Move_Rotate(float deltaTime)
    {
        transform.Rotate(0f, 0f, 30 * deltaTime);
    }
}
