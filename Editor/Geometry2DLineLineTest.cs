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
            for (int i = 0; i < 360; i++)
            {
                var line = new Line2(Vector2.zero, Vector2.up.RotateCW(i).normalized);
                AreEqual_DistanceToLine(line, line, 0);
            }
        }

        [Test]
        public void Distance_CollinearCodirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction*100, direction);
                AreEqual_DistanceToLineSwap(lineA, lineB, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(-direction*100, direction);
                var lineB = new Line2(Vector2.zero, direction);
                AreEqual_DistanceToLineSwap(lineA, lineB, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(-direction*100, direction);
                var lineB = new Line2(direction*100, direction);
                AreEqual_DistanceToLineSwap(lineA, lineB, 0);
            }
        }

        [Test]
        public void Distance_CollinearContradirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction*100, -direction);
                AreEqual_DistanceToLineSwap(lineA, lineB, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(-direction*100, direction);
                var lineB = new Line2(Vector2.zero, -direction);
                AreEqual_DistanceToLineSwap(lineA, lineB, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(-direction*100, direction);
                var lineB = new Line2(direction*100, -direction);
                AreEqual_DistanceToLineSwap(lineA, lineB, 0);
            }
        }

        [Test]
        public void Distance_ParallelCodirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90), direction);
                AreEqual_DistanceToLineSwap(lineA, lineB, 1);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90) + direction, direction);
                AreEqual_DistanceToLineSwap(lineA, lineB, 1);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90) - direction, direction);
                AreEqual_DistanceToLineSwap(lineA, lineB, 1);
            }
        }

        [Test]
        public void Distance_ParallelContradirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90), -direction);
                AreEqual_DistanceToLineSwap(lineA, lineB, 1);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90) + direction, -direction);
                AreEqual_DistanceToLineSwap(lineA, lineB, 1);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90) - direction, -direction);
                AreEqual_DistanceToLineSwap(lineA, lineB, 1);
            }
        }

        [Test]
        public void Distance_Perpendicular()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(Vector2.zero, direction.RotateCW(90));
                AreEqual_DistanceToLineSwap(lineA, lineB, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90)*100, direction.RotateCW(90));
                AreEqual_DistanceToLineSwap(lineA, lineB, 0);
            }
        }

        private void AreEqual_DistanceToLineSwap(Line2 lineA, Line2 lineB, float expected)
        {
            AreEqual_DistanceToLine(lineA, lineB, expected);
            AreEqual_DistanceToLine(lineB, lineA, expected);
        }

        private void AreEqual_DistanceToLine(Line2 lineA, Line2 lineB, float expected)
        {
            float distance = Geometry.DistanceToLine(lineA, lineB);
            float delta = Mathf.Abs(expected - distance);
            Assert.True(delta < Geometry.Epsilon, string.Format("{0}\n{1}\ndistance: {2:G9} expected: {3:G9}\ndelta: {4:F8}",
                lineA.ToString("G9"), lineB.ToString("G9"), distance, expected, delta));
        }

        #endregion Distance

        #region ClosestPoints

        [Test]
        public void ClosestPoints_Coincident()
        {
            for (int i = 0; i < 360; i++)
            {
                var line = new Line2(Vector2.zero, Vector2.up.RotateCW(i).normalized);
                AreEqual_ClosestPoints(line, line, line.origin, line.origin);
            }
        }

        [Test]
        public void ClosestPoints_CollinearCodirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction*100, direction);
                AreEqual_ClosestPoints(lineA, lineB, lineA.origin, lineA.origin);
                AreEqual_ClosestPoints(lineB, lineA, lineB.origin, lineB.origin);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(-direction*100, direction);
                var lineB = new Line2(Vector2.zero, direction);
                AreEqual_ClosestPoints(lineA, lineB, lineA.origin, lineA.origin);
                AreEqual_ClosestPoints(lineB, lineA, lineB.origin, lineB.origin);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(-direction*100, direction);
                var lineB = new Line2(direction*100, direction);
                AreEqual_ClosestPoints(lineA, lineB, lineA.origin, lineA.origin);
                AreEqual_ClosestPoints(lineB, lineA, lineB.origin, lineB.origin);
            }
        }

        [Test]
        public void ClosestPoints_CollinearContradirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction*100, -direction);
                AreEqual_ClosestPoints(lineA, lineB, lineA.origin, lineA.origin);
                AreEqual_ClosestPoints(lineB, lineA, lineB.origin, lineB.origin);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(-direction*100, direction);
                var lineB = new Line2(Vector2.zero, -direction);
                AreEqual_ClosestPoints(lineA, lineB, lineA.origin, lineA.origin);
                AreEqual_ClosestPoints(lineB, lineA, lineB.origin, lineB.origin);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(-direction*100, direction);
                var lineB = new Line2(direction*100, -direction);
                AreEqual_ClosestPoints(lineA, lineB, lineA.origin, lineA.origin);
                AreEqual_ClosestPoints(lineB, lineA, lineB.origin, lineB.origin);
            }
        }

        [Test]
        public void ClosestPoints_ParallelCodirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90), direction);
                AreEqual_ClosestPointsSwap(lineA, lineB, lineA.origin, lineB.origin);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90) + direction, direction);
                AreEqual_ClosestPoints(lineA, lineB, lineA.origin, lineB.origin - direction);
                AreEqual_ClosestPoints(lineB, lineA, lineB.origin, lineA.origin + direction);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90) - direction, direction);
                AreEqual_ClosestPoints(lineA, lineB, lineA.origin, lineB.origin + direction);
                AreEqual_ClosestPoints(lineB, lineA, lineB.origin, lineA.origin - direction);
            }
        }

        [Test]
        public void ClosestPoints_ParallelContradirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90), -direction);
                AreEqual_ClosestPointsSwap(lineA, lineB, lineA.origin, lineB.origin);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90) + direction, -direction);
                AreEqual_ClosestPoints(lineA, lineB, lineA.origin, lineB.origin - direction);
                AreEqual_ClosestPoints(lineB, lineA, lineB.origin, lineA.origin + direction);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90) - direction, -direction);
                AreEqual_ClosestPoints(lineA, lineB, lineA.origin, lineB.origin + direction);
                AreEqual_ClosestPoints(lineB, lineA, lineB.origin, lineA.origin - direction);
            }
        }

        [Test]
        public void ClosestPoints_Perpendicular()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(Vector2.zero, direction.RotateCW(90));
                AreEqual_ClosestPointsSwap(lineA, lineB, lineA.origin, lineA.origin);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(-direction.RotateCW(90), direction.RotateCW(90));
                AreEqual_ClosestPointsSwap(lineA, lineB, lineA.origin, lineA.origin);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90), direction.RotateCW(90));
                AreEqual_ClosestPointsSwap(lineA, lineB, lineA.origin, lineA.origin);
            }
        }

        private void AreEqual_ClosestPointsSwap(Line2 lineA, Line2 lineB, Vector2 expectedA, Vector2 expectedB)
        {
            AreEqual_ClosestPoints(lineA, lineB, expectedA, expectedB);
            AreEqual_ClosestPoints(lineB, lineA, expectedB, expectedA);
        }

        private void AreEqual_ClosestPoints(Line2 lineA, Line2 lineB, Vector2 expectedA, Vector2 expectedB)
        {
            Vector2 pointA;
            Vector2 pointB;
            Geometry.ClosestPointsOnLines(lineA, lineB, out pointA, out pointB);
            AreEqual(lineA, lineB, pointA, expectedA);
            AreEqual(lineA, lineB, pointB, expectedB);
        }

        private void AreEqual(Line2 lineA, Line2 lineB, Vector2 point, Vector2 expected)
        {
            float delta = (point - expected).magnitude;
            Assert.True(delta < Geometry.Epsilon, string.Format("{0}\n{1}\npoint: {2} expected: {3:G9}\ndelta: {4:F8}",
                lineA.ToString("G9"), lineB.ToString("G9"), point.ToString("G9"), expected.ToString("G9"), delta));
        }

        #endregion ClosestPoints

        #region Intersect

        [Test]
        public void Intersect_Coincident()
        {
            for (int i = 0; i < 360; i++)
            {
                Intersect_Coincident(new Line2(Vector2.zero, Vector2.up.RotateCW(i).normalized));
            }
        }

        private void Intersect_Coincident(Line2 line)
        {
            IntersectionLineLine2 intersection;
            Assert.IsTrue(Geometry.IntersectLineLine(line, line, out intersection), line.ToString("F8"));
            Assert.AreEqual(intersection.type, IntersectionType.Line);
            AreEqual(intersection.point, line.origin);
        }

        [Test]
        public void Intersect_CollinearCodirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction*100, direction);
                Intersect_Collinear(lineA, lineB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(-direction*100, direction);
                var lineB = new Line2(Vector2.zero, direction);
                Intersect_Collinear(lineA, lineB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(-direction*100, direction);
                var lineB = new Line2(direction*100, direction);
                Intersect_Collinear(lineA, lineB);
            }
        }

        [Test]
        public void Intersect_CollinearContradirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction*100, -direction);
                Intersect_Collinear(lineA, lineB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(-direction*100, direction);
                var lineB = new Line2(Vector2.zero, -direction);
                Intersect_Collinear(lineA, lineB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(-direction*100, direction);
                var lineB = new Line2(direction*100, -direction);
                Intersect_Collinear(lineA, lineB);
            }
        }

        private void Intersect_Collinear(Line2 lineA, Line2 lineB)
        {
            IntersectionLineLine2 intersection;
            IsTrue_IntersectLineLine(lineA, lineB, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Line);
            AreEqual(lineA, lineB, intersection.point, lineA.origin);
            IsTrue_IntersectLineLine(lineB, lineA, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Line);
            AreEqual(lineA, lineB, intersection.point, lineB.origin);
        }

        [Test]
        public void Intersect_ParallelCodirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90), direction);
                IsFalse_IntersectLineLine(lineA, lineB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90) + direction, direction);
                IsFalse_IntersectLineLine(lineA, lineB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90) - direction, direction);
                IsFalse_IntersectLineLine(lineA, lineB);
            }
        }

        [Test]
        public void Intersect_ParallelContradirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90), -direction);
                IsFalse_IntersectLineLine(lineA, lineB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90) + direction, -direction);
                IsFalse_IntersectLineLine(lineA, lineB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90) - direction, -direction);
                IsFalse_IntersectLineLine(lineA, lineB);
            }
        }

        [Test]
        public void Intersect_Perpendicular()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(Vector2.zero, direction.RotateCW(90));
                Intersect_Perpendicular(lineA, lineB, lineA.origin);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(-direction.RotateCW(90), direction.RotateCW(90));
                Intersect_Perpendicular(lineA, lineB, lineA.origin);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var lineA = new Line2(Vector2.zero, direction);
                var lineB = new Line2(direction.RotateCW(90), direction.RotateCW(90));
                Intersect_Perpendicular(lineA, lineB, lineA.origin);
            }
        }

        private void Intersect_Perpendicular(Line2 lineA, Line2 lineB, Vector2 expected)
        {
            IntersectionLineLine2 intersection;
            IsTrue_IntersectLineLine(lineA, lineB, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Point);
            AreEqual(lineA, lineB, intersection.point, expected);
            IsTrue_IntersectLineLine(lineB, lineA, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Point);
            AreEqual(lineA, lineB, intersection.point, expected);
        }

        private void IsTrue_IntersectLineLine(Line2 lineA, Line2 lineB, out IntersectionLineLine2 intersection)
        {
            Assert.IsTrue(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
        }

        private void IsFalse_IntersectLineLine(Line2 lineA, Line2 lineB)
        {
            IntersectionLineLine2 intersection;
            IsFalse_IntersectLineLine(lineA, lineB, out intersection);
            IsFalse_IntersectLineLine(lineB, lineA, out intersection);
        }

        private void IsFalse_IntersectLineLine(Line2 lineA, Line2 lineB, out IntersectionLineLine2 intersection)
        {
            Assert.IsFalse(Geometry.IntersectLineLine(lineA, lineB, out intersection), lineA.ToString("F8") + "\n" + lineB.ToString("F8"));
        }

        #endregion Intersect
    }
}
