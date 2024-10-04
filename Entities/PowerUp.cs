using MyFast2DGame;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RescueSpacey.Entities
{
    // Classe PowerUp che eredita da Entity
    public class PowerUp : Entity
    {
        public int Type { get; private set; }

        public PowerUp(int type, string texturePath, Vector2 initialPosition)
            : base(texturePath, initialPosition, 1) // Hp non rilevante per i power-up
        {
            Type = type;
        }

        public override void Update()
        {
            switch (Type)
            {
                case 1:
                    // Logica per PowerUp1 (ad esempio, nessun movimento)
                    break;
                case 2:
                    // Logica per PowerUp2 (ad esempio, un power-up che lampeggia o si muove)
                    break;
            }

            base.Update();
        }
    }
}
