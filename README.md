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

## Entità del Gioco

Il gioco attualmente include le seguenti entità:

### Player

Il **Player** è controllato dall'utente tramite le frecce direzionali. La classe **Player** eredita dalla classe base **Entity** e gestisce il movimento del personaggio e la salute del player. Il player può subire danni se entra in collisione con i nemici e, se la sua salute scende a 0, viene rimosso dal gioco.

### Nemici

Il progetto include due tipi di nemici, gestiti dalla classe **Enemy**. Ogni nemico ha comportamenti diversi in base al tipo:

-   **Enemy1**: Si muove lentamente e ha 50 punti vita e 10 di attacco.
-   **Enemy2**: Si muove più velocemente e ha 70 punti vita e 20 di attacco.

### Power-Up

Il gioco include due tipi di power-up, gestiti dalla classe **PowerUp**. I power-up sono oggetti che possono fornire bonus al player quando raccolti:

-   **PowerUp1**: Un power-up stazionario.
-   **PowerUp2**: Un power-up che si muove o lampeggia.

## Ciclo di Gioco

Il ciclo di gioco è gestito all'interno della classe **Program** nel file `Program.cs`. Il ciclo esegue continuamente le seguenti operazioni:

1.  **Update**: Aggiorna lo stato di tutte le entità presenti nel gioco (player, nemici, power-up).
2.  **Draw**: Disegna tutte le entità nella finestra di gioco.
3.  **Input Handling**: Gestisce gli input da tastiera per il movimento del player e la gestione dei danni ai nemici.

## Gestione delle Collisioni

Le collisioni tra il player e le altre entità vengono gestite nel ciclo di gioco. Se il player collide con un nemico, subisce danni basati sull'attributo `Atk` del nemico. Se la salute del player scende a 0, viene rimosso dalla scena.

## Future Espansioni

Possibili miglioramenti e aggiunte al progetto includono:

1.  **Gestione avanzata delle collisioni**: Aggiungere più tipi di collisioni, come tra nemici e power-up o tra entità diverse.
2.  **Effetti visivi**: Aggiungere animazioni o effetti visivi quando il player o i nemici vengono distrutti.
3.  **Bonus dai Power-Up**: Implementare effetti specifici che i power-up conferiscono al player quando raccolti.

