using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DemoLogic2 : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public float MovementSpeed = 2f;

    Transform TargetTransform;
    System.Action ReachedTarget;

    // Start is called before the first frame update
    void Start()
    {
        MoveTo(StartPoint, () =>
        {
            TargetTransform = Random.value < 0.5f ? StartPoint : EndPoint;
        });
    }

    void MoveTo(Transform newTarget, System.Action onReachedTarget)
    {
        TargetTransform = newTarget;
        ReachedTarget = onReachedTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - TargetTransform.position).sqrMagnitude < float.Epsilon)
        {
            ReachedTarget.Invoke();
        }

        transform.position = Vector3.MoveTowards(transform.position, TargetTransform.position, MovementSpeed * Time.deltaTime);
    }
}
