using Aiv.Fast2D;
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
        public int Hp { get; private set; } // Punti vita (hp)

        public Entity(string texturePath, Vector2 initialPosition, int hp)
        {
            // Carica la texture e crea lo sprite
            Texture = new Texture(texturePath);
            Sprite = new Sprite(Texture.Width, Texture.Height);
            Position = initialPosition;
            Sprite.position = Position;
            Hp = hp; // Imposta i punti vita
        }

        // Metodo per subire danni
        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp <= 0)
            {
                Destroy(); // Se hp scende a 0 o sotto, l'entità viene distrutta
            }
        }

        // Metodo per distruggere l'entità
        public virtual void Destroy()
        {
            // Distrugge l'entità (potrebbe includere animazioni o effetti)
            // Nel caso più semplice, potresti rimuovere l'entità dalla lista di entità attive
            Sprite = null; // Rimuove lo sprite
            Texture = null; // Rimuove la texture
        }

        // Metodo per aggiornare la posizione
        public virtual void Update()
        {
            if (Sprite != null)
            {
                Sprite.position = Position;
            }
        }

        // Metodo per disegnare lo sprite
        public virtual void Draw()
        {
            if (Sprite != null && Texture != null)
            {
                Sprite.DrawTexture(Texture);
            }
        }
    }
}
