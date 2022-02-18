using System;
using Xunit;

namespace GameEngine.Tests
{
    public class BossEnemyShould
    {

		[Fact]
		[Trait("Category", "Enemy")]
		public void HaveCorrectPower()
		{
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
