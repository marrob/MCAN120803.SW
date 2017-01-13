// -----------------------------------------------------------------------
// <copyright file="Class1.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------



namespace Konvolucio.MCAN120803.GUI.UnitTest
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;
    using Common;
 

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class UnitTest_CustomArbitrationIdColumns
    {

        [Test]
        public void _0001_ConvertStdIdToAtmelIDT_0x7A0()
        {
            byte canIDT1, canIDT2;// canIDT3, canIDT4;
            UInt32 sendedId = 0x7A0;
            canIDT1 = (byte)((sendedId >> 3) & 0x00FF);
            canIDT2 = (byte)(sendedId << 5);
            canIDT2 = (byte)(canIDT2 & 0xE0);
            //canIDT3 = 0;
            //canIDT4 = 0;
            Assert.AreEqual(0xF4, canIDT1);
            Assert.AreEqual(0, canIDT2);
        }

        [Test]
        public void _0002_ConvertStdIdToAtmelIDT_0x7A2()
        {
            byte canIDT1, canIDT2;// canIDT3, canIDT4;
            UInt32 sendedId = 0x7A2;

            canIDT1 = (byte)((sendedId >> 3) & 0x00FF);
            canIDT2 = (byte)(sendedId << 5);
            canIDT2 = (byte)(canIDT2 & 0xE0);
            //canIDT3 = 0;
            //canIDT4 = 0;
            Assert.AreEqual(0xF4, canIDT1);
            Assert.AreEqual(0x40, canIDT2);
        }

        [Test]
        public void _0003_ConvertStdIdFromAtmelIDT_0x7A0()
        {
            byte canIDT1 = 0xF4, canIDT2 = 0x00;// canIDT3 = 0x00, canIDT4 = 0x00;
            UInt32 ralconId = 0x00;
            ralconId = (UInt32)canIDT1 << 3;
            ralconId |= (UInt32)((canIDT2 >> 5) & 0x03);
            Assert.AreEqual(0x7A0, ralconId);
        }

        [Test]
        public void _0004_ConvertStdIdFromAtmelIDT_0x7A2()
        {
            byte canIDT1 = 0xF4, canIDT2 = 0x40;// canIDT3 = 0x00, canIDT4 = 0x00;
            UInt32 ralconId = 0x00;
            ralconId = (UInt32)canIDT1 << 3;
            ralconId |= (UInt32)((canIDT2 >> 5) & 0x03);
            Assert.AreEqual(0x7A2, ralconId);
        }

        [Test]
        public void _2001_ValueRangeSelection_StartBit8_LengthBit3_NoShift()
        {
            /*
             *  bit index         0 9 8 7 6 5 4 3 2 1 0
             *                    x x x
             *  
             *  Current ArbId:    1 0 1 0 0 0 0 0 0 0 1   (0x501) (induló értéke az ArbId-nek)
             *  CustomValue:                      1 0 1   (0x05)  (ezt az értéket várom a felső 3-bitről)
             *  StartBit          8
             *  LengthBit         3
             *  StartBit:            0                       (nem kell shiftelni...)
             *  ***************************************
             *  
             *  ArbId:            1 0 1 0 0 0 0 0 0 0 1
             *                                        &
             *  NormMask:         1 1 1 0 0 0 0 0 0 0 0 (ezel kiejtem felesleges biteket)
             *                    =====================
             *                    1 0 1 0 0 0 0 0 0 0 0
             *                                     >> 8
             *                    =====================
             *  NormArbId:        0 0 0 0 0 0 0 0 1 0 1 (Normailzált ArbId) 
             *                                     << 0
             *  StartBit:
             *                    =====================
             *  CustomValue:      0 0 0 0 0 0 0 0 1 0 1 (ez az eredmény)                  
             *                                    
             */

            var target = new CustomArbIdColumnItem("",ArbitrationIdType.Standard, 8, 3, 0, "", "");
            Assert.AreEqual(0x05, target.GetValue(0x501));
        }

        [Test]
        public void _2002_ValueRangeSelection_StartBit8_LengthBit3_Shift1bit()
        {
            /*
             *  bit index         0 9 8 7 6 5 4 3 2 1 0
             *                    x x x
             *  
             *  Current ArbId:    1 0 1 0 0 0 0 0 0 0 1   (0x501) (induló értéke az ArbId-nek)
             *  CustomValue:                    1 0 1 0   (0x0A)  (ezt az értéket várom a felső 3-bitről)
             *  StartBit          8
             *  LengthBit         3
             *  StartBit:            1                       (1 bit-tel balra tolom az eredmény érdekében)
             *  ***************************************
             *  
             *  ArbId:            1 0 1 0 0 0 0 0 0 0 1 
             *                                        &
             *  NormMask:         1 1 1 0 0 0 0 0 0 0 0  
             *                                     >> 8                                                    
             *                    =====================
             *  NormArbId:        0 0 0 0 0 0 0 0 1 0 1
             *                                     << 1
             *                    =====================
             *  shArbId:          0 0 0 0 0 0 0 1 0 1 0
             *  
             *  CustomValue:      0 0 0 0 0 0 0 1 0 1 0 (ez az eredmény)                  
             *                                    
             */
            var target = new CustomArbIdColumnItem("", ArbitrationIdType.Standard, 8, 3, 1, "", "");
            Assert.AreEqual(0x0A, target.GetValue(0x501));
        }

        [Test]
        public void _2003_SetSelectedRange_StartBit8_LengthBit3_NoShift()
        {
            /*
             *  bit index         0 9 8 7 6 5 4 3 2 1 0
             *                    x x x
             *  
             *  CustomValue:                     1 1 0   (0x06)  (ezt az értéket szeretném a felső 3-bitre írni, úgy hogy ne rontasam el a többit.)
             *  Current ArbId:   1 0 1 0 0 0 0 0 0 0 1   (0x501) (induló értéke az ArbId-nek)
             *  Expect ArbId:    1 1 0 0 0 0 0 0 0 0 1   (0x601) (várt értéke)
             *  StartBit         8
             *  LengthBit        3
             *  StartBit:           0                       (nem kell shiftelni...)           
             * 
             *  ***************************************
             *
             *  CustomValue:                     1 1 0 
             *                                    << 8
             *                   =====================
             *  shCustomValue:   1 1 0 0 0 0 0 0 0 0 0 
             * 
             * 
             *  clrMask:         0 0 0 1 1 1 1 1 1 1 1                          
             *                                       &
             *  ArbId:           1 0 1 0 0 0 0 0 0 0 1
             *                                       |
             *  shCustomValue:   1 1 0 0 0 0 0 0 0 0 0 
             *  
             *  Expect ArbId:    1 1 0 0 0 0 0 0 0 0 1 (0x601)
             */
            var target = new CustomArbIdColumnItem("", ArbitrationIdType.Standard, 8, 3, 0, "", "");
            Assert.AreEqual(0x601, target.SetValue(0x06,0x501));
        }

        [Test]
        public void _2004_SetSelectedRange_StartBit8_LengthBit3_Shift1bit()
        {
            /*
             *  bit index         0 9 8 7 6 5 4 3 2 1 0
             *                    x x x
             *  
             *  CustomValue:                   1 1 0 0   (0x0C)  (ezt az értéket szeretném a felső 3-bitre írni, úgy hogy jobbra siftelem 1-szer mivel ebből csak a felső 3 bit értékes)
             *  Current ArbId:   1 0 1 0 0 0 0 0 0 0 1   (0x501) (induló értéke az ArbId-nek)
             *  Expect ArbId:    1 1 0 0 0 0 0 0 0 0 1   (0x601) (várt értéke)
             *  StartBit         8
             *  LengthBit        3
             *  StartBit:           1                           
             * 
             *  ***************************************
             *
             *  CustomValue:                   1 1 0 0 
             *                                    >> 1
             *  shToValue:       0 0 0 0 0 0 0 0 1 1 0  (ezzel kapom meg az pozició helyes értéket amit be kell illeszteni a start-length helyekre.)
             *                                    << 8
             *  shToPos:         1 1 0 0 0 0 0 0 0 0 0           
             *                   =====================
             *  shCustomValue:   1 1 0 0 0 0 0 0 0 0 0 
             *  
             *  ***
             *  
             *  clrMask:         0 0 0 1 1 1 1 1 1 1 1                          
             *                                       &
             *  ArbId:           1 0 1 0 0 0 0 0 0 0 1
             *                                       |
             *  shCustomValue:   1 1 0 0 0 0 0 0 0 0 0 
             *  
             *  Expect ArbId:    1 1 0 0 0 0 0 0 0 0 1 (0x601)
             */
            var target = new CustomArbIdColumnItem("", ArbitrationIdType.Standard, 8, 3, 1, "", "");
            Assert.AreEqual(0x601, target.SetValue(0x0C, 0x501));
        }

        [Test]
        public void _2005_SetSelectedRange_StartBit8_LengthBit3_Shift0bit_ArbId0x7A2_CustomValue0x50()
        {
            var target = new CustomArbIdColumnItem("11-bites Id-ból a felső 8 bitjét módosítja.", ArbitrationIdType.Standard, 3, 8, 0, "X1", "");

            /*
             *  bit index         0 9 8 7 6 5 4 3 2 1 0
             *                    x x x x x x x x 
             *   
             *  CustomValue:           0 1 0 1 0 0 0 0   (0x50)
             *  Current ArbId:   1 1 1 1 0 1 0 0 0 1 0   (0x7A2)
             *  Expect ArbId:    0 1 0 1 0 0 0 0 0 1 0   (0x282)
             *  StartBit: 3
             *  LengthBit: 8
             *  StartBit: 0
             *  ***************************************
             */
            Assert.AreEqual(0x282, target.SetValue(0x50, 0x7A2), "A 11-bit felső 8-bijét 0x50-re változtatja és ebből lesz a 0x282");
        }

        [Test]
        public void _2006_SetSelectedRange_StartBit0_LengthBit3_Shift0bit_ArbId0x7A2_CustomValue0x50()
        {
            var target = new CustomArbIdColumnItem("", ArbitrationIdType.Standard, 0, 3, 4, "X1", "");

            /*
             *  bit index         0 9 8 7 6 5 4 3 2 1 0
             *                                    x x x
             *   
             *  CustomValue:           0 1 0 1 0 0 0 0   (0x50) 
             *  Current ArbId:   1 1 1 1 0 1 0 0 0 1 0   (0x7A2)
             *  Expect ArbId:    0 1 0 1 0 0 0 0 0 1 0   (0x7A5)
             *  StartBit: 0
             *  LengthBit: 3
             *  StartBit: 4
             *  ***************************************
             */
            Assert.AreEqual(0x7A5, target.SetValue(0x50, 0x7A2), "CustomValue-t Balra shifteli 4-el ebből lesz 5, majd az alsó 3 bitre teszi.");
        }

        [Test]
        public void _2007_Comulative()
        {
            var target = new CustomArbIdColumnItem("Arb Id MSB bit", ArbitrationIdType.Extended, 31, 1, 0, "X1", "");
            Assert.AreEqual(0x7FFFFFFF, target.SetValue(0, 0xFFFFFFFF));

            target = new CustomArbIdColumnItem("Arb Id LSB bit", ArbitrationIdType.Extended, 0, 1, 0, "X1", "");
            Assert.AreEqual(0xFFFFFFFE, target.SetValue(0, 0xFFFFFFFF));

            target = new CustomArbIdColumnItem("Std Arb Id MSB bit", ArbitrationIdType.Standard, 10, 1, 0, "X1", "");
            Assert.AreEqual(0xFBFF, target.SetValue(0, 0xFFFF));

            target = new CustomArbIdColumnItem("Ext Arb Id MSB bit", ArbitrationIdType.Extended, 28, 1, 0, "X1", "");
            Assert.AreEqual(0xEFFFFFFF, target.SetValue(0, 0xFFFFFFFF));

            target = new CustomArbIdColumnItem("Ext Arb Id Clone", ArbitrationIdType.Extended, 0, 29, 0, "X8", "");
            Assert.AreEqual(0xAAAAAAA, target.SetValue(0xAAAAAAA, 0xAAAAAAA));

            target = new CustomArbIdColumnItem("Std Arb Id IDT1 Reg", ArbitrationIdType.Standard, 3, 8, 0, "X2", "");
            Assert.AreEqual(0x39, target.SetValue(0x07, 0x00001));

            target = new CustomArbIdColumnItem("Std Arb Id IDT1 Reg", ArbitrationIdType.Standard, 3, 8, 0, "X2", "");
            Assert.AreEqual(0x7F9, target.SetValue(0xFF, 0x00001));

            target = new CustomArbIdColumnItem("Std Arb Id IDT1 Reg", ArbitrationIdType.Standard, 3, 8, 0, "X2", "");
            Assert.AreEqual(0x07, target.SetValue(0x00, 0x007FF));

            target = new CustomArbIdColumnItem("Std Arb Id IDT1 Reg", ArbitrationIdType.Standard, 3, 8, 0, "X2", "");
            Assert.AreEqual(0x7A0, target.SetValue(0xF4, 0x00000));

            target = new CustomArbIdColumnItem("Std Arb Id IDT2 Reg", ArbitrationIdType.Standard, 0, 3, 5, "X2", "");
            Assert.AreEqual(0x000, target.SetValue(0x00, 0x00000));

            target = new CustomArbIdColumnItem("Std Arb Id IDT2 Reg", ArbitrationIdType.Standard, 0, 3, 5, "X2", "");
            Assert.AreEqual(0x02, target.SetValue(0x40, 0x00000));
        }

        [Test]
        public void _3002()
        {
            CustomArbIdColumnCollection customColumns = new CustomArbIdColumnCollection();
        
        }


        [Test]
        public void _9001_SetMask()
        {
            var caici = new CustomArbIdColumnItem();
            Assert.AreEqual(0x700, caici.SetMask(8,3));
        }
             
        [Test]
        public void _9002_ClrMask()
        {
            var caici = new CustomArbIdColumnItem();
            Assert.AreEqual(0xFFFFF8FF, caici.ClrMask(8,3));
        }


    }
}
