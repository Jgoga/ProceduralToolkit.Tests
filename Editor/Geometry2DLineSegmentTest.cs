using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry2DLineSegmentTest : GeometryTest
    {
        #region Intersect

        [Test]
        public void Intersect_Collinear()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var line = new Line2(Vector2.zero, direction);

                Intersect_Collinear(line, Vector2.zero, direction);
                Intersect_Collinear(line, Vector2.zero, direction*100);
                Intersect_Collinear(line, direction, direction*2);
                Intersect_Collinear(line, direction*100, direction*200);

                Intersect_Collinear(line, -direction, direction);
                Intersect_Collinear(line, -direction*100, direction*100);

                Intersect_Collinear(line, -direction, Vector2.zero);
                Intersect_Collinear(line, -direction*100, Vector2.zero);
                Intersect_Collinear(line, -direction*2, -direction);
                Intersect_Collinear(line, -direction*200, -direction*100);
            }
        }

        private void Intersect_Collinear(Line2 line, Vector2 a, Vector2 b)
        {
            IntersectionLineSegment2 intersection;
            IsTrue_Intersect(line, new Segment2(a, b), out intersection);
            Assert.AreEqual(IntersectionType.Segment, intersection.type);
            AreEqual(intersection.pointA, a);
            AreEqual(intersection.pointB, b);
            IsTrue_Intersect(line, new Segment2(b, a), out intersection);
            Assert.AreEqual(IntersectionType.Segment, intersection.type);
            AreEqual(intersection.pointA, a);
            AreEqual(intersection.pointB, b);
        }

        [Test]
        public void Intersect_Parallel()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Vector2 perpendicular = direction.RotateCW(90);
                var line = new Line2(Vector2.zero, direction);
                Intersect_Parallel(line, perpendicular, perpendicular + direction);
                Intersect_Parallel(line, perpendicular, perpendicular + direction*100);
                Intersect_Parallel(line, perpendicular - direction, perpendicular + direction);
                Intersect_Parallel(line, perpendicular - direction*100, perpendicular + direction*100);
                Intersect_Parallel(line, perpendicular*100, perpendicular*100 + direction);
                Intersect_Parallel(line, perpendicular*100, perpendicular*100 + direction*100);
                Intersect_Parallel(line, perpendicular*100 - direction, perpendicular*100 + direction);
                Intersect_Parallel(line, perpendicular*100 - direction*100, perpendicular*100 + direction*100);
            }
        }

        private void Intersect_Parallel(Line2 line, Vector2 a, Vector2 b)
        {
            IsFalse_Intersect(line, new Segment2(a, b));
            IsFalse_Intersect(line, new Segment2(b, a));
        }

        [Test]
        public void Intersect_Perpendicular()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Vector2 perpendicular = direction.RotateCW(90);
                var line = new Line2(Vector2.zero, direction);
                IsTrue_Intersect_Perpendicular(line, perpendicular, direction, direction);
                IsTrue_Intersect_Perpendicular(line, perpendicular, direction*50, direction*50);
                IsTrue_Intersect_Perpendicular(line, perpendicular*100, direction*50, direction*50);
                IsTrue_Intersect_Perpendicular(line, perpendicular*100, direction*50 + perpendicular*20, direction*50);
                IsTrue_Intersect_Perpendicular(line, perpendicular, -direction, -direction);
                IsTrue_Intersect_Perpendicular(line, perpendicular, -direction*50, -direction*50);
                IsTrue_Intersect_Perpendicular(line, perpendicular*100, -direction*50, -direction*50);
                IsTrue_Intersect_Perpendicular(line, perpendicular*100, -direction*50 + perpendicular*20, -direction*50);

                IsFalse_Intersect_Perpendicular(line, perpendicular, perpendicular*2);
                IsFalse_Intersect_Perpendicular(line, perpendicular, perpendicular*100);
                IsFalse_Intersect_Perpendicular(line, perpendicular, -perpendicular*2);
                IsFalse_Intersect_Perpendicular(line, perpendicular, -perpendicular*100);
            }
        }

        private void IsTrue_Intersect_Perpendicular(Line2 line, Vector2 perpendicular, Vector2 offset, Vector2 expectedIntersection)
        {
            var segment = new Segment2(-perpendicular + offset, perpendicular + offset);
            IntersectionLineSegment2 intersection;
            IsTrue_Intersect(line, segment, out intersection);
            Assert.AreEqual(IntersectionType.Point, intersection.type);
            AreEqual(intersection.pointA, expectedIntersection);
        }

        private void IsFalse_Intersect_Perpendicular(Line2 line, Vector2 perpendicular, Vector2 offset)
        {
            var segment = new Segment2(-perpendicular + offset, perpendicular + offset);
            IsFalse_Intersect(line, segment);
        }

        [Test]
        public void Intersect_DegenerateSegment()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Vector2 perpendicular = direction.RotateCW(90);
                var line = new Line2(Vector2.zero, direction);

                Intersect_DegenerateSegment(line, Vector2.zero);
                Intersect_DegenerateSegment(line, direction);
                Intersect_DegenerateSegment(line, direction*100);
                Intersect_DegenerateSegment(line, -direction);
                Intersect_DegenerateSegment(line, -direction*100);

                IsFalse_Intersect(line, new Segment2(perpendicular, perpendicular));
                IsFalse_Intersect(line, new Segment2(perpendicular + direction, perpendicular + direction));
                IsFalse_Intersect(line, new Segment2(perpendicular + direction*100, perpendicular + direction*100));
                IsFalse_Intersect(line, new Segment2(perpendicular - direction, perpendicular - direction));
                IsFalse_Intersect(line, new Segment2(perpendicular - direction*100, perpendicular - direction*100));
            }
        }

        private void Intersect_DegenerateSegment(Line2 line, Vector2 a)
        {
            IntersectionLineSegment2 intersection;
            IsTrue_Intersect(line, new Segment2(a, a), out intersection);
            Assert.AreEqual(IntersectionType.Point, intersection.type);
            AreEqual(intersection.pointA, a);
        }

        private void IsTrue_Intersect(Line2 line, Segment2 segment, out IntersectionLineSegment2 intersection)
        {
            Assert.IsTrue(Geometry.IntersectLineSegment(line.origin, line.direction, segment.a, segment.b, out intersection),
                line.ToString("F8") + "\n" + segment.ToString("F8"));
        }

        private void IsFalse_Intersect(Line2 line, Segment2 segment)
        {
            IntersectionLineSegment2 intersection;
            Assert.IsFalse(Geometry.IntersectLineSegment(line.origin, line.direction, segment.a, segment.b, out intersection),
                line.ToString("F8") + "\n" + segment.ToString("F8"));
        }

        #endregion Intersect
    }
}
