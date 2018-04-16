using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry2DLineLineTest : GeometryTest
    {
        #region Distance

        [Test]
        public void Distance_Coincident()
        {
            Assert.AreEqual(Geometry.DistanceToLine(Line2.xAxis, Line2.xAxis), 0);
            Assert.AreEqual(Geometry.DistanceToLine(Line2.yAxis, Line2.yAxis), 0);

            var line = new Line2(Vector2.zero, Vector2.up.RotateCW(1).normalized);
            Assert.AreEqual(Geometry.DistanceToLine(line, line), 0);
        }

        [Test]
        public void Distance_Codirected()
        {
            Assert.AreEqual(Geometry.DistanceToLine(Line2.xAxis, Line2.xAxis + Vector2.right*1000), 0);
            Assert.AreEqual(Geometry.DistanceToLine(Line2.yAxis, Line2.yAxis + Vector2.up*1000), 0);

            var line = new Line2(Vector2.zero, Vector2.up.RotateCW(1).normalized);
            Assert.AreEqual(Geometry.DistanceToLine(line, line + line.direction*1000), 0);
        }

        [Test]
        public void Distance_Collinear()
        {
            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(Vector2.zero, direction);
            var lineB = new Line2(direction*1000, -direction);
            Assert.AreEqual(Geometry.DistanceToLine(lineA, lineB), 0);
        }

        [Test]
        public void Distance_Parallel()
        {
            Assert.AreEqual(Geometry.DistanceToLine(Line2.xAxis, Line2.xAxis + Vector2.up), 1);
            Assert.AreEqual(Geometry.DistanceToLine(Line2.xAxis, Line2.xAxis + Vector2.up*1000), 1000);
            Assert.AreEqual(Geometry.DistanceToLine(Line2.yAxis, Line2.yAxis + Vector2.right), 1);
            Assert.AreEqual(Geometry.DistanceToLine(Line2.yAxis, Line2.yAxis + Vector2.right*1000), 1000);

            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(Vector2.zero, direction);
            var lineB = new Line2(direction.RotateCW(90), direction);
            Assert.AreEqual(Geometry.DistanceToLine(lineA, lineB), 1);
        }

        [Test]
        public void Distance_Perpendicular()
        {
            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(Vector2.zero, direction);
            var lineB = new Line2(Vector2.zero, direction.RotateCW(90));
            Assert.AreEqual(Geometry.DistanceToLine(lineA, lineB), 0);
        }

        #endregion Distance

        #region ClosestPoints

        [Test]
        public void ClosestPoints_Coincident()
        {
            Vector2 pointA;
            Vector2 pointB;
            Geometry.ClosestPointsOnLines(Line2.xAxis, Line2.xAxis, out pointA, out pointB);
            AreEqual(pointA, Vector2.zero);
            AreEqual(pointB, Vector2.zero);
            Geometry.ClosestPointsOnLines(Line2.yAxis, Line2.yAxis, out pointA, out pointB);
            AreEqual(pointA, Vector2.zero);
            AreEqual(pointB, Vector2.zero);

            var line = new Line2(Vector2.zero, Vector2.up.RotateCW(1).normalized);
            Geometry.ClosestPointsOnLines(line, line, out pointA, out pointB);
            AreEqual(pointA, Vector2.zero);
            AreEqual(pointB, Vector2.zero);
        }

        [Test]
        public void ClosestPoints_Codirected()
        {
            Vector2 pointA;
            Vector2 pointB;
            Geometry.ClosestPointsOnLines(Line2.xAxis, Line2.xAxis + Vector2.right*1000, out pointA, out pointB);
            AreEqual(pointA, Vector2.right*1000);
            AreEqual(pointB, Vector2.right*1000);
            Geometry.ClosestPointsOnLines(Line2.xAxis + Vector2.right*1000, Line2.xAxis, out pointA, out pointB);
            AreEqual(pointA, Vector2.zero);
            AreEqual(pointB, Vector2.zero);
            Geometry.ClosestPointsOnLines(Line2.yAxis, Line2.yAxis + Vector2.up*1000, out pointA, out pointB);
            AreEqual(pointA, Vector2.up*1000);
            AreEqual(pointB, Vector2.up*1000);
            Geometry.ClosestPointsOnLines(Line2.yAxis + Vector2.up*1000, Line2.yAxis, out pointA, out pointB);
            AreEqual(pointA, Vector2.zero);
            AreEqual(pointB, Vector2.zero);

            var lineA = new Line2(Vector2.zero, Vector2.up.RotateCW(1).normalized);
            var lineB = lineA + lineA.direction*1000;
            Geometry.ClosestPointsOnLines(lineA, lineB, out pointA, out pointB);
            AreEqual(pointA, lineB.origin);
            AreEqual(pointB, lineB.origin);
            Geometry.ClosestPointsOnLines(lineB, lineA, out pointA, out pointB);
            AreEqual(pointA, lineA.origin);
            AreEqual(pointB, lineA.origin);
        }

        [Test]
        public void ClosestPoints_Collinear()
        {
            Vector2 pointA;
            Vector2 pointB;
            Geometry.ClosestPointsOnLines(Line2.xAxis, new Line2(Vector2.right*1000, Vector2.left), out pointA, out pointB);
            AreEqual(pointA, Vector2.right*1000);
            AreEqual(pointB, Vector2.right*1000);
            Geometry.ClosestPointsOnLines(new Line2(Vector2.right*1000, Vector2.left), Line2.xAxis, out pointA, out pointB);
            AreEqual(pointA, Vector2.zero);
            AreEqual(pointB, Vector2.zero);
            Geometry.ClosestPointsOnLines(Line2.yAxis, new Line2(Vector2.up*1000, Vector2.down), out pointA, out pointB);
            AreEqual(pointA, Vector2.up*1000);
            AreEqual(pointB, Vector2.up*1000);
            Geometry.ClosestPointsOnLines(new Line2(Vector2.up*1000, Vector2.down), Line2.yAxis, out pointA, out pointB);
            AreEqual(pointA, Vector2.zero);
            AreEqual(pointB, Vector2.zero);

            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(Vector2.zero, direction);
            var lineB = new Line2(direction*1000, -direction);
            Geometry.ClosestPointsOnLines(lineA, lineB, out pointA, out pointB);
            AreEqual(pointA, lineB.origin);
            AreEqual(pointB, lineB.origin);
            Geometry.ClosestPointsOnLines(lineB, lineA, out pointA, out pointB);
            AreEqual(pointA, lineA.origin);
            AreEqual(pointB, lineA.origin);
        }

        [Test]
        public void ClosestPoints_Parallel()
        {
            Vector2 pointA;
            Vector2 pointB;
            Geometry.ClosestPointsOnLines(Line2.xAxis, Line2.xAxis + Vector2.up, out pointA, out pointB);
            AreEqual(pointA, Vector2.zero);
            AreEqual(pointB, Vector2.up);
            Geometry.ClosestPointsOnLines(Line2.xAxis, Line2.xAxis + Vector2.up*1000, out pointA, out pointB);
            AreEqual(pointA, Vector2.zero);
            AreEqual(pointB, Vector2.up*1000);
            Geometry.ClosestPointsOnLines(Line2.yAxis, Line2.yAxis + Vector2.right, out pointA, out pointB);
            AreEqual(pointA, Vector2.zero);
            AreEqual(pointB, Vector2.right);
            Geometry.ClosestPointsOnLines(Line2.yAxis, Line2.yAxis + Vector2.right*1000, out pointA, out pointB);
            AreEqual(pointA, Vector2.zero);
            AreEqual(pointB, Vector2.right*1000);

            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(Vector2.zero, direction);
            var lineB = new Line2(direction.RotateCW(90), direction);
            Geometry.ClosestPointsOnLines(lineA, lineB, out pointA, out pointB);
            AreEqual(pointA, lineA.origin);
            AreEqual(pointB, lineB.origin);
            Geometry.ClosestPointsOnLines(lineB, lineA, out pointA, out pointB);
            AreEqual(pointA, lineB.origin);
            AreEqual(pointB, lineA.origin);
        }

        [Test]
        public void ClosestPoints_Perpendicular()
        {
            Vector2 pointA;
            Vector2 pointB;
            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(Vector2.zero, direction);
            var lineB = new Line2(Vector2.zero, direction.RotateCW(90));
            Geometry.ClosestPointsOnLines(lineA, lineB, out pointA, out pointB);
            AreEqual(pointA, Vector2.zero);
            AreEqual(pointB, Vector2.zero);
        }

        #endregion ClosestPoints

        #region Intersect

        [Test]
        public void Intersect_Coincident()
        {
            Vector2 intersection;
            Assert.IsTrue(Geometry.IntersectLineLine(Line2.xAxis, Line2.xAxis, out intersection));
            AreEqual(intersection, Vector2.zero);
            Assert.IsTrue(Geometry.IntersectLineLine(Line2.yAxis, Line2.yAxis, out intersection));
            AreEqual(intersection, Vector2.zero);
            var line = new Line2(Vector2.zero, Vector2.up.RotateCW(1).normalized);
            Assert.IsTrue(Geometry.IntersectLineLine(line, line, out intersection));
            AreEqual(intersection, line.origin);
        }

        [Test]
        public void Intersect_Codirected()
        {
            Vector2 intersection;
            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(Vector2.zero, direction);
            var lineB = new Line2(direction*1000, direction);
            Assert.IsTrue(Geometry.IntersectLineLine(lineA, lineB, out intersection));
            AreEqual(intersection, Vector2.zero);
        }

        [Test]
        public void Intersect_Collinear()
        {
            Vector2 intersection;
            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(Vector2.zero, direction);
            var lineB = new Line2(direction*1000, -direction);
            Assert.IsTrue(Geometry.IntersectLineLine(lineA, lineB, out intersection));
            AreEqual(intersection, Vector2.zero);
        }

        [Test]
        public void Intersect_Parallel()
        {
            Vector2 intersection;
            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(Vector2.zero, direction);
            var lineB = new Line2(direction.RotateCW(90), direction);
            Assert.IsFalse(Geometry.IntersectLineLine(lineA, lineB, out intersection));
        }

        [Test]
        public void Intersect_Perpendicular()
        {
            Vector2 intersection;
            Vector2 direction = Vector2.up.RotateCW(1).normalized;
            var lineA = new Line2(Vector2.zero, direction);
            var lineB = new Line2(Vector2.zero, direction.RotateCW(90));
            Assert.IsTrue(Geometry.IntersectLineLine(lineA, lineB, out intersection));
            AreEqual(intersection, Vector2.zero);
        }

        [Test]
        public void Intersect_RandomCoincident()
        {
            for (int i = 0; i < testCycles; i++)
            {
                Vector2 intersection;
                var line = new Line2(GetRandomOrigin2(), GetRandomDirection2());
                Assert.IsTrue(Geometry.IntersectLineLine(line, line, out intersection), line.ToString("F8"));
                AreEqual(intersection, line.origin);
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
                AreEqual(intersection, origin);
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
                AreEqual(intersection, origin);
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
                AreEqual(intersection, origin);
            }
        }

        #endregion Intersect
    }
}
