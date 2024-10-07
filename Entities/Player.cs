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
            // Reset dell'accelerazione a ogni frame
            Vector2 newAcceleration = Vector2.Zero;  // Crea una nuova accelerazione

            // Valore di accelerazione unificato per tutte le direzioni
            float accelerationValue = 150f;

            // Accelera in base all'input, mantenendo lo stesso valore per tutte le direzioni
            if (Game.window.GetKey(KeyCode.Right))
                newAcceleration.X = accelerationValue;  // Accelerazione verso destra
            if (Game.window.GetKey(KeyCode.Left))
                newAcceleration.X = -accelerationValue;  // Accelerazione verso sinistra
            if (Game.window.GetKey(KeyCode.Up))
                newAcceleration.Y = -accelerationValue;  // Accelerazione verso l'alto
            if (Game.window.GetKey(KeyCode.Down))
                newAcceleration.Y = accelerationValue;  // Accelerazione verso il basso

            // Imposta la nuova accelerazione tramite la proprietà della classe base
            Acceleration = newAcceleration;

            // Chiama il metodo Update della classe base per aggiornare la posizione e la velocità
            base.Update();
        }
    }
}
