using Aiv.Fast2D;
using OpenTK;
using RescueSpacey.Entities;
using System;
using System.Collections.Generic;

namespace MyFast2DGame
{
    public class Game
    {
        public static Window window;
        public static List<Entity> activeEntities;    // Lista delle entità attive
        public static List<Entity> inactiveEntities;  // Lista delle entità inattive
        private Player player;

        public Game()
        {
            // Crea una finestra 720x720
            window = new Window(720, 720, "Rescue Spacey");

            // Inizializza le liste
            activeEntities = new List<Entity>();
            inactiveEntities = new List<Entity>();

            // Crea il player
            player = new Player("Assets/player.gif", new Vector2(360, 360), 100, 100);
            activeEntities.Add(player);

            // Crea il nemico 1 (con 50 punti vita e 10 di atk)
            Enemy enemy1 = new Enemy(1, "Assets/enemy1.png", new Vector2(100, 100), 50, 10);
            activeEntities.Add(enemy1);

            // Crea il nemico 2 (con 70 punti vita e 20 di atk)
            Enemy enemy2 = new Enemy(2, "Assets/enemy2.png", new Vector2(500, 100), 70, 20);
            activeEntities.Add(enemy2);

            // Crea il power-up 1
            PowerUp powerUp1 = new PowerUp(1, "Assets/powerup1.png", new Vector2(200, 200));
            activeEntities.Add(powerUp1);

            // Crea il power-up 2
            PowerUp powerUp2 = new PowerUp(2, "Assets/powerup2.png", new Vector2(400, 400));
            activeEntities.Add(powerUp2);

            // Crea 20 proiettili e li aggiunge alla lista delle entità inattive
            for (int i = 0; i < 20; i++)
            {
                Bullet bullet = new Bullet(new Vector2(-100, -100));  // Posiziona i proiettili fuori schermo inizialmente
                inactiveEntities.Add(bullet);
            }
        }

        public void Run()
        {
            while (window.IsOpened)
            {
                Update();
                Draw();
                window.Update(); // Aggiorna la finestra e gli input
            }
        }

        private void Update()
        {
            List<Entity> entitiesToRemove = new List<Entity>();  // Lista temporanea per le entità da rimuovere
            List<Entity> currentEntities = new List<Entity>(activeEntities);  // Crea una copia delle entità attive

            // Itera su tutte le entità attive
            for (int i = 0; i < currentEntities.Count; i++)
            {
                // Gestione collisioni tra proiettili e nemici
                if (currentEntities[i] is Bullet bullet)
                {
                    for (int j = 0; j < currentEntities.Count; j++)
                    {
                        if (currentEntities[j] is Enemy enemy && bullet.CheckCollision(enemy))
                        {
                            // Il nemico subisce danno pari all'attacco del player
                            enemy.TakeDamage(player.Atk);
                            entitiesToRemove.Add(bullet);  // Segna il proiettile per la rimozione
                            break;
                        }
                    }
                }

                // Verifica collisioni tra il player e altre entità
                if (currentEntities[i] is Player playerEntity)
                {
                    for (int j = 0; j < currentEntities.Count; j++)
                    {
                        if (i != j && playerEntity.CheckCollision(currentEntities[j]))
                        {
                            if (currentEntities[j] is Enemy enemy)
                            {
                                // Applica il danno al player basato sull'attacco del nemico
                                playerEntity.TakeDamage(enemy.Atk);
                            }
                        }
                    }
                }

                // Aggiorna ogni entità
                currentEntities[i].Update();
            }

            // Rimuovi le entità segnate per la rimozione dalla lista originale
            foreach (Entity entity in entitiesToRemove)
            {
                activeEntities.Remove(entity);
            }
        }

        private void Draw()
        {
            // Disegna tutte le entità attive
            foreach (var entity in activeEntities)
            {
                entity.Draw();
            }
        }

        public static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }
    }
}
