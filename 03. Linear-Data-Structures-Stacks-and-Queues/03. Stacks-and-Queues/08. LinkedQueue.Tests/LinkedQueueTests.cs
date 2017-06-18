namespace _08.LinkedQueue.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using _07.LinkedQueue;

    [TestClass]
    public class LinkedQueueTests
    {
        private LinkedQueue<int> stack;

        [TestInitialize]
        public void Initialize()
        {
            this.stack = new LinkedQueue<int>();
        }

        [TestMethod]
        public void Enqueue_EmpyStack_ShouldWork()
        {
            Assert.AreEqual(0, this.stack.Count);
            this.stack.Enqueue(1);
            Assert.AreEqual(1, this.stack.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Dequeue_EmptyStack_ShouldThrow()
        {
            this.stack.Dequeue();
        }

        [TestMethod]
        public void Dequeue_NonEmptyStack_ShouldWork()
        {
            const int Item = 10;
            this.stack.Enqueue(Item);
            Assert.AreEqual(1, this.stack.Count);
            var poped = this.stack.Dequeue();
            Assert.AreEqual(Item, poped);
            Assert.AreEqual(0, this.stack.Count);
        }

        [TestMethod]
        public void Enqueue_1000Elements_ShouldWork()
        {
            Assert.AreEqual(0, this.stack.Count);
            for (int i = 0; i < 1000; i++)
            {
                this.stack.Enqueue(i);
                Assert.AreEqual(i + 1, this.stack.Count);
            }
        }

        [TestMethod]
        public void Dequeue_1000Elements_ShouldWork()
        {
            for (int i = 0; i < 1000; i++)
            {
                this.stack.Enqueue(i);
            }

            Assert.AreEqual(1000, this.stack.Count);
            for (int i = 999; i >= 0; i--)
            {
                this.stack.Dequeue();
                Assert.AreEqual(i, this.stack.Count);
            }
        }

        [TestMethod]
        public void ToArray_NonEmptyStack_ShouldWork()
        {
            this.stack.Enqueue(3);
            this.stack.Enqueue(5);
            this.stack.Enqueue(-2);
            this.stack.Enqueue(7);
            var arr = this.stack.ToArray();
            Assert.AreEqual(arr.Length, this.stack.Count);
            Assert.AreEqual("3, 5, -2, 7", string.Join(", ", arr));
        }

        [TestMethod]
        public void ToArray_EmptyStack_ShouldWork()
        {
            var arr = this.stack.ToArray();
            Assert.AreEqual(0, arr.Length);
        }
    }
}
