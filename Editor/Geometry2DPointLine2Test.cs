using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry2DPointLine2Test : GeometryTest
    {
        [Test]
        public void Distance_PointOnLine()
        {
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.zero, Line2.xAxis), 0);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.right, Line2.xAxis), 0);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.left, Line2.xAxis), 0);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.right*1000, Line2.xAxis), 0);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.left*1000, Line2.xAxis), 0);

            Assert.AreEqual(Geometry.DistanceToLine(Vector2.zero, Line2.yAxis), 0);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.up, Line2.yAxis), 0);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.down, Line2.yAxis), 0);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.up*1000, Line2.yAxis), 0);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.down*1000, Line2.yAxis), 0);

            var diagonal = new Line2(Vector2.zero, Vector2.one.normalized);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.zero, diagonal), 0);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.one, diagonal), 0);
            Assert.AreEqual(Geometry.DistanceToLine(-Vector2.one, diagonal), 0);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.one*1000, diagonal), 0);
            Assert.AreEqual(Geometry.DistanceToLine(-Vector2.one*1000, diagonal), 0);
        }

        [Test]
        public void Distance_PointNotOnLine()
        {
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.up, Line2.xAxis), 1);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.down, Line2.xAxis), 1);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.up*1000, Line2.xAxis), 1000);
            Assert.AreEqual(Geometry.DistanceToLine(Vector2.down*1000, Line2.xAxis), 1000);
        }

        [Test]
        public void ClosestPoint_PointOnLine()
        {
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.zero, Line2.xAxis), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.right, Line2.xAxis), Vector2.right);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.left, Line2.xAxis), Vector2.left);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.right*1000, Line2.xAxis), Vector2.right*1000);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.left*1000, Line2.xAxis), Vector2.left*1000);

            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.zero, Line2.yAxis), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.up, Line2.yAxis), Vector2.up);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.down, Line2.yAxis), Vector2.down);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.up*1000, Line2.yAxis), Vector2.up*1000);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.down*1000, Line2.yAxis), Vector2.down*1000);

            var diagonal = new Line2(Vector2.zero, Vector2.one.normalized);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.zero, diagonal), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.one, diagonal), Vector2.one);
            Assert.AreEqual(Geometry.ClosestPointOnLine(-Vector2.one, diagonal), -Vector2.one);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.one*1000, diagonal), Vector2.one*1000);
            Assert.AreEqual(Geometry.ClosestPointOnLine(-Vector2.one*1000, diagonal), -Vector2.one*1000);
        }

        [Test]
        public void ClosestPoint_PointNotOnLine()
        {
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.up, Line2.xAxis), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.down, Line2.xAxis), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.up*1000, Line2.xAxis), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnLine(Vector2.down*1000, Line2.xAxis), Vector2.zero);
        }

        [Test]
        public void ClosestPoint_RandomPointOnLine()
        {
            for (int i = 0; i < testCycles; i++)
            {
                Vector2 origin = GetRandomOrigin2();
                Vector2 direction = GetRandomDirection2();
                Vector2 point = origin + direction*GetRandomOffset();
                var ray = new Ray2D(origin, direction);

                Vector2 closest = Geometry.ClosestPointOnLine(point, ray);
                float delta = (closest - point).magnitude;
                Assert.IsTrue(delta < Geometry.Epsilon,
                    "originA: " + ray.origin.ToString("F8") + " directionA: " + ray.direction.ToString("F8") +
                    "\npoint: " + point.ToString("F8") + " closest: " + closest.ToString("F8") +
                    "\ndelta: " + delta.ToString("F8"));
            }
        }

        [Test]
        public void Intersect_PointOnLine()
        {
            Assert.True(Geometry.IntersectPointLine(Vector2.zero, Line2.xAxis));
            Assert.True(Geometry.IntersectPointLine(Vector2.right, Line2.xAxis));
            Assert.True(Geometry.IntersectPointLine(Vector2.left, Line2.xAxis));
            Assert.True(Geometry.IntersectPointLine(Vector2.right*1000, Line2.xAxis));
            Assert.True(Geometry.IntersectPointLine(Vector2.left*1000, Line2.xAxis));

            Assert.True(Geometry.IntersectPointLine(Vector2.zero, Line2.yAxis));
            Assert.True(Geometry.IntersectPointLine(Vector2.up, Line2.yAxis));
            Assert.True(Geometry.IntersectPointLine(Vector2.down, Line2.yAxis));
            Assert.True(Geometry.IntersectPointLine(Vector2.up*1000, Line2.yAxis));
            Assert.True(Geometry.IntersectPointLine(Vector2.down*1000, Line2.yAxis));

            var diagonal = new Line2(Vector2.zero, Vector2.one.normalized);
            Assert.True(Geometry.IntersectPointLine(Vector2.zero, diagonal));
            Assert.True(Geometry.IntersectPointLine(Vector2.one, diagonal));
            Assert.True(Geometry.IntersectPointLine(-Vector2.one, diagonal));
            Assert.True(Geometry.IntersectPointLine(Vector2.one*1000, diagonal));
            Assert.True(Geometry.IntersectPointLine(-Vector2.one*1000, diagonal));
        }

        [Test]
        public void Intersect_PointNotOnLine()
        {
            int side;
            Assert.False(Geometry.IntersectPointLine(Vector2.up, Line2.xAxis, out side));
            Assert.AreEqual(side, -1);
            Assert.False(Geometry.IntersectPointLine(Vector2.down, Line2.xAxis, out side));
            Assert.AreEqual(side, 1);
            Assert.False(Geometry.IntersectPointLine(Vector2.up*1000, Line2.xAxis, out side));
            Assert.AreEqual(side, -1);
            Assert.False(Geometry.IntersectPointLine(Vector2.down*1000, Line2.xAxis, out side));
            Assert.AreEqual(side, 1);

            Assert.False(Geometry.IntersectPointLine(Vector2.left, Line2.yAxis, out side));
            Assert.AreEqual(side, -1);
            Assert.False(Geometry.IntersectPointLine(Vector2.right, Line2.yAxis, out side));
            Assert.AreEqual(side, 1);
            Assert.False(Geometry.IntersectPointLine(Vector2.left*1000, Line2.yAxis, out side));
            Assert.AreEqual(side, -1);
            Assert.False(Geometry.IntersectPointLine(Vector2.right*1000, Line2.yAxis, out side));
            Assert.AreEqual(side, 1);
        }
    }
}
