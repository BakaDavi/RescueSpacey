using MyFast2DGame;
using OpenTK;

namespace RescueSpacey.Entities
{
    public class Bullet : Entity
    {
        // Percorso statico per la texture del proiettile
        private static readonly string BulletTexturePath = "Assets/bullet.png";

        // Posizione iniziale nullabile (Vector2? permette di avere null)
        private Vector2? InitialPosition = null;

        public Bullet(Vector2 initialPosition)
            : base(BulletTexturePath, initialPosition, 1)
        {
            MaxSpeed = 300f; // Imposta una velocità elevata per i proiettili
            Velocity = new Vector2(MaxSpeed, 0);  // I proiettili si muovono verso destra
            Friction = 1;  // Nessun attrito per i proiettili
        }

        public override void Update()
        {
            // Se InitialPosition è null, impostala alla posizione corrente
            if (InitialPosition == null)
            {
                InitialPosition = Position;  // Assegna tutta la posizione come Vector2
            }

            base.Update();  // Aggiorna la posizione del proiettile

            // Se il proiettile ha percorso più di 300 pixel a destra rispetto alla posizione iniziale
            if (Position.X - InitialPosition.Value.X > 300f)
            {
                Destroy();
            }
        }

        public override void Destroy()
        {
            InitialPosition = null;  // Resetta la posizione iniziale

            // Rimuove il proiettile dalla lista attiva e lo aggiunge alla lista inattiva
            Game.activeEntities.Remove(this);
            Game.inactiveEntities.Add(this);
        }
    }
}
