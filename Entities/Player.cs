using Aiv.Fast2D;
using MyFast2DGame;
using OpenTK;

namespace RescueSpacey.Entities
{
    public class Player : Entity
    {
        // Definizione degli stati possibili del giocatore
        public enum PlayerState
        {
            Normal,        // Stato normale
            Invulnerable,  // Stato invulnerabile dopo aver subito danni
            PoweredUp      // Stato potenziato, attivato da un power-up
        }

        public PlayerState CurrentState { get; private set; }  // Stato attuale del giocatore
        public int Atk { get; private set; }  // Potere d'attacco del giocatore
        public float Invulnerability { get; set; }  // Durata dell'invulnerabilità in secondi
        public float PoweredUpDuration { get; set; }  // Durata dello stato potenziato in secondi
        private float stateTimer;  // Timer per gestire i tempi degli stati (invulnerabile o potenziato)
        private float fireRate;  // Frequenza di fuoco del giocatore
        private float fireCooldown;  // Tempo di cooldown tra un colpo e l'altro

        // Percorsi delle texture per i diversi stati del giocatore
        private static readonly string NormalTexture = "Assets/player.gif";
        private static readonly string InvincibleTexture = "Assets/player2.gif";
        private static readonly string PoweredUpTexture = "Assets/player3.gif";

        // Costruttore del player, inizializza la posizione, i punti vita, l'attacco e i tempi degli stati
        public Player(Vector2 initialPosition, int hp, int atk)
            : base(NormalTexture, initialPosition, hp)
        {
            this.Atk = atk;  // Imposta il valore di attacco
            this.Invulnerability = 3f;  // Durata di base dell'invulnerabilità è di 3 secondi
            this.PoweredUpDuration = 5f;  // Durata di base dello stato potenziato è di 5 secondi
            this.stateTimer = 0f;  // Il timer degli stati parte da zero
            this.CurrentState = PlayerState.Normal;  // Il giocatore inizia nello stato normale
            fireRate = 0.5f;  // Il giocatore può sparare ogni mezzo secondo
            fireCooldown = 0f;  // Il cooldown del fuoco parte da zero
        }

        // Metodo di aggiornamento del giocatore, chiamato ad ogni frame
        public override void Update()
        {
            // Gestisce il timer per il cambiamento dello stato
            if (stateTimer > 0f)
            {
                stateTimer -= Game.window.DeltaTime;  // Diminuisce il timer in base al tempo trascorso
                if (stateTimer <= 0f)
                {
                    // Quando il timer scade, il giocatore torna allo stato normale
                    ChangeState(PlayerState.Normal);
                }
            }

            // Logica di aggiornamento basata sullo stato corrente
            switch (CurrentState)
            {
                case PlayerState.Normal:
                    HandleNormalState();  // Gestisce il comportamento nello stato normale
                    break;
                case PlayerState.Invulnerable:
                    HandleInvulnerableState();  // Gestisce il comportamento nello stato invulnerabile
                    break;
                case PlayerState.PoweredUp:
                    HandlePoweredUpState();  // Gestisce il comportamento nello stato potenziato
                    break;
            }

            // Gestione del fuoco dei proiettili (sparo)
            if (fireCooldown <= 0f && Game.window.GetKey(KeyCode.X))
            {
                Shoot();  // Il giocatore spara un proiettile
                fireCooldown = fireRate;  // Reset del cooldown per impedire il fuoco continuo
            }

            if (fireCooldown > 0f)
            {
                fireCooldown -= Game.window.DeltaTime;  // Riduce il cooldown del fuoco
            }
        }

        // Gestisce il movimento e l'input del giocatore nello stato normale
        private void HandleNormalState()
        {
            Vector2 newAcceleration = Vector2.Zero;  // Resetta l'accelerazione prima di ogni frame
            float accelerationValue = 150f;  // Valore dell'accelerazione base

            // Imposta l'accelerazione in base all'input del giocatore
            if (Game.window.GetKey(KeyCode.Right))
                newAcceleration.X = accelerationValue;  // Muove il giocatore a destra
            if (Game.window.GetKey(KeyCode.Left))
                newAcceleration.X = -accelerationValue;  // Muove il giocatore a sinistra
            if (Game.window.GetKey(KeyCode.Up))
                newAcceleration.Y = -accelerationValue;  // Muove il giocatore in alto
            if (Game.window.GetKey(KeyCode.Down))
                newAcceleration.Y = accelerationValue;  // Muove il giocatore in basso

            Acceleration = newAcceleration;  // Aggiorna l'accelerazione del giocatore
            base.Update();  // Chiama il metodo Update della classe base Entity
        }

        // Gestisce il comportamento del giocatore nello stato invulnerabile
        private void HandleInvulnerableState()
        {
            HandleNormalState();  // Il giocatore si comporta come nello stato normale
        }

        // Gestisce il comportamento del giocatore nello stato potenziato
        private void HandlePoweredUpState()
        {
            HandleNormalState();  // Il comportamento dello stato potenziato è simile a quello normale
        }

        // Cambia lo stato del giocatore (invulnerabile, potenziato, normale) e imposta i relativi timer e texture
        public void ChangeState(PlayerState newState)
        {
            CurrentState = newState;  // Aggiorna lo stato corrente

            switch (newState)
            {
                case PlayerState.Invulnerable:
                    stateTimer = Invulnerability;  // Imposta il timer per l'invulnerabilità
                    Texture = new Texture(InvincibleTexture);  // Cambia la texture durante l'invulnerabilità
                    break;
                case PlayerState.PoweredUp:
                    stateTimer = PoweredUpDuration;  // Imposta il timer per lo stato potenziato
                    Texture = new Texture(PoweredUpTexture);  // Cambia la texture durante il potenziamento
                    break;
                case PlayerState.Normal:
                    Texture = new Texture(NormalTexture);  // Ripristina la texture normale
                    break;
            }
        }

        // Override del metodo TakeDamage per implementare l'invulnerabilità
        public override void TakeDamage(int damage)
        {
            // Il giocatore subisce danni solo se non è nello stato invulnerabile
            if (CurrentState != PlayerState.Invulnerable)
            {
                base.TakeDamage(damage);  // Chiama il metodo TakeDamage della classe base Entity
                ChangeState(PlayerState.Invulnerable);  // Cambia lo stato in invulnerabile dopo aver subito danni
            }
        }

        // Metodo per sparare proiettili
        private void Shoot()
        {
            // Trova un proiettile inattivo nella lista delle entità inattive
            Bullet bullet = (Bullet)Game.inactiveEntities.Find(e => e is Bullet);

            if (bullet != null)
            {
                // Posiziona il proiettile di fronte al giocatore
                bullet.Position = new Vector2(Position.X + Sprite.Width, Position.Y + Sprite.Height / 2);

                // Rimuove il proiettile dalla lista inattiva e lo aggiunge a quella attiva
                Game.inactiveEntities.Remove(bullet);
                Game.activeEntities.Add(bullet);
            }
        }
    }
}
