using System;
using System.Collections.Generic;

namespace GameEngine.Tests
{
    public class InternalHealthDamageTestData
    {
        // This is how we can create the same data to be tested on PlayerCharacterShould and NonPlayerCharacterShould
        // We can now expose this List of test data.
        // Xunit can read this test Data from either a property, a field or as the return type of a method.
        // In this example we are using the property
        public static IEnumerable<object[]> TestData 
        {
            get
            {
                yield return new object[] { 0, 100 };
                yield return new object[] { 1, 99 };
                yield return new object[] { 50, 50 };
                yield return new object[] { 101, 1 };
            }
        }


        
    }
}
