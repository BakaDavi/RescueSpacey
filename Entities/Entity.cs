﻿using Aiv.Fast2D;
using MyFast2DGame;
using OpenTK;
using System;

namespace RescueSpacey.Entities
{
    // Classe base per tutte le entità nel gioco
    public class Entity
    {
        public Vector2 Position { get; set; }   // Posizione corrente dell'entità
        public Vector2 Velocity { get; set; }   // Velocità corrente dell'entità
        public Vector2 Acceleration { get; set; }   // Accelerazione applicata all'entità
        public float MaxSpeed { get; set; }     // Velocità massima dell'entità
        public float Friction { get; set; }     // Coefficiente di attrito

        public Sprite Sprite { get; set; }  // Sprite associato all'entità
        public Texture Texture { get; set; }  // Texture associata allo sprite
        public int Hp { get; private set; }   // Punti vita dell'entità

        // Costruttore della classe Entity, che accetta il percorso della texture, la posizione iniziale e i punti vita
        public Entity(string texturePath, Vector2 initialPosition, int hp)
        {
            // Carica la texture e lo sprite associato
            Texture = new Texture(texturePath);
            Sprite = new Sprite(Texture.Width, Texture.Height);
            Position = initialPosition;
            Sprite.position = Position;  // Imposta la posizione iniziale dello sprite
            Hp = hp;  // Imposta i punti vita dell'entità

            Velocity = new Vector2(0, 0);  // Imposta la velocità iniziale a 0
            Acceleration = new Vector2(0, 0);  // Imposta l'accelerazione iniziale a 0
            MaxSpeed = 250f;  // Imposta una velocità massima di default
            Friction = 0.99f;  // Coefficiente di attrito di default
        }

        // Metodo per aggiornare la posizione dell'entità in base alla velocità e accelerazione
        public virtual void Update()
        {
            // Aggiorna la velocità con l'accelerazione e limita la velocità alla velocità massima
            Velocity += Acceleration * Game.window.DeltaTime;

            // Se l'accelerazione è zero, applica l'attrito per rallentare l'entità
            if (Acceleration.Length == 0)
            {
                Velocity *= Friction;  // Applica l'attrito alla velocità
            }

            // Se la velocità eccede la velocità massima, viene normalizzata e ridotta alla velocità massima
            if (Velocity.Length > MaxSpeed)
            {
                Velocity = Vector2.Normalize(Velocity) * MaxSpeed;
            }

            // Se la velocità è molto bassa, la azzera completamente per fermare l'entità
            if (Velocity.Length < 0.1f)
            {
                Velocity = Vector2.Zero;
            }

            // Aggiorna la posizione in base alla velocità
            Position += Velocity * Game.window.DeltaTime;

            // Aggiorna la posizione dello sprite in base alla nuova posizione dell'entità
            Sprite.position = Position;
        }

        // Metodo per disegnare l'entità sullo schermo
        public virtual void Draw()
        {
            if (Sprite != null && Texture != null)
            {
                Sprite.DrawTexture(Texture);  // Disegna la texture associata allo sprite
            }
        }

        // Metodo per distruggere l'entità, rimuovendola dalla lista delle entità attive
        public virtual void Destroy()
        {
            Console.WriteLine($"{this.GetType().Name} è stato distrutto!");
            Game.activeEntities.Remove(this);  // Rimuove l'entità dalla lista delle entità attive
        }

        // Metodo per infliggere danni all'entità
        public virtual void TakeDamage(int damage)
        {
            Hp -= damage;  // Riduce i punti vita dell'entità
            if (Hp <= 0)
            {
                Destroy();  // Se i punti vita scendono a 0 o sotto, distrugge l'entità
            }
        }

        // Metodo per verificare se questa entità sta collidendo con un'altra entità
        public bool CheckCollision(Entity other)
        {
            float thisLeft = this.Position.X;
            float thisRight = this.Position.X + this.Sprite.Width;
            float thisTop = this.Position.Y;
            float thisBottom = this.Position.Y + this.Sprite.Height;

            float otherLeft = other.Position.X;
            float otherRight = other.Position.X + other.Sprite.Width;
            float otherTop = other.Position.Y;
            float otherBottom = other.Position.Y + other.Sprite.Height;

            // Controlla se i bounding box delle due entità si sovrappongono
            return !(thisLeft > otherRight || thisRight < otherLeft || thisTop > otherBottom || thisBottom < otherTop);
        }
    }
}
