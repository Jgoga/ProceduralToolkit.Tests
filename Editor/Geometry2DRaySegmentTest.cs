using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry2DRaySegmentTest : GeometryTest
    {
        #region Intersect

        [Test]
        public void Intersect_CollinearSegment()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var ray = new Ray2D(Vector2.zero, direction);

                Intersect_CollinearSegment(ray, Vector2.zero, direction);
                Intersect_CollinearSegment(ray, Vector2.zero, direction*100);
                Intersect_CollinearSegment(ray, direction, direction*2);
                Intersect_CollinearSegment(ray, direction*100, direction*200);

                Intersect_CollinearSegment(ray, -direction, direction, ray.origin, direction);
                Intersect_CollinearSegment(ray, -direction*100, direction*100, ray.origin, direction*100);
            }
        }

        private void Intersect_CollinearSegment(Ray2D ray, Vector2 a, Vector2 b)
        {
            Intersect_CollinearSegment(ray, a, b, a, b);
        }

        private void Intersect_CollinearSegment(Ray2D ray, Vector2 a, Vector2 b, Vector2 expectedA, Vector2 expectedB)
        {
            IntersectionRaySegment2 intersection;
            IsTrue_Intersect(ray, new Segment2(a, b), out intersection);
            Assert.AreEqual(IntersectionType.Segment, intersection.type);
            AreEqual(intersection.pointA, expectedA);
            AreEqual(intersection.pointB, expectedB);
            IsTrue_Intersect(ray, new Segment2(b, a), out intersection);
            Assert.AreEqual(IntersectionType.Segment, intersection.type);
            AreEqual(intersection.pointA, expectedA);
            AreEqual(intersection.pointB, expectedB);
        }

        [Test]
        public void Intersect_CollinearPoint()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var ray = new Ray2D(Vector2.zero, direction);

                Intersect_CollinearPoint(ray, -direction, Vector2.zero);
                Intersect_CollinearPoint(ray, -direction*100, Vector2.zero);
            }
        }

        private void Intersect_CollinearPoint(Ray2D ray, Vector2 a, Vector2 b)
        {
            IntersectionRaySegment2 intersection;
            IsTrue_Intersect(ray, new Segment2(a, b), out intersection);
            Assert.AreEqual(IntersectionType.Point, intersection.type);
            AreEqual(intersection.pointA, ray.origin);
            IsTrue_Intersect(ray, new Segment2(b, a), out intersection);
            Assert.AreEqual(IntersectionType.Point, intersection.type);
            AreEqual(intersection.pointA, ray.origin);
        }

        [Test]
        public void Intersect_Parallel()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Vector2 perpendicular = direction.RotateCW(90);
                var ray = new Ray2D(Vector2.zero, direction);
                Intersect_Parallel(ray, perpendicular, perpendicular + direction);
                Intersect_Parallel(ray, perpendicular, perpendicular + direction*100);
                Intersect_Parallel(ray, perpendicular*100, perpendicular*100 + direction);
                Intersect_Parallel(ray, perpendicular*100, perpendicular*100 + direction*100);
            }
        }

        private void Intersect_Parallel(Ray2D ray, Vector2 a, Vector2 b)
        {
            IsFalse_Intersect(ray, new Segment2(a, b));
            IsFalse_Intersect(ray, new Segment2(b, a));
        }

        [Test]
        public void Intersect_Perpendicular()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Vector2 perpendicular = direction.RotateCW(90);
                var ray = new Ray2D(Vector2.zero, direction);
                IsTrue_Intersect_Perpendicular(ray, perpendicular, direction, direction);
                IsTrue_Intersect_Perpendicular(ray, perpendicular, direction*50, direction*50);
                IsTrue_Intersect_Perpendicular(ray, perpendicular*100, direction*50, direction*50);
                IsTrue_Intersect_Perpendicular(ray, perpendicular*100, direction*50 + perpendicular*20, direction*50);
                IsFalse_Intersect_Perpendicular(ray, perpendicular, -direction);
                IsFalse_Intersect_Perpendicular(ray, perpendicular, direction + perpendicular*2);
                IsFalse_Intersect_Perpendicular(ray, perpendicular, direction - perpendicular*2);
            }
        }

        private void IsTrue_Intersect_Perpendicular(Ray2D ray, Vector2 perpendicular, Vector2 offset, Vector2 expectedIntersection)
        {
            var segment = new Segment2(-perpendicular + offset, perpendicular + offset);
            IntersectionRaySegment2 intersection;
            IsTrue_Intersect(ray, segment, out intersection);
            Assert.AreEqual(IntersectionType.Point, intersection.type);
            AreEqual(intersection.pointA, expectedIntersection);
        }

        private void IsFalse_Intersect_Perpendicular(Ray2D ray, Vector2 perpendicular, Vector2 offset)
        {
            var segment = new Segment2(-perpendicular + offset, perpendicular + offset);
            IsFalse_Intersect(ray, segment);
        }

        [Test]
        public void Intersect_DegenerateSegment()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Vector2 perpendicular = direction.RotateCW(90);
                var ray = new Ray2D(Vector2.zero, direction);

                Intersect_DegenerateSegment(ray, Vector2.zero);
                Intersect_DegenerateSegment(ray, direction);
                Intersect_DegenerateSegment(ray, direction*100);

                IsFalse_Intersect(ray, new Segment2(-direction, -direction));
                IsFalse_Intersect(ray, new Segment2(-direction*100, -direction*100));

                IsFalse_Intersect(ray, new Segment2(perpendicular, perpendicular));
                IsFalse_Intersect(ray, new Segment2(perpendicular + direction, perpendicular + direction));
                IsFalse_Intersect(ray, new Segment2(perpendicular + direction*100, perpendicular + direction*100));
                IsFalse_Intersect(ray, new Segment2(perpendicular - direction, perpendicular - direction));
                IsFalse_Intersect(ray, new Segment2(perpendicular - direction*100, perpendicular - direction*100));
            }
        }

        private void Intersect_DegenerateSegment(Ray2D ray, Vector2 a)
        {
            IntersectionRaySegment2 intersection;
            IsTrue_Intersect(ray, new Segment2(a, a), out intersection);
            Assert.AreEqual(IntersectionType.Point, intersection.type);
            AreEqual(intersection.pointA, a);
        }

        private void IsTrue_Intersect(Ray2D ray, Segment2 segment, out IntersectionRaySegment2 intersection)
        {
            Assert.IsTrue(Geometry.IntersectRaySegment(ray.origin, ray.direction, segment.a, segment.b, out intersection),
                ray.ToString("F8") + "\n" + segment.ToString("F8"));
        }

        private void IsFalse_Intersect(Ray2D ray, Segment2 segment)
        {
            IntersectionRaySegment2 intersection;
            Assert.IsFalse(Geometry.IntersectRaySegment(ray.origin, ray.direction, segment.a, segment.b, out intersection),
                ray.ToString("F8") + "\n" + segment.ToString("F8"));
        }

        #endregion Intersect
    }
}
