﻿/* Dexer Copyright (c) 2010 Sebastien LEBRETON

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
"Software"), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. */

using System;
using System.Collections.Generic;
using System.IO;
using Dexer.Core;
using Dexer.Extensions;
using Dexer.IO.Collector;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dexer.Test
{
    [TestClass]
    public class SortTest : BaseCollectorTest
    {

        public void TestGlobalSort<T>(Func<Dex, List<T>> provider, IComparer<T> comparer)
        {
            foreach (string file in Directory.GetFiles(FilesDirectory))
            {
                TestContext.WriteLine("Testing {0}", file);

                Dex dex = Dex.Read(file);
                List<T> items = new List<T>(provider(dex));
                items.Shuffle();
                items.Sort(comparer);

                if (comparer is IPartialComparer<T>)
                {
                    TopologicalSorter tsorter = new TopologicalSorter();
                    items = new List<T>(tsorter.TopologicalSort(items, comparer as IPartialComparer<T>));
                }

                for (int i = 0; i < items.Count; i++)
                    Assert.AreEqual(items[i], provider(dex)[i]);

            }
        }

        [TestMethod]
        public void TestMethodReferenceSort()
        {
            TestGlobalSort<MethodReference>((dex) => dex.MethodReferences, new MethodReferenceComparer());
        }

        [TestMethod]
        public void TestFieldReferenceSort()
        {
            TestGlobalSort<FieldReference>((dex) => dex.FieldReferences, new FieldReferenceComparer());
        }

        private void SortAndCheck<T>(List<T> source, IComparer<T> comparer)
        {
            var items = new List<T>(source);
            items.Shuffle();
            items.Sort(comparer);

            for (int i = 0; i < items.Count; i++)
                Assert.AreEqual(items[i], source[i]);
        }

        [TestMethod]
        public void TestMethodDefinitionSort()
        {
            foreach (string file in Directory.GetFiles(FilesDirectory))
            {
                TestContext.WriteLine("Testing {0}", file);
                Dex dex = Dex.Read(file);

                foreach (ClassDefinition @class in dex.Classes)
                    SortAndCheck(@class.Methods, new MethodDefinitionComparer());
            }
        }

        [TestMethod]
        public void TestAnnotationSort()
        {
            foreach (string file in Directory.GetFiles(FilesDirectory))
            {
                TestContext.WriteLine("Testing {0}", file);
                Dex dex = Dex.Read(file);
                List<Annotation> items = null;

                foreach (ClassDefinition @class in dex.Classes)
                {
                    SortAndCheck(@class.Annotations, new AnnotationComparer());

                    foreach (FieldDefinition field in @class.Fields)
                        SortAndCheck(field.Annotations, new AnnotationComparer());

                    foreach (MethodDefinition method in @class.Methods)
                    {
                        SortAndCheck(method.Annotations, new AnnotationComparer());

                        foreach (Parameter parameter in method.Prototype.Parameters)
                            SortAndCheck(parameter.Annotations, new AnnotationComparer());
                    }
                }
            }
        }
        

        [TestMethod]
        public void TestFieldDefinitionSort()
        {
            foreach (string file in Directory.GetFiles(FilesDirectory))
            {
                TestContext.WriteLine("Testing {0}", file);
                Dex dex = Dex.Read(file);

                foreach (ClassDefinition @class in dex.Classes)
                    SortAndCheck(@class.Fields, new FieldDefinitionComparer());
            }
        }

        [TestMethod]
        public void TestTypeReferenceSort()
        {
            TestGlobalSort<TypeReference>((dex) => dex.TypeReferences, new TypeReferenceComparer());
        }

        [TestMethod]
        public void TestClassDefinitionTopologicalSort()
        {
            TestGlobalSort<ClassDefinition>((dex) => dex.Classes, new ClassDefinitionComparer());
        }

        [TestMethod]
        public void TestStringSort()
        {
            TestGlobalSort<string>((dex) => dex.Strings, new Dexer.IO.Collector.StringComparer());
        }

        [TestMethod]
        public void TestPrototypeSort()
        {
            TestGlobalSort<Prototype>((dex) => dex.Prototypes, new PrototypeComparer());
        }

    }
}