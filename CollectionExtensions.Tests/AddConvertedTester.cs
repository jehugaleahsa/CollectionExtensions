using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CollectionExtensions;
using System.Collections.Generic;

namespace CollectionExtensions.Test
{
    /// <summary>
    /// Tests the AddConverted methods.
    /// </summary>
    [TestClass]
    public class AddConvertedTester
    {
        #region Real World Example

        /// <summary>
        /// We can convert a group of numeric-strings.
        /// </summary>
        [TestMethod]
        public void TestAddConverted_BetweenStringAndDouble()
        {
            Random random = new Random();

            // build a list of numbers
            var numbers = new List<double>(100);
            Sublist.Grow(numbers, 100, random.NextDouble() * 100);

            // convert to a list of strings
            var strings = new List<string>(100);
            Sublist.AddConverted(numbers.ToSublist(), strings.ToSublist());

            // convert back to a list of doubles
            var converted = new List<double>(100);
            Sublist.AddConverted(strings.ToSublist(), converted.ToSublist());

            // check that the numbers are mostly the same
            // numbers will change a little due to precision issues
            bool result = Sublist.AreEqual(numbers.ToSublist(), converted.ToSublist(), (n, c) => Math.Abs(n - c) < .0001);
            Assert.IsTrue(result, "Could not convert between strings and numbers.");
        }

        #endregion

        #region Argument Checking

        /// <summary>
        /// An exception should be thrown if the source list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddConverted_DefaultConverter_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Sublist.AddConverted(list, destination);
        }

        /// <summary>
        /// An exception should be thrown if the source list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddConverted_NullList_Throws()
        {
            Sublist<List<int>, int> list = null;
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int> converter = i => i;
            Sublist.AddConverted(list, destination, converter);
        }

        /// <summary>
        /// An exception should be thrown if the destination list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddConverted_DefaultConverter_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Sublist.AddConverted(list, destination);
        }

        /// <summary>
        /// An exception should be thrown if the destination list is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddConverted_NullDestination_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = null;
            Func<int, int> converter = i => i;
            Sublist.AddConverted(list, destination, converter);
        }

        /// <summary>
        /// An exception should be thrown if the conversion delegate is null.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddConverted_NullConverter_Throws()
        {
            Sublist<List<int>, int> list = new List<int>();
            Sublist<List<int>, int> destination = new List<int>();
            Func<int, int> converter = null;
            Sublist.AddConverted(list, destination, converter);
        }

        #endregion

        /// <summary>
        /// We will make sure we can use convert to double a list of numbers.
        /// </summary>
        [TestMethod]
        public void TestAddConverted_DoubleValues()
        {
            var list = TestHelper.Wrap(new List<int>() { 1, 2, 3 });
            var destination = TestHelper.Wrap(new List<int>());
            Sublist.AddConverted(list, destination, i => i * 2);
            int[] expected = { 2, 4, 6, };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "Not all of the items were added as expected.");
            TestHelper.CheckHeaderAndFooter(list);
            TestHelper.CheckHeaderAndFooter(destination);
        }

        /// <summary>
        /// We should be able to convert from one nullable type to another.
        /// </summary>
        [TestMethod]
        public void TestAddConverted_NullableToNullable()
        {
            var list = new List<int?>() { 1, 2, 3 }.ToSublist();
            var destination = new List<long?>().ToSublist();
            Sublist.AddConverted(list, destination);
            long?[] expected = { 1L, 2L, 3L };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not converted correctly.");
        }

        /// <summary>
        /// An InvalidCastException should be thrown if we try to convert null to a primitive type.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCastException))]
        public void TestAddConverted_NullToPrimitive_Throws()
        {
            var list = new List<int?>() { 1, 2, null }.ToSublist();
            var destination = new List<int>().ToSublist();
            Sublist.AddConverted(list, destination);
        }

        /// <summary>
        /// It is fine to convert a null to a nullable.
        /// </summary>
        [TestMethod]
        public void TestAddConverted_NullToNullable()
        {
            var list = new List<int?>() { 1, 2, null }.ToSublist();
            var destination = new List<long?>().ToSublist();
            Sublist.AddConverted(list, destination);
            long?[] expected = { 1L, 2L, null };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not converted correctly.");
        }

        /// <summary>
        /// It is fine to objects to user-defined types.
        /// </summary>
        [TestMethod]
        public void TestAddConverted_ObjectToUserDefinedType()
        {
            var first = new UserDefined();
            var second = new UserDefined();
            var third = new UserDefined();
            var source = new List<object>() { first, second, third }.ToSublist();
            var destination = new List<UserDefined>().ToSublist();
            Sublist.AddConverted(source, destination);
            UserDefined[] expected = new UserDefined[] { first, second, third };
            Assert.IsTrue(Sublist.AreEqual(expected.ToSublist(), destination), "The items were not converted correctly.");
        }

        private class UserDefined
        {
        }
    }
}
