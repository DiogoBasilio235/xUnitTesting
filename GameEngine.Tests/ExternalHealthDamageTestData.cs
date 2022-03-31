using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GameEngine.Tests
{
    // This time we will add our test cases from a *.csv file
    // We can also use this piece of code on a Custom Attribute (ref -> HealthDamageDataAttribute)
    public class ExternalHealthDamageTestData
    {
        public static IEnumerable<object[]> TestData
        {
            get
            {
                string[] csvLines = File.ReadAllLines("TestData.csv");

                var testCases = new List<object[]>();

                foreach (var csvLine in csvLines)
                {
                    IEnumerable<int> values = csvLine.Split(',').Select(int.Parse);

                    object[] testCase = values.Cast<object>().ToArray();

                    testCases.Add(testCase);
                }

                return testCases;
            }
        }
    }
}
