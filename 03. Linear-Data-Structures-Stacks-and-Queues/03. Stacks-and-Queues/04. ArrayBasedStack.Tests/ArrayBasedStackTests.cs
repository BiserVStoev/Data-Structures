namespace _04.ArrayBasedStack.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using _03.ImplementAnArrayBasedStack;

    [TestClass]
    public class ArrayBasedStackTests
    {
        [TestMethod]
        public void PushAndPop_ShouldWork()
        {
            var stack = new ArrayStack<int>();
            const int initialCount = 0;
            Assert.AreEqual(initialCount, stack.Count);

            int element = 5;
            stack.Push(element);
            const int countAfterOnePush = 1;
            Assert.AreEqual(countAfterOnePush, stack.Count);

            int poppedElement = stack.Pop();
            Assert.AreEqual(poppedElement, element);
            Assert.AreEqual(initialCount, stack.Count);
        }

        [TestMethod]
        public void PushPop_1000Elements_ShouldWork()
        {
            const int iterations = 1000;
            const int initialCount = 0;

            var stack = new ArrayStack<string>();
            Assert.AreEqual(initialCount, stack.Count);

            for (int i = 0; i < iterations; i++)
            {
                stack.Push(i.ToString());
                Assert.AreEqual(i + 1, stack.Count);
            }

            for (int i = iterations - 1; i >= 0; i--)
            {
                var currentElement = stack.Pop();
                Assert.AreEqual(i.ToString(), currentElement);
                Assert.AreEqual(i, stack.Count);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Pop_EmptyStack_ShouldThrow()
        {
            var stack = new ArrayStack<int>();

            stack.Pop();
        }

        [TestMethod]
        public void PushPop_InitialCapacity1_ShouldWork()
        {
            var stack = new ArrayStack<int>(1);
            var element = 2;

            const int initialCount = 0;
            Assert.AreEqual(initialCount, stack.Count);

            stack.Push(element);
            Assert.AreEqual(1, stack.Count);

            var secondElement = 3;
            stack.Push(secondElement);
            Assert.AreEqual(2, stack.Count);

            var poppedElement = stack.Pop();
            Assert.AreEqual(secondElement, poppedElement);
            Assert.AreEqual(1, stack.Count);

            var otherPoppedElement = stack.Pop();
            Assert.AreEqual(element, otherPoppedElement);
            Assert.AreEqual(initialCount, stack.Count);
        }

        [TestMethod]
        public void ToArray_ShouldReturnCorrectArray()
        {
            var array = new[] { 7, -2, 5, 3 };

            var stack = new ArrayStack<int>();
            for (int i = array.Length - 1; i >= 0; i--)
            {
                stack.Push(array[i]);
            }

            var arrayFromStack = stack.ToArray();
            CollectionAssert.AreEqual(array, arrayFromStack);
        }

        [TestMethod]
        public void EmptyStack_ToArray_ShouldWork()
        {
            var arr = new ArrayStack<DateTime>().ToArray();
            Assert.AreEqual(0, arr.Length);
        }
    }
}
