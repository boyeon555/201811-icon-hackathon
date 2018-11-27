﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IconSDK.Types
{
    public class Bytes
    {
        public readonly ImmutableArray<byte> Binary;

        public virtual uint Size => 0;
        public virtual string Prefix => string.Empty;

        public Bytes(IEnumerable<byte> bytes)
        {
            if (Size > 0 && Size != bytes.Count())
                throw new Exception($"{Size} != {bytes.Count()}");

            Binary = bytes.ToImmutableArray();
        }

        public Bytes(string hex)
        {
            var builder = ImmutableArray.CreateBuilder<byte>(hex.Length / 2);
            for (int i = 0; i < builder.Capacity; ++i)
            {
                byte b = Convert.ToByte(hex.Substring(i * 2, 2), 16);
                builder.Add(b);
            }

            if (Size > 0 && Size != builder.Count)
                throw new Exception();

            Binary = builder.ToImmutable();
        }

        public Bytes(BigInteger value)
        {
            var bytes = value.ToByteArray();

            if (Size > 0 && Size != bytes.Count())
                throw new Exception();

            Binary = bytes.ToImmutableArray();
        }

        public string ToHex()
        {
            StringBuilder hex = new StringBuilder(Binary.Length * 2);
            foreach (byte b in Binary)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public override string ToString()
        {
            return Prefix + ToHex();
        }

        public override int GetHashCode()
        {
            return Binary.Sum(item => item);
        }

        public override bool Equals(object obj)
        {
            var bytes = obj as Bytes;
            if (bytes != null)
                return this == bytes;

            return false;
        }

        public static bool operator ==(Bytes x, Bytes y)
        {
            return Enumerable.SequenceEqual(x.Binary, y.Binary);
        }

        public static bool operator !=(Bytes x, Bytes y)
        {
            return !(x == y);
        }
    }
}
