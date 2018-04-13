using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry2DPointSegmentTest : GeometryTest
    {
        [Test]
        public void Distance_PointOnLine()
        {
            var xSegment = new Segment2(Vector2.zero, Vector2.right);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.zero, xSegment), 0);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.right, xSegment), 0);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.left, xSegment), 1);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.right*1000, xSegment), 999);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.left*1000, xSegment), 1000);

            var ySegment = new Segment2(Vector2.zero, Vector2.up);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.zero, ySegment), 0);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.up, ySegment), 0);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.down, ySegment), 1);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.up*1000, ySegment), 999);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.down*1000, ySegment), 1000);

            var diagonal = new Segment2(Vector2.zero, Vector2.one.normalized);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.zero, diagonal), 0);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.one, diagonal), Vector2.one.magnitude - Vector2.one.normalized.magnitude);
            Assert.AreEqual(Geometry.DistanceToSegment(-Vector2.one, diagonal), Vector2.one.magnitude);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.one*1000, diagonal), (Vector2.one*1000).magnitude - Vector2.one.normalized.magnitude);
            Assert.AreEqual(Geometry.DistanceToSegment(-Vector2.one*1000, diagonal), (Vector2.one*1000).magnitude);
        }

        [Test]
        public void Distance_PointNotOnLine()
        {
            var xSegment = new Segment2(Vector2.zero, Vector2.right);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.up, xSegment), 1);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.down, xSegment), 1);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.up*1000, xSegment), 1000);
            Assert.AreEqual(Geometry.DistanceToSegment(Vector2.down*1000, xSegment), 1000);
        }

        [Test]
        public void ClosestPoint_PointOnLine()
        {
            var xSegment = new Segment2(Vector2.zero, Vector2.right);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.zero, xSegment), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.right, xSegment), Vector2.right);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.left, xSegment), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.right*1000, xSegment), Vector2.right);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.left*1000, xSegment), Vector2.zero);

            var ySegment = new Segment2(Vector2.zero, Vector2.up);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.zero, ySegment), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.up, ySegment), Vector2.up);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.down, ySegment), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.up*1000, ySegment), Vector2.up);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.down*1000, ySegment), Vector2.zero);

            var diagonal = new Segment2(Vector2.zero, Vector2.one.normalized);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.zero, diagonal), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.one, diagonal), Vector2.one.normalized);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(-Vector2.one, diagonal), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.one*1000, diagonal), Vector2.one.normalized);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(-Vector2.one*1000, diagonal), Vector2.zero);
        }

        [Test]
        public void ClosestPoint_PointNotOnLine()
        {
            var xSegment = new Segment2(Vector2.zero, Vector2.right);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.up, xSegment), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.down, xSegment), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.up*1000, xSegment), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnSegment(Vector2.down*1000, xSegment), Vector2.zero);
        }

        [Test]
        public void Intersect_PointOnLine()
        {
            var xSegment = new Segment2(Vector2.zero, Vector2.right);
            Assert.True(Geometry.IntersectPointSegment(Vector2.zero, xSegment));
            Assert.True(Geometry.IntersectPointSegment(Vector2.right, xSegment));
            Assert.False(Geometry.IntersectPointSegment(Vector2.left, xSegment));
            Assert.False(Geometry.IntersectPointSegment(Vector2.right*1000, xSegment));
            Assert.False(Geometry.IntersectPointSegment(Vector2.left*1000, xSegment));

            var ySegment = new Segment2(Vector2.zero, Vector2.up);
            Assert.True(Geometry.IntersectPointSegment(Vector2.zero, ySegment));
            Assert.True(Geometry.IntersectPointSegment(Vector2.up, ySegment));
            Assert.False(Geometry.IntersectPointSegment(Vector2.down, ySegment));
            Assert.False(Geometry.IntersectPointSegment(Vector2.up*1000, ySegment));
            Assert.False(Geometry.IntersectPointSegment(Vector2.down*1000, ySegment));

            var diagonal = new Segment2(Vector2.zero, Vector2.one.normalized);
            Assert.True(Geometry.IntersectPointSegment(Vector2.zero, diagonal));
            Assert.True(Geometry.IntersectPointSegment(Vector2.one.normalized, diagonal));
            Assert.False(Geometry.IntersectPointSegment(-Vector2.one, diagonal));
            Assert.False(Geometry.IntersectPointSegment(Vector2.one*1000, diagonal));
            Assert.False(Geometry.IntersectPointSegment(-Vector2.one*1000, diagonal));
        }

        [Test]
        public void Intersect_PointNotOnLine()
        {
            int side;
            var xSegment = new Segment2(Vector2.zero, Vector2.right);
            Assert.False(Geometry.IntersectPointSegment(Vector2.up, xSegment, out side));
            Assert.AreEqual(side, -1);
            Assert.False(Geometry.IntersectPointSegment(Vector2.down, xSegment, out side));
            Assert.AreEqual(side, 1);
            Assert.False(Geometry.IntersectPointSegment(Vector2.up*1000, xSegment, out side));
            Assert.AreEqual(side, -1);
            Assert.False(Geometry.IntersectPointSegment(Vector2.down*1000, xSegment, out side));
            Assert.AreEqual(side, 1);

            var ySegment = new Segment2(Vector2.zero, Vector2.up);
            Assert.False(Geometry.IntersectPointSegment(Vector2.left, ySegment, out side));
            Assert.AreEqual(side, -1);
            Assert.False(Geometry.IntersectPointSegment(Vector2.right, ySegment, out side));
            Assert.AreEqual(side, 1);
            Assert.False(Geometry.IntersectPointSegment(Vector2.left*1000, ySegment, out side));
            Assert.AreEqual(side, -1);
            Assert.False(Geometry.IntersectPointSegment(Vector2.right*1000, ySegment, out side));
            Assert.AreEqual(side, 1);
        }
    }
}
