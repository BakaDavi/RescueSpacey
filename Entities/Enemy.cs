using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueSpacey.Entities
{
    public class Enemy : Entity
    {
        public int Type { get; private set; }

        public Enemy(int type, string texturePath, Vector2 initialPosition, int hp)
            : base(texturePath, initialPosition, hp)
        {
            Type = type;
        }

        public override void Update()
        {
            switch (Type)
            {
                case 1:
                    // Logica per Enemy1 (ad esempio, si muove lentamente)
                    break;
                case 2:
                    // Logica per Enemy2 (ad esempio, si muove più velocemente)
                    break;
            }

            base.Update();
        }
    }

}
