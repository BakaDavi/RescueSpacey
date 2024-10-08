# Documentazione del Progetto Rescue Spacey

## Introduzione

Questo documento descrive lo stato attuale del progetto **Rescue Spacey**, un gioco 2D sviluppato utilizzando la libreria **AIV.Fast2D**. Il progetto include un **player**, due tipi di **nemici** e due tipi di **power-up**. Ogni entità ha comportamenti distinti e viene gestita attraverso classi che ereditano da una classe base chiamata **Entity**


## Struttura del Progetto

Il progetto è organizzato come segue:

```
/RescueSpacey
│
├── /Assets
│   ├── player.gif      # Immagine del player
│   ├── player2.gif      # Immagine del player invulnerabile
│   ├── player3.gif      # Immagine del player potenziato
│   ├── enemy1.png      # Immagine del nemico 1
│   ├── enemy2.png      # Immagine del nemico 2
│   ├── powerup1.png    # Immagine del power-up 1
│   └── powerup2.png    # Immagine del power-up 2
│   └── bullet.png      # Immagine del power-up 2
│
├── /Entities
│   ├── Entity.cs       # Classe base per tutte le entità
│   ├── Player.cs       # Classe specifica per il Player
│   ├── Enemy.cs        # Classe che gestisce tutti i tipi di nemici
│   └── PowerUp.cs      # Classe che gestisce tutti i tipi di power-up
│   └── Bullet.cs      # Classe che gestisce i proiettili del giocatore
│
├── Program.cs          # File principale che contiene il ciclo di gioco
└── /bin                # Contiene i file binari compilati
```

## **Entity**

### **Riassunto**:

`Entity` è la classe base da cui derivano tutte le entità del gioco. Gestisce proprietà comuni come posizione, velocità, accelerazione, attrito, punti vita e texture. Fornisce anche metodi per il movimento, il disegno sullo schermo, la gestione delle collisioni e la distruzione dell'entità.

### **Attributi**:

-   **Position** (`Vector2`): La posizione attuale dell'entità.
-   **Velocity** (`Vector2`): La velocità attuale dell'entità.
-   **Acceleration** (`Vector2`): L'accelerazione applicata all'entità.
-   **MaxSpeed** (`float`): Velocità massima consentita all'entità.
-   **Friction** (`float`): Coefficiente di attrito che rallenta l'entità.
-   **Sprite** (`Sprite`): Sprite associato all'entità.
-   **Texture** (`Texture`): Texture caricata dallo sprite.
-   **Hp** (`int`): Punti vita dell'entità.

### **Metodi**:

-   **Entity(string texturePath, Vector2 initialPosition, int hp)**: Costruttore che inizializza l'entità con una texture, una posizione e dei punti vita.
-   **void Update()**: Aggiorna la posizione dell'entità in base alla velocità e all'accelerazione.
-   **void Draw()**: Disegna l'entità sullo schermo utilizzando la sua texture e sprite.
-   **void Destroy()**: Rimuove l'entità dalla lista attiva e la sposta in quella inattiva.
-   **void TakeDamage(int damage)**: Riduce i punti vita dell'entità in base al danno subito. Se i punti vita scendono a zero, l'entità viene distrutta.
-   **bool CheckCollision(Entity other)**: Verifica se questa entità sta collidendo con un'altra entità usando i bounding box.
* * *

## **Player**

### **Riassunto**:

Il `Player` rappresenta il personaggio controllato dal giocatore. Oltre ai comportamenti di base delle entità, il `Player` ha una macchina a stati che gli consente di passare tra tre stati: normale, invulnerabile e potenziato. Il `Player` può subire danni, diventare invulnerabile e sparare proiettili.

### **Attributi**:

-   **Atk** (`int`): Potenza d'attacco del giocatore.
-   **CurrentState** (`PlayerState`): Stato attuale del giocatore (Normale, Invulnerabile, Potenziato).
-   **Invulnerability** (`float`): Durata dell'invulnerabilità in secondi.
-   **PoweredUpDuration** (`float`): Durata dello stato potenziato in secondi.
-   **fireRate** (`float`): Frequenza di fuoco del giocatore.
-   **fireCooldown** (`float`): Tempo di attesa tra un colpo e l'altro.
-   **normalTexture, invincibleTexture, poweredUpTexture** (`string`): Percorsi delle texture in base allo stato.

### **Metodi**:

-   **Player(Vector2 initialPosition, int hp, int atk)**: Costruttore che inizializza il `Player` con una posizione, punti vita e potenza d'attacco.
-   **void Update()**: Aggiorna lo stato del giocatore, gestisce il movimento, i timer degli stati e il fuoco.
-   **void ChangeState(PlayerState newState)**: Cambia lo stato del giocatore e imposta il timer e la texture corretti.
-   **void TakeDamage(int damage)**: Se il giocatore non è invulnerabile, subisce danni e passa allo stato invulnerabile.
-   **void Shoot()**: Trova un proiettile dalla lista inattiva e lo spara.
* * *

## **Bullet**

### **Riassunto**:

La classe `Bullet` rappresenta i proiettili sparati dal giocatore. Si muovono a una velocità costante e vengono distrutti dopo aver percorso 300 pixel.

### **Attributi**:

-   **MaxSpeed** (`float`): Velocità fissa del proiettile.
-   **initialPosition** (`Vector2?`): Posizione iniziale del proiettile.

### **Metodi**:

-   **Bullet(Vector2 initialPosition)**: Costruttore che inizializza il proiettile con la posizione iniziale.
-   **void Update()**: Aggiorna la posizione del proiettile e lo distrugge se percorre più di 300 pixel.
-   **void Destroy()**: Reimposta la posizione iniziale e rimuove il proiettile dalla lista attiva.
* * *

## **Enemy**

### **Riassunto**:

`Enemy` rappresenta i nemici del gioco, ognuno con punti vita e un valore di attacco. Diversi tipi di nemici hanno comportamenti di movimento differenti.

### **Attributi**:

-   **Type** (`int`): Tipo di nemico, che influisce sul movimento.
-   **Atk** (`int`): Valore di attacco del nemico.

### **Metodi**:

-   **Enemy(int type, string texturePath, Vector2 initialPosition, int hp, int atk)**: Costruttore che inizializza il nemico con il tipo, la posizione iniziale, i punti vita e il valore d'attacco.
-   **void Update()**: Aggiorna la posizione del nemico in base al tipo (movimento differente per ogni tipo).
* * *

## **PowerUp**

### **Riassunto**:

`PowerUp` rappresenta i potenziamenti che il giocatore può raccogliere. A seconda del tipo, il giocatore può diventare invulnerabile o potenziato.

### **Attributi**:

-   **Type** (`int`): Tipo di potenziamento (1 per invulnerabilità, 2 per potenziamento).

### **Metodi**:

-   **PowerUp(int type, string texturePath, Vector2 initialPosition)**: Costruttore che inizializza il potenziamento con il tipo e la posizione iniziale.
-   **void Update()**: Esegue la logica specifica per il tipo di potenziamento (ad esempio, movimento o effetti visivi).
* * *

## **Game**

### **Riassunto**:

La classe `Game` gestisce l'intero ciclo di gioco, incluse la creazione della finestra, il ciclo di aggiornamento e disegno delle entità e la gestione delle collisioni tra entità.

### **Attributi**:

-   **window** (`Window`): La finestra del gioco.
-   **activeEntities** (`List<Entity>`): Lista delle entità attive nel gioco.
-   **inactiveEntities** (`List<Entity>`): Lista delle entità inattive.

### **Metodi**:

-   **Game()**: Costruttore che inizializza la finestra, crea il giocatore, i nemici, i power-up e prealloca i proiettili.
-   **void Run()**: Metodo principale che avvia il ciclo di gioco.
-   **void Update()**: Aggiorna tutte le entità attive, gestisce le collisioni tra giocatore, nemici, proiettili e power-up.
-   **void Draw()**: Disegna tutte le entità attive sullo schermo.

* * *

### **7\. Todo**

1.  **Nemici**: Il nemico1 ha un moto sinusoidale che lo fa oscillare su e giù.
2.  **Livelli**: Lo scorrimento orizzontale avviene muovendo tutte le entità eccetto il player verso le ascisse negative. I livelli possono essere realizzati in modo semplice istanziando i nemici nella lista delle entità inattive, per poi essere spostati nella lista delle entità attive un po' per volta. Si possono creare dei layout di nemici da pescare casualmente dalla lista per avere più varietà nei livelli.
3.  **Interfaccia**: In alto a destra viene visualizzata la vita rimanente al giocatore e in alto a sinistra il punteggio.
4.  **Sfondo**: Facendo muovere 3 sfondi con le stelle di diversa luminosità a 3 velocità diverse si ottiene un bellissimo effetto parallasse.
5.  **Title-Screen, Game Over, Win**: In program.cs si può implementare una macchina a stati che definisce in quale scena ti trovi ed è possibile quindi passare da Title Screen a Partita a Game Over.
6.  **Condizione di vittoria**: Dopo 5 minuti dall'inizio della partita, se il giocatore è ancora in vita vince la partita.
7.  **Animazioni**: Non so se è possibile utilizzare le gif per le animazioni, ma in caso contrario posso usare uno spritesheet o una serie di immagini per ogni frame e modificare la texture in un arco di tempo per regolare la velocità di animazione.

