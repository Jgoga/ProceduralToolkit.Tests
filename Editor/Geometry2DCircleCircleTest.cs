using UnityEngine;
using NUnit.Framework;

namespace ProceduralToolkit.Tests
{
    public class Geometry2DCircleCircleTest
    {
        [Test]
        public void Intersect_Coincident()
        {
            IntersectionCircleCircle intersection;
            Assert.True(Geometry.IntersectCircleCircle(Circle.unit, Circle.unit, out intersection));
            Assert.True(Geometry.IntersectCircleCircle(new Circle(Vector2.one, 2), new Circle(Vector2.one, 2), out intersection));
        }

        [Test]
        public void Intersect_InsideCentered()
        {
            IntersectionCircleCircle intersection;
            Assert.True(Geometry.IntersectCircleCircle(new Circle(Vector2.zero, 2), Circle.unit, out intersection));
            Assert.True(Geometry.IntersectCircleCircle(Circle.unit, new Circle(Vector2.zero, 2), out intersection));
        }

        [Test]
        public void Intersect_InsideNonCentered()
        {
            IntersectionCircleCircle intersection;
            Assert.True(Geometry.IntersectCircleCircle(new Circle(Vector2.zero, 3), new Circle(Vector2.one, 1), out intersection));
            Assert.True(Geometry.IntersectCircleCircle(new Circle(Vector2.one, 1), new Circle(Vector2.zero, 3), out intersection));
        }

        [Test]
        public void Intersect_InsideOnePoint()
        {
            IntersectionCircleCircle intersection;
            Assert.True(Geometry.IntersectCircleCircle(new Circle(Vector2.zero, 2), new Circle(Vector2.right, 1), out intersection));
            Assert.True(intersection.pointA == Vector2.right*2);
            Assert.True(Geometry.IntersectCircleCircle(new Circle(Vector2.right, 1), new Circle(Vector2.zero, 2), out intersection));
            Assert.True(intersection.pointA == Vector2.right*2);
        }

        [Test]
        public void Intersect_InsideTwoPoints()
        {
            IntersectionCircleCircle intersection;
            Assert.True(Geometry.IntersectCircleCircle(new Circle(Vector2.zero, 1.5f), new Circle(Vector2.right, 1), out intersection));
            Assert.True(intersection.pointA == new Vector2(1.125f, -0.9921567f));
            Assert.True(intersection.pointB == new Vector2(1.125f, 0.9921567f));
            Assert.True(Geometry.IntersectCircleCircle(new Circle(Vector2.right, 1), new Circle(Vector2.zero, 1.5f), out intersection));
            Assert.True(intersection.pointA == new Vector2(1.125f, 0.9921567f));
            Assert.True(intersection.pointB == new Vector2(1.125f, -0.9921567f));
        }

        [Test]
        public void Intersect_OutsideTwoPoints()
        {
            IntersectionCircleCircle intersection;
            Assert.True(Geometry.IntersectCircleCircle(new Circle(Vector2.zero, 5), new Circle(Vector2.right*8, 5), out intersection));
            Assert.True(intersection.pointA == new Vector2(4, -3));
            Assert.True(intersection.pointB == new Vector2(4, 3));
            Assert.True(Geometry.IntersectCircleCircle(new Circle(Vector2.right*8, 5), new Circle(Vector2.zero, 5), out intersection));
            Assert.True(intersection.pointA == new Vector2(4, 3));
            Assert.True(intersection.pointB == new Vector2(4, -3));
        }

        [Test]
        public void Intersect_OutsideOnePoint()
        {
            IntersectionCircleCircle intersection;
            Assert.True(Geometry.IntersectCircleCircle(Circle.unit, new Circle(Vector2.right*2, 1), out intersection));
            Assert.True(intersection.pointA == Vector2.right);
            Assert.True(Geometry.IntersectCircleCircle(new Circle(Vector2.right*2, 1), Circle.unit, out intersection));
            Assert.True(intersection.pointA == Vector2.right);
        }

        [Test]
        public void Intersect_Separate()
        {
            IntersectionCircleCircle intersection;
            Assert.False(Geometry.IntersectCircleCircle(Circle.unit, new Circle(Vector2.one*2, 1), out intersection));
            Assert.False(Geometry.IntersectCircleCircle(new Circle(Vector2.one*2, 1), Circle.unit, out intersection));
        }
    }
}
