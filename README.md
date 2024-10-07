# Documentazione del Progetto Rescue Spacey

## Introduzione

Questo documento descrive lo stato attuale del progetto **Rescue Spacey**, un gioco 2D sviluppato utilizzando la libreria **AIV.Fast2D**. Il progetto include un **player**, due tipi di **nemici** e due tipi di **power-up**. Ogni entità ha comportamenti distinti e viene gestita attraverso classi che ereditano da una classe base chiamata **Entity**


## Struttura del Progetto

Il progetto è organizzato come segue:

```
/RescueSpacey
│
├── /Assets
│   ├── player.png      # Immagine del player
│   ├── enemy1.png      # Immagine del nemico 1
│   ├── enemy2.png      # Immagine del nemico 2
│   ├── powerup1.png    # Immagine del power-up 1
│   └── powerup2.png    # Immagine del power-up 2
│
├── /Entities
│   ├── Entity.cs       # Classe base per tutte le entità
│   ├── Player.cs       # Classe specifica per il Player
│   ├── Enemy.cs        # Classe che gestisce tutti i tipi di nemici
│   └── PowerUp.cs      # Classe che gestisce tutti i tipi di power-up
│
├── Program.cs          # File principale che contiene il ciclo di gioco
└── /bin                # Contiene i file binari compilati
```

## 1\. **Classe `Entity`**

La classe base per tutte le entità del gioco. Ogni entità ha una posizione, una velocità, un'accelerazione, un valore di punti vita (`Hp`), e uno sprite visibile.

### Attributi:

-   **Position**: La posizione corrente dell'entità nel mondo di gioco.
-   **Velocity**: La velocità corrente dell'entità.
-   **Acceleration**: Il valore di accelerazione dell'entità, aggiornato in base all'input.
-   **MaxSpeed**: La velocità massima che l'entità può raggiungere.
-   **Sprite**: Lo sprite grafico associato all'entità.
-   **Texture**: La texture utilizzata per disegnare lo sprite.
-   **Hp**: I punti vita dell'entità.

### Metodi:

-   **Entity(string texturePath, Vector2 initialPosition, int hp)**: Costruttore che inizializza la texture, la posizione, e i punti vita.
-   **Update()**: Aggiorna la posizione e la velocità dell'entità in base all'accelerazione. Limita la velocità massima e arresta l'entità se la velocità è troppo bassa.
-   **Draw()**: Disegna lo sprite dell'entità sullo schermo.
-   **Destroy()**: Distrugge l'entità, rimuovendola dalla lista delle entità attive.
-   **TakeDamage(int damage)**: Riduce i punti vita dell'entità in base ai danni ricevuti e distrugge l'entità se i punti vita scendono a zero.
-   **CheckCollision(Entity other)**: Controlla se questa entità sta collidendo con un'altra entità.
* * *

## 2\. **Classe `Player`**

Questa classe eredita da `Entity` e rappresenta il giocatore nel gioco. Gestisce il movimento del player attraverso l'input da tastiera.

### Attributi:

-   **health**: Salute del giocatore, definita nel costruttore.

### Metodi:

-   **Player(string texturePath, Vector2 initialPosition, int hp)**: Costruttore che inizializza la posizione e la salute del giocatore.
-   **Update()**: Gestisce l'input del giocatore. Imposta un'accelerazione fissa basata sulla direzione dell'input (tasti freccia). Se non c'è input, l'accelerazione viene azzerata.
**Dettagli del Movimento**: L'accelerazione viene applicata in base ai tasti premuti (su, giù, sinistra, destra). Quando non si preme nessun tasto, l'accelerazione torna a zero, il che significa che il giocatore smette di accelerare.

* * *

## 3\. **Classe `PowerUp`**

Questa classe rappresenta i power-up nel gioco. Ogni power-up ha un tipo (ad es., tipo 1 o tipo 2) che ne determina il comportamento.

### Attributi:

-   **Type**: Identifica il tipo di power-up (ad esempio, `1` o `2`).

### Metodi:

-   **PowerUp(int type, string texturePath, Vector2 initialPosition)**: Costruttore che imposta il tipo di power-up e la posizione iniziale.
-   **Update()**: Aggiorna il comportamento del power-up in base al tipo (ad esempio, tipo 1 potrebbe rimanere fermo, mentre tipo 2 potrebbe muoversi).
* * *

## 4\. **Classe `Enemy`**

Questa classe rappresenta i nemici nel gioco. Ogni nemico ha un tipo (ad es., tipo 1 o tipo 2) e un valore di attacco (`Atk`), che determina quanto danno infligge al giocatore.

### Attributi:

-   **Type**: Identifica il tipo di nemico.
-   **Atk**: Il valore di attacco del nemico, che determina quanti danni infligge al giocatore durante una collisione.

### Metodi:

-   **Enemy(int type, string texturePath, Vector2 initialPosition, int hp, int atk)**: Costruttore che imposta il tipo di nemico, i punti vita, e l'attacco.
-   **Update()**: Aggiorna il movimento del nemico in base al tipo (ad esempio, tipo 1 potrebbe muoversi lentamente, mentre tipo 2 potrebbe muoversi più velocemente).
* * *

## 5\. **Classe `Game`**

La classe `Game` gestisce la logica principale del gioco, inclusa la creazione della finestra, l'aggiornamento delle entità attive e la gestione delle collisioni.

### Attributi:

-   **window**: La finestra del gioco.
-   **activeEntities**: Lista delle entità attive nel gioco.
-   **inactiveEntities**: Lista delle entità inattive.
-   **player**: L'istanza del giocatore.

### Metodi:

-   **Game()**: Costruttore che crea la finestra di gioco, inizializza le liste delle entità e crea il giocatore, i nemici, e i power-up.
-   **Run()**: Il ciclo principale del gioco, che chiama `Update()` e `Draw()` ogni frame.
-   **Update()**: Aggiorna lo stato di tutte le entità attive e gestisce le collisioni tra il giocatore e altre entità (nemici e power-up).
-   **Draw()**: Disegna tutte le entità attive sullo schermo.
* * *

## 6\. **Dettagli sul Movimento e l'Accelerazione**

Il sistema di movimento è basato su un modello di accelerazione. Ogni entità ha un valore di accelerazione che viene applicato ogni frame:

-   **Player**: L'accelerazione del player viene gestita in base all'input. Quando il giocatore preme i tasti direzionali, l'accelerazione aumenta in quella direzione. Se non ci sono input, l'accelerazione viene riportata a zero, fermando la crescita della velocità.
-   **Enemy**: Ogni nemico ha un comportamento di movimento diverso in base al tipo. Ad esempio, un nemico potrebbe muoversi lentamente in una direzione, mentre un altro potrebbe muoversi più velocemente.
