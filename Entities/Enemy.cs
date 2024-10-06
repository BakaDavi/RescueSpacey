using MyFast2DGame;
using OpenTK;
using System;

namespace RescueSpacey.Entities
{
    public class Enemy : Entity
    {
        public int Type { get; private set; }
        public int Atk { get; private set; }  // Attributo che rappresenta l'attacco del nemico

        public Enemy(int type, string texturePath, Vector2 initialPosition, int hp, int atk)
            : base(texturePath, initialPosition, hp)
        {
            Type = type;
            Atk = atk;  // Imposta il valore dell'attacco
        }

        public override void Update()
        {
            switch (Type)
            {
                case 1:
                    // Logica per Enemy1 (si muove lentamente)
                    break;
                case 2:
                    // Logica per Enemy2 (si muove più velocemente)
                    break;
            }

            base.Update();
        }
    }
}
