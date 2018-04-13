using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry2DPointRayTest : GeometryTest
    {
        [Test]
        public void Distance_PointOnLine()
        {
            var xRay = new Ray2D(Vector2.zero, Vector2.right);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.zero, xRay), 0);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.right, xRay), 0);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.left, xRay), 1);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.right*1000, xRay), 0);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.left*1000, xRay), 1000);

            var yRay = new Ray2D(Vector2.zero, Vector2.up);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.zero, yRay), 0);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.up, yRay), 0);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.down, yRay), 1);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.up*1000, yRay), 0);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.down*1000, yRay), 1000);

            var diagonal = new Ray2D(Vector2.zero, Vector2.one.normalized);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.zero, diagonal), 0);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.one, diagonal), 0);
            Assert.AreEqual(Geometry.DistanceToRay(-Vector2.one, diagonal), Vector2.one.magnitude);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.one*1000, diagonal), 0);
            Assert.AreEqual(Geometry.DistanceToRay(-Vector2.one*1000, diagonal), (Vector2.one*1000).magnitude);
        }

        [Test]
        public void Distance_PointNotOnLine()
        {
            var xRay = new Ray2D(Vector2.zero, Vector2.right);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.up, xRay), 1);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.down, xRay), 1);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.up*1000, xRay), 1000);
            Assert.AreEqual(Geometry.DistanceToRay(Vector2.down*1000, xRay), 1000);
        }

        [Test]
        public void ClosestPoint_PointOnLine()
        {
            var xRay = new Ray2D(Vector2.zero, Vector2.right);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.zero, xRay), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.right, xRay), Vector2.right);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.left, xRay), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.right*1000, xRay), Vector2.right*1000);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.left*1000, xRay), Vector2.zero);

            var yRay = new Ray2D(Vector2.zero, Vector2.up);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.zero, yRay), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.up, yRay), Vector2.up);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.down, yRay), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.up*1000, yRay), Vector2.up*1000);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.down*1000, yRay), Vector2.zero);

            var diagonal = new Ray2D(Vector2.zero, Vector2.one.normalized);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.zero, diagonal), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.one, diagonal), Vector2.one);
            Assert.AreEqual(Geometry.ClosestPointOnRay(-Vector2.one, diagonal), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.one*1000, diagonal), Vector2.one*1000);
            Assert.AreEqual(Geometry.ClosestPointOnRay(-Vector2.one*1000, diagonal), Vector2.zero);
        }

        [Test]
        public void ClosestPoint_PointNotOnLine()
        {
            var xRay = new Ray2D(Vector2.zero, Vector2.right);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.up, xRay), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.down, xRay), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.up*1000, xRay), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnRay(Vector2.down*1000, xRay), Vector2.zero);
        }

        [Test]
        public void Intersect_PointOnLine()
        {
            var xRay = new Ray2D(Vector2.zero, Vector2.right);
            Assert.True(Geometry.IntersectPointRay(Vector2.zero, xRay));
            Assert.True(Geometry.IntersectPointRay(Vector2.right, xRay));
            Assert.False(Geometry.IntersectPointRay(Vector2.left, xRay));
            Assert.True(Geometry.IntersectPointRay(Vector2.right*1000, xRay));
            Assert.False(Geometry.IntersectPointRay(Vector2.left*1000, xRay));

            var yRay = new Ray2D(Vector2.zero, Vector2.up);
            Assert.True(Geometry.IntersectPointRay(Vector2.zero, yRay));
            Assert.True(Geometry.IntersectPointRay(Vector2.up, yRay));
            Assert.False(Geometry.IntersectPointRay(Vector2.down, yRay));
            Assert.True(Geometry.IntersectPointRay(Vector2.up*1000, yRay));
            Assert.False(Geometry.IntersectPointRay(Vector2.down*1000, yRay));

            var diagonal = new Ray2D(Vector2.zero, Vector2.one.normalized);
            Assert.True(Geometry.IntersectPointRay(Vector2.zero, diagonal));
            Assert.True(Geometry.IntersectPointRay(Vector2.one, diagonal));
            Assert.False(Geometry.IntersectPointRay(-Vector2.one, diagonal));
            Assert.True(Geometry.IntersectPointRay(Vector2.one*1000, diagonal));
            Assert.False(Geometry.IntersectPointRay(-Vector2.one*1000, diagonal));
        }

        [Test]
        public void Intersect_PointNotOnLine()
        {
            int side;
            var xRay = new Ray2D(Vector2.zero, Vector2.right);
            Assert.False(Geometry.IntersectPointRay(Vector2.up, xRay, out side));
            Assert.AreEqual(side, -1);
            Assert.False(Geometry.IntersectPointRay(Vector2.down, xRay, out side));
            Assert.AreEqual(side, 1);
            Assert.False(Geometry.IntersectPointRay(Vector2.up*1000, xRay, out side));
            Assert.AreEqual(side, -1);
            Assert.False(Geometry.IntersectPointRay(Vector2.down*1000, xRay, out side));
            Assert.AreEqual(side, 1);

            var yRay = new Ray2D(Vector2.zero, Vector2.up);
            Assert.False(Geometry.IntersectPointRay(Vector2.left, yRay, out side));
            Assert.AreEqual(side, -1);
            Assert.False(Geometry.IntersectPointRay(Vector2.right, yRay, out side));
            Assert.AreEqual(side, 1);
            Assert.False(Geometry.IntersectPointRay(Vector2.left*1000, yRay, out side));
            Assert.AreEqual(side, -1);
            Assert.False(Geometry.IntersectPointRay(Vector2.right*1000, yRay, out side));
            Assert.AreEqual(side, 1);
        }
    }
}
