# Documentazione del Progetto Rescue Spacey

## Introduzione

Questo documento descrive lo stato attuale del progetto **Rescue Spacey**, un gioco 2D sviluppato utilizzando la libreria **AIV.Fast2D**. Il progetto include un **player**, due tipi di **nemici** e due tipi di **power-up**. Ogni entità ha comportamenti distinti e viene gestita attraverso classi che ereditano da una classe base chiamata **Entity**.

## Struttura del Progetto

Il progetto è organizzato come segue:

```
/RescueSpacey │ ├── /Assets │   ├── player.png      # Immagine del player │   ├── enemy1.png      # Immagine del nemico 1 │   ├── enemy2.png      # Immagine del nemico 2 │   ├── powerup1.png    # Immagine del power-up 1 │   └── powerup2.png    # Immagine del power-up 2 │ ├── /Entities │   ├── Entity.cs       # Classe base per tutte le entità │   ├── Player.cs       # Classe specifica per il Player │   ├── Enemy.cs        # Classe che gestisce tutti i tipi di nemici │   └── PowerUp.cs      # Classe che gestisce tutti i tipi di power-up │ ├── Program.cs          # File principale che contiene il ciclo di gioco └── /bin                # Contiene i file binari compilati
```

## Entità del Gioco

Il gioco attualmente include le seguenti entità:

### Player

Il **Player** è controllato dall'utente tramite le frecce direzionali. Ha 100 punti vita (**hp**) e può muoversi liberamente nello spazio di gioco. La classe **Player** eredita dalla classe base **Entity** e gestisce il movimento del personaggio.

### Nemici

Il progetto include due tipi di nemici, gestiti dalla classe **Enemy**. Ogni nemico ha comportamenti diversi in base al tipo:

1.  **Enemy1**: Si muove lentamente nello spazio di gioco e ha 50 punti vita.
2.  **Enemy2**: Si muove più velocemente rispetto a Enemy1 e ha 70 punti vita.

### Power-Up

Il gioco include due tipi di power-up, gestiti dalla classe **PowerUp**. I power-up sono oggetti che possono fornire bonus al player quando raccolti.

1.  **PowerUp1**: Resta fermo nello spazio di gioco.
2.  **PowerUp2**: Ha un comportamento di movimento, come spostamenti verticali o lampeggiamento.

## Ciclo di Gioco

Il ciclo di gioco è gestito all'interno della classe **Program** nel file `Program.cs`. Il ciclo esegue continuamente le seguenti operazioni:

1.  **Update**: Aggiorna lo stato di tutte le entità presenti nel gioco (player, nemici, power-up).
2.  **Draw**: Disegna tutte le entità nella finestra di gioco.
3.  **Input Handling**: Gestisce gli input da tastiera per il movimento del player e la gestione dei danni ai nemici.

## Future Espansioni

Possibili miglioramenti e aggiunte al progetto includono:

1.  **Gestione delle collisioni**: Aggiungere la rilevazione delle collisioni tra il player e i power-up per raccogliere bonus, o tra il player e i nemici.
2.  **Interazioni avanzate**: Implementare logiche più complesse per i nemici, come la capacità di inseguire il player o attaccarlo.
3.  **Bonus dai Power-Up**: Aggiungere effetti specifici quando il player raccoglie un power-up, come incrementi temporanei di velocità o invulnerabilità.
