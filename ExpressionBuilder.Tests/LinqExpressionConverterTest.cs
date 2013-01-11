using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpressionBuilder.Expressions;
using ExpressionBuilder.Expressions.Converters;

namespace ExpressionBuilder.Tests
{
    [TestClass]
    public class LinqExpressionConverterTest
    {
        [TestMethod]
        public void TestComparisionEqual()
        {
            var checker = new ComparisionExpression("FirstName", "Ivan").ToPredicate<Customer>();

            Assert.IsTrue(checker(new Customer() { FirstName = "Ivan" }));
            Assert.IsFalse(checker(new Customer() { FirstName = "John" }));
        }

        [TestMethod]
        public void TestComparisionNotEqual()
        {
            var checker = new ComparisionExpression("FirstName", "Ivan") { Type = ComparisionType.NotEqual }.ToPredicate<Customer>();

            Assert.IsFalse(checker(new Customer() { FirstName = "Ivan" }));
            Assert.IsTrue(checker(new Customer() { FirstName = "John" }));
        }

        [TestMethod]
        public void TestComparisionLikeStartsWith()
        {
            var checker = new ComparisionExpression("FirstName", "I*") { Type = ComparisionType.Like }.ToPredicate<Customer>();

            Assert.IsTrue(checker(new Customer() { FirstName = "Ivan" }));
            Assert.IsTrue(checker(new Customer() { FirstName = "iVAN" }));
            Assert.IsFalse(checker(new Customer() { FirstName = "John" }));
        }

        [TestMethod]
        public void TestComparisionLikeEndsWith()
        {
            var checker = new ComparisionExpression("FirstName", "*n") { Type = ComparisionType.Like }.ToPredicate<Customer>();

            Assert.IsTrue(checker(new Customer() { FirstName = "Ivan" }));
            Assert.IsTrue(checker(new Customer() { FirstName = "John" }));
            Assert.IsFalse(checker(new Customer() { FirstName = "Kate" }));
        }

        [TestMethod]
        public void TestComparisionLikeStartsEndsWith()
        {
            var checker = new ComparisionExpression("FirstName", "I*n") { Type = ComparisionType.Like }.ToPredicate<Customer>();

            Assert.IsTrue(checker(new Customer() { FirstName = "Ivan" }));
            Assert.IsTrue(checker(new Customer() { FirstName = "iXXXXXXXXXXN" }));
            Assert.IsFalse(checker(new Customer() { FirstName = "John" }));
            Assert.IsFalse(checker(new Customer() { FirstName = "Kate" }));
        }

        [TestMethod]
        public void TestComparisionLikeContains()
        {
            var checker = new ComparisionExpression("FirstName", "*va*") { Type = ComparisionType.Like }.ToPredicate<Customer>();

            Assert.IsTrue(checker(new Customer() { FirstName = "Ivan" }));
            Assert.IsFalse(checker(new Customer() { FirstName = "iXXXXXXXXXXN" }));
            Assert.IsFalse(checker(new Customer() { FirstName = "John" }));
            Assert.IsFalse(checker(new Customer() { FirstName = "Kate" }));
            Assert.IsTrue(checker(new Customer() { FirstName = "jjhshsVAjjjhjhjh" }));
        }

        [TestMethod]
        public void TestComparisionNotLikeStartsWith()
        {
            var checker = new ComparisionExpression("FirstName", "I*") { Type = ComparisionType.NotLike }.ToPredicate<Customer>();

            Assert.IsFalse(checker(new Customer() { FirstName = "Ivan" }));
            Assert.IsFalse(checker(new Customer() { FirstName = "iVAN" }));
            Assert.IsTrue(checker(new Customer() { FirstName = "John" }));
        }

        [TestMethod]
        public void TestComparisionNotLikeEndsWith()
        {
            var checker = new ComparisionExpression("FirstName", "*n") { Type = ComparisionType.NotLike }.ToPredicate<Customer>();

            Assert.IsFalse(checker(new Customer() { FirstName = "Ivan" }));
            Assert.IsFalse(checker(new Customer() { FirstName = "John" }));
            Assert.IsTrue(checker(new Customer() { FirstName = "Kate" }));
        }

        [TestMethod]
        public void TestComparisionNotLikeStartsEndsWith()
        {
            var checker = new ComparisionExpression("FirstName", "I*n") { Type = ComparisionType.NotLike }.ToPredicate<Customer>();

            Assert.IsFalse(checker(new Customer() { FirstName = "Ivan" }));
            Assert.IsFalse(checker(new Customer() { FirstName = "iXXXXXXXXXXN" }));
            Assert.IsTrue(checker(new Customer() { FirstName = "John" }));
            Assert.IsTrue(checker(new Customer() { FirstName = "Kate" }));
        }

        [TestMethod]
        public void TestComparisionNotLikeContains()
        {
            var checker = new ComparisionExpression("FirstName", "*va*") { Type = ComparisionType.NotLike }.ToPredicate<Customer>();

            Assert.IsFalse(checker(new Customer() { FirstName = "Ivan" }));
            Assert.IsTrue(checker(new Customer() { FirstName = "iXXXXXXXXXXN" }));
            Assert.IsTrue(checker(new Customer() { FirstName = "John" }));
            Assert.IsTrue(checker(new Customer() { FirstName = "Kate" }));
            Assert.IsFalse(checker(new Customer() { FirstName = "jjhshsVAjjjhjhjh" }));
        }

        [TestMethod]
        public void TestComparisionLess()
        {
            var checker = new ComparisionExpression()
                {
                    Type = ComparisionType.Less,
                    Left = new LeftValueExpression() { Value = "Age" },
                    Right = new RightValueExpression() { Value = "20", TypeName = typeof(int).FullName }
                }.ToPredicate<Customer>();

            Assert.IsTrue(checker(new Customer() { Age = 10 }));
            Assert.IsTrue(checker(new Customer() { Age = 19 }));
            Assert.IsFalse(checker(new Customer() { Age = 20 }));
        }

        [TestMethod]
        public void TestComparisionLessOrEqual()
        {
            var checker = new ComparisionExpression()
            {
                Type = ComparisionType.LessOrEqual,
                Left = new LeftValueExpression() { Value = "Age" },
                Right = new RightValueExpression() { Value = "20", TypeName = typeof(int).FullName }
            }.ToPredicate<Customer>();

            Assert.IsTrue(checker(new Customer() { Age = 10 }));
            Assert.IsTrue(checker(new Customer() { Age = 19 }));
            Assert.IsTrue(checker(new Customer() { Age = 20 }));
            Assert.IsFalse(checker(new Customer() { Age = 21 }));
        }

        [TestMethod]
        public void TestComparisionGreater()
        {
            var checker = new ComparisionExpression()
            {
                Type = ComparisionType.Greater,
                Left = new LeftValueExpression() { Value = "Age" },
                Right = new RightValueExpression() { Value = "20", TypeName = typeof(int).FullName }
            }.ToPredicate<Customer>();

            Assert.IsTrue(checker(new Customer() { Age = 30 }));
            Assert.IsTrue(checker(new Customer() { Age = 21 }));
            Assert.IsFalse(checker(new Customer() { Age = 20 }));
        }

        [TestMethod]
        public void TestComparisionGreaterOrEqual()
        {
            var checker = new ComparisionExpression()
            {
                Type = ComparisionType.GreaterOrEqual,
                Left = new LeftValueExpression() { Value = "Age" },
                Right = new RightValueExpression() { Value = "20", TypeName = typeof(int).FullName }
            }.ToPredicate<Customer>();

            Assert.IsTrue(checker(new Customer() { Age = 30 }));
            Assert.IsTrue(checker(new Customer() { Age = 21 }));
            Assert.IsTrue(checker(new Customer() { Age = 20 }));
            Assert.IsFalse(checker(new Customer() { Age = 19 }));
        }
    }
}