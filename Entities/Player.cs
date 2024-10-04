using Aiv.Fast2D;
using MyFast2DGame;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueSpacey.Entities
{
    // Classe Player che eredita da Entity
    public class Player : Entity
    {
        public Player(string texturePath, Vector2 initialPosition, int hp)
            : base(texturePath, initialPosition, hp)
        {
        }

        public override void Update()
        {
            // Movimento con frecce direzionali
            if (Game.window.GetKey(KeyCode.Right))
                Position = new Vector2(Position.X + 200 * Game.window.DeltaTime, Position.Y);
            if (Game.window.GetKey(KeyCode.Left))
                Position = new Vector2(Position.X - 200 * Game.window.DeltaTime, Position.Y);
            if (Game.window.GetKey(KeyCode.Up))
                Position = new Vector2(Position.X, Position.Y - 200 * Game.window.DeltaTime);
            if (Game.window.GetKey(KeyCode.Down))
                Position = new Vector2(Position.X, Position.Y + 200 * Game.window.DeltaTime);

            base.Update(); // Aggiorna la posizione dello sprite
        }

    }
}
