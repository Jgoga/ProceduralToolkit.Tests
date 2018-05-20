using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry2DSegmentSegmentTest : GeometryTest
    {
        #region Intersect

        [Test]
        public void Intersect_CoincidentCodirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Intersect_CoincidentCodirected(Vector2.zero, Vector2.up.RotateCW(i).normalized);
                Intersect_CoincidentCodirected(Vector2.zero, Vector2.up.RotateCW(i).normalized*1000);
            }
        }

        private void Intersect_CoincidentCodirected(Vector2 a, Vector2 b)
        {
            var segment = new Segment2(a, b);
            IntersectionSegmentSegment2 intersection;
            Assert.IsTrue(Geometry.IntersectSegmentSegment(segment, segment, out intersection), segment.ToString("F8"));
            Assert.AreEqual(IntersectionType.Segment, intersection.type);
            AreEqual(intersection.pointA, segment.a);
            AreEqual(intersection.pointB, segment.b);
        }

        [Test]
        public void Intersect_CoincidentContradirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Intersect_CoincidentContradirected(Vector2.zero, Vector2.up.RotateCW(i).normalized);
                Intersect_CoincidentContradirected(Vector2.zero, Vector2.up.RotateCW(i).normalized*1000);
            }
        }

        private void Intersect_CoincidentContradirected(Vector2 a, Vector2 b)
        {
            var segment1 = new Segment2(a, b);
            var segment2 = new Segment2(b, a);
            IntersectionSegmentSegment2 intersection;
            Assert.IsTrue(Geometry.IntersectSegmentSegment(segment1, segment2, out intersection), segment1.ToString("F8"));
            Assert.AreEqual(IntersectionType.Segment, intersection.type);
            AreEqual(intersection.pointA, segment1.a);
            AreEqual(intersection.pointB, segment1.b);
        }

        #region Collinear

        [Test]
        public void Intersect_CollinearOverlappingSegmentCodirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var segment1 = new Segment2(Vector2.zero, direction);
                var segment2 = new Segment2(direction*0.5f, direction*2);
                Intersect_CollinearOverlappingSegmentCodirected(segment1, segment2);
            }
        }

        private void Intersect_CollinearOverlappingSegmentCodirected(Segment2 segment1, Segment2 segment2)
        {
            IntersectionSegmentSegment2 intersection;
            IsTrue_IntersectSegmentSegment(segment1, segment2, out intersection);
            Assert.AreEqual(IntersectionType.Segment, intersection.type);
            AreEqual(intersection.pointA, segment2.a);
            AreEqual(intersection.pointB, segment1.b);
            IsTrue_IntersectSegmentSegment(segment2, segment1, out intersection);
            Assert.AreEqual(IntersectionType.Segment, intersection.type);
            AreEqual(intersection.pointA, segment2.a);
            AreEqual(intersection.pointB, segment1.b);
        }

        [Test]
        public void Intersect_CollinearOverlappingSegmentContradirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var segment1 = new Segment2(Vector2.zero, direction);
                var segment2 = new Segment2(direction*2, direction*0.5f);
                Intersect_CollinearOverlappingSegmentContradirected(segment1, segment2);
            }
        }

        private void Intersect_CollinearOverlappingSegmentContradirected(Segment2 segment1, Segment2 segment2)
        {
            IntersectionSegmentSegment2 intersection;
            IsTrue_IntersectSegmentSegment(segment1, segment2, out intersection);
            Assert.AreEqual(IntersectionType.Segment, intersection.type);
            AreEqual(intersection.pointA, segment2.b);
            AreEqual(intersection.pointB, segment1.b);
            IsTrue_IntersectSegmentSegment(segment2, segment1, out intersection);
            Assert.AreEqual(IntersectionType.Segment, intersection.type);
            AreEqual(intersection.pointA, segment1.b);
            AreEqual(intersection.pointB, segment2.b);
        }

        [Test]
        public void Intersect_CollinearOverlappingPoint()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var segment1 = new Segment2(Vector2.zero, direction);
                Intersect_CollinearOverlappingPoint(segment1, new Segment2(direction, direction*2));
                Intersect_CollinearOverlappingPoint(segment1, new Segment2(direction*2, direction));
            }
        }

        private void Intersect_CollinearOverlappingPoint(Segment2 segment1, Segment2 segment2)
        {
            IntersectionSegmentSegment2 intersection;
            IsTrue_IntersectSegmentSegment(segment1, segment2, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Point);
            AreEqual(intersection.pointA, segment1.b);
            IsTrue_IntersectSegmentSegment(segment2, segment1, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Point);
            AreEqual(intersection.pointA, segment1.b);
        }

        [Test]
        public void Intersect_CollinearContaining()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Intersect_CollinearContaining(new Segment2(Vector2.zero, direction*10), new Segment2(direction*2, direction*8));
                Intersect_CollinearContaining(new Segment2(Vector2.zero, direction*10), new Segment2(Vector2.zero, direction*5));
                Intersect_CollinearContaining(new Segment2(Vector2.zero, direction*10), new Segment2(direction*5, direction*10));
                Intersect_CollinearContaining(new Segment2(-direction*10, direction*10), new Segment2(-direction*5, direction*5));
                Intersect_CollinearContaining(new Segment2(-direction*10, direction*10), new Segment2(-direction*10, Vector2.zero));
                Intersect_CollinearContaining(new Segment2(-direction*10, direction*10), new Segment2(Vector2.zero, direction*10));
            }
        }

        private void Intersect_CollinearContaining(Segment2 segment1, Segment2 segment2)
        {
            IntersectionSegmentSegment2 intersection;
            IsTrue_IntersectSegmentSegment(segment1, segment2, out intersection);
            Assert.AreEqual(IntersectionType.Segment, intersection.type, segment1.ToString("F8") + "\n" + segment2.ToString("F8"));
            AreEqual(intersection.pointA, segment2.a);
            AreEqual(intersection.pointB, segment2.b);
            IsTrue_IntersectSegmentSegment(segment2, segment1, out intersection);
            Assert.AreEqual(IntersectionType.Segment, intersection.type, segment1.ToString("F8") + "\n" + segment2.ToString("F8"));
            AreEqual(intersection.pointA, segment2.a);
            AreEqual(intersection.pointB, segment2.b);
        }

        [Test]
        public void Intersect_CollinearNonOverlappingCodirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                IsFalse_IntersectSegmentSegment(new Segment2(Vector2.zero, direction), new Segment2(direction*2, direction*3));
                IsFalse_IntersectSegmentSegment(new Segment2(Vector2.zero, direction), new Segment2(-direction*2, -direction));
                IsFalse_IntersectSegmentSegment(new Segment2(-direction*2, -direction), new Segment2(direction, direction*2));
            }
        }

        [Test]
        public void Intersect_CollinearNonOverlappingContradirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                IsFalse_IntersectSegmentSegment(new Segment2(Vector2.zero, direction), new Segment2(direction*3, direction*2));
                IsFalse_IntersectSegmentSegment(new Segment2(Vector2.zero, direction), new Segment2(-direction, -direction*2));
                IsFalse_IntersectSegmentSegment(new Segment2(-direction*2, -direction), new Segment2(direction*2, direction));
            }
        }

        #endregion Collinear

        #region Degenerate

        [Test]
        public void Intersect_DegenerateTwoPointsNonCoincident()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Intersect_DegenerateTwoPointsNonCoincident(Vector2.zero, direction);
                Intersect_DegenerateTwoPointsNonCoincident(Vector2.zero, direction*1000);
                Intersect_DegenerateTwoPointsNonCoincident(-direction, direction);
                Intersect_DegenerateTwoPointsNonCoincident(-direction*1000, direction*1000);
            }
        }

        private void Intersect_DegenerateTwoPointsNonCoincident(Vector2 point1, Vector2 point2)
        {
            var segment1 = new Segment2(point1, point1);
            var segment2 = new Segment2(point2, point2);
            IntersectionSegmentSegment2 intersection;
            IsFalse_IntersectSegmentSegment(segment1, segment2, out intersection);
            IsFalse_IntersectSegmentSegment(segment2, segment1, out intersection);
        }

        [Test]
        public void Intersect_DegenerateTwoPointsCoincident()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Intersect_DegenerateTwoPointsCoincident(direction);
                Intersect_DegenerateTwoPointsCoincident(direction*1000);
            }
        }

        private void Intersect_DegenerateTwoPointsCoincident(Vector2 point)
        {
            var segment = new Segment2(point, point);
            IntersectionSegmentSegment2 intersection;
            IsTrue_IntersectSegmentSegment(segment, segment, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Point);
            AreEqual(intersection.pointA, point);
            IsTrue_IntersectSegmentSegment(segment, segment, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Point);
            AreEqual(intersection.pointA, point);
        }

        [Test]
        public void Intersect_DegenerateOnePointContaining()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var segment1 = new Segment2(Vector2.zero, direction);
                Intersect_DegenerateOnePointContaining(segment1, Vector2.zero);
                Intersect_DegenerateOnePointContaining(segment1, direction*0.5f);
                Intersect_DegenerateOnePointContaining(segment1, direction);
                segment1 = new Segment2(-direction, direction);
                Intersect_DegenerateOnePointContaining(segment1, -direction);
                Intersect_DegenerateOnePointContaining(segment1, Vector2.zero);
                Intersect_DegenerateOnePointContaining(segment1, direction);
            }
        }

        private void Intersect_DegenerateOnePointContaining(Segment2 segment1, Vector2 point)
        {
            var segment2 = new Segment2(point, point);
            IntersectionSegmentSegment2 intersection;
            IsTrue_IntersectSegmentSegment(segment1, segment2, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Point);
            AreEqual(intersection.pointA, point);
            IsTrue_IntersectSegmentSegment(segment2, segment1, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Point);
            AreEqual(intersection.pointA, point);
        }

        [Test]
        public void Intersect_DegenerateOnePointNonContaining()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Vector2 perpendicular = direction.RotateCW(90);
                Intersect_DegenerateOnePointNonContaining(new Segment2(Vector2.zero, direction), perpendicular);
                Intersect_DegenerateOnePointNonContaining(new Segment2(Vector2.zero, direction*1000), perpendicular);
                Intersect_DegenerateOnePointNonContaining(new Segment2(-direction, direction), perpendicular);
                Intersect_DegenerateOnePointNonContaining(new Segment2(-direction*1000, direction*1000), perpendicular);
            }
        }

        private void Intersect_DegenerateOnePointNonContaining(Segment2 segment1, Vector2 point)
        {
            var segment2 = new Segment2(point, point);
            IntersectionSegmentSegment2 intersection;
            IsFalse_IntersectSegmentSegment(segment1, segment2, out intersection);
            IsFalse_IntersectSegmentSegment(segment2, segment1, out intersection);
        }

        #endregion Degenerate

        [Test]
        public void Intersect_ParallelCodirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Vector2 perpendicular = direction.RotateCW(90);
                var segment1 = new Segment2(Vector2.zero, direction);
                IsFalse_IntersectSegmentSegment(segment1, new Segment2(perpendicular, perpendicular + direction));
                IsFalse_IntersectSegmentSegment(segment1, new Segment2(perpendicular + direction, perpendicular + direction*2));
                IsFalse_IntersectSegmentSegment(segment1, new Segment2(perpendicular - direction, perpendicular));
            }
        }

        [Test]
        public void Intersect_ParallelContradirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Vector2 perpendicular = direction.RotateCW(90);
                var segment1 = new Segment2(Vector2.zero, direction);
                IsFalse_IntersectSegmentSegment(segment1, new Segment2(perpendicular, perpendicular - direction));
                IsFalse_IntersectSegmentSegment(segment1, new Segment2(perpendicular + direction, perpendicular));
                IsFalse_IntersectSegmentSegment(segment1, new Segment2(perpendicular - direction, perpendicular - direction*2));
            }
        }

        [Test]
        public void Intersect_Perpendicular()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Vector2 perpendicular = direction.RotateCW(90);
                var segment1 = new Segment2(Vector2.zero, direction);
                Intersect_Perpendicular(segment1, new Segment2(Vector2.zero, perpendicular), segment1.a);
                Intersect_Perpendicular(segment1, new Segment2(-perpendicular, Vector2.zero), segment1.a);
                Intersect_Perpendicular(segment1, new Segment2(-perpendicular, perpendicular), segment1.a);
                IsFalse_IntersectSegmentSegment(segment1, new Segment2(perpendicular, perpendicular*2));
                IsFalse_IntersectSegmentSegment(segment1, new Segment2(perpendicular*2, perpendicular));
                IsFalse_IntersectSegmentSegment(segment1, new Segment2(-perpendicular*2, -perpendicular));
                IsFalse_IntersectSegmentSegment(segment1, new Segment2(-perpendicular, -perpendicular*2));
            }
        }

        private void Intersect_Perpendicular(Segment2 segment1, Segment2 segment2, Vector2 expected)
        {
            IntersectionSegmentSegment2 intersection;
            IsTrue_IntersectSegmentSegment(segment1, segment2, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Point);
            AreEqual(intersection.pointA, expected);
            IsTrue_IntersectSegmentSegment(segment2, segment1, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Point);
            AreEqual(intersection.pointA, expected);
        }

        private void IsTrue_IntersectSegmentSegment(Segment2 segment1, Segment2 segment2, out IntersectionSegmentSegment2 intersection)
        {
            Assert.IsTrue(Geometry.IntersectSegmentSegment(segment1, segment2, out intersection),
                segment1.ToString("F8") + "\n" + segment2.ToString("F8"));
        }

        private void IsFalse_IntersectSegmentSegment(Segment2 segment1, Segment2 segment2)
        {
            IntersectionSegmentSegment2 intersection;
            IsFalse_IntersectSegmentSegment(segment1, segment2, out intersection);
            IsFalse_IntersectSegmentSegment(segment2, segment1, out intersection);
        }

        private void IsFalse_IntersectSegmentSegment(Segment2 segment1, Segment2 segment2, out IntersectionSegmentSegment2 intersection)
        {
            Assert.IsFalse(Geometry.IntersectSegmentSegment(segment1, segment2, out intersection),
                segment1.ToString("F8") + "\n" + segment2.ToString("F8"));
        }

        #endregion Intersect
    }
}
