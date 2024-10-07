using MyFast2DGame;
using OpenTK;
using System;

namespace RescueSpacey.Entities
{
    // Classe Enemy che eredita da Entity e rappresenta i nemici
    public class Enemy : Entity
    {
        public int Type { get; private set; }  // Tipo di nemico
        public int Atk { get; private set; }   // Danno inflitto dal nemico

        // Costruttore che accetta il tipo, il percorso della texture, posizione iniziale, punti vita e attacco
        public Enemy(int type, string texturePath, Vector2 initialPosition, int hp, int atk)
            : base(texturePath, initialPosition, hp)
        {
            Type = type;  // Imposta il tipo di nemico
            Atk = atk;  // Imposta il danno del nemico
            MaxSpeed = (type == 1) ? 100f : 200f;  // Imposta velocità massima specifica per tipo di nemico
        }

        // Aggiorna lo stato del nemico in base al tipo
        public override void Update()
        {
            // Logica di movimento del nemico in base al tipo
            switch (Type)
            {
                case 1:
                    Acceleration = new Vector2(0, 10f);  // Movimento lento verso il basso
                    break;
                case 2:
                    Acceleration = new Vector2(-30f, 0);  // Movimento veloce verso sinistra
                    break;
            }

            base.Update();  // Chiama il metodo Update della classe base per aggiornare posizione e velocità
        }
    }
}
