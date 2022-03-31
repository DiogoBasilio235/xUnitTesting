using System;
using Xunit;

namespace GameEngine.Tests
{
    public class NonPlayerCharacterShould
    {
		// With the property "MemberData", we can fetch the test cases we want, stating the nameof "Class.TestData"
		// and specifying the MemberType with the class name (InternalHealthDamageTestData)
		// To share the test data with PlayerCharacterShould class, we need to insert the MemberData property on the test
		/*
			If we want to make use of an external file with our test data, we can also use the MemberData property with a small change:
			We need to modify the csv file property "Copy to output directory: Always copy", and use the "Rebuild Solution"

			[MemberData(nameof(ExternalHealthDamageTestData.TestData), MemberType = typeof(ExternalHealthDamageTestData))]
		 */

		[Theory]
		[MemberData(nameof(InternalHealthDamageTestData.TestData), MemberType = typeof(InternalHealthDamageTestData))]
		//[MemberData(nameof(ExternalHealthDamageTestData.TestData), MemberType = typeof(ExternalHealthDamageTestData))]
		public void TakeDamage(int damage, int expectedHealth)
		{
			NonPlayerCharacter sut = new NonPlayerCharacter();

			sut.TakeDamage(damage);

			Assert.Equal(expectedHealth, sut.Health);
		}
	}
}
