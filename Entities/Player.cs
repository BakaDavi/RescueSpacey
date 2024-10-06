using Aiv.Fast2D;
using MyFast2DGame;
using OpenTK;
using System;

namespace RescueSpacey.Entities
{
    public class Player : Entity
    {
        private int health;  // Attributo per la salute del player

        public Player(string texturePath, Vector2 initialPosition, int hp)
            : base(texturePath, initialPosition, hp)
        {
            health = hp;  // Imposta la salute iniziale del player
        }

        public override void Update()
        {
            // Movimento con frecce direzionali
            Vector2 newPosition = Position;  // Crea una copia della posizione corrente

            if (Game.window.GetKey(KeyCode.Right))
                newPosition.X += 200 * Game.window.DeltaTime;
            if (Game.window.GetKey(KeyCode.Left))
                newPosition.X -= 200 * Game.window.DeltaTime;
            if (Game.window.GetKey(KeyCode.Up))
                newPosition.Y -= 200 * Game.window.DeltaTime;
            if (Game.window.GetKey(KeyCode.Down))
                newPosition.Y += 200 * Game.window.DeltaTime;

            Position = newPosition;  // Assegna la nuova posizione

            base.Update();  // Richiama il metodo Update della classe base
        }
    }
}
