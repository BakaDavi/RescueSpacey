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

### **1\. Classe `Entity`**

#### **Riassunto del funzionamento della classe:**

`Entity` è la classe base per tutte le entità presenti nel gioco, come il giocatore, i nemici, i proiettili e i power-up. Gestisce attributi comuni come la posizione, la velocità e la texture associata, oltre a metodi per aggiornare, disegnare e verificare le collisioni tra entità.

#### **Attributi:**

-   **`Position`** (`Vector2`): La posizione corrente dell'entità nel mondo di gioco.
-   **`Velocity`** (`Vector2`): La velocità corrente dell'entità. Questa viene sommata alla posizione in ogni aggiornamento.
-   **`Acceleration`** (`Vector2`): L'accelerazione applicata all'entità. Influisce sulla velocità nel tempo.
-   **`MaxSpeed`** (`float`): La velocità massima che l'entità può raggiungere.
-   **`Friction`** (`float`): L'attrito che rallenta gradualmente l'entità quando non ci sono input o accelerazione.
-   **`Sprite`** (`Sprite`): Lo sprite associato all'entità, utilizzato per disegnare l'entità a schermo.
-   **`Texture`** (`Texture`): La texture associata allo sprite dell'entità.
-   **`Hp`** (`int`): I punti vita dell'entità. Quando scendono a 0 o meno, l'entità viene distrutta.

#### **Metodi:**

-   **`Update()`**: Aggiorna lo stato dell'entità, modificando la posizione in base alla velocità e all'accelerazione, e applica l'attrito per rallentare l'entità.
-   **`Draw()`**: Disegna l'entità sullo schermo utilizzando lo sprite e la texture associata.
-   **`TakeDamage(int damage)`**: Riduce i punti vita dell'entità del valore `damage` passato come parametro. Se i punti vita scendono a 0, l'entità viene distrutta.
-   **`Destroy()`**: Rimuove l'entità dalla scena, spostandola nella lista delle entità inattive.
-   **`CheckCollision(Entity other)`**: Verifica se questa entità collide con un'altra entità, restituisce `true` se c'è una collisione.
* * *

### **2\. Classe `Player`**

#### **Riassunto del funzionamento della classe:**

`Player` rappresenta l'entità controllata dal giocatore. Il giocatore può muoversi in tutte le direzioni, sparare proiettili e raccogliere power-up che cambiano il suo stato (invulnerabile o potenziato). Gestisce l'invulnerabilità ed i powerup tramite macchina a stati.

#### **Attributi:**

-   **`Atk`** (`int`): Il potere d'attacco del giocatore, ovvero quanti danni infligge ai nemici quando spara.
-   **`Invulnerability`** (`float`): La durata dell'invulnerabilità in secondi, durante la quale il giocatore non può subire danni.
-   **`CurrentState`** (`PlayerState`): Lo stato corrente del giocatore, che può essere `Normal`, `Invulnerable`, o `PoweredUp`.
-   **`stateTimer`** (`float`): Timer utilizzato per tenere traccia della durata dello stato corrente.
-   **`fireRate`** (`float`): La frequenza con cui il giocatore può sparare proiettili.
-   **`fireCooldown`** (`float`): Timer che impedisce al giocatore di sparare continuamente senza un intervallo.

#### **Metodi:**

-   **`Update()`**: Aggiorna lo stato del giocatore, gestisce l'input per il movimento, la sparatoria e la transizione tra gli stati (Normale, Invulnerabile, Potenziato).
-   **`TakeDamage(int damage)`**: Se il giocatore non è invulnerabile, riduce i suoi punti vita del valore `damage` passato come parametro e lo mette in uno stato invulnerabile per un breve periodo.
-   **`Shoot()`**: Spara un proiettile se il timer del fuoco (`fireCooldown`) è scaduto. Il proiettile viene prelevato dalla lista delle entità inattive e spostato nella lista delle entità attive.
-   **`ChangeState(PlayerState newState)`**: Cambia lo stato del giocatore in base al `newState` passato. Gestisce la transizione tra stati normali, invulnerabili e potenziati.
* * *

### **3\. Classe `PowerUp`**

#### **Riassunto del funzionamento della classe:**

`PowerUp` rappresenta gli oggetti che il giocatore può raccogliere. I power-up possono influire sullo stato del giocatore, rendendolo invulnerabile o potenziando il suo attacco.

#### **Attributi:**

-   **`Type`** (`int`): Il tipo di power-up. `1` rende il giocatore invulnerabile, mentre `2` potenzia l'attacco del giocatore.

#### **Metodi:**

-   **`Update()`**: Gestisce la logica del power-up, verificando se è stato raccolto dal giocatore.
-   **`Destroy()`**: Rimuove il power-up dalla scena e lo sposta nella lista delle entità inattive una volta raccolto dal giocatore.
* * *

### **4\. Classe `Bullet`**

#### **Riassunto del funzionamento della classe:**

`Bullet` rappresenta i proiettili sparati dal giocatore. Ogni proiettile viaggia a velocità costante e può infliggere danni ai nemici. I proiettili vengono gestiti tramite un pool, dove i proiettili vengono riciclati.

#### **Attributi:**

-   **`InitialPosition`** (`Vector2`): La posizione iniziale del proiettile quando viene sparato. Questo è usato per determinare se il proiettile si è allontanato troppo dal punto di origine.

#### **Metodi:**

-   **`Update()`**: Aggiorna la posizione del proiettile. Se il proiettile si allontana di oltre 300 pixel dalla sua posizione iniziale, viene distrutto.
-   **`Destroy()`**: Rimuove il proiettile dalla lista delle entità attive e lo sposta nella lista delle entità inattive, permettendo al pool di gestire i proiettili riciclati.
* * *

### **5\. Classe `Enemy`**

#### **Riassunto del funzionamento della classe:**

`Enemy` gestisce i nemici presenti nel gioco. I nemici possono avere comportamenti differenti (ad esempio, movimento sinusoidale per `Enemy1`) e possono infliggere danni al giocatore in caso di collisione.

#### **Attributi:**

-   **`Atk`** (`int`): La quantità di danni inflitti al giocatore quando si verifica una collisione.
-   **`sinTime`** (`float`): Timer per gestire il movimento sinusoidale del nemico.

#### **Metodi:**

-   **`Update()`**: Aggiorna la posizione e il comportamento del nemico. Se il nemico è del tipo `Enemy1`, viene applicato un movimento sinusoidale lungo l'asse Y.
-   **`TakeDamage(int damage)`**: Riduce i punti vita del nemico e, se i punti vita scendono a 0, il nemico viene distrutto.
* * *

### **6\. Classe `Program`**

#### **Riassunto del funzionamento della classe:**

`Program` contiene la logica principale del gioco. Gestisce il ciclo di aggiornamento e disegno delle entità, oltre alla gestione delle collisioni e alla transizione delle entità tra le liste attive e inattive.

#### **Attributi:**

-   **`activeEntities`** (`List<Entity>`): Lista delle entità attive attualmente in gioco (es. player, nemici, power-up).
-   **`inactiveEntities`** (`List<Entity>`): Lista delle entità inattive (ad esempio, proiettili non in uso).
-   **`player`** (`Player`): Riferimento all'istanza del giocatore.

#### **Metodi:**

-   **`Run()`**: Avvia il ciclo di gioco, che continua finché la finestra è aperta. Chiama il metodo `Update` per aggiornare tutte le entità e `Draw` per disegnarle.
-   **`Update()`**: Aggiorna tutte le entità attive, gestisce le collisioni tra player, nemici e proiettili, e controlla se qualche entità deve essere rimossa o distrutta.
-   **`Draw()`**: Disegna tutte le entità attive sulla finestra di gioco.
* * *

### **7\. Todo**

1.  **Power-up**: Il power-up1 ti rende invulnerabile, mentre il powerup2 potenzia l'attacco. Deve essere implementato in player.cs tramite la macchina a stati già implementata. La texture dello sprite del giocatore cambia in base al powerup.
2.  **Nemici**: Il nemico1 ha un moto sinusoidale che lo fa oscillare su e giù.
3.  **Livelli**: Lo scorrimento orizzontale avviene muovendo tutte le entità eccetto il player verso le ascisse negative. I livelli possono essere realizzati in modo semplice istanziando i nemici nella lista delle entità inattive, per poi essere spostati nella lista delle entità attive un po' per volta. Si possono creare dei layout di nemici da pescare casualmente dalla lista per avere più varietà nei livelli.
4.  **Interfaccia**: In alto a destra viene visualizzata la vita rimanente al giocatore e in alto a sinistra il punteggio.
5.  **Sfondo**: Facendo muovere 3 sfondi con le stelle di diversa luminosità a 3 velocità diverse si ottiene un bellissimo effetto parallasse.
6.  **Title-Screen, Game Over, Win**: In program.cs si può implementare una macchina a stati che definisce in quale scena ti trovi ed è possibile quindi passare da Title Screen a Partita a Game Over.
7.  **Condizione di vittoria**: Dopo 5 minuti dall'inizio della partita, se il giocatore è ancora in vita vince la partita.
8.  **Animazioni**: Non so se è possibile utilizzare le gif per le animazioni, ma in caso contrario posso usare uno spritesheet o una serie di immagini per ogni frame e modificare la texture in un arco di tempo per regolare la velocità di animazione.

