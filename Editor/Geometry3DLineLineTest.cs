using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry3DLineLineTest : GeometryTest
    {
        [Test]
        public void Intersect_RandomCoincident()
        {
            for (int i = 0; i < testCycles; i++)
            {
                Vector3 intersection;
                var line = new Line3(GetRandomOrigin3(), GetRandomDirection3());
                Assert.IsTrue(Geometry.IntersectLineLine(line, line, out intersection), line.ToString("F8"));
                Assert.IsTrue(intersection == line.origin, "intersection: " + intersection);
            }
        }

        [Test]
        public void Intersect_RandomCodirected()
        {
            for (int i = 0; i < testCycles; i++)
            {
                Vector3 intersection;
                Vector3 origin = GetRandomOrigin3();
                Vector3 direction = GetRandomDirection3();
                var lineA = new Line3(origin, direction);
                var lineB = new Line3(origin + direction*GetRandomOffset(), direction);
                Assert.IsTrue(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
                Assert.IsTrue(intersection == origin, "intersection: " + intersection);
            }
        }

        [Test]
        public void Intersect_RandomCollinear()
        {
            for (int i = 0; i < testCycles; i++)
            {
                Vector3 intersection;
                Vector3 origin = GetRandomOrigin3();
                Vector3 direction = GetRandomDirection3();
                var lineA = new Line3(origin, direction);
                var lineB = new Line3(origin + direction*GetRandomOffset(), -direction);
                Assert.IsTrue(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
                Assert.IsTrue(intersection == origin, "intersection: " + intersection);
            }
        }

        [Test]
        public void Intersect_RandomParallel()
        {
            for (int i = 0; i < testCycles; i++)
            {
                Vector3 intersection;
                Vector3 origin = GetRandomOrigin3();
                Vector3 axis = GetRandomDirection3();
                Vector3 direction = GetRandomDirection3(axis);
                var lineA = new Line3(origin, direction);
                var lineB = new Line3(origin + Quaternion.AngleAxis(90, axis)*direction, direction);
                Assert.IsFalse(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
            }
        }

        [Test]
        public void Intersect_RandomPerpendicular()
        {
            for (int i = 0; i < testCycles; i++)
            {
                Vector3 intersection;
                Vector3 origin = GetRandomOrigin3();
                Vector3 axis = GetRandomDirection3();
                Vector3 direction = GetRandomDirection3(axis);
                var lineA = new Line3(origin, direction);
                var lineB = new Line3(origin, Quaternion.AngleAxis(90, axis)*direction);
                Assert.IsTrue(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
                Assert.IsTrue(intersection == origin, "intersection: " + intersection);
            }
        }
    }
}
