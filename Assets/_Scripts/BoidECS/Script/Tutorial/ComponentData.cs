﻿using Unity.Entities;
using Unity.Mathematics;
namespace BoidECS
{

    public struct Velocity : IComponentData
    {
        public float3 Value;
    }

    public struct Acceleration : IComponentData
    {
        public float3 Value;
    }

    [InternalBufferCapacity(4)]
    public struct NeighborsEntityBuffer : IBufferElementData
    {
        public Entity Value;
    }

    public struct TargetPos : IComponentData
    {
        public float3 Value;
    }
}