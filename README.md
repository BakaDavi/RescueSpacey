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

## **1\. Classe Entity**

La classe `Entity` serve come classe base per tutte le entità del gioco. Contiene attributi comuni come posizione, velocità, accelerazione e punti vita (HP), nonché uno sprite e una texture per il rendering.

### **Attributi:**

-   **Position**: La posizione attuale dell'entità nel mondo di gioco.
-   **Velocity**: La velocità corrente e la direzione dell'entità.
-   **Acceleration**: La variazione della velocità applicata all'entità, tipicamente basata su input o logica di gioco.
-   **MaxSpeed**: La velocità massima che l'entità può raggiungere.
-   **Sprite**: Lo sprite associato all'entità per il rendering.
-   **Texture**: La texture utilizzata per disegnare lo sprite.
-   **Hp**: I punti vita dell'entità.

### **Metodi:**

-   **Entity(string texturePath, Vector2 initialPosition, int hp)**: Costruttore che inizializza la texture, la posizione e i punti vita.
-   **Update()**: Aggiorna la posizione e la velocità dell'entità in base all'accelerazione. Limita la velocità a `MaxSpeed` e ferma l'entità se la velocità è troppo bassa.
-   **Draw()**: Disegna lo sprite dell'entità sullo schermo.
-   **Destroy()**: Distrugge l'entità, rimuovendola dalla lista delle entità attive.
-   **TakeDamage(int damage)**: Riduce l'HP dell'entità del valore specificato e distrugge l'entità se l'HP scende a zero o sotto.
-   **CheckCollision(Entity other)**: Verifica se questa entità collide con un'altra entità tramite un controllo di bounding box.
* * *

## **2\. Classe Player**

La classe `Player` eredita da `Entity` e rappresenta il personaggio controllato dal giocatore. Gestisce il movimento basato sull'input e la meccanica di sparo dei proiettili.

### **Attributi:**

-   **Atk**: Il potere d'attacco del giocatore, usato quando i proiettili colpiscono i nemici.
-   **fireRate**: La velocità di fuoco del giocatore.
-   **fireCooldown**: Il timer di cooldown tra un colpo e l'altro.

### **Metodi:**

-   **Player(string texturePath, Vector2 initialPosition, int hp, int atk)**: Costruttore che inizializza il giocatore con texture, posizione, punti vita e potere d'attacco.
-   **Update()**: Gestisce l'input del giocatore per il movimento e lo sparo. Imposta l'accelerazione in base ai tasti direzionali (su, giù, sinistra, destra). Quando nessun tasto è premuto, l'accelerazione è resettata a zero.
-   **Shoot()**: Cerca un proiettile inattivo e lo spara di fronte al giocatore. Aggiunge il proiettile alla lista delle entità attive e lo rimuove dalla lista delle entità inattive.
* * *

## **3\. Classe PowerUp**

La classe `PowerUp` rappresenta i power-up nel gioco. Ogni power-up ha un tipo che ne determina il comportamento e gli effetti.

### **Attributi:**

-   **Type**: Il tipo di power-up, che ne determina il comportamento specifico (es. power-up statico o mobile).

### **Metodi:**

-   **PowerUp(int type, string texturePath, Vector2 initialPosition)**: Costruttore che inizializza il power-up con il suo tipo e la posizione.
-   **Update()**: Aggiorna il comportamento del power-up in base al suo tipo.
* * *

## **4\. Classe Bullet**

La classe `Bullet` eredita da `Entity` e rappresenta i proiettili sparati dal giocatore. I proiettili fanno parte della meccanica di sparo del gioco e vengono gestiti utilizzando un pool di proiettili inattivi per evitare una creazione eccessiva di oggetti.

### **Attributi:**

-   **InitialPosition**: La posizione di partenza del proiettile, usata per calcolare la distanza percorsa.

### **Metodi:**

-   **Bullet(Vector2 initialPosition)**: Costruttore che inizializza il proiettile con una posizione di partenza.
-   **Update()**: Muove il proiettile a una velocità costante e verifica se ha percorso più di 300 pixel dalla sua posizione iniziale. In tal caso, il proiettile viene distrutto e restituito alla lista delle entità inattive.
-   **Destroy()**: Resetta `InitialPosition` a null e sposta il proiettile nella lista delle entità inattive.
-   **Shoot(Vector2 startPosition, Vector2 direction)**: Spara il proiettile dalla posizione di partenza specificata nella direzione con velocità fissa.
* * *

## **5\. Classe Enemy**

La classe `Enemy` eredita da `Entity` e rappresenta i nemici nel gioco. Ci sono diversi tipi di nemici, ciascuno con un proprio comportamento di movimento e forza di attacco.

### **Attributi:**

-   **Type**: Il tipo di nemico, che determina il suo comportamento e movimento.
-   **Atk**: La forza d'attacco del nemico, che determina quanti danni infligge al giocatore in caso di collisione.

### **Metodi:**

-   **Enemy(int type, string texturePath, Vector2 initialPosition, int hp, int atk)**: Costruttore che inizializza il tipo di nemico, i punti vita e la forza d'attacco.
-   **Update()**: Aggiorna il movimento del nemico in base al suo tipo. Ogni tipo ha una logica di movimento specifica (es. movimento orizzontale o verticale).
* * *

## **6\. Classe Game**

La classe `Game` contiene il ciclo principale del gioco e gestisce la creazione della finestra, l'aggiornamento delle entità attive e la gestione delle collisioni.

### **Attributi:**

-   **window**: La finestra del gioco.
-   **activeEntities**: La lista delle entità attualmente attive.
-   **inactiveEntities**: La lista delle entità attualmente inattive, pronte per essere riutilizzate (es. proiettili).
-   **player**: L'istanza del giocatore.

### **Metodi:**

-   **Game()**: Costruttore che crea la finestra del gioco e inizializza il giocatore, i nemici, i power-up e il pool di proiettili.
-   **Run()**: Il ciclo principale del gioco, che chiama `Update()` e `Draw()` a ogni frame.
-   **Update()**: Aggiorna lo stato di tutte le entità attive e gestisce le collisioni tra giocatore, nemici e power-up.
-   **Draw()**: Disegna tutte le entità attive.
* * *

## **7\. Movimento e Accelerazione**

Il sistema di movimento utilizza un modello basato sull'accelerazione. Ogni entità ha un valore di accelerazione che viene applicato a ogni frame:

-   **Giocatore**: L'accelerazione del giocatore è basata sull'input direzionale. Quando non viene rilevato alcun input, l'accelerazione viene azzerata, causando l'arresto graduale del giocatore a causa dell'attrito.
-   **Nemici**: Ogni tipo di nemico ha un comportamento di movimento predefinito, come il movimento lento verticale o il movimento rapido orizzontale.
