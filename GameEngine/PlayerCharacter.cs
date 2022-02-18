using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GameEngine
{
    public class PlayerCharacter : INotifyPropertyChanged
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public bool IsNoob { get; set; }
        public List<string> Weapons { get; set; }

        public string Nickname { get; set; }

        public int _health = 100;

        public event EventHandler<EventArgs> PlayerSlept;
        public event PropertyChangedEventHandler PropertyChanged;

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                OnPropertyChanged();
            }
        }

        public PlayerCharacter()
        {
            FirstName = GenerateRandomFirstName();
            IsNoob = true;
            CreateStartingWeapons();
        }

        public void Sleep()
        {
            var healthIncrease = CalculateHealthIncrease();
            Health += healthIncrease;

            OnPlayerSlept(EventArgs.Empty);
        }

        protected virtual void OnPlayerSlept(EventArgs e)
        {
            PlayerSlept?.Invoke(this, e);
        }

        public void TakeDamage(int damage)
        {
            Health = Math.Max(1, Health -= damage);
        }

        private int CalculateHealthIncrease()
        {
            var rnd = new Random();
            return rnd.Next(1, 101);
        }

        private void CreateStartingWeapons()
        {
            Weapons = new List<string> {
            "Long Bow",
            "Short Bow",
            "Short Sword"
            };
        }

        private string GenerateRandomFirstName()
        {
            var possibleRandomStartingNames = new[]
            {
                "Danieth",
                "Derick",
                "Shalnorr",
                "G'Toth'lop",
                "Boldrakteethshop"
            };

            return possibleRandomStartingNames[
                new Random().Next(0, possibleRandomStartingNames.Length)];
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
