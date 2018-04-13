using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry2DPointCircleTest
    {
        [Test]
        public void Distance_Coincident()
        {
            Assert.AreEqual(Geometry.DistanceToCircle(Vector2.zero, Circle.unit), -1);
            Assert.AreEqual(Geometry.DistanceToCircle(Vector2.one, new Circle(Vector2.one, 2)), -2);
        }

        [Test]
        public void Distance_OnCircle()
        {
            Assert.AreEqual(Geometry.DistanceToCircle(Vector2.zero, new Circle(Vector2.right, 1)), 0);
            Assert.AreEqual(Geometry.DistanceToCircle(Vector2.right, new Circle(Vector2.zero, 1)), 0);
        }

        [Test]
        public void Distance_Separate()
        {
            Assert.AreEqual(Geometry.DistanceToCircle(Vector2.zero, new Circle(Vector2.right*2, 1)), 1);
            Assert.AreEqual(Geometry.DistanceToCircle(Vector2.right*2, new Circle(Vector2.zero, 1)), 1);
        }

        [Test]
        public void ClosestPoint_Coincident()
        {
            Assert.AreEqual(Geometry.ClosestPointOnCircle(Vector2.zero, Circle.unit), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnCircle(Vector2.one, new Circle(Vector2.one, 2)), Vector2.one);
        }

        [Test]
        public void ClosestPoint_OnCircle()
        {
            Assert.AreEqual(Geometry.ClosestPointOnCircle(Vector2.zero, new Circle(Vector2.right, 1)), Vector2.zero);
            Assert.AreEqual(Geometry.ClosestPointOnCircle(Vector2.right, new Circle(Vector2.zero, 1)), Vector2.right);
        }

        [Test]
        public void ClosestPoint_Separate()
        {
            Assert.AreEqual(Geometry.ClosestPointOnCircle(Vector2.zero, new Circle(Vector2.right*2, 1)), Vector2.right);
            Assert.AreEqual(Geometry.ClosestPointOnCircle(Vector2.right*2, new Circle(Vector2.zero, 1)), Vector2.right);
        }

        [Test]
        public void Intersect_Coincident()
        {
            Assert.True(Geometry.IntersectPointCircle(Vector2.zero, Circle.unit));
            Assert.True(Geometry.IntersectPointCircle(Vector2.one, new Circle(Vector2.one, 2)));
        }

        [Test]
        public void Intersect_OffCenter()
        {
            Assert.True(Geometry.IntersectPointCircle(Vector2.zero, new Circle(Vector2.right, 2)));
            Assert.True(Geometry.IntersectPointCircle(Vector2.right, new Circle(Vector2.zero, 2)));
        }

        [Test]
        public void Intersect_OnCircle()
        {
            Assert.True(Geometry.IntersectPointCircle(Vector2.zero, new Circle(Vector2.right, 1)));
            Assert.True(Geometry.IntersectPointCircle(Vector2.right, Circle.unit));
        }

        [Test]
        public void Intersect_Separate()
        {
            Assert.False(Geometry.IntersectPointCircle(Vector2.zero, new Circle(Vector2.one*2, 1)));
            Assert.False(Geometry.IntersectPointCircle(Vector2.one*2, Circle.unit));
        }
    }
}
