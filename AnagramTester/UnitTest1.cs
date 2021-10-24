using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using AnagramFinderNS;
using FluentAssertions;

namespace AnagramTester
{
    [TestClass]
    public class UnitTest1
    {
        private AnagramFinder af;

        //Test 1
        [TestMethod]
        public void TestNullListE()
        {
            af = new AnagramFinder();

            var actual = Assert.ThrowsException<ArgumentException>(() =>
                af.FindAllAnagramsWithDuplicateLetters(null));

            Assert.AreEqual(actual.Message, "Param 'wordList' is null or empty");
        }
        //Test 2
        [TestMethod]
        public void TestEmptyListE()
        {
            af = new AnagramFinder();
            List<List<String>> emptyListE = af.FindAllAnagramsWithDuplicateLetters(new List<String>());

            Assert.AreEqual(0, emptyListE.Count);

        }

        //Test 3
        [TestMethod]
        public void TestNullValE()
        {
            af = new AnagramFinder();

            List<String> nullValL = new List<String> { "dale", "vale", "veal", null, "lead" };

            var actual = Assert.ThrowsException<ArgumentException>(() =>
                 af.FindAllAnagramsWithDuplicateLetters(nullValL));
            Assert.AreEqual("Param 'wordList' contains null or empty val", actual.Message);
        }

        //Test 4
        [TestMethod]
        public void TestEmptyValE()
        {
            af = new AnagramFinder();
            List<String> emptyValL = new List<String> { "dale", "vale", "veal", "", "lead" };

            var actual = Assert.ThrowsException<ArgumentException>(() =>
                  af.FindAllAnagramsWithDuplicateLetters(emptyValL));

            Assert.AreEqual("Param 'wordList' contains null or empty val", actual.Message);

        }

        //Test 5
        [TestMethod]
        public void TestIgnoreDuplicate()
        {
            af = new AnagramFinder();
            List<String> list = new List<String> { "lead", "silent", "dale", "talent", "new york times", "vile", "talent", "deal", "listen", "monkeys write", "evil" };
            List<List<String>> test = af.FindAllAnagramsWithDuplicateLetters(list);
            List<String> actual = new List<String>();
            foreach (List<String> sl in test)
            {
                actual.AddRange(sl);
            }
            Assert.IsTrue(list.Count > actual.Count);

        }

        //Test 6
        [TestMethod]
        public void TestSingleWord()
        {
            af = new AnagramFinder();
            List<String> list = new List<String> { "fish" };
            Assert.AreEqual(0, af.FindAllAnagramsWithDuplicateLetters(list).Count);
        }

        //Test 7
        [TestMethod]
        public void TestNoAnagrams()
        {
            af = new AnagramFinder();
            List<String> list = new List<String> { "bee", "ebbc" };
            Assert.AreEqual(1, af.FindAllAnagramsWithDuplicateLetters(list).Count);
        }

        //Test 8
        [TestMethod]
        public void TestTwoWords()
        {
            af = new AnagramFinder();

            List<String> list = new List<String> { "bee", "ebb" };
            List<List<String>> actual = af.FindAllAnagramsWithDuplicateLetters(list);
            Assert.AreEqual(1, actual.Count);

            foreach (List<String> l in actual)
            {
                l.Should().BeInAscendingOrder();
            }

        }

        //Test 9
        [TestMethod]
        public void TestReturns()
        {
            af = new AnagramFinder();

            List<String> list = new List<String> { "lead", "silent", "dale", "talent", "new york times", "vile", "deal", "listen", "monkeys write", "evil" };
            List<String> expected = new List<String>();
            foreach (List<String> sList in af.FindAllAnagramsWithDuplicateLetters(list))
            {
                expected.Add(sList[0]);
                sList.Should().BeInAscendingOrder();
            }
            expected.Should().BeInAscendingOrder();
        }

        //Test 10
        [TestMethod]
        public void TestCaseInsensitive()
        {
            af = new AnagramFinder();
            List<String> l = new List<String>{ "SILENT", "listen", "taLent", "LatenT"};
            List<List<String>> actualL = af.FindAllAnagramsWithDuplicateLetters(l);
            String expected = "LatenT, taLent, listen, SILENT";
            String actualS = "";
            actualL.ForEach(y => y.ForEach(x => actualS+=(x + ", ")));
            actualS = actualS.Substring(0, actualS.Length - 2);
            Assert.AreEqual(expected, actualS);
        }
    }
}
