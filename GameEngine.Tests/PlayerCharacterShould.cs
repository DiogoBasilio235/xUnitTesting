using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class PlayerCharacterShould : IDisposable
    {
		//  We will reduce the amount of times we Arrange the PlayerCharacter
		private readonly PlayerCharacter _sut;

		private readonly ITestOutputHelper _output;

		public PlayerCharacterShould(ITestOutputHelper output) {
			// To be used, we need to create an instance of the PlayerCharacter
			// Then we delete all occurrences on each test and rename them to _sut
			_sut = new PlayerCharacter();

			_output = output;
			_output.WriteLine("Creating new PlayerCharacter");
		}

		// After the IDisposable interface is added to the class, we need to implement it
		// This will only be run at the end of the tests
		public void Dispose()
        {
			_output.WriteLine("Disposing PlayerCharecter {_sut.FullName}");

			// Method to Dispose 
			//_sut.Dispose();

        }

		[Fact]
		public void BeInexperiencedWhenNew()
		{
			//sut = System Under Testing

			Assert.True(_sut.IsNoob);        //It can also be changed to *.False()
		}

		[Fact]
		public void CalculateFullName()
		{
			_sut.FirstName = "Sarah";
			_sut.LastName = "Smith";

			// PlayerCharacter object has FullName property
			// public string FullName => $"{FirstName} {LastName}";

			Assert.Equal("Sarah Smith", _sut.FullName);

			//We can compare also case sensitivity with this method.
			Assert.Equal("Sarah Smith", _sut.FullName, ignoreCase: true);

			Assert.StartsWith("Sarah", _sut.FullName);
			Assert.EndsWith("Smith", _sut.LastName);
			Assert.Contains("ah Sm", _sut.FullName);
			Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", _sut.FullName);
		}

		[Fact]
		public void StartWithDefaultHealth()
		{
			Assert.Equal(100, _sut.Health);
			Assert.NotEqual(0, _sut.Health);
		}

		[Fact]
		public void IncreaseHealthAfterSleeping()
		{

			_sut.Sleep();    //Expect increase between 1 to 100 inclusive

			Assert.True(_sut.Health >= 101 && _sut.Health <= 200);
			Assert.InRange<int>(_sut.Health, 101, 200);
		}

		[Fact]
		public void NotHaveNicknameByDefault()
		{

			Assert.Null(_sut.Nickname);  //We can also run *.NotNull()
		}


		[Fact]
		public void HaveLongBow()
		{

			Assert.Contains("Long Bow", _sut.Weapons);
		}

		[Fact]
		public void NotHaveStaffOfWonder()
		{

			Assert.DoesNotContain("Staff Of Wonder", _sut.Weapons);
		}

		[Fact]
		public void HaveAtLeastOneKindOfSword()
		{
			//Here we are overloading a method that the first parameter is a collection
			// and the second parameter is a predicate.
			//We need to specify that the collection contains an object that matches the predicate.
			//For that we use a Lambda expression. "weapon" represents the string containing the description of the weapon
			// and that the weapon description contains the substring "Sword".
			Assert.Contains(_sut.Weapons, weapon => weapon.Contains("Sword"));
		}

		[Fact]
		public void HaveAllExpectedCollection()
		{
			var expectedWeapons = new[] {
			"Long Bow",
			"Short Bow",
			"Short Sword"
		};

			Assert.Equal(expectedWeapons, _sut.Weapons);
		}

		[Fact]
		public void HaveNoEmptyDefaultWeapons()
		{
			//The *.All() method is going to loop through all of the weapons 
			// and is going to Assert.False() method against every item of the collection.
			//In this case we are testing that no weapon in the collection is null or is just set to white space. 
			Assert.All(_sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));
		}

		[Fact]
		public void RaiseSleptEvent()
		{

			Assert.Raises<EventArgs>(
				handler => _sut.PlayerSlept += handler,  //action which attaches the argument to the event(PlayerSlept)
				handler => _sut.PlayerSlept -= handler,  //action which detaches the argument from the event(PlayerSlept)
				() => _sut.Sleep()                       //action which will cause the event to be raised
			);
		}

		[Fact]
		public void RaisePropertyChangedEvent()
		{

			//If the interface "INotifyPropertyChanged" is used in our class we can use a special version
			// of event Assert to check specifically for the PropertyChanged events.
			Assert.PropertyChanged(_sut,		//Object that is going to raise the PropertyChangedEvent
			"Health",						// Name of the property for which the change notification should be raised
			() => _sut.TakeDamage(10)		//Action that should cause the PropertyChangedEvent to fire
			);
		}

		/*
		 ********************************************************************************************************************************** 
		*/

		// The following 4 tests show small amounts of damage being taken from the PlayerCharacter
		// We will refactor them to Data-Driven tests to reduce code duplication.
		// They will be kept for learning purposes
		[Fact]
		public void TakeZeroDamage()
        {
			_sut.TakeDamage(0);
			Assert.Equal(100, _sut.Health);
        }

		[Fact]
		public void TakeSmallDamage()
		{
			_sut.TakeDamage(1);
			Assert.Equal(99, _sut.Health);
		}

		[Fact]
		public void TakeMediumDamage()
		{
			_sut.TakeDamage(50);
			Assert.Equal(50, _sut.Health);
		}

		[Fact]
		public void HaveMinimum1Health()
		{
			_sut.TakeDamage(101);
			Assert.Equal(1, _sut.Health);
		}

		// To create a Data-driven Test, we use Xunit's Theory attribute, which will tell that this test needs to be executed multiple times.
		// In this case we insert "int damage" into the test as well as "int expectedHealth" to assert the final result.
		// We use "[InlineData]" attribute, to specify the data being tested
        // but it just belongs to this specific test, it cant be shared with other tests

		[Theory]
		[InlineData(0, 100)] // Parameters to replace the first of the 4 tests above
		[InlineData(1, 99)]  // Parameters to replace the second of the 4 tests above
		[InlineData(50, 50)] // Parameters to replace the third of the 4 tests above
		[InlineData(101, 1)] // Parameters to replace the fourth of the 4 tests above
		public void TakeDamage(int damage, int expectedHealth)
		{
			_sut.TakeDamage(damage);
			Assert.Equal(expectedHealth, _sut.Health);
		}

		/*
		 ********************************************************************************************************************************** 
		*/

		//Name of this test case was changed with _1 to prevent conflicts
		// With the use of a custom attribute (HealthDamageDataAttribute), we can include our different scenarios
        // with a simple property above our test, in this case [HealthDamageData]
		[Theory]
		[HealthDamageData]
		public void TakeDamage_1(int damage, int expectedHealth)
		{
			_sut.TakeDamage(damage);
			Assert.Equal(expectedHealth, _sut.Health);
		}

}
}
