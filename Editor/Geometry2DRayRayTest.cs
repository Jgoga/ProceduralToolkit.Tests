using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry2DRayRayTest : GeometryTest
    {
        #region Distance

        [Test]
        public void Distance_Coincident()
        {
            for (int i = 0; i < 360; i++)
            {
                var ray = new Ray2D(Vector2.zero, Vector2.up.RotateCW(i).normalized);
                AreEqual_DistanceToRay(ray, ray, 0);
            }
        }

        [Test]
        public void Distance_CollinearCodirected()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(direction*100, direction);
                AreEqual_DistanceToRaySwap(rayA, rayB, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(-direction*100, direction);
                var rayB = new Ray2D(Vector2.zero, direction);
                AreEqual_DistanceToRaySwap(rayA, rayB, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(-direction*100, direction);
                var rayB = new Ray2D(direction*100, direction);
                AreEqual_DistanceToRaySwap(rayA, rayB, 0);
            }
        }

        [Test]
        public void Distance_CollinearContradirectedOverlapping()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(direction*100, -direction);
                AreEqual_DistanceToRaySwap(rayA, rayB, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(-direction*100, direction);
                var rayB = new Ray2D(Vector2.zero, -direction);
                AreEqual_DistanceToRaySwap(rayA, rayB, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(-direction*100, direction);
                var rayB = new Ray2D(direction*100, -direction);
                AreEqual_DistanceToRaySwap(rayA, rayB, 0);
            }
        }

        [Test]
        public void Distance_CollinearContradirectedNonOverlapping()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, -direction);
                var rayB = new Ray2D(direction*100, direction);
                AreEqual_DistanceToRaySwap(rayA, rayB, 100);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(-direction*100, -direction);
                var rayB = new Ray2D(Vector2.zero, direction);
                AreEqual_DistanceToRaySwap(rayA, rayB, 100);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(-direction*50, -direction);
                var rayB = new Ray2D(direction*50, direction);
                AreEqual_DistanceToRaySwap(rayA, rayB, 100);
            }
        }

        [Test]
        public void Distance_Parallel()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(direction.RotateCW(90), direction);
                AreEqual_DistanceToRaySwap(rayA, rayB, 1);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(direction.RotateCW(90) + direction, direction);
                AreEqual_DistanceToRaySwap(rayA, rayB, 1);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(direction.RotateCW(90) - direction, direction);
                AreEqual_DistanceToRaySwap(rayA, rayB, 1);
            }
        }

        [Test]
        public void Distance_Perpendicular()
        {
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(Vector2.zero, direction.RotateCW(90));
                AreEqual_DistanceToRaySwap(rayA, rayB, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(-direction.RotateCW(90), direction.RotateCW(90));
                AreEqual_DistanceToRaySwap(rayA, rayB, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 direction = Vector2.up.RotateCW(i).normalized;
                var rayA = new Ray2D(Vector2.zero, direction);
                var rayB = new Ray2D(direction.RotateCW(90), direction.RotateCW(90));
                AreEqual_DistanceToRaySwap(rayA, rayB, 1);
            }
        }

        private void AreEqual_DistanceToRaySwap(Ray2D rayA, Ray2D rayB, float expected)
        {
            AreEqual_DistanceToRay(rayA, rayB, expected);
            AreEqual_DistanceToRay(rayB, rayA, expected);
        }

        private void AreEqual_DistanceToRay(Ray2D rayA, Ray2D rayB, float expected)
        {
            float distance = Geometry.DistanceToRay(rayA, rayB);
            float delta = Mathf.Abs(expected - distance);
            Assert.True(delta < Geometry.Epsilon, string.Format("{0}\n{1}\ndistance: {2:G9} expected: {3:G9}\ndelta: {4:F8}",
                rayA.ToString("G9"), rayB.ToString("G9"), distance, expected, delta));
        }

        #endregion Distance

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
