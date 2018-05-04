﻿using System;
using System.Collections.Generic;

namespace Reppertum 
{
    public class Block 
    {
        public readonly BlockHeader Header;
        public readonly List<Transaction> Data;

        public Block(BlockHeader header, List<Transaction> data)
        {
            if (data == null) data = new List<Transaction>();
            Header = header;
            Data = data;
        }
    }
}