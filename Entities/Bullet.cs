using MyFast2DGame;
using OpenTK;

namespace RescueSpacey.Entities
{
    public class Bullet : Entity
    {
        // Percorso statico per la texture del proiettile
        private static readonly string BulletTexturePath = "C:\\Users\\Baka\\source\\repos\\RescueSpacey\\Assets\\bullet.png";

        public Bullet(Vector2 initialPosition)
            : base(BulletTexturePath, initialPosition, 1)  // Usa il percorso della texture e HP minimi
        {
            MaxSpeed = 300f;  // Imposta una velocità elevata per i proiettili
            Velocity = new Vector2(MaxSpeed, 0);  // I proiettili si muovono verso destra
        }

        public override void Update()
        {
            base.Update();  // Aggiorna la posizione del proiettile

            // Se il proiettile esce dalla finestra di gioco, torna alla lista inattiva
            if (Position.X > Game.window.Width)
            {
                Destroy();
            }
        }

        public override void Destroy()
        {
            // Sposta il proiettile dalla lista attiva a quella inattiva
            Game.activeEntities.Remove(this);
            Game.inactiveEntities.Add(this);
        }
    }
}
