using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class BossEnemyShould
    {
		// This will create an Output to the Test Results, to get more info about the tests
		private readonly ITestOutputHelper _output;

		public BossEnemyShould(ITestOutputHelper output) {
			_output = output;
		}


		[Fact]
		[Trait("Category", "Boss")]	//This test now belongs to a Category with the value of Enemy
		public void HaveCorrectPower()
		{
			_output.WriteLine("Creating Boss Enemy"); // If run with "--logger:trx" on cmd, it will create a xml file with all details 
			BossEnemy sut = new BossEnemy();

			//This is the result of TotalSpecialPower / SpecialPowerUses (1000/6) specified on Enemy class
			//This test will fail because the actual value = 166.666666667
			//Assert.Equal(166.666, sut.SpecialAttackPower);

			//We can specify a precision of 3 decimal points if we want to.
			//This way the test passes. Behind the scenes there is Rounding being calculated on this test.
			Assert.Equal(166.667, sut.SpecialAttackPower, 3);
		}


	}
}