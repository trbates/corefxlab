﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xunit;

namespace System.Numerics.Experimental.Tests
{
    public partial class HalfTests
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe ushort HalfToUInt16Bits(Half value)
        {
            return *((ushort*)&value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe Half UInt16BitsToHalf(ushort value)
        {
            return *((Half*)&value);
        }

        [Fact]
        public static void Epsilon()
        {
            Assert.Equal(0x0001u, HalfToUInt16Bits(Half.Epsilon));
        }

        [Fact]
        public static void PositiveInfinity()
        {
            Assert.Equal(0x7C00u, HalfToUInt16Bits(Half.PositiveInfinity));
        }

        [Fact]
        public static void NegativeInfinity()
        {
            Assert.Equal(0xFC00u, HalfToUInt16Bits(Half.NegativeInfinity));
        }

        [Fact]
        public static void NaN()
        {
            Assert.Equal(0xFE00u, HalfToUInt16Bits(Half.NaN));
        }

        [Fact]
        public static void MinValue()
        {
            Assert.Equal(0xFBFFu, HalfToUInt16Bits(Half.MinValue));
        }

        [Fact]
        public static void MaxValue()
        {
            Assert.Equal(0x7BFFu, HalfToUInt16Bits(Half.MaxValue));
        }

        [Fact]
        public static void Ctor_Empty()
        {
            var value = new Half();
            Assert.Equal(0x0000, HalfToUInt16Bits(value));
        }

        public static IEnumerable<object[]> IsFinite_TestData()
        {
            yield return new object[] { Half.NegativeInfinity, false };     // Negative Infinity
            yield return new object[] { Half.MinValue, true };              // Min Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x8400), true };   // Max Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x83FF), true };   // Min Negative Subnormal
            yield return new object[] { UInt16BitsToHalf(0x8001), true };   // Max Negative Subnormal
            yield return new object[] { UInt16BitsToHalf(0x8000), true };   // Negative Zero
            yield return new object[] { Half.NaN, false };                  // NaN
            yield return new object[] { UInt16BitsToHalf(0x0000), true };   // Positive Zero
            yield return new object[] { Half.Epsilon, true };               // Min Positive Subnormal
            yield return new object[] { UInt16BitsToHalf(0x03FF), true };   // Max Positive Subnormal
            yield return new object[] { UInt16BitsToHalf(0x0400), true };   // Min Positive Normal
            yield return new object[] { Half.MaxValue, true };              // Max Positive Normal
            yield return new object[] { Half.PositiveInfinity, false };     // Positive Infinity
        }

        [Theory]
        [MemberData(nameof(IsFinite_TestData))]
        public static void IsFinite(Half value, bool expected)
        {
            Assert.Equal(expected, Half.IsFinite(value));
        }

        public static IEnumerable<object[]> IsInfinity_TestData()
        {
            yield return new object[] { Half.NegativeInfinity, true };      // Negative Infinity
            yield return new object[] { Half.MinValue, false };             // Min Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x8400), false };  // Max Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x83FF), false };  // Min Negative Subnormal
            yield return new object[] { UInt16BitsToHalf(0x8001), false };  // Max Negative Subnormal (Negative Epsilon)
            yield return new object[] { UInt16BitsToHalf(0x8000), false };  // Negative Zero
            yield return new object[] { Half.NaN, false };                  // NaN
            yield return new object[] { UInt16BitsToHalf(0x0000), false };  // Positive Zero
            yield return new object[] { Half.Epsilon, false };              // Min Positive Subnormal (Positive Epsilon)
            yield return new object[] { UInt16BitsToHalf(0x03FF), false };  // Max Positive Subnormal
            yield return new object[] { UInt16BitsToHalf(0x0400), false };  // Min Positive Normal
            yield return new object[] { Half.MaxValue, false };             // Max Positive Normal
            yield return new object[] { Half.PositiveInfinity, true };      // Positive Infinity
        }

        [Theory]
        [MemberData(nameof(IsInfinity_TestData))]
        public static void IsInfinity(Half value, bool expected)
        {
            Assert.Equal(expected, Half.IsInfinity(value));
        }

        public static IEnumerable<object[]> IsNaN_TestData()
        {
            yield return new object[] { Half.NegativeInfinity, false };     // Negative Infinity
            yield return new object[] { Half.MinValue, false };             // Min Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x8400), false };  // Max Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x83FF), false };  // Min Negative Subnormal
            yield return new object[] { UInt16BitsToHalf(0x8001), false };  // Max Negative Subnormal (Negative Epsilon)
            yield return new object[] { UInt16BitsToHalf(0x8000), false };  // Negative Zero
            yield return new object[] { Half.NaN, true };                   // NaN
            yield return new object[] { UInt16BitsToHalf(0x0000), false };  // Positive Zero
            yield return new object[] { Half.Epsilon, false };              // Min Positive Subnormal (Positive Epsilon)
            yield return new object[] { UInt16BitsToHalf(0x03FF), false };  // Max Positive Subnormal
            yield return new object[] { UInt16BitsToHalf(0x0400), false };  // Min Positive Normal
            yield return new object[] { Half.MaxValue, false };             // Max Positive Normal
            yield return new object[] { Half.PositiveInfinity, false };     // Positive Infinity
        }

        [Theory]
        [MemberData(nameof(IsNaN_TestData))]
        public static void IsNaN(Half value, bool expected)
        {
            Assert.Equal(expected, Half.IsNaN(value));
        }

        public static IEnumerable<object[]> IsNegative_TestData()
        {
            yield return new object[] { Half.NegativeInfinity, true };      // Negative Infinity
            yield return new object[] { Half.MinValue, true };              // Min Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x8400), true };   // Max Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x83FF), true };   // Min Negative Subnormal
            yield return new object[] { UInt16BitsToHalf(0x8001), true };   // Max Negative Subnormal
            yield return new object[] { UInt16BitsToHalf(0x8000), true };   // Negative Zero
            yield return new object[] { Half.NaN, true };                   // NaN
            yield return new object[] { UInt16BitsToHalf(0x0000), false };  // Positive Zero
            yield return new object[] { Half.Epsilon, false };              // Min Positive Subnormal
            yield return new object[] { UInt16BitsToHalf(0x03FF), false };  // Max Positive Subnormal
            yield return new object[] { UInt16BitsToHalf(0x0400), false };  // Min Positive Normal
            yield return new object[] { Half.MaxValue, false };             // Max Positive Normal
            yield return new object[] { Half.PositiveInfinity, false };     // Positive Infinity
        }

        [Theory]
        [MemberData(nameof(IsNegative_TestData))]
        public static void IsNegative(Half value, bool expected)
        {
            Assert.Equal(expected, Half.IsNegative(value));
        }

        public static IEnumerable<object[]> IsNegativeInfinity_TestData()
        {
            yield return new object[] { Half.NegativeInfinity, true };      // Negative Infinity
            yield return new object[] { Half.MinValue, false };             // Min Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x8400), false };  // Max Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x83FF), false };  // Min Negative Subnormal
            yield return new object[] { UInt16BitsToHalf(0x8001), false };  // Max Negative Subnormal (Negative Epsilon)
            yield return new object[] { UInt16BitsToHalf(0x8000), false };  // Negative Zero
            yield return new object[] { Half.NaN, false };                  // NaN
            yield return new object[] { UInt16BitsToHalf(0x0000), false };  // Positive Zero
            yield return new object[] { Half.Epsilon, false };              // Min Positive Subnormal (Positive Epsilon)
            yield return new object[] { UInt16BitsToHalf(0x03FF), false };  // Max Positive Subnormal
            yield return new object[] { UInt16BitsToHalf(0x0400), false };  // Min Positive Normal
            yield return new object[] { Half.MaxValue, false };             // Max Positive Normal
            yield return new object[] { Half.PositiveInfinity, false };     // Positive Infinity
        }

        [Theory]
        [MemberData(nameof(IsNegativeInfinity_TestData))]
        public static void IsNegativeInfinity(Half value, bool expected)
        {
            Assert.Equal(expected, Half.IsNegativeInfinity(value));
        }

        public static IEnumerable<object[]> IsNormal_TestData()
        {
            yield return new object[] { Half.NegativeInfinity, false };     // Negative Infinity
            yield return new object[] { Half.MinValue, true };              // Min Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x8400), true };   // Max Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x83FF), false };  // Min Negative Subnormal
            yield return new object[] { UInt16BitsToHalf(0x8001), false };  // Max Negative Subnormal
            yield return new object[] { UInt16BitsToHalf(0x8000), false };  // Negative Zero
            yield return new object[] { Half.NaN, false };                  // NaN
            yield return new object[] { UInt16BitsToHalf(0x0000), false };  // Positive Zero
            yield return new object[] { Half.Epsilon, false };              // Min Positive Subnormal
            yield return new object[] { UInt16BitsToHalf(0x03FF), false };  // Max Positive Subnormal
            yield return new object[] { UInt16BitsToHalf(0x0400), true };   // Min Positive Normal
            yield return new object[] { Half.MaxValue, true };              // Max Positive Normal
            yield return new object[] { Half.PositiveInfinity, false };     // Positive Infinity
        }

        [Theory]
        [MemberData(nameof(IsNormal_TestData))]
        public static void IsNormal(Half value, bool expected)
        {
            Assert.Equal(expected, Half.IsNormal(value));
        }

        public static IEnumerable<object[]> IsPositiveInfinity_TestData()
        {
            yield return new object[] { Half.NegativeInfinity, false };     // Negative Infinity
            yield return new object[] { Half.MinValue, false };             // Min Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x8400), false };  // Max Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x83FF), false };  // Min Negative Subnormal
            yield return new object[] { UInt16BitsToHalf(0x8001), false };  // Max Negative Subnormal (Negative Epsilon)
            yield return new object[] { UInt16BitsToHalf(0x8000), false };  // Negative Zero
            yield return new object[] { Half.NaN, false };                  // NaN
            yield return new object[] { UInt16BitsToHalf(0x0000), false };  // Positive Zero
            yield return new object[] { Half.Epsilon, false };              // Min Positive Subnormal (Positive Epsilon)
            yield return new object[] { UInt16BitsToHalf(0x03FF), false };  // Max Positive Subnormal
            yield return new object[] { UInt16BitsToHalf(0x0400), false };  // Min Positive Normal
            yield return new object[] { Half.MaxValue, false };             // Max Positive Normal
            yield return new object[] { Half.PositiveInfinity, true };      // Positive Infinity
        }

        [Theory]
        [MemberData(nameof(IsPositiveInfinity_TestData))]
        public static void IsPositiveInfinity(Half value, bool expected)
        {
            Assert.Equal(expected, Half.IsPositiveInfinity(value));
        }

        public static IEnumerable<object[]> IsSubnormal_TestData()
        {
            yield return new object[] { Half.NegativeInfinity, false };     // Negative Infinity
            yield return new object[] { Half.MinValue, false };             // Min Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x8400), false };  // Max Negative Normal
            yield return new object[] { UInt16BitsToHalf(0x83FF), true };   // Min Negative Subnormal
            yield return new object[] { UInt16BitsToHalf(0x8001), true };   // Max Negative Subnormal
            yield return new object[] { UInt16BitsToHalf(0x8000), false };  // Negative Zero
            yield return new object[] { Half.NaN, false };                  // NaN
            yield return new object[] { UInt16BitsToHalf(0x0000), false };  // Positive Zero
            yield return new object[] { Half.Epsilon, true };               // Min Positive Subnormal
            yield return new object[] { UInt16BitsToHalf(0x03FF), true };   // Max Positive Subnormal
            yield return new object[] { UInt16BitsToHalf(0x0400), false };  // Min Positive Normal
            yield return new object[] { Half.MaxValue, false };             // Max Positive Normal
            yield return new object[] { Half.PositiveInfinity, false };     // Positive Infinity
        }

        [Theory]
        [MemberData(nameof(IsSubnormal_TestData))]
        public static void IsSubnormal(Half value, bool expected)
        {
            Assert.Equal(expected, Half.IsSubnormal(value));
        }

        public static IEnumerable<object[]> CompareTo_ThrowsArgumentException_TestData()
        {
            yield return new object[] { "a" };
            yield return new object[] { 234.0 };
        }

        [Theory]
        [MemberData(nameof(CompareTo_ThrowsArgumentException_TestData))]
        public static void CompareTo_ThrowsArgumentException(object obj)
        {
            Assert.Throws(typeof(ArgumentException), () => Half.MaxValue.CompareTo(obj));
        }

        public static IEnumerable<object[]> CompareTo_TestData()
        {
            yield return new object[] { Half.MaxValue, Half.MaxValue, 0 };
            yield return new object[] { Half.MaxValue, Half.MinValue, 1 };
            yield return new object[] { Half.Epsilon, UInt16BitsToHalf(0x8001), 1 };
            yield return new object[] { Half.MaxValue, UInt16BitsToHalf(0x0000), 1 };
            yield return new object[] { Half.MaxValue, Half.Epsilon, 1 };
            yield return new object[] { Half.MaxValue, Half.PositiveInfinity, -1 };
            yield return new object[] { Half.MinValue, Half.MaxValue, -1 };
            yield return new object[] { Half.MaxValue, Half.NaN, 1 };
            yield return new object[] { Half.NaN, Half.NaN, 0 };
            yield return new object[] { Half.NaN, UInt16BitsToHalf(0x0000), -1 };
            yield return new object[] { Half.MaxValue, null, 1 };
        }

        [Theory]
        [MemberData(nameof(CompareTo_TestData))]
        public static void CompareTo(Half value, object obj, int expected)
        {
            if (obj is Half other)
            {
                Assert.Equal(expected, Math.Sign(value.CompareTo(other)));

                if (Half.IsNaN(value) || Half.IsNaN(other))
                {
                    Assert.False(value >= other);
                    Assert.False(value > other);
                    Assert.False(value <= other);
                    Assert.False(value < other);
                }
                else
                {
                    if (expected >= 0)
                    {
                        Assert.True(value >= other);
                        Assert.False(value < other);
                    }
                    if (expected > 0)
                    {
                        Assert.True(value > other);
                        Assert.False(value <= other);
                    }
                    if (expected <= 0)
                    {
                        Assert.True(value <= other);
                        Assert.False(value > other);
                    }
                    if (expected < 0)
                    {
                        Assert.True(value < other);
                        Assert.False(value >= other);
                    }
                }
            }

            Assert.Equal(expected, Math.Sign(value.CompareTo(obj)));
        }

        public static IEnumerable<object[]> Equals_TestData()
        {
            yield return new object[] { Half.MaxValue, Half.MaxValue, true };
            yield return new object[] { Half.MaxValue, Half.MinValue, false };
            yield return new object[] { Half.MaxValue, UInt16BitsToHalf(0x0000), false };
            yield return new object[] { Half.NaN, Half.NaN, true };
            yield return new object[] { Half.MaxValue, 789.0f, false };
            yield return new object[] { Half.MaxValue, "789", false };
        }

        [Theory]
        [MemberData(nameof(Equals_TestData))]
        public static void Equals(Half value, object obj, bool expected)
        {
            if (obj is Half other)
            {
                Assert.Equal(expected, value.Equals(other));

                if (Half.IsNaN(value) && Half.IsNaN(other))
                {
                    Assert.Equal(!expected, value == other);
                    Assert.Equal(expected, value != other);
                }
                else
                {
                    Assert.Equal(expected, value == other);
                    Assert.Equal(!expected, value != other);
                }
                Assert.Equal(expected, value.GetHashCode().Equals(other.GetHashCode()));
            }
            Assert.Equal(expected, value.Equals(obj));
        }

        // ---------- Start of To-half conversion tests ----------
        
        public static IEnumerable<object[]> ImplicitConversion_FromInt64_TestData()
        {
            (long Original, Half Expected)[] data =
            {
                (65504L, Half.MaxValue), // Half.MaxValue
                (-65504L, Half.MinValue), // Half.MinValue
                (1L, UInt16BitsToHalf(0b0_01111_0000000000)), // 1
                (-1L, UInt16BitsToHalf(0b1_01111_0000000000)), // -1
                (0L, UInt16BitsToHalf(0)), // 0
                (long.MaxValue, Half.PositiveInfinity), // Overflow
                (long.MinValue, Half.NegativeInfinity), // OverFlow
                (65504L + 16, Half.PositiveInfinity), // Overflow
                (-(65504L + 16), Half.NegativeInfinity), // Overflow
                (65504L + 16 - 1, Half.MaxValue), // MaxValue
                (-(65504L + 16 - 1), Half.MinValue), // MinValue
                (65504L + 16 + 1, Half.PositiveInfinity), // MaxValue + half the increment unit + 1, should overflow
                (-(65504L + 16 + 1), Half.NegativeInfinity) // MinValue - half the increment unit - 1, should overflow
            };

            foreach ((long original, Half expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ImplicitConversion_FromInt64_TestData))]
        [Theory]
        public static void ImplicitConversion_FromInt64(long l, Half expected)
        {
            Half h = l;
            Assert.Equal(expected, h);
        }

        public static IEnumerable<object[]> ImplicitConversion_FromUInt64_TestData()
        {
            (ulong Original, Half Expected)[] data =
                unchecked
                (
                    new (ulong, Half)[]
                    {
                        (65504UL, Half.MaxValue), // Half.MaxValue
                        ((ulong)-65504L, Half.PositiveInfinity), // Overflow
                        (1UL, UInt16BitsToHalf(0b0_01111_0000000000)), // 1
                        ((ulong)-1L, Half.PositiveInfinity), // Overflow
                        (0UL, UInt16BitsToHalf(0)), // 0
                        (long.MaxValue, Half.PositiveInfinity), // Overflow
                        ((ulong)long.MinValue, Half.PositiveInfinity), // OverFlow
                        (65504UL + 16, Half.PositiveInfinity), // Overflow
                        ((ulong)-(65504L + 16), Half.PositiveInfinity), // Overflow
                        (65504UL + 16 - 1, Half.MaxValue), // MaxValue
                        ((ulong)-(65504L + 16 - 1), Half.PositiveInfinity), // OverFlow
                        (65504UL + 16 + 1, Half.PositiveInfinity), // MaxValue + half the increment unit + 1, should overflow
                        ((ulong)-(65504L + 16 + 1), Half.PositiveInfinity), // Overflow
                    }
                );

            foreach ((ulong original, Half expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ImplicitConversion_FromUInt64_TestData))]
        [Theory]
        public static void ImplicitConversion_FromUInt64(ulong l, Half expected)
        {
            Half h = l;
            Assert.Equal(expected, h);
        }

        public static IEnumerable<object[]> ImplicitConversion_FromInt32_TestData()
        {
            (int Original, Half Expected)[] data =
            {
                (65504, Half.MaxValue), // Half.MaxValue
                (-65504, Half.MinValue), // Half.MinValue
                (1, UInt16BitsToHalf(0b0_01111_0000000000)), // 1
                (-1, UInt16BitsToHalf(0b1_01111_0000000000)), // -1
                (0, UInt16BitsToHalf(0)), // 0
                (int.MaxValue, Half.PositiveInfinity), // Overflow
                (int.MinValue, Half.NegativeInfinity), // OverFlow
                (65504 + 16, Half.PositiveInfinity), // Overflow
                (-(65504 + 16), Half.NegativeInfinity), // Overflow
                (65504 + 16 - 1, Half.MaxValue), // MaxValue
                (-(65504 + 16 - 1), Half.MinValue), // MinValue
                (65504 + 16 + 1, Half.PositiveInfinity), // MaxValue + half the increment unit + 1, should overflow
                (-(65504 + 16 + 1), Half.NegativeInfinity) // MinValue - half the increment unit - 1, should overflow
            };

            foreach ((int original, Half expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ImplicitConversion_FromInt32_TestData))]
        [Theory]
        public static void ImplicitConversion_FromInt32(int i, Half expected)
        {
            Half h = i;
            Assert.Equal(expected, h);
        }

        public static IEnumerable<object[]> ImplicitConversion_FromUInt32_TestData()
        {
            (uint Original, Half Expected)[] data =
                unchecked
                (
                    new []
                    {
                        (65504U, Half.MaxValue), // Half.MaxValue
                        ((uint)-65504, Half.PositiveInfinity), // Overflow
                        (1U, UInt16BitsToHalf(0b0_01111_0000000000)), // 1
                        ((uint)-1, Half.PositiveInfinity), // Overflow 
                        (0U, UInt16BitsToHalf(0)), // 0
                        ((uint)int.MaxValue, Half.PositiveInfinity), // Overflow
                        ((uint)int.MinValue, Half.PositiveInfinity), // OverFlow
                        (uint.MaxValue, Half.PositiveInfinity),
                        (65504U + 16, Half.PositiveInfinity), // Overflow
                        ((uint)-(65504 + 16), Half.PositiveInfinity), // Overflow
                        (65504U + 16 - 1, Half.MaxValue), // MaxValue
                        ((uint)-(65504 + 16 - 1), Half.PositiveInfinity), // Overflow
                        (65504U + 16 + 1, Half.PositiveInfinity), // Overflow
                        ((uint)-(65504 + 16 + 1), Half.PositiveInfinity) // Overflow
                    }
                );

            foreach ((uint original, Half expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ImplicitConversion_FromUInt32_TestData))]
        [Theory]
        public static void ImplicitConversion_FromUInt32(uint i, Half expected)
        {
            Half h = i;
            Assert.Equal(expected, h);
        }

        public static IEnumerable<object[]> ImplicitConversion_FromInt16_TestData()
        {
            (short Original, Half Expected)[] data =
            {
                (1, UInt16BitsToHalf(0b0_01111_0000000000)), // 1
                (-1, UInt16BitsToHalf(0b1_01111_0000000000)), // -1 
                (0, UInt16BitsToHalf(0)), // 0
                (short.MaxValue, UInt16BitsToHalf(0b0_11110_0000000000)), // rounds to 32768
                (short.MinValue, UInt16BitsToHalf(0b1_11110_0000000000)) // -32768
            };

            foreach ((short original, Half expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ImplicitConversion_FromInt16_TestData))]
        [Theory]
        public static void ImplicitConversion_FromInt16(short s, Half expected)
        {
            Half h = s;
            Assert.Equal(expected, h);
        }

        public static IEnumerable<object[]> ImplicitConversion_FromUInt16_TestData()
        {
            (ushort, Half)[] data =
                unchecked
                (
                    new[]
                    {
                        ((ushort)1U, UInt16BitsToHalf(0b0_01111_0000000000)), // 1
                        ((ushort)-1, Half.PositiveInfinity), // Overflow
                        ((ushort)0U, (ushort)0), // 0
                        ((ushort)short.MaxValue, UInt16BitsToHalf(0b0_11110_0000000000)), // rounds to 32768
                        ((ushort)short.MinValue, UInt16BitsToHalf(0b0_11110_0000000000)), // 32768
                        (ushort.MaxValue, Half.PositiveInfinity), // Overflow
                    }
                );

            foreach ((ushort original, Half expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ImplicitConversion_FromUInt16_TestData))]
        [Theory]
        public static void ImplicitConversion_FromUInt16(ushort s, Half expected)
        {
            Half h = s;
            Assert.Equal(expected, h);
        }

        public static IEnumerable<object[]> ImplicitConversion_FromByte_TestData()
        {
            (byte, Half)[] data =
                unchecked
                (
                    new[]
                    {
                        ((byte)1U, UInt16BitsToHalf(0b0_01111_0000000000)), // 1
                        ((byte)0U, (ushort)0), // 0
                        ((byte)sbyte.MaxValue, UInt16BitsToHalf(0b0_10101_1111110000)), // 127
                        ((byte)sbyte.MinValue, UInt16BitsToHalf(0b0_10110_0000000000)), // 128
                        (byte.MaxValue, UInt16BitsToHalf(0b0_10110_1111111000)), // 255
                    }
                );

            foreach ((byte original, Half expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ImplicitConversion_FromByte_TestData))]
        [Theory]
        public static void ImplicitConversion_FromByte(byte b, Half expected)
        {
            Half h = b;
            Assert.Equal(expected, h);
        }

        public static IEnumerable<object[]> ImplicitConversion_FromSByte_TestData()
        {
            (sbyte, Half)[] data =
            {
                ((sbyte)1U, UInt16BitsToHalf(0b0_01111_0000000000)), // 1
                (-1, UInt16BitsToHalf(0b1_01111_0000000000)), // -1
                ((sbyte)0U, (ushort)0), // 0
                (sbyte.MaxValue, UInt16BitsToHalf(0b0_10101_1111110000)), // 127
                (sbyte.MinValue, UInt16BitsToHalf(0b1_10110_0000000000)), // -128
            };

            foreach ((sbyte original, Half expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ImplicitConversion_FromSByte_TestData))]
        [Theory]
        public static void ImplicitConversion_FromSByte(sbyte b, Half expected)
        {
            Half h = b;
            Assert.Equal(expected, h);
        }

        public static IEnumerable<object[]> ExplicitConversion_FromSingle_TestData()
        {
            (float, Half)[] data = 
            {
                (MathF.PI, UInt16BitsToHalf(0b0_10000_1001001000)), // 3.140625
                (MathF.E, UInt16BitsToHalf(0b0_10000_0101110000)), // 2.71875
                (-MathF.PI, UInt16BitsToHalf(0b1_10000_1001001000)), // -3.140625
                (-MathF.E, UInt16BitsToHalf(0b1_10000_0101110000)), // -2.71875
                (float.MaxValue, Half.PositiveInfinity), // Overflow
                (float.MinValue, Half.NegativeInfinity), // Overflow
                (float.NaN, Half.NaN), // Quiet Negative NaN
                (BitConverter.Int32BitsToSingle(0x7FC00000), UInt16BitsToHalf(0b0_11111_1000000000)), // Quiet Positive NaN
                (BitConverter.Int32BitsToSingle(unchecked((int)0xFFD55555)),
                    UInt16BitsToHalf(0b1_11111_1010101010)), // Signalling Negative NaN
                (BitConverter.Int32BitsToSingle(0x7FD55555), UInt16BitsToHalf(0b0_11111_1010101010)), // Signalling Positive NaN
                (float.Epsilon, UInt16BitsToHalf(0)), // Underflow
                (-float.Epsilon, UInt16BitsToHalf(0b1_00000_0000000000)), // Underflow
                (1f, UInt16BitsToHalf(0b0_01111_0000000000)), // 1
                (-1f, UInt16BitsToHalf(0b1_01111_0000000000)), // -1
                (0f, UInt16BitsToHalf(0)), // 0
                (-0f, UInt16BitsToHalf(0b1_00000_0000000000)), // -0
                (42f, UInt16BitsToHalf(0b0_10100_0101000000)), // 42
                (-42f, UInt16BitsToHalf(0b1_10100_0101000000)), // -42
                (0.1f, UInt16BitsToHalf(0b0_01011_1001100110)), // 0.0999755859375
                (-0.1f, UInt16BitsToHalf(0b1_01011_1001100110)), // -0.0999755859375
                (1.5f, UInt16BitsToHalf(0b0_01111_1000000000)), // 1.5
                (-1.5f, UInt16BitsToHalf(0b1_01111_1000000000)), // -1.5
                (1.5009765625f, UInt16BitsToHalf(0b0_01111_1000000001)), // 1.5009765625
                (-1.5009765625f, UInt16BitsToHalf(0b1_01111_1000000001)), // -1.5009765625
            };

            foreach ((float original, Half expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ExplicitConversion_FromSingle_TestData))]
        [Theory]
        public static void ExplicitConversion_FromSingle(float f, Half expected) // Check the underlying bits for verifying NaNs
        {
            Half h = (Half)f;
            Assert.Equal(HalfToUInt16Bits(expected), HalfToUInt16Bits(h));
        }

        public static IEnumerable<object[]> ExplicitConversion_FromDouble_TestData()
        {
            (double, Half)[] data =
            {
                (Math.PI, UInt16BitsToHalf(0b0_10000_1001001000)), // 3.140625
                (Math.E, UInt16BitsToHalf(0b0_10000_0101110000)), // 2.71875
                (-Math.PI, UInt16BitsToHalf(0b1_10000_1001001000)), // -3.140625
                (-Math.E, UInt16BitsToHalf(0b1_10000_0101110000)), // -2.71875
                (double.MaxValue, Half.PositiveInfinity), // Overflow
                (double.MinValue, Half.NegativeInfinity), // Overflow
                (double.NaN, Half.NaN), // Quiet Negative NaN
                (BitConverter.Int64BitsToDouble(0x7FF80000_00000000),
                    UInt16BitsToHalf(0b0_11111_1000000000)), // Quiet Positive NaN
                (BitConverter.Int64BitsToDouble(unchecked((long)0xFFFAAAAA_AAAAAAAA)),
                    UInt16BitsToHalf(0b1_11111_1010101010)), // Signalling Negative NaN
                (BitConverter.Int64BitsToDouble(0x7FFAAAAA_AAAAAAAA),
                    UInt16BitsToHalf(0b0_11111_1010101010)), // Signalling Positive NaN
                (double.Epsilon, UInt16BitsToHalf(0)), // Underflow
                (-double.Epsilon, UInt16BitsToHalf(0b1_00000_0000000000)), // Underflow
                (1d, UInt16BitsToHalf(0b0_01111_0000000000)), // 1
                (-1d, UInt16BitsToHalf(0b1_01111_0000000000)), // -1
                (0d, UInt16BitsToHalf(0)), // 0
                (-0d, UInt16BitsToHalf(0b1_00000_0000000000)), // -0
                (42d, UInt16BitsToHalf(0b0_10100_0101000000)), // 42
                (-42d, UInt16BitsToHalf(0b1_10100_0101000000)), // -42
                (0.1d, UInt16BitsToHalf(0b0_01011_1001100110)), // 0.0999755859375
                (-0.1d, UInt16BitsToHalf(0b1_01011_1001100110)), // -0.0999755859375
                (1.5d, UInt16BitsToHalf(0b0_01111_1000000000)), // 1.5
                (-1.5d, UInt16BitsToHalf(0b1_01111_1000000000)), // -1.5
                (1.5009765625d, UInt16BitsToHalf(0b0_01111_1000000001)), // 1.5009765625
                (-1.5009765625d, UInt16BitsToHalf(0b1_01111_1000000001)), // -1.5009765625
            };

            foreach ((double original, Half expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ExplicitConversion_FromDouble_TestData))]
        [Theory]
        public static void ExplicitConversion_FromDouble(double d, Half expected) // Check the underlying bits for verifying NaNs
        {
            Half h = (Half)d;
            Assert.Equal(HalfToUInt16Bits(expected), HalfToUInt16Bits(h));
        }

        // ---------- Start of From-half conversion tests ----------

        private const long IllegalValueToInt64 = long.MinValue;
        private const ulong IllegalValueToUInt64 = ulong.MinValue;
        private const int IllegalValueToInt32 = int.MinValue;
        private const uint IllegalValueToUInt32 = uint.MinValue;
        private const short IllegalValueToInt16 = 0;
        private const ushort IllegalValueToUInt16 = 0;
        private const byte IllegalValueToByte = 0;
        private const sbyte IllegalValueToSByte = 0;

        public static IEnumerable<object[]> ExplicitConversion_ToInt64_TestData()
        {
            (Half Original, long Expected)[] data =
            {
                (UInt16BitsToHalf(0b0_01111_0000000000), 1L), // 1
                (UInt16BitsToHalf(0b1_01111_0000000000), -1L), // -1
                (Half.MaxValue, 65504L), // Half.MaxValue -> 65504
                (Half.MinValue, -65504L), // Half.MinValue -> -65504
                (UInt16BitsToHalf(0b0_01011_1001100110), 0L), // 0.1 -> 0 
                (UInt16BitsToHalf(0b1_01011_1001100110), 0L), // -0.1 -> 0
                (UInt16BitsToHalf(0b0_10100_0101000000), 42L), // 42
                (UInt16BitsToHalf(0b1_10100_0101000000), -42L), // -42
                (Half.PositiveInfinity, IllegalValueToInt64), // PosInfinity -> MinValue
                (Half.NegativeInfinity, IllegalValueToInt64), // NegInfinity -> MinValue
                (UInt16BitsToHalf(0b0_11111_1000000000), IllegalValueToInt64), // Positive Quiet NaN -> Minvalue
                (Half.NaN, IllegalValueToInt64), // Negative Quiet NaN -> Minvalue
                (UInt16BitsToHalf(0b0_11111_1010101010), IllegalValueToInt64), // Positive Signalling NaN -> MinValue
                (UInt16BitsToHalf(0b1_11111_1010101010), IllegalValueToInt64), // Negative Signalling NaN -> MinValue
                (Half.Epsilon, 0L), // PosEpsilon -> 0
                (-Half.Epsilon, 0L), // NegEpsilon -> 0
                (UInt16BitsToHalf(0), 0L), // 0
                (UInt16BitsToHalf(0b1_00000_0000000000), 0L), // -0 -> 0 
                (UInt16BitsToHalf(0b0_10000_1001001000), 3L), // Pi -> 3
                (UInt16BitsToHalf(0b1_10000_1001001000), -3L), // -Pi -> -3
                (UInt16BitsToHalf(0b0_10000_0101110000), 2L), // E -> 2
                (UInt16BitsToHalf(0b1_10000_0101110000), -2L), // -E -> -2
                (UInt16BitsToHalf(0b0_01111_1000000000), 1L), // 1.5 -> 1
                (UInt16BitsToHalf(0b1_01111_1000000000), -1L), // -1.5 -> -1
                (UInt16BitsToHalf(0b0_01111_1000000001), 1L), // 1.5009765625 -> 1
                (UInt16BitsToHalf(0b1_01111_1000000001), -1L), // -1.5009765625 -> -1
            };

            foreach ((Half original, long expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ExplicitConversion_ToInt64_TestData))]
        [Theory]
        public static void ExplicitConversion_ToInt64(Half value, long expected)
        {
            long l = (long)value;
            Assert.Equal(expected, l);
        }

        public static IEnumerable<object[]> ExplicitConversion_ToUInt64_TestData()
        {
            (Half Original, ulong Expected)[] data =
                unchecked
                (
                    new []
                    {
                        (UInt16BitsToHalf(0b0_01111_0000000000), 1UL), // 1
                        (UInt16BitsToHalf(0b1_01111_0000000000), (ulong)-1L), // -1 -> -1 in two's complement
                        (Half.MaxValue, 65504UL), // Half.MaxValue -> 65504
                        (Half.MinValue, (ulong)-65504L), // Half.MinValue -> -65504
                        (UInt16BitsToHalf(0b0_01011_1001100110), 0UL), // 0.1 -> 0 
                        (UInt16BitsToHalf(0b1_01011_1001100110), 0UL), // -0.1 -> 0
                        (UInt16BitsToHalf(0b0_10100_0101000000), 42UL), // 42
                        (UInt16BitsToHalf(0b1_10100_0101000000), (ulong)-42L), // -42
                        (Half.PositiveInfinity, IllegalValueToUInt64), // PosInfinity -> MinValue
                        (Half.NegativeInfinity, IllegalValueToUInt64), // NegInfinity -> MinValue
                        (UInt16BitsToHalf(0b0_11111_1000000000),
                            IllegalValueToUInt64), // Positive Quiet NaN -> Minvalue
                        (Half.NaN,
                            IllegalValueToUInt64), // Negative Quiet NaN -> Minvalue
                        (UInt16BitsToHalf(0b0_11111_1010101010),
                            IllegalValueToUInt64), // Positive Signalling NaN -> MinValue
                        (UInt16BitsToHalf(0b1_11111_1010101010),
                            IllegalValueToUInt64), // Negative Signalling NaN -> MinValue
                        (Half.Epsilon, 0UL), // PosEpsilon -> 0
                        (-Half.Epsilon, 0UL), // NegEpsilon -> 0
                        (UInt16BitsToHalf(0), 0UL), // 0
                        (UInt16BitsToHalf(0b1_00000_0000000000), 0UL), // -0 -> 0 
                        (UInt16BitsToHalf(0b0_10000_1001001000), 3UL), // Pi -> 3
                        (UInt16BitsToHalf(0b1_10000_1001001000), (ulong)-3L), // -Pi -> -3 in two's complement
                        (UInt16BitsToHalf(0b0_10000_0101110000), 2UL), // E -> 2
                        (UInt16BitsToHalf(0b1_10000_0101110000), (ulong)-2L), // -E -> -2 in two's complement
                        (UInt16BitsToHalf(0b0_01111_1000000000), 1UL), // 1.5 -> 1
                        (UInt16BitsToHalf(0b1_01111_1000000000), (ulong)-1L), // -1.5 -> -1 in two's complement
                        (UInt16BitsToHalf(0b0_01111_1000000001), 1UL), // 1.5009765625 -> 1
                        (UInt16BitsToHalf(0b1_01111_1000000001), (ulong)-1L), // -1.5009765625 -> -1 in two's complement
                    });

            foreach ((Half original, ulong expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ExplicitConversion_ToUInt64_TestData))]
        [Theory]
        public static void ExplicitConversion_ToUInt64(Half value, ulong expected)
        {
            ulong l = (ulong)value;
            Assert.Equal(expected, l);
        }

        public static IEnumerable<object[]> ExplicitConversion_ToInt32_TestData()
        {
            (Half Original, int Expected)[] data =
            {
                (UInt16BitsToHalf(0b0_01111_0000000000), 1), // 1
                (UInt16BitsToHalf(0b1_01111_0000000000), -1), // -1
                (Half.MaxValue, 65504), // Half.MaxValue -> 65504
                (Half.MinValue, -65504), // Half.MinValue -> -65504
                (UInt16BitsToHalf(0b0_01011_1001100110), 0), // 0.1 -> 0 
                (UInt16BitsToHalf(0b1_01011_1001100110), 0), // -0.1 -> 0
                (UInt16BitsToHalf(0b0_10100_0101000000), 42), // 42
                (UInt16BitsToHalf(0b1_10100_0101000000), -42), // -42
                (Half.PositiveInfinity, IllegalValueToInt32), // PosInfinity -> MinValue
                (Half.NegativeInfinity, IllegalValueToInt32), // NegInfinity -> MinValue
                (UInt16BitsToHalf(0b0_11111_1000000000), IllegalValueToInt32), // Positive Quiet NaN -> Minvalue
                (Half.NaN, IllegalValueToInt32), // Negative Quiet NaN -> Minvalue
                (UInt16BitsToHalf(0b0_11111_1010101010),
                    IllegalValueToInt32), // Positive Signalling NaN -> MinValue
                (UInt16BitsToHalf(0b1_11111_1010101010),
                    IllegalValueToInt32), // Negative Signalling NaN -> MinValue
                (Half.Epsilon, 0), // PosEpsilon -> 0
                (-Half.Epsilon, 0), // NegEpsilon -> 0
                (UInt16BitsToHalf(0), 0), // 0
                (UInt16BitsToHalf(0b1_00000_0000000000), 0), // -0 -> 0 
                (UInt16BitsToHalf(0b0_10000_1001001000), 3), // Pi -> 3
                (UInt16BitsToHalf(0b1_10000_1001001000), -3), // -Pi -> -3
                (UInt16BitsToHalf(0b0_10000_0101110000), 2), // E -> 2
                (UInt16BitsToHalf(0b1_10000_0101110000), -2), // -E -> -2
                (UInt16BitsToHalf(0b0_01111_1000000000), 1), // 1.5 -> 1
                (UInt16BitsToHalf(0b1_01111_1000000000), -1), // -1.5 -> -1
                (UInt16BitsToHalf(0b0_01111_1000000001), 1), // 1.5009765625 -> 1
                (UInt16BitsToHalf(0b1_01111_1000000001), -1), // -1.5009765625 -> -1
            };

            foreach ((Half original, int expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ExplicitConversion_ToInt32_TestData))]
        [Theory]
        public static void ExplicitConversion_ToInt32(Half value, int expected)
        {
            int i = (int)value;
            Assert.Equal(expected, i);
        }

        public static IEnumerable<object[]> ExplicitConversion_ToUInt32_TestData()
        {
            (Half Original, uint Expected)[] data =
            unchecked
            (
                new [] 
                {
                    (UInt16BitsToHalf(0b0_01111_0000000000), 1U), // 1
                    (UInt16BitsToHalf(0b1_01111_0000000000), (uint)-1), // -1 in two's complement
                    (Half.MaxValue, 65504U), // Half.MaxValue -> 65504
                    (Half.MinValue, (uint)-65504), // Half.MinValue -> -65504 in two's complement
                    (UInt16BitsToHalf(0b0_01011_1001100110), 0U), // 0.1 -> 0 
                    (UInt16BitsToHalf(0b1_01011_1001100110), 0U), // -0.1 -> 0
                    (UInt16BitsToHalf(0b0_10100_0101000000), 42U), // 42
                    (UInt16BitsToHalf(0b1_10100_0101000000), (uint)-42), // -42 in two's complement
                    (Half.PositiveInfinity, IllegalValueToUInt32), // PosInfinity -> MinValue
                    (Half.NegativeInfinity, IllegalValueToUInt32), // NegInfinity -> MinValue
                    (UInt16BitsToHalf(0b0_11111_1000000000), IllegalValueToUInt32), // Positive Quiet NaN -> Minvalue
                    (Half.NaN, IllegalValueToUInt32), // Negative Quiet NaN -> Minvalue
                    (UInt16BitsToHalf(0b0_11111_1010101010), IllegalValueToUInt32), // Positive Signalling NaN -> MinValue
                    (UInt16BitsToHalf(0b1_11111_1010101010), IllegalValueToUInt32), // Negative Signalling NaN -> MinValue
                    (Half.Epsilon, 0U), // PosEpsilon -> 0
                    (-Half.Epsilon, 0U), // NegEpsilon -> 0
                    (UInt16BitsToHalf(0), 0U), // 0
                    (UInt16BitsToHalf(0b1_00000_0000000000), 0U), // -0 -> 0 
                    (UInt16BitsToHalf(0b0_10000_1001001000), 3U), // Pi -> 3
                    (UInt16BitsToHalf(0b1_10000_1001001000), (uint)-3), // -Pi -> -3 in two's complement
                    (UInt16BitsToHalf(0b0_10000_0101110000), 2U), // E -> 2
                    (UInt16BitsToHalf(0b1_10000_0101110000), (uint)-2), // -E -> -2 in two's complement
                    (UInt16BitsToHalf(0b0_01111_1000000000), 1U), // 1.5 -> 1
                    (UInt16BitsToHalf(0b1_01111_1000000000), (uint)-1), // -1.5 -> -1 in two's complement
                    (UInt16BitsToHalf(0b0_01111_1000000001), 1U), // 1.5009765625 -> 1
                    (UInt16BitsToHalf(0b1_01111_1000000001), (uint)-1), // -1.5009765625 -> -1 in two's complement
                }
            );

            foreach ((Half original, uint expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ExplicitConversion_ToUInt32_TestData))]
        [Theory]
        public static void ExplicitConversion_ToUInt32(Half value, uint expected)
        {
            uint i = (uint)value;
            Assert.Equal(expected, i);
        }

        public static IEnumerable<object[]> ExplicitConversion_ToInt16_TestData()
        {
            (Half Original, short Expected)[] data =
            {
                (UInt16BitsToHalf(0b0_01111_0000000000), 1), // 1
                (UInt16BitsToHalf(0b1_01111_0000000000), -1), // -1
                (Half.MaxValue, unchecked((short)(65504 & 0xFFFF))), // Half.MaxValue -> -32, 65504 bitmasked
                (Half.MinValue, -65504 & 0xFFFF), // Half.MinValue -> 32, -65504 bitmasked
                (UInt16BitsToHalf(0b0_01011_1001100110), 0), // 0.1 -> 0 
                (UInt16BitsToHalf(0b1_01011_1001100110), 0), // -0.1 -> 0
                (UInt16BitsToHalf(0b0_10100_0101000000), 42), // 42
                (UInt16BitsToHalf(0b1_10100_0101000000), -42), // -42
                (Half.PositiveInfinity, IllegalValueToInt16), // PosInfinity -> MinValue
                (Half.NegativeInfinity, IllegalValueToInt16), // NegInfinity -> MinValue
                (UInt16BitsToHalf(0b0_11111_1000000000), IllegalValueToInt16), // Positive Quiet NaN -> Minvalue
                (Half.NaN, IllegalValueToInt16), // Negative Quiet NaN -> Minvalue
                (UInt16BitsToHalf(0b0_11111_1010101010), IllegalValueToInt16), // Positive Signalling NaN -> MinValue
                (UInt16BitsToHalf(0b1_11111_1010101010), IllegalValueToInt16), // Negative Signalling NaN -> MinValue
                (Half.Epsilon, 0), // PosEpsilon -> 0
                (-Half.Epsilon, 0), // NegEpsilon -> 0
                (UInt16BitsToHalf(0), 0), // 0
                (UInt16BitsToHalf(0b1_00000_0000000000), 0), // -0 -> 0 
                (UInt16BitsToHalf(0b0_10000_1001001000), 3), // Pi -> 3
                (UInt16BitsToHalf(0b1_10000_1001001000), -3), // -Pi -> -3
                (UInt16BitsToHalf(0b0_10000_0101110000), 2), // E -> 2
                (UInt16BitsToHalf(0b1_10000_0101110000), -2), // -E -> -2
                (UInt16BitsToHalf(0b0_01111_1000000000), 1), // 1.5 -> 1
                (UInt16BitsToHalf(0b1_01111_1000000000), -1), // -1.5 -> -1
                (UInt16BitsToHalf(0b0_01111_1000000001), 1), // 1.5009765625 -> 1
                (UInt16BitsToHalf(0b1_01111_1000000001), -1) // -1.5009765625 -> -1
            };

            foreach ((Half original, short expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ExplicitConversion_ToInt16_TestData))]
        [Theory]
        public static void ExplicitConversion_ToInt16(Half value, short expected)
        {
            short i = (short)value;
            Assert.Equal(expected, i);
        }

        public static IEnumerable<object[]> ExplicitConversion_ToUInt16_TestData()
        {
            (Half Original, ushort Expected)[] data =
                unchecked
                (
                    new []
                    {
                        (UInt16BitsToHalf(0b0_01111_0000000000), (ushort)1), // 1
                        (UInt16BitsToHalf(0b1_01111_0000000000), (ushort)-1), // -1 in two's complement
                        (Half.MaxValue, (ushort)65504), // Half.MaxValue -> 65504
                        (Half.MinValue, (ushort)(-65504 & 0xFFFF)), // Half.MinValue -> 32, -65504 bitmasked
                        (UInt16BitsToHalf(0b0_01011_1001100110), (ushort)0), // 0.1 -> 0 
                        (UInt16BitsToHalf(0b1_01011_1001100110), (ushort)0), // -0.1 -> 0
                        (UInt16BitsToHalf(0b0_10100_0101000000), (ushort)42), // 42
                        (UInt16BitsToHalf(0b1_10100_0101000000), (ushort)-42), // -42 in two's complement
                        (Half.PositiveInfinity, IllegalValueToUInt16), // PosInfinity -> MinValue
                        (Half.NegativeInfinity, IllegalValueToUInt16), // NegInfinity -> MinValue
                        (UInt16BitsToHalf(0b0_11111_1000000000),
                            IllegalValueToUInt16), // Positive Quiet NaN -> Minvalue
                        (Half.NaN, IllegalValueToUInt16), // Negative Quiet NaN -> Minvalue
                        (UInt16BitsToHalf(0b0_11111_1010101010),
                            IllegalValueToUInt16), // Positive Signalling NaN -> MinValue
                        (UInt16BitsToHalf(0b1_11111_1010101010),
                            IllegalValueToUInt16), // Negative Signalling NaN -> MinValue
                        (Half.Epsilon, (ushort)0), // PosEpsilon -> 0
                        (-Half.Epsilon, (ushort)0), // NegEpsilon -> 0
                        (UInt16BitsToHalf(0), (ushort)0), // 0
                        (UInt16BitsToHalf(0b1_00000_0000000000), (ushort)0), // -0 -> 0 
                        (UInt16BitsToHalf(0b0_10000_1001001000), (ushort)3), // Pi -> 3
                        (UInt16BitsToHalf(0b1_10000_1001001000), (ushort)-3), // -Pi -> -3 in two's complement
                        (UInt16BitsToHalf(0b0_10000_0101110000), (ushort)2), // E -> 2
                        (UInt16BitsToHalf(0b1_10000_0101110000), (ushort)-2), // -E -> -2 in two's complement
                        (UInt16BitsToHalf(0b0_01111_1000000000), (ushort)1), // 1.5 -> 1
                        (UInt16BitsToHalf(0b1_01111_1000000000), (ushort)-1), // -1.5 -> -1 in two's complement
                        (UInt16BitsToHalf(0b0_01111_1000000001), (ushort)1), // 1.5009765625 -> 1
                        (UInt16BitsToHalf(0b1_01111_1000000001), (ushort)-1), // -1.5009765625 -> -1 in two's complement
                    }
                );

            foreach ((Half original, ushort expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ExplicitConversion_ToUInt16_TestData))]
        [Theory]
        public static void ExplicitConversion_ToUInt16(Half value, ushort expected)
        {
            ushort i = (ushort)value;
            Assert.Equal(expected, i);
        }

        public static IEnumerable<object[]> ExplicitConversion_ToByte_TestData()
        {
            (Half Original, byte Expected)[] data =
                unchecked
                (
                    new[]
                    {
                        (UInt16BitsToHalf(0b0_01111_0000000000), (byte)1), // 1
                        (UInt16BitsToHalf(0b1_01111_0000000000), (byte)-1), // -1
                        (Half.MaxValue, (byte)(65504 & 0xFF)), // Half.MaxValue -> 224, 65504 bitmasked
                        (Half.MinValue, (byte)(-65504 & 0xFF)), // Half.MinValue -> 32, -65504 bitmasked
                        (UInt16BitsToHalf(0b0_01011_1001100110), (byte)0), // 0.1 -> 0 
                        (UInt16BitsToHalf(0b1_01011_1001100110), (byte)0), // -0.1 -> 0
                        (UInt16BitsToHalf(0b0_10100_0101000000), (byte)42), // 42
                        (UInt16BitsToHalf(0b1_10100_0101000000), (byte)-42), // -42
                        (Half.PositiveInfinity, IllegalValueToByte), // PosInfinity -> MinValue
                        (Half.NegativeInfinity, IllegalValueToByte), // NegInfinity -> MinValue
                        (UInt16BitsToHalf(0b0_11111_1000000000), IllegalValueToByte), // Positive Quiet NaN -> Minvalue
                        (Half.NaN, IllegalValueToByte), // Negative Quiet NaN -> Minvalue
                        (UInt16BitsToHalf(0b0_11111_1010101010),
                            IllegalValueToByte), // Positive Signalling NaN -> MinValue
                        (UInt16BitsToHalf(0b1_11111_1010101010),
                            IllegalValueToByte), // Negative Signalling NaN -> MinValue
                        (Half.Epsilon, (byte)0), // PosEpsilon -> 0
                        (-Half.Epsilon, (byte)0), // NegEpsilon -> 0
                        (UInt16BitsToHalf(0), (byte)0), // 0
                        (UInt16BitsToHalf(0b1_00000_0000000000), (byte)0), // -0 -> 0 
                        (UInt16BitsToHalf(0b0_10000_1001001000), (byte)3), // Pi -> 3
                        (UInt16BitsToHalf(0b1_10000_1001001000), (byte)-3), // -Pi -> -3
                        (UInt16BitsToHalf(0b0_10000_0101110000), (byte)2), // E -> 2
                        (UInt16BitsToHalf(0b1_10000_0101110000), (byte)-2), // -E -> -2
                        (UInt16BitsToHalf(0b0_01111_1000000000), (byte)1), // 1.5 -> 1
                        (UInt16BitsToHalf(0b1_01111_1000000000), (byte)-1), // -1.5 -> -1
                        (UInt16BitsToHalf(0b0_01111_1000000001), (byte)1), // 1.5009765625 -> 1
                        (UInt16BitsToHalf(0b1_01111_1000000001), (byte)-1), // -1.5009765625 -> -1
                    }
                );
            foreach ((Half original, byte expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ExplicitConversion_ToByte_TestData))]
        [Theory]
        public static void ExplicitConversion_ToByte(Half value, byte expected)
        {
            byte i = (byte)value;
            Assert.Equal(expected, i);
        }

        public static IEnumerable<object[]> ExplicitConversion_ToSByte_TestData()
        {
            (Half Original, sbyte Expected)[] data =
            {
                (UInt16BitsToHalf(0b0_01111_0000000000), 1), // 1
                (UInt16BitsToHalf(0b1_01111_0000000000), -1), // -1
                (Half.MaxValue, unchecked((sbyte)(65504 & 0xFF))), // Half.MaxValue -> -32, -65504 bitmasked
                (Half.MinValue, (sbyte)(-65504 & 0xFF)), // Half.MinValue -> 32, -65504 bitmasked
                (UInt16BitsToHalf(0b0_01011_1001100110), 0), // 0.1 -> 0 
                (UInt16BitsToHalf(0b1_01011_1001100110), 0), // -0.1 -> 0
                (UInt16BitsToHalf(0b0_10100_0101000000), 42), // 42
                (UInt16BitsToHalf(0b1_10100_0101000000), -42), // -42
                (Half.PositiveInfinity, IllegalValueToSByte), // PosInfinity -> MinValue
                (Half.NegativeInfinity, IllegalValueToSByte), // NegInfinity -> MinValue
                (UInt16BitsToHalf(0b0_11111_1000000000), IllegalValueToSByte), // Positive Quiet NaN -> Minvalue
                (Half.NaN, IllegalValueToSByte), // Negative Quiet NaN -> Minvalue
                (UInt16BitsToHalf(0b0_11111_1010101010), IllegalValueToSByte), // Positive Signalling NaN -> MinValue
                (UInt16BitsToHalf(0b1_11111_1010101010), IllegalValueToSByte), // Negative Signalling NaN -> MinValue
                (Half.Epsilon, 0), // PosEpsilon -> 0
                (-Half.Epsilon, 0), // NegEpsilon -> 0
                (UInt16BitsToHalf(0), 0), // 0
                (UInt16BitsToHalf(0b1_00000_0000000000), 0), // -0 -> 0 
                (UInt16BitsToHalf(0b0_10000_1001001000), 3), // Pi -> 3
                (UInt16BitsToHalf(0b1_10000_1001001000), -3), // -Pi -> -3
                (UInt16BitsToHalf(0b0_10000_0101110000), 2), // E -> 2
                (UInt16BitsToHalf(0b1_10000_0101110000), -2), // -E -> -2
                (UInt16BitsToHalf(0b0_01111_1000000000), 1), // 1.5 -> 1
                (UInt16BitsToHalf(0b1_01111_1000000000), -1), // -1.5 -> -1
                (UInt16BitsToHalf(0b0_01111_1000000001), 1), // 1.5009765625 -> 1
                (UInt16BitsToHalf(0b1_01111_1000000001), -1), // -1.5009765625 -> -1
            };
            foreach ((Half original, sbyte expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ExplicitConversion_ToSByte_TestData))]
        [Theory]
        public static void ExplicitConversion_ToSByte(Half value, sbyte expected)
        {
            sbyte i = (sbyte)value;
            Assert.Equal(expected, i);
        }

        public static IEnumerable<object[]> ImplicitConversion_ToSingle_TestData()
        {
            (Half Original, float Expected)[] data = // Fraction is shifted left by 42, Exponent is -15 then +127 = +112
            {
                (UInt16BitsToHalf(0b0_01111_0000000000), 1f), // 1
                (UInt16BitsToHalf(0b1_01111_0000000000), -1f), // -1
                (Half.MaxValue, 65504f), // 65504
                (Half.MinValue, -65504f), // -65504
                (UInt16BitsToHalf(0b0_01011_1001100110), 0.0999755859375f), // 0.1ish
                (UInt16BitsToHalf(0b1_01011_1001100110), -0.0999755859375f), // -0.1ish
                (UInt16BitsToHalf(0b0_10100_0101000000), 42f), // 42
                (UInt16BitsToHalf(0b1_10100_0101000000), -42f), // -42
                (Half.PositiveInfinity, float.PositiveInfinity), // PosInfinity
                (Half.NegativeInfinity, float.NegativeInfinity), // NegInfinity
                (UInt16BitsToHalf(0b0_11111_1000000000), BitConverter.Int32BitsToSingle(0x7FC00000)), // Positive Quiet NaN
                (Half.NaN, float.NaN), // Negative Quiet NaN
                (UInt16BitsToHalf(0b0_11111_1010101010), BitConverter.Int32BitsToSingle(0x7FD54000)), // Positive Signalling NaN - Should preserve payload
                (UInt16BitsToHalf(0b1_11111_1010101010), BitConverter.Int32BitsToSingle(unchecked((int)0xFFD54000))), // Negative Signalling NaN - Should preserve payload
                (Half.Epsilon, 1/16777216f), // PosEpsilon = 0.000000059605...
                (-Half.Epsilon, -1/16777216f), // NegEpsilon = 0.000000059605...
                (UInt16BitsToHalf(0), 0), // 0
                (UInt16BitsToHalf(0b1_00000_0000000000), -0f), // -0 
                (UInt16BitsToHalf(0b0_10000_1001001000), 3.140625f), // 3.140625
                (UInt16BitsToHalf(0b1_10000_1001001000), -3.140625f), // -3.140625
                (UInt16BitsToHalf(0b0_10000_0101110000), 2.71875f), // 2.71875
                (UInt16BitsToHalf(0b1_10000_0101110000), -2.71875f), // -2.71875
                (UInt16BitsToHalf(0b0_01111_1000000000), 1.5f), // 1.5
                (UInt16BitsToHalf(0b1_01111_1000000000), -1.5f), // -1.5
                (UInt16BitsToHalf(0b0_01111_1000000001), 1.5009765625f), // 1.5009765625
                (UInt16BitsToHalf(0b1_01111_1000000001), -1.5009765625f), // -1.5009765625
            };

            foreach ((Half original, float expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ImplicitConversion_ToSingle_TestData))]
        [Theory]
        public static void ImplicitConversion_ToSingle(Half value, float expected) // Check the underlying bits for verifying NaNs
        {
            float f = value;
            Assert.Equal(BitConverter.SingleToInt32Bits(expected), BitConverter.SingleToInt32Bits(f));
        }

        public static IEnumerable<object[]> ImplicitConversion_ToDouble_TestData()
        {
            (Half Original, double Expected)[] data = // Fraction is shifted left by 42, Exponent is -15 then +127 = +112
            {
                (UInt16BitsToHalf(0b0_01111_0000000000), 1d), // 1
                (UInt16BitsToHalf(0b1_01111_0000000000), -1d), // -1
                (Half.MaxValue, 65504d), // 65504
                (Half.MinValue, -65504d), // -65504
                (UInt16BitsToHalf(0b0_01011_1001100110), 0.0999755859375d), // 0.1ish
                (UInt16BitsToHalf(0b1_01011_1001100110), -0.0999755859375d), // -0.1ish
                (UInt16BitsToHalf(0b0_10100_0101000000), 42d), // 42
                (UInt16BitsToHalf(0b1_10100_0101000000), -42d), // -42
                (Half.PositiveInfinity, double.PositiveInfinity), // PosInfinity
                (Half.NegativeInfinity, double.NegativeInfinity), // NegInfinity
                (UInt16BitsToHalf(0b0_11111_1000000000), BitConverter.Int64BitsToDouble(0x7FF80000_00000000)), // Positive Quiet NaN
                (Half.NaN, double.NaN), // Negative Quiet NaN
                (UInt16BitsToHalf(0b0_11111_1010101010), BitConverter.Int64BitsToDouble(0x7FFAA800_00000000)), // Positive Signalling NaN - Should preserve payload
                (UInt16BitsToHalf(0b1_11111_1010101010), BitConverter.Int64BitsToDouble(unchecked((long)0xFFFAA800_00000000))), // Negative Signalling NaN - Should preserve payload
                (Half.Epsilon, 1/16777216d), // PosEpsilon = 0.000000059605...
                (-Half.Epsilon, -1/16777216d), // NegEpsilon = 0.000000059605...
                (UInt16BitsToHalf(0), 0d), // 0
                (UInt16BitsToHalf(0b1_00000_0000000000), -0d), // -0 
                (UInt16BitsToHalf(0b0_10000_1001001000), 3.140625d), // 3.140625
                (UInt16BitsToHalf(0b1_10000_1001001000), -3.140625d), // -3.140625
                (UInt16BitsToHalf(0b0_10000_0101110000), 2.71875d), // 2.71875
                (UInt16BitsToHalf(0b1_10000_0101110000), -2.71875d), // -2.71875
                (UInt16BitsToHalf(0b0_01111_1000000000), 1.5d), // 1.5
                (UInt16BitsToHalf(0b1_01111_1000000000), -1.5d), // -1.5
                (UInt16BitsToHalf(0b0_01111_1000000001), 1.5009765625d), // 1.5009765625
                (UInt16BitsToHalf(0b1_01111_1000000001), -1.5009765625d) // -1.5009765625
            };

            foreach ((Half original, double expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(ImplicitConversion_ToDouble_TestData))]
        [Theory]
        public static void ImplicitConversion_ToDouble(Half value, double expected) // Check the underlying bits for verifying NaNs
        {
            double d = value;
            Assert.Equal(BitConverter.DoubleToInt64Bits(expected), BitConverter.DoubleToInt64Bits(d));
        }

        public static IEnumerable<object[]> UnaryNegativeOperator_TestData()
        {
            (Half original, Half expected)[] data =
            {
                (UInt16BitsToHalf(0b0_01111_0000000000), UInt16BitsToHalf(0b1_01111_0000000000)), // 1
                (UInt16BitsToHalf(0b1_01111_0000000000), UInt16BitsToHalf(0b0_01111_0000000000)), // -1
                (Half.MaxValue, Half.MinValue), // MaxValue
                (Half.MinValue, Half.MaxValue), // MinValue
                (UInt16BitsToHalf(0b0_01011_1001100110), UInt16BitsToHalf(0b1_01011_1001100110)), // 0.1ish
                (UInt16BitsToHalf(0b1_01011_1001100110), UInt16BitsToHalf(0b0_01011_1001100110)), // -0.1ish
                (UInt16BitsToHalf(0b0_10100_0101000000), UInt16BitsToHalf(0b1_10100_0101000000)), // 42
                (UInt16BitsToHalf(0b1_10100_0101000000), UInt16BitsToHalf(0b0_10100_0101000000)), // -42
                (Half.PositiveInfinity, Half.NegativeInfinity), // PosInfinity
                (Half.NegativeInfinity, Half.PositiveInfinity), // NegInfinity
                // NaNs should be propagated back
                (UInt16BitsToHalf(0b0_11111_1000000000), UInt16BitsToHalf(0b0_11111_1000000000)), // Positive Quiet NaN
                (Half.NaN, Half.NaN), // Negative Quiet NaN
                (UInt16BitsToHalf(0b0_11111_1010101010), UInt16BitsToHalf(0b0_11111_1010101010)), // Positive Signalling NaN
                (UInt16BitsToHalf(0b1_11111_1010101010), UInt16BitsToHalf(0b1_11111_1010101010)), // Negative Signalling NaN
                // End of NaNs
                (UInt16BitsToHalf(0), UInt16BitsToHalf(0b1_00000_0000000000)), // 0
                (UInt16BitsToHalf(0b1_00000_0000000000), UInt16BitsToHalf(0)), // -0
                (UInt16BitsToHalf(0b0_00000_0000000001), UInt16BitsToHalf(0b1_00000_0000000001)), // Epsilon
                (UInt16BitsToHalf(0b1_00000_0000000001), UInt16BitsToHalf(0b0_00000_0000000001)), // -Epsilon
                (UInt16BitsToHalf(0b0_10000_1001001000), UInt16BitsToHalf(0b1_10000_1001001000)), // 3.140625
                (UInt16BitsToHalf(0b1_10000_1001001000), UInt16BitsToHalf(0b0_10000_1001001000)), // -3.140625
                (UInt16BitsToHalf(0b0_10000_0101110000), UInt16BitsToHalf(0b1_10000_0101110000)), // 2.71875
                (UInt16BitsToHalf(0b1_10000_0101110000), UInt16BitsToHalf(0b0_10000_0101110000)), // -2.71875
                (UInt16BitsToHalf(0b0_01111_1000000000), UInt16BitsToHalf(0b1_01111_1000000000)), // 1.5
                (UInt16BitsToHalf(0b1_01111_1000000000), UInt16BitsToHalf(0b0_01111_1000000000)), // -1.5
                (UInt16BitsToHalf(0b0_01111_1000000001), UInt16BitsToHalf(0b1_01111_1000000001)), // 1.5009765625
                (UInt16BitsToHalf(0b1_01111_1000000001), UInt16BitsToHalf(0b0_01111_1000000001)), // -1.5009765625
            };

            foreach ((Half original, Half expected) in data)
            {
                yield return new object[] { original, expected };
            }
        }

        [MemberData(nameof(UnaryNegativeOperator_TestData))]
        [Theory]
        public static void UnaryNegativeOperator(Half value, Half expected)
        {
            Assert.Equal(HalfToUInt16Bits(-value), HalfToUInt16Bits(expected));
        }

        public static IEnumerable<object[]> UnaryPositiveOperator_TestData()
        {
            Half[] data =
            {
                UInt16BitsToHalf(0b0_01111_0000000000), // 1
                UInt16BitsToHalf(0b1_01111_0000000000), // -1
                Half.MaxValue, // MaxValue
                Half.MinValue, // MinValue
                UInt16BitsToHalf(0b0_01011_1001100110), // 0.1ish
                UInt16BitsToHalf(0b1_01011_1001100110), // -0.1ish
                UInt16BitsToHalf(0b0_10100_0101000000), // 42
                UInt16BitsToHalf(0b1_10100_0101000000), // -42
                Half.PositiveInfinity, // PosInfinity
                Half.NegativeInfinity, // NegInfinity
                UInt16BitsToHalf(0b0_11111_1000000000), // Positive Quiet NaN
                Half.NaN, // Negative Quiet NaN
                UInt16BitsToHalf(0b0_11111_1010101010), // Positive Signalling NaN
                UInt16BitsToHalf(0b1_11111_1010101010), // Negative Signalling NaN
                UInt16BitsToHalf(0), // 0
                UInt16BitsToHalf(0b1_00000_0000000000), // -0
                UInt16BitsToHalf(0b0_00000_0000000001), // Epsilon
                UInt16BitsToHalf(0b1_00000_0000000001), // -Epsilon
                UInt16BitsToHalf(0b0_10000_1001001000), // 3.140625
                UInt16BitsToHalf(0b1_10000_1001001000), // -3.140625
                UInt16BitsToHalf(0b0_10000_0101110000), // 2.71875
                UInt16BitsToHalf(0b1_10000_0101110000), // -2.71875
                UInt16BitsToHalf(0b0_01111_1000000000), // 1.5
                UInt16BitsToHalf(0b1_01111_1000000000), // -1.5
                UInt16BitsToHalf(0b0_01111_1000000001), // 1.5009765625
                UInt16BitsToHalf(0b1_01111_1000000001), // -1.5009765625
            };                                          
                                                        
            foreach (Half original in data)
            {
                yield return new object[] { original };
            }
        }

        [MemberData(nameof(UnaryPositiveOperator_TestData))]
        [Theory]
        public static void UnaryPositiveOperator(Half value)
        {
            Assert.Equal(HalfToUInt16Bits(value), HalfToUInt16Bits(+value));
        }
    }
}
