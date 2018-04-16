using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class GeometryTest
    {
        protected const int testCycles = 100;
        protected const int testRange = 1000;

        protected Vector2 GetRandomOrigin2()
        {
            return RandomE.Range(-Vector2.one*testRange, Vector2.one*testRange);
        }

        protected Vector2 GetRandomDirection2()
        {
            return RandomE.onUnitCircle2;
        }

        protected Vector3 GetRandomOrigin3()
        {
            return RandomE.Range(-Vector3.one*testRange, Vector3.one*testRange);
        }

        protected Vector3 GetRandomDirection3()
        {
            return Random.onUnitSphere;
        }

        protected Vector3 GetRandomDirection3(Vector3 axis)
        {
            return Quaternion.AngleAxis(Random.value*360, axis)*Vector3.forward;
        }

        protected float GetRandomOffset()
        {
            return Random.Range(-testRange, testRange);
        }

        protected void AreEqual(Vector2 actual, Vector2 expected)
        {
            Assert.True((expected - actual).sqrMagnitude < Geometry.Epsilon, actual.ToString("F8") + " " + expected.ToString("F8") +
                                                                             "\ndelta:" + (actual - expected).sqrMagnitude);
        }
    }
}
