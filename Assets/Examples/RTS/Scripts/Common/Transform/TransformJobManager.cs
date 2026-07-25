using UnityEngine;
using Unity.Jobs;
using Unity.Collections;
using UnityEngine.Jobs;
using Unity.Mathematics;
using System.Collections.Generic;

namespace RTSGame
{
    public struct TransformRequest
    {
        public Transform transform;
        public float3 position;
        public quaternion rotation;
    }

    [DefaultExecutionOrder(-1000)]
    public class TransformJobManager : MonoBehaviour
    {
        public static TransformJobManager Instance;

        private TransformAccessArray transformArray;

        private NativeArray<float3> positions;
        private NativeArray<quaternion> rotations;
        private NativeArray<byte> dirtyMask;

        private readonly Dictionary<Transform, int> indexMap = new();
        private Transform[] indexToTransform;

        private readonly List<TransformRequest> requests = new();

        private int capacity = 128;
        private int count;

        private JobHandle lastHandle;

        void Awake()
        {
            Instance = this;

            transformArray = new TransformAccessArray(capacity);

            positions = new NativeArray<float3>(capacity, Allocator.Persistent);
            rotations = new NativeArray<quaternion>(capacity, Allocator.Persistent);
            dirtyMask = new NativeArray<byte>(capacity, Allocator.Persistent);

            indexToTransform = new Transform[capacity];
        }

        public void RequestTransform(Transform t, Vector3 pos, Quaternion rot)
        {
            requests.Add(new TransformRequest
            {
                transform = t,
                position = pos,
                rotation = rot
            });
        }

        public void Register(Transform t)
        {
            if (indexMap.ContainsKey(t))
                return;

            if (count >= capacity)
                Resize();

            transformArray.Add(t);

            indexMap[t] = count;
            indexToTransform[count] = t;

            positions[count] = t.position;
            rotations[count] = t.rotation;
            dirtyMask[count] = 0;

            count++;
        }

        public void Unregister(Transform t)
        {
            if (!indexMap.TryGetValue(t, out int index))
                return;

            int lastIndex = count - 1;

            transformArray.RemoveAtSwapBack(index);

            positions[index] = positions[lastIndex];
            rotations[index] = rotations[lastIndex];
            dirtyMask[index] = dirtyMask[lastIndex];

            var lastTransform = indexToTransform[lastIndex];
            indexToTransform[index] = lastTransform;
            indexMap[lastTransform] = index;

            indexMap.Remove(t);
            count--;
        }

        void Update()
        {
            // ✅ ждём прошлый job
            lastHandle.Complete();

            // ✅ применяем запросы
            for (int i = 0; i < requests.Count; i++)
            {
                var req = requests[i];

                if (!indexMap.TryGetValue(req.transform, out int index))
                    continue;

                positions[index] = req.position;
                rotations[index] = req.rotation;
                dirtyMask[index] = 1;
            }

            requests.Clear();

            // ✅ запускаем job
            var job = new ApplyTransformJob
            {
                positions = positions,
                rotations = rotations,
                dirtyMask = dirtyMask
            };

            lastHandle = job.Schedule(transformArray);
        }

        private void Resize()
        {
            int newCapacity = capacity * 2;

            transformArray.capacity = newCapacity;

            var newPos = new NativeArray<float3>(newCapacity, Allocator.Persistent);
            var newRot = new NativeArray<quaternion>(newCapacity, Allocator.Persistent);
            var newMask = new NativeArray<byte>(newCapacity, Allocator.Persistent);
            var newIndexToTransform = new Transform[newCapacity];

            NativeArray<float3>.Copy(positions, newPos, count);
            NativeArray<quaternion>.Copy(rotations, newRot, count);
            NativeArray<byte>.Copy(dirtyMask, newMask, count);
            System.Array.Copy(indexToTransform, newIndexToTransform, count);

            positions.Dispose();
            rotations.Dispose();
            dirtyMask.Dispose();

            positions = newPos;
            rotations = newRot;
            dirtyMask = newMask;
            indexToTransform = newIndexToTransform;

            capacity = newCapacity;
        }

        void LateUpdate()
        {
            // ✅ сброс маски ПОСЛЕ выполнения job
            lastHandle.Complete();

            for (int i = 0; i < count; i++)
                dirtyMask[i] = 0;
        }

        void OnDestroy()
        {
            lastHandle.Complete();

            transformArray.Dispose();
            positions.Dispose();
            rotations.Dispose();
            dirtyMask.Dispose();
        }
    }

    public struct ApplyTransformJob : IJobParallelForTransform
    {
        [ReadOnly] public NativeArray<float3> positions;
        [ReadOnly] public NativeArray<quaternion> rotations;
        [ReadOnly] public NativeArray<byte> dirtyMask;

        public void Execute(int index, TransformAccess transform)
        {
            if (dirtyMask[index] == 0)
                return;

            transform.position = positions[index];
            transform.rotation = rotations[index];
        }
    }
}