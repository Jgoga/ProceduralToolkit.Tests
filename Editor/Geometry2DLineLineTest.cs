using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry2DLineLineTest : GeometryTest
    {
        [Test]
        public void Intersect_Coincident()
        {
            Vector2 intersection;
            Assert.IsTrue(Geometry.IntersectLineLine(Line2.xAxis, Line2.xAxis, out intersection));
            Assert.AreEqual(intersection, Vector2.zero);
            Assert.IsTrue(Geometry.IntersectLineLine(Line2.yAxis, Line2.yAxis, out intersection));
            Assert.AreEqual(intersection, Vector2.zero);
            var line = new Line2(Vector2.zero, Vector2.up.RotateCW(1).normalized);
            Assert.IsTrue(Geometry.IntersectLineLine(line, line, out intersection));
            Assert.AreEqual(intersection, line.origin);
        }

        [Test]
        public void Intersect_Codirected()
        {
            Vector2 intersection;
            Vector2 origin = Vector2.zero;
            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(origin, direction);
            var lineB = new Line2(origin + direction*1000, direction);
            Assert.IsTrue(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
            Assert.IsTrue(intersection == origin, "intersection: " + intersection);
        }

        [Test]
        public void Intersect_Collinear()
        {
            Vector2 intersection;
            Vector2 origin = Vector2.zero;
            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(origin, direction);
            var lineB = new Line2(origin + direction*1000, -direction);
            Assert.IsTrue(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
            Assert.IsTrue(intersection == origin, "intersection: " + intersection);
        }

        [Test]
        public void Intersect_Parallel()
        {
            Vector2 intersection;
            Vector2 origin = Vector2.zero;
            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(origin, direction);
            var lineB = new Line2(origin + direction.RotateCW(90), direction);
            Assert.IsFalse(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
        }

        [Test]
        public void Intersect_Perpendicular()
        {
            Vector2 intersection;
            Vector2 origin = Vector2.zero;
            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(origin, direction);
            var lineB = new Line2(origin, direction.RotateCW(90));
            Assert.IsTrue(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
            Assert.IsTrue(intersection == origin, "intersection: " + intersection);
        }

        [Test]
        public void Intersect_RandomCoincident()
        {
            for (int i = 0; i < testCycles; i++)
            {
                Vector2 intersection;
                var line = new Line2(GetRandomOrigin2(), GetRandomDirection2());
                Assert.IsTrue(Geometry.IntersectLineLine(line, line, out intersection), line.ToString("F8"));
                Assert.IsTrue(intersection == line.origin, "intersection: " + intersection);
            }
        }

        [Test]
        public void Intersect_RandomCodirected()
        {
            for (int i = 0; i < testCycles; i++)
            {
                Vector2 intersection;
                Vector2 origin = GetRandomOrigin2();
                Vector2 direction = GetRandomDirection2();
                var lineA = new Line2(origin, direction);
                var lineB = new Line2(origin + direction*GetRandomOffset(), direction);
                Assert.IsTrue(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
                Assert.IsTrue(intersection == origin, "intersection: " + intersection);
            }
        }

        [Test]
        public void Intersect_RandomCollinear()
        {
            for (int i = 0; i < testCycles; i++)
            {
                Vector2 intersection;
                Vector2 origin = GetRandomOrigin2();
                Vector2 direction = GetRandomDirection2();
                var lineA = new Line2(origin, direction);
                var lineB = new Line2(origin + direction*GetRandomOffset(), -direction);
                Assert.IsTrue(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
                Assert.IsTrue(intersection == origin, "intersection: " + intersection);
            }
        }

        [Test]
        public void Intersect_RandomParallel()
        {
            for (int i = 0; i < testCycles; i++)
            {
                Vector2 intersection;
                Vector2 origin = GetRandomOrigin2();
                Vector2 direction = GetRandomDirection2();
                var lineA = new Line2(origin, direction);
                var lineB = new Line2(origin + direction.RotateCW(90), direction);
                Assert.IsFalse(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
            }
        }

        [Test]
        public void Intersect_RandomPerpendicular()
        {
            for (int i = 0; i < testCycles; i++)
            {
                Vector2 intersection;
                Vector2 origin = GetRandomOrigin2();
                Vector2 direction = GetRandomDirection2();
                var lineA = new Line2(origin, direction);
                var lineB = new Line2(origin, direction.RotateCW(90));
                Assert.IsTrue(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
                Assert.IsTrue(intersection == origin, "intersection: " + intersection);
            }
        }
    }
}
