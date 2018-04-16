using NUnit.Framework;
using UnityEngine;

namespace ProceduralToolkit.Tests
{
    public class VectorETest
    {
        [Test]
        public void SignedAngle()
        {
            Assert.AreEqual(VectorE.SignedAngle(Vector2.up, Vector2.up), 0);
            Assert.AreEqual(VectorE.SignedAngle(Vector2.up, Vector2.right), 90);
            Assert.AreEqual(VectorE.SignedAngle(Vector2.up, Vector2.down), 180);
            Assert.AreEqual(VectorE.SignedAngle(Vector2.up, Vector2.left), -90);
        }
    }
}
