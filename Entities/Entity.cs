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
    // Classe base per tutte le entità nel gioco
    public class Entity
    {
        public Vector2 Position { get; set; }
        public Sprite Sprite { get; set; }
        public Texture Texture { get; set; }
        public int Hp { get; private set; }  // Punti vita (hp)

        public Entity(string texturePath, Vector2 initialPosition, int hp)
        {
            // Carica la texture e crea lo sprite
            Texture = new Texture(texturePath);
            Sprite = new Sprite(Texture.Width, Texture.Height);
            Position = initialPosition;
            Sprite.position = Position;
            Hp = hp;  // Imposta i punti vita
        }

        // Metodo per subire danni
        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                Destroy();  // Se i punti vita scendono a 0 o sotto, distrugge l'entità
            }
        }

        // Metodo per distruggere l'entità
        public virtual void Destroy()
        {
            Console.WriteLine($"{this.GetType().Name} è stato distrutto!");

            // Rimuove l'entità dalla lista delle entità attive
            Game.activeEntities.Remove(this);
        }

        // Metodo per aggiornare la posizione dell'entità
        public virtual void Update()
        {
            if (Sprite != null)
            {
                Sprite.position = Position;
            }
        }

        // Metodo per disegnare l'entità
        public virtual void Draw()
        {
            if (Sprite != null && Texture != null)
            {
                Sprite.DrawTexture(Texture);
            }
        }

        // Metodo per verificare se due entità stanno collidendo (Bounding Box)
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

            // Verifica la sovrapposizione dei rettangoli
            return !(thisLeft > otherRight || thisRight < otherLeft || thisTop > otherBottom || thisBottom < otherTop);
        }
    }
}
