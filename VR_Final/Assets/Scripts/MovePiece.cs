using UnityEngine;
using System.Collections;

public class MovePiece : MonoBehaviour
{
    private Vector3 start;
    public Vector3 end;
    private Vector3 arc;

    // Time to move from sunrise to sunset position, in seconds.
    public float journeyTime = 1.0f;

    // The time at which the animation started.
    private float startTime;

    void Start()
    {
        start = transform.position;
        arc  = start + (end - start) / 2 + Vector3.up * 1f;
    }
    
        float count = 0.0f;
        void Update()
        {
            if (count < 1.0f)
            {
                count += 1.0f * Time.deltaTime;

                Vector3 m1 = Vector3.Lerp(start, arc, count);
                Vector3 m2 = Vector3.Lerp(arc, end, count);
                transform.position = Vector3.Lerp(m1, m2, count);
            }
        }
}