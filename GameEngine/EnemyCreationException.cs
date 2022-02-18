using System;
namespace GameEngine
{
    public class EnemyCreationException : Exception
    {
        public string RequestedEnemyName { get; set; }
        public EnemyCreationException(string message, string enemyName) : base(message)
        {
            RequestedEnemyName = enemyName;
        }
    }
}
