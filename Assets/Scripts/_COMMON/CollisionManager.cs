using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class CollisionManager
{
    private float impulseMultiplier = 5f;

    private struct JobResultStruct
    {
        public int ThisInstanceID;
        public int OtherInstanceID;
        public Vector3 AverageNormal;
    }

    private NativeArray<JobResultStruct> resultsArray;
    private int count;
    private JobHandle jobHandle;

    private readonly Dictionary<int, Rigidbody> rigidbodyMapping = new();

    private void OnEnable()
    {
        resultsArray = new NativeArray<JobResultStruct>(16, Allocator.Persistent);

        Physics.ContactEvent += OnContactEvent;

        Rigidbody[] allRBs = GameObject.FindObjectsOfType<Rigidbody>();
        foreach (Rigidbody rb in allRBs)
        {
            rigidbodyMapping.Add(rb.GetInstanceID(), rb);
        }
    }

    private void OnDisable()
    {
        jobHandle.Complete();
        resultsArray.Dispose();

        Physics.ContactEvent -= OnContactEvent;

        rigidbodyMapping.Clear();
    }

    private void FixedUpdate()
    {
        jobHandle.Complete(); // The buffer is valid until the next Physics.Simulate() call. Be it internal or manual

        // Do something with the contact data.
        // E.g. Add force based on the average contact normal for that body
        for (int i = 0; i < count; i++)
        {
            int thisInstanceID = resultsArray[i].ThisInstanceID;
            int otherInstanceID = resultsArray[i].OtherInstanceID;

            Rigidbody rb0 = thisInstanceID != 0 ? rigidbodyMapping[thisInstanceID] : null;
            Rigidbody rb1 = otherInstanceID != 0 ? rigidbodyMapping[otherInstanceID] : null;

            if (rb0) { rb0.AddForce(resultsArray[i].AverageNormal * impulseMultiplier, ForceMode.Impulse); }
            if (rb1) { rb1.AddForce(resultsArray[i].AverageNormal * -impulseMultiplier, ForceMode.Impulse); }
        }
    }

    private void OnContactEvent(PhysicsScene _scene, NativeArray<ContactPairHeader>.ReadOnly _pairHeaders)
    {
        int n = _pairHeaders.Length;

        if (resultsArray.Length < n)
        {
            resultsArray.Dispose();
            resultsArray = new NativeArray<JobResultStruct>(Mathf.NextPowerOfTwo(n), Allocator.Persistent);
        }

        count = n;

        ContactResponseJob job = new()
        {
            PairHeaders = _pairHeaders,
            ResultsArray = resultsArray
        };

        jobHandle = job.Schedule(n, 256);
    }

    private struct ContactResponseJob : IJobParallelFor
    {
        [ReadOnly]
        public NativeArray<ContactPairHeader>.ReadOnly PairHeaders;

        public NativeArray<JobResultStruct> ResultsArray;

        public void Execute(int _index)
        {
            Vector3 averageNormal = Vector3.zero;
            int count = 0;

            for (int j = 0; j < PairHeaders[_index].PairCount; j++)
            {
                ref readonly ContactPair pair = ref PairHeaders[_index].GetContactPair(j);

                if (pair.IsCollisionExit)
                    continue;

                for (int k = 0; k < pair.ContactCount; k++)
                {
                    ref readonly ContactPairPoint contact = ref pair.GetContactPoint(k);
                    averageNormal += contact.Normal;
                }

                count += pair.ContactCount;
            }

            if (count != 0) { averageNormal /= (float)count; }

            JobResultStruct result = new()
            {
                ThisInstanceID = PairHeaders[_index].BodyInstanceID,
                OtherInstanceID = PairHeaders[_index].OtherBodyInstanceID,
                AverageNormal = averageNormal
            };

            ResultsArray[_index] = result;
        }
    }
}