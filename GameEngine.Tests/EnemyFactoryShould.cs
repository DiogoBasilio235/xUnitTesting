using System;
using Xunit;
namespace GameEngine.Tests
{
    public class EnemyFactoryShould
    {
		[Fact]
		public void CreateNormalEnemyByDefault()
		{

			EnemyFactory sut = new EnemyFactory();
			Enemy enemy = sut.Create("Zombie");

			Assert.IsType<NormalEnemy>(enemy); // can also be run with *.IsNotType<DateTime>()
		}

		[Fact]
		public void CreateBossEnemy()
		{
			EnemyFactory sut = new EnemyFactory();
			Enemy enemy = sut.Create("Zombie King", true);

			Assert.IsType<BossEnemy>(enemy);
		}

		[Fact]
		public void CreateBossEnemy_CastReturnedTypeExample()
		{
			EnemyFactory sut = new EnemyFactory();
			Enemy enemy = sut.Create("Zombie King", true);

			//Assert and get cast result
			BossEnemy boss = Assert.IsType<BossEnemy>(enemy);

			//Additional asserts on typed object
			Assert.Equal("Zombie King", boss.Name);
		}

		[Fact]
		public void CreateBossEnemy_AssertAssignableTypes()
		{
			EnemyFactory sut = new EnemyFactory();
			Enemy enemy = sut.Create("Zombie King", true);

			//*.IsAssignableFrom<>() will take into account any Inheritance.
			//Although enemy == BossEnemy, it still derives from Enemy object
			Assert.IsAssignableFrom<Enemy>(enemy);
		}

		[Fact]
		public void CreateSeparateInstances()
		{

			EnemyFactory sut = new EnemyFactory();

			Enemy enemy1 = sut.Create("Zombie");
			Enemy enemy2 = sut.Create("Zombie");

			//Verifies that these 2 objects are not the same instance
			Assert.NotSame(enemy1, enemy2);  // *.Same() can check if they are the same reference.
		}

		[Fact]
		public void NotAllowNullName()
		{
			EnemyFactory sut = new EnemyFactory();

			Assert.Throws<ArgumentNullException>(() => sut.Create(null));

			//The additional parameter "name" is the argument we are expecting to cause
			// the argument NullException
			Assert.Throws<ArgumentNullException>("name", () => sut.Create(null));
		}

		[Fact]
		public void OnlyAllowKingOrQueenBossEnemies()
		{
			EnemyFactory sut = new EnemyFactory();

			//As the name doesn't end with "King" or "Queen", the EnemyCreationException is thrown.
			//With the variable "ex", we can make further Asserts with it.
			EnemyCreationException ex =
				Assert.Throws<EnemyCreationException>(() => sut.Create("Zombie", true));

			Assert.Equal("Zombie", ex.RequestedEnemyName);
		}
	}
}
