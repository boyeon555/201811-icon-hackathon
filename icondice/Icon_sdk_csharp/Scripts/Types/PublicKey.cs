﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IconSDK.Types
{
    public class PublicKey : Bytes
    {
        public override uint Size => 65;

        public PublicKey(IEnumerable<byte> bytes)
            : base(bytes)
        {

        }

        public PublicKey(string hex)
           : base(hex)
        {

        }

        public PublicKey(BigInteger value)
            : base(value)
        {

        }
    }
}
