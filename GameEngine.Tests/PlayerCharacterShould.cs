using System;
using Xunit;

namespace GameEngine.Tests
{
    public class PlayerCharacterShould
    {

		[Fact]
		public void BeInexperiencedWhenNew()
		{
			PlayerCharacter sut = new PlayerCharacter();        //sut = System Under Testing

			Assert.True(sut.IsNoob);        //It can also be changed to *.False()
		}

		[Fact]
		public void CalculateFullName()
		{
			PlayerCharacter sut = new PlayerCharacter();
			sut.FirstName = "Sarah";
			sut.LastName = "Smith";

			// PlayerCharacter object has FullName property
			// public string FullName => $"{FirstName} {LastName}";

			Assert.Equal("Sarah Smith", sut.FullName);

			//We can compare also case sensitivity with this method.
			Assert.Equal("Sarah Smith", sut.FullName, ignoreCase: true);

			Assert.StartsWith("Sarah", sut.FullName);
			Assert.EndsWith("Smith", sut.LastName);
			Assert.Contains("ah Sm", sut.FullName);
			Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", sut.FullName);
		}

		[Fact]
		public void StartWithDefaultHealth()
		{
			PlayerCharacter sut = new PlayerCharacter();

			Assert.Equal(100, sut.Health);
			Assert.NotEqual(0, sut.Health);
		}

		[Fact]
		public void IncreaseHealthAfterSleeping()
		{
			PlayerCharacter sut = new PlayerCharacter();

			sut.Sleep();    //Expect increase between 1 to 100 inclusive

			Assert.True(sut.Health >= 101 && sut.Health <= 200);
			Assert.InRange<int>(sut.Health, 101, 200);
		}

		[Fact]
		public void NotHaveNicknameByDefault()
		{
			PlayerCharacter sut = new PlayerCharacter();

			Assert.Null(sut.Nickname);  //We can also run *.NotNull()
		}


		[Fact]
		public void HaveLongBow()
		{
			PlayerCharacter sut = new PlayerCharacter();

			Assert.Contains("Long Bow", sut.Weapons);
		}

		[Fact]
		public void NotHaveStaffOfWonder()
		{
			PlayerCharacter sut = new PlayerCharacter();

			Assert.DoesNotContain("Staff Of Wonder", sut.Weapons);
		}

		[Fact]
		public void HaveAtLeastOneKindOfSword()
		{
			PlayerCharacter sut = new PlayerCharacter();

			//Here we are overloading a method that the first parameter is a collection
			// and the second parameter is a predicate.
			//We need to specify that the collection contains an object that matches the predicate.
			//For that we use a Lambda expression. "weapon" represents the string containing the description of the weapon
			// and that the weapon description contains the substring "Sword".
			Assert.Contains(sut.Weapons, weapon => weapon.Contains("Sword"));
		}

		[Fact]
		public void HaveAllExpectedCollection()
		{
			PlayerCharacter sut = new PlayerCharacter();

			var expectedWeapons = new[] {
			"Long Bow",
			"Short Bow",
			"Short Sword"
		};

			Assert.Equal(expectedWeapons, sut.Weapons);
		}

		[Fact]
		public void HaveNoEmptyDefaultWeapons()
		{
			PlayerCharacter sut = new PlayerCharacter();

			//The *.All() method is going to loop through all of the weapons 
			// and is going to Assert.False() method against every item of the collection.
			//In this case we are testing that no weapon in the collection is null or is just set to white space. 
			Assert.All(sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));
		}

		[Fact]
		public void RaiseSleptEvent()
		{
			PlayerCharacter sut = new PlayerCharacter();

			Assert.Raises<EventArgs>(
				handler => sut.PlayerSlept += handler,  //action which attaches the argument to the event(PlayerSlept)
				handler => sut.PlayerSlept -= handler,  //action which detaches the argument from the event(PlayerSlept)
				() => sut.Sleep()                       //action which will cause the event to be raised
			);
		}

		[Fact]
		public void RaisePropertyChangedEvent()
		{
			PlayerCharacter sut = new PlayerCharacter();

			//If the interface "INotifyPropertyChanged" is used in our class we can use a special version
			// of event Assert to check specifically for the PropertyChanged events.
			Assert.PropertyChanged(sut,		//Object that is going to raise the PropertyChangedEvent
			"Health",						// Name of the property for which the change notification should be raised
			() => sut.TakeDamage(10)		//Action that should cause the PropertyChangedEvent to fire
			);
		}
	}
}
