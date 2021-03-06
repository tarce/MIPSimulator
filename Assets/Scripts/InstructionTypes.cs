﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MIPS
{


    public abstract class Instruciton_R : Instruction
    {
        protected string _opcode;
        protected int _rs;
        protected int _rt;
        protected int _rd;
        protected int _shamt;
        protected string _funct;

        protected BitArray _opcodeBits;
        protected BitArray _rsBits;
        protected BitArray _rtBits;
        protected BitArray _rdBits;
        protected BitArray _shamtBits;
        protected BitArray _functBits;

        public Instruciton_R(Word word) :
            base(word)
        {
            _type = Type.rType;

            _opcodeBits = word.GetBits(26,31);
            _rsBits = word.GetBits(21, 25);
            _rtBits = word.GetBits(16, 20);
            _rdBits = word.GetBits(11, 15);
            _shamtBits = word.GetBits(6, 10);
            _functBits = word.GetBits(0, 5);

            _opcode = "R-Type"; //There is no opcode for R, instead uses funct
            _rs = GetUnsignedInt(_rsBits);
            _rt = GetUnsignedInt(_rtBits);
            _rd = GetUnsignedInt(_rdBits);
            _shamt = GetUnsignedInt(_shamtBits);
        }

        public override string Binary()
        {
            string opcode = Helpers.GetString(Helpers.Reverse(_opcodeBits));
            string rs = Helpers.GetString(Helpers.Reverse(_rsBits));
            string rt = Helpers.GetString(Helpers.Reverse(_rtBits));
            string rd = Helpers.GetString(Helpers.Reverse(_rdBits));
            string shamt = Helpers.GetString(Helpers.Reverse(_shamtBits));
            string funct = Helpers.GetString(Helpers.Reverse(_functBits));

            return opcode + " " + rs + " " + rt + " " + rd + " " + shamt + " " + funct;
        }
    }

    public abstract class Instruction_J : Instruction
    {
        protected string _opcode;
        protected int _address;

        protected BitArray _opcodeBits;
        protected BitArray _addressBits;

        public Instruction_J(Word word) :
            base(word)
        {
            _type = Type.iType;

            _opcodeBits = word.GetBits(26, 31);
            _addressBits = word.GetBits(0, 25);

            BitArray lowOrder = new BitArray(2);
            _address =GetUnsignedInt(Concatenate(lowOrder, _addressBits));
        }

        public override string Binary()
        {
            string opcode = Helpers.GetString(Helpers.Reverse(_opcodeBits));
            string address = Helpers.GetString(Helpers.Reverse(_addressBits));

            return opcode + " " + address;
        }

        public override string ToString()
        {
            return _opcode + " #" + _address;
        }
    }

    public abstract class Instruction_I : Instruction
    {
        protected string _opcode;
        protected int _rs;
        protected int _rt;
        protected int _imm;

        protected BitArray _opcodeBits;
        protected BitArray _rsBits;
        protected BitArray _rtBits;
        protected BitArray _immBits;

        public Instruction_I(Word word) :
            base(word)
        {
            _type = Type.jType;

            _opcodeBits = word.GetBits(26, 31);
            _rsBits = word.GetBits(21, 25);
            _rtBits = word.GetBits(16, 20);
            _immBits = word.GetBits(0, 15);

            _rs = GetUnsignedInt(_rsBits);
            _rt = GetUnsignedInt(_rtBits);
        }

        public override string Binary()
        {
            string opcode = Helpers.GetString(Helpers.Reverse(_opcodeBits));
            string rs = Helpers.GetString(Helpers.Reverse(_rsBits));
            string rt = Helpers.GetString(Helpers.Reverse(_rtBits));
            string imm = Helpers.GetString(Helpers.Reverse(_immBits));

            return opcode + " " + rs + " " + rt + " " + imm;
        }

        protected BitArray SignExtend32(BitArray bits)
        {
            if(bits.Count>= 32)
            {
                throw new IndexOutOfRangeException("SignExtend32: detected bit count >= 32");
            } 
            bool sign = bits[bits.Count - 1];
            BitArray bits32 = new BitArray(32, sign);
            for (int i = 0; i < bits.Count; i++)
            {
                bits32[i] = bits[i];
            }

            return bits32;
        }
    }

}