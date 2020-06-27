using Kpax.Core.Collections;
using FluentAssertions;
using Xunit;
using System;

namespace Kpax.Core.UnitTests.Collections
{
    public class TwoWayDictionaryTests
    {
        public class Add
        {
            [Fact]
            public void Add_AddsElementToBothDictionaries_WhenCalled()
            {
                var dictionary = new TwoWayDictionary<int, string>();

                dictionary.Add(1, "one");

                dictionary.Count.Should().Be(1);
                dictionary[1].Should().Be("one");
                dictionary["one"].Should().Be(1);
            }

            [Fact]
            public void Add_ThrowsException_WhenKeyIsNull()
            {
                var dictionary = new TwoWayDictionary<string, int>();

                Action add = () => dictionary.Add(null, 1);

                add.Should().Throw<ArgumentNullException>();
                dictionary.Count.Should().Be(0);
            }

            [Fact]
            public void Add_ThrowsException_WhenValueIsNull()
            {
                var dictionary = new TwoWayDictionary<int, string>();

                Action add = () => dictionary.Add(1, null);

                add.Should().Throw<ArgumentNullException>();
                dictionary.Count.Should().Be(0);
            }

            [Fact]
            public void Add_ThrowsException_WhenKeyAlreadyPresent()
            {
                var dictionary = new TwoWayDictionary<string, int>();
                dictionary.Add("a", 1);

                Action add = () => dictionary.Add("a", 2);

                add.Should().Throw<ArgumentException>();
                dictionary.Count.Should().Be(1);
                dictionary["a"].Should().Be(1);
                dictionary[1].Should().Be("a");
            }

            [Fact]
            public void Add_ThrowsException_WhenValueAlreadyPresent()
            {
                var dictionary = new TwoWayDictionary<string, int>();
                dictionary.Add("a", 1);

                Action add = () => dictionary.Add("b", 1);

                add.Should().Throw<ArgumentException>();
                dictionary.Count.Should().Be(1);
                dictionary["a"].Should().Be(1);
                dictionary[1].Should().Be("a");
            }
        }

        public class Remove
        {
            [Fact]
            public void Remove_ThrowsException_WhenKeyIsNull()
            {
                var dictionary = new TwoWayDictionary<string, int>();

                Action add = () => dictionary.Remove(null);

                add.Should().Throw<ArgumentNullException>();
                dictionary.Count.Should().Be(0);
            }

            [Fact]
            public void Remove_ThrowsException_WhenKeyIsAbsent()
            {
                var dictionary = new TwoWayDictionary<string, int>();
                dictionary.Add("one", 1);

                bool result = dictionary.Remove("two");

                result.Should().BeFalse();
                dictionary.Count.Should().Be(1);
            }

            [Fact]
            public void Remove_RemovesElementFromBothDictionaries_WhenCalled()
            {
                var dictionary = new TwoWayDictionary<string, int>();
                dictionary.Add("one", 1);
                dictionary.Add("two", 2);

                bool result = dictionary.Remove("two");

                result.Should().BeTrue();
                dictionary["one"].Should().Be(1);
                dictionary[1].Should().Be("one");
                dictionary.Count.Should().Be(1);
            }
        }
    }
}