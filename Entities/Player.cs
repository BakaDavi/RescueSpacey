using Aiv.Fast2D;
using MyFast2DGame;
using OpenTK;

namespace RescueSpacey.Entities
{
    public class Player : Entity
    {
        public enum PlayerState
        {
            Normal,        // Stato normale
            Invulnerable,  // Stato invulnerabile dopo aver subito danni
            PoweredUp      // Stato potenziato, attivato da un power-up
        }

        public PlayerState CurrentState { get; private set; }  // Stato attuale del giocatore
        public int Atk { get; private set; }  // Potere d'attacco del giocatore
        public float Invulnerability { get; set; }  // Durata dell'invulnerabilità in secondi
        private float stateTimer;  // Timer per gestire i tempi degli stati
        private float fireRate;
        private float fireCooldown;

        public Player(string texturePath, Vector2 initialPosition, int hp, int atk)
            : base(texturePath, initialPosition, hp)
        {
            this.Atk = atk;  // Imposta il valore di attacco
            this.Invulnerability = 0.5f;  // Imposta l'invulnerabilità di base a 3 secondi
            this.stateTimer = 0f;  // Timer inizialmente a zero
            this.CurrentState = PlayerState.Normal;  // Stato iniziale: Normale
            fireRate = 0.5f;  // Imposta la frequenza di fuoco
            fireCooldown = 0f;  // Timer di cooldown per il fuoco
        }

        public override void Update()
        {
            // Gestione del timer dello stato attuale
            if (stateTimer > 0f)
            {
                stateTimer -= Game.window.DeltaTime;
                if (stateTimer <= 0f)
                {
                    // Quando il timer scade, torna allo stato normale
                    ChangeState(PlayerState.Normal);
                }
            }

            // Logica di aggiornamento basata sullo stato attuale
            switch (CurrentState)
            {
                case PlayerState.Normal:
                    HandleNormalState();  // Comportamento del giocatore in stato normale
                    break;
                case PlayerState.Invulnerable:
                    HandleInvulnerableState();  // Comportamento del giocatore in stato invulnerabile
                    break;
                case PlayerState.PoweredUp:
                    HandlePoweredUpState();  // Comportamento del giocatore potenziato
                    break;
            }

            // Gestione del fuoco dei proiettili
            if (fireCooldown <= 0f && Game.window.GetKey(KeyCode.X))
            {
                Shoot();  // Spara un proiettile
                fireCooldown = fireRate;  // Reset del cooldown
            }

            if (fireCooldown > 0f)
            {
                fireCooldown -= Game.window.DeltaTime;
            }
        }

        // Metodo per gestire lo stato normale
        private void HandleNormalState()
        {
            // Inizializza l'accelerazione a zero per questo frame
            Vector2 newAcceleration = Vector2.Zero;
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

            Acceleration = newAcceleration;  // Aggiorna l'accelerazione
            base.Update();  // Chiama il metodo Update della classe base
        }

        // Metodo per gestire lo stato invulnerabile
        private void HandleInvulnerableState()
        {
            // Il giocatore si comporta come nello stato normale ma non può subire danni
            HandleNormalState();
        }

        // Metodo per gestire lo stato potenziato
        private void HandlePoweredUpState()
        {
            // Per ora, lo stato potenziato è lo stesso di quello normale
            // Logica futura per lo stato potenziato può essere aggiunta qui
            HandleNormalState();
        }

        // Metodo per cambiare lo stato del giocatore
        private void ChangeState(PlayerState newState)
        {
            CurrentState = newState;

            switch (newState)
            {
                case PlayerState.Invulnerable:
                    stateTimer = Invulnerability;  // Imposta il timer per l'invulnerabilità
                    break;
                case PlayerState.PoweredUp:
                    // Logica futura per lo stato potenziato
                    break;
                case PlayerState.Normal:
                    // Nessun comportamento speciale per tornare allo stato normale
                    break;
            }
        }

        // Override del metodo TakeDamage per implementare l'invulnerabilità
        public override void TakeDamage(int damage)
        {
            if (CurrentState != PlayerState.Invulnerable)  // Il giocatore può subire danni solo se non è invulnerabile
            {
                base.TakeDamage(damage);  // Chiama la logica di TakeDamage della classe base
                ChangeState(PlayerState.Invulnerable);  // Cambia lo stato del giocatore in invulnerabile
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
