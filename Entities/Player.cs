using Aiv.Fast2D;
using MyFast2DGame;
using OpenTK;

namespace RescueSpacey.Entities
{
    public class Player : Entity
    {
        public int Atk { get; private set; }  // Proprietà pubblica per l'attacco
        private float fireRate;
        private float fireCooldown;

        public Player(string texturePath, Vector2 initialPosition, int hp, int atk)
            : base(texturePath, initialPosition, hp)
        {
            this.Atk = atk;  // Imposta il valore di attacco
            fireRate = 0.5f;  // Imposta la frequenza di fuoco
            fireCooldown = 0f;  // Timer di cooldown per il fuoco
        }

        public override void Update()
        {
            // Inizializza l'accelerazione a zero per questo frame
            Vector2 newAcceleration = Vector2.Zero;

            // Valore uniforme di accelerazione
            float accelerationValue = 150f;

            // Imposta l'accelerazione in base all'input dell'utente
            if (Game.window.GetKey(KeyCode.Right))
                newAcceleration.X = accelerationValue;  // Accelerazione verso destra
            if (Game.window.GetKey(KeyCode.Left))
                newAcceleration.X = -accelerationValue;  // Accelerazione verso sinistra
            if (Game.window.GetKey(KeyCode.Up))
                newAcceleration.Y = -accelerationValue;  // Accelerazione verso l'alto
            if (Game.window.GetKey(KeyCode.Down))
                newAcceleration.Y = accelerationValue;  // Accelerazione verso il basso

            // Imposta la nuova accelerazione nell'entità
            Acceleration = newAcceleration;

            // Chiama l'Update della classe base per aggiornare la posizione
            base.Update();

            // Gestione del fuoco dei proiettili
            if (fireCooldown <= 0f && Game.window.GetKey(KeyCode.X))
            {
                Shoot();  // Spara un proiettile
                fireCooldown = fireRate;  // Reset del cooldown
            }

            // Riduci il cooldown del fuoco
            if (fireCooldown > 0f)
            {
                fireCooldown -= Game.window.DeltaTime;
            }
        }

        private void Shoot()
        {
            // Trova un proiettile inattivo nella lista delle entità inattive
            Bullet bullet = (Bullet)Game.inactiveEntities.Find(e => e is Bullet);

            if (bullet != null)
            {
                // Posiziona il proiettile di fronte al player
                bullet.Position = new Vector2(Position.X + Sprite.Width, Position.Y + Sprite.Height / 2);

                // Rimuovi il proiettile dalla lista inattiva e aggiungilo alla lista attiva
                Game.inactiveEntities.Remove(bullet);
                Game.activeEntities.Add(bullet);
            }
        }
    }
}
