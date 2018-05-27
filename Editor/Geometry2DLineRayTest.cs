using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry2DLineRayTest : GeometryTest
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
                Intersect_Collinear(line, direction, direction);
                Intersect_Collinear(line, direction*100, direction);
                Intersect_Collinear(line, -direction, direction);
                Intersect_Collinear(line, -direction*100, direction);
            }
        }

        private void Intersect_Collinear(Line2 line, Vector2 origin, Vector2 direction)
        {
            IntersectionLineRay2 intersection;
            IsTrue_Intersect(line, new Ray2D(origin, direction), out intersection);
            Assert.AreEqual(IntersectionType.Ray, intersection.type);
            AreEqual(intersection.point, origin);
            IsTrue_Intersect(line, new Ray2D(origin, -direction), out intersection);
            Assert.AreEqual(IntersectionType.Ray, intersection.type);
            AreEqual(intersection.point, origin);
        }

        [Test]
        public void Intersect_Parallel()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Vector2 perpendicular = direction.RotateCW(90);
                var line = new Line2(Vector2.zero, direction);
                Intersect_Parallel(line, perpendicular, direction);
                Intersect_Parallel(line, perpendicular + direction*100, direction);
                Intersect_Parallel(line, perpendicular - direction*100, direction);
                Intersect_Parallel(line, perpendicular*100, direction);
                Intersect_Parallel(line, perpendicular*100 + direction*100, direction);
                Intersect_Parallel(line, perpendicular*100 - direction*100, direction);
            }
        }

        private void Intersect_Parallel(Line2 line, Vector2 origin, Vector2 direction)
        {
            IsFalse_Intersect(line, new Ray2D(origin, direction));
            IsFalse_Intersect(line, new Ray2D(origin, -direction));
        }

        [Test]
        public void Intersect_Perpendicular()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                Vector2 perpendicular = direction.RotateCW(90);
                var line = new Line2(Vector2.zero, direction);
                IsTrue_Intersect_Perpendicular(line, Vector2.zero, perpendicular, Vector2.zero);
                IsTrue_Intersect_Perpendicular(line, -perpendicular, perpendicular, Vector2.zero);
                IsTrue_Intersect_Perpendicular(line, -perpendicular*100, perpendicular, Vector2.zero);

                IsFalse_Intersect_Perpendicular(line, perpendicular, perpendicular);
                IsFalse_Intersect_Perpendicular(line, perpendicular*100, perpendicular);
                IsFalse_Intersect_Perpendicular(line, -perpendicular, -perpendicular);
                IsFalse_Intersect_Perpendicular(line, -perpendicular*100, -perpendicular);
            }
        }

        private void IsTrue_Intersect_Perpendicular(Line2 line, Vector2 origin, Vector2 direction, Vector2 expectedIntersection)
        {
            var ray = new Ray2D(origin, direction);
            IntersectionLineRay2 intersection;
            IsTrue_Intersect(line, ray, out intersection);
            Assert.AreEqual(IntersectionType.Point, intersection.type);
            AreEqual(intersection.point, expectedIntersection);
        }

        private void IsFalse_Intersect_Perpendicular(Line2 line, Vector2 origin, Vector2 direction)
        {
            IsFalse_Intersect(line, new Ray2D(origin, direction));
        }

        private void IsTrue_Intersect(Line2 line, Ray2D ray, out IntersectionLineRay2 intersection)
        {
            Assert.IsTrue(Geometry.IntersectLineRay(line, ray, out intersection),
                line.ToString("F8") + "\n" + ray.ToString("F8"));
        }

        private void IsFalse_Intersect(Line2 line, Ray2D ray)
        {
            IntersectionLineRay2 intersection;
            Assert.IsFalse(Geometry.IntersectLineRay(line, ray, out intersection),
                line.ToString("F8") + "\n" + ray.ToString("F8"));
        }

        #endregion Intersect
    }
}
