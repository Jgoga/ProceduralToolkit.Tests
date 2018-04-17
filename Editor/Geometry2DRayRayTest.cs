using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry2DRayRayTest : GeometryTest
    {
        #region Intersect

        [Test]
        public void Intersect_Coincident()
        {
            for (int i = 0; i < 360; i++)
            {
                Intersect_Coincident(new Ray2D(Vector2.zero, Vector2.up.RotateCW(i).normalized));
            }
        }

        private void Intersect_Coincident(Ray2D ray)
        {
            IntersectionRayRay2 intersection;
            Assert.IsTrue(Geometry.IntersectRayRay(ray, ray, out intersection), ray.ToString("F8"));
            Assert.AreEqual(intersection.type, IntersectionType.Ray);
            AreEqual(intersection.pointA, ray.origin);
            AreEqual(intersection.pointB, ray.direction);
        }

        [Test]
        public void Intersect_CollinearCodirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(direction*100, direction);
                Intersect_CollinearCodirected(rayA, rayB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(-direction*100, direction);
                var rayB = new Ray2D(Vector2.zero, direction);
                Intersect_CollinearCodirected(rayA, rayB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(-direction*100, direction);
                var rayB = new Ray2D(direction*100, direction);
                Intersect_CollinearCodirected(rayA, rayB);
            }
        }

        private void Intersect_CollinearCodirected(Ray2D rayA, Ray2D rayB)
        {
            IntersectionRayRay2 intersection;
            IsTrue_IntersectRayRay(rayA, rayB, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Ray);
            AreEqual(intersection.pointA, rayB.origin);
            IsTrue_IntersectRayRay(rayB, rayA, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Ray);
            AreEqual(intersection.pointA, rayB.origin);
        }

        [Test]
        public void Intersect_CollinearContradirectedOverlapping()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(direction*100, -direction);
                Intersect_CollinearContradirectedOverlapping(rayA, rayB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(-direction*100, direction);
                var rayB = new Ray2D(Vector2.zero, -direction);
                Intersect_CollinearContradirectedOverlapping(rayA, rayB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(-direction*100, direction);
                var rayB = new Ray2D(direction*100, -direction);
                Intersect_CollinearContradirectedOverlapping(rayA, rayB);
            }
        }

        private void Intersect_CollinearContradirectedOverlapping(Ray2D rayA, Ray2D rayB)
        {
            IntersectionRayRay2 intersection;
            IsTrue_IntersectRayRay(rayA, rayB, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Segment);
            AreEqual(intersection.pointA, rayA.origin);
            AreEqual(intersection.pointB, rayB.origin);
            IsTrue_IntersectRayRay(rayB, rayA, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Segment);
            AreEqual(intersection.pointA, rayB.origin);
            AreEqual(intersection.pointB, rayA.origin);
        }

        [Test]
        public void Intersect_CollinearContradirectedNonOverlapping()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, -direction);
                var rayB = new Ray2D(direction*100, direction);
                IsFalse_IntersectRayRay(rayA, rayB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(-direction*100, -direction);
                var rayB = new Ray2D(Vector2.zero, direction);
                IsFalse_IntersectRayRay(rayA, rayB);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(-direction*100, -direction);
                var rayB = new Ray2D(direction*100, direction);
                IsFalse_IntersectRayRay(rayA, rayB);
            }
        }

        [Test]
        public void Intersect_Parallel()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(direction.RotateCW(90), direction);
                IsFalse_IntersectRayRay(rayA, rayB);
            }
        }

        [Test]
        public void Intersect_Perpendicular()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(Vector2.zero, direction.RotateCW(90));
                Intersect_Perpendicular(rayA, rayB, rayA.origin);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(-direction.RotateCW(90), direction.RotateCW(90));
                Intersect_Perpendicular(rayA, rayB, rayA.origin);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(direction.RotateCW(90), direction.RotateCW(90));
                IsFalse_IntersectRayRay(rayA, rayB);
            }
        }

        private void Intersect_Perpendicular(Ray2D rayA, Ray2D rayB, Vector2 expected)
        {
            IntersectionRayRay2 intersection;
            IsTrue_IntersectRayRay(rayA, rayB, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Point);
            AreEqual(intersection.pointA, expected);
            IsTrue_IntersectRayRay(rayB, rayA, out intersection);
            Assert.AreEqual(intersection.type, IntersectionType.Point);
            AreEqual(intersection.pointA, expected);
        }

        private void IsTrue_IntersectRayRay(Ray2D rayA, Ray2D rayB, out IntersectionRayRay2 intersection)
        {
            Assert.IsTrue(Geometry.IntersectRayRay(rayA, rayB, out intersection), rayA.ToString("F8") + "\n" + rayB.ToString("F8"));
        }

        private void IsFalse_IntersectRayRay(Ray2D rayA, Ray2D rayB)
        {
            IntersectionRayRay2 intersection;
            IsFalse_IntersectRayRay(rayA, rayB, out intersection);
            IsFalse_IntersectRayRay(rayB, rayA, out intersection);
        }

        private void IsFalse_IntersectRayRay(Ray2D rayA, Ray2D rayB, out IntersectionRayRay2 intersection)
        {
            Assert.IsFalse(Geometry.IntersectRayRay(rayA, rayB, out intersection), rayA.ToString("F8") + "\n" + rayB.ToString("F8"));
        }

        #endregion Intersect
    }
}
