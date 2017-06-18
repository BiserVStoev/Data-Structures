namespace _06.LinkedStack.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using _05.LinkedStack;

    [TestClass]
    public class LinkedStackTests
    {
        private LinkedStack<int> stack;

        [TestInitialize]
        public void Initialize()
        {
            this.stack = new LinkedStack<int>();
        }

        [TestMethod]
        public void Push_EmptyStack_ShouldWork()
        {
            Assert.AreEqual(0, this.stack.Count);
            this.stack.Push(1);
            Assert.AreEqual(1, this.stack.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Pop_EmptyStack_ShouldThrow()
        {
            this.stack.Pop();
        }

        [TestMethod]
        public void Pop_NonEmpyStack_ShouldWork()
        {
            const int Item = 10;
            this.stack.Push(Item);
            Assert.AreEqual(1, this.stack.Count);
            var poped = this.stack.Pop();
            Assert.AreEqual(Item, poped);
            Assert.AreEqual(0, this.stack.Count);
        }

        [TestMethod]
        public void Push_1000Elements_ShouldWork()
        {
            Assert.AreEqual(0, this.stack.Count);
            for (int i = 0; i < 1000; i++)
            {
                this.stack.Push(i);
                Assert.AreEqual(i + 1, this.stack.Count);
            }
        }

        [TestMethod]
        public void Pop_1000Elements_ShouldWork()
        {
            for (int i = 0; i < 1000; i++)
            {
                this.stack.Push(i);
            }

            Assert.AreEqual(1000, this.stack.Count);
            for (int i = 999; i >= 0; i--)
            {
                this.stack.Pop();
                Assert.AreEqual(i, this.stack.Count);
            }
        }

        [TestMethod]
        public void ToArray_NonEmptyStack_ShouldWork()
        {
            this.stack.Push(3);
            this.stack.Push(5);
            this.stack.Push(-2);
            this.stack.Push(7);
            var arr = this.stack.ToArray();
            Assert.AreEqual(arr.Length, this.stack.Count);
            Assert.AreEqual("7, -2, 5, 3", string.Join(", ", arr));
        }

        [TestMethod]
        public void ToArray_EmptyStack_ShouldWork()
        {
            var arr = this.stack.ToArray();
            Assert.AreEqual(0, arr.Length);
        }
    }
}
