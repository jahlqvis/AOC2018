using System;
using System.Collections.Generic;
using System.Linq;

namespace AdvCode2018_Day09.git
{
    public class Circle
    {


        public class Marble
        {
            public Marble Next;
            public Marble Prev;
            public long Value; 
        }

        public Marble Current;

        public void Init()
        {
            Marble m0 = new Marble();
            m0.Value = 0;

            Marble m1 = new Marble();
            m1.Value = 1;

            Marble m2 = new Marble();
            m2.Value = 2;

            m0.Next = m2;
            m0.Prev = m1;

            m1.Next = m0;
            m1.Prev = m2;

            m2.Next = m1;
            m2.Prev = m0;

            Current = m2;
        }

        public long Add(long value)
        {
            // requires Circle to be initialized

            bool v = value % 23 == 0d;
            if (v)
            {
                // do not add value if it's a multiple of 23
                // calculate points and return

                Marble m = Current.Prev.Prev.Prev.Prev.Prev.Prev.Prev; // get the marble 7 marbles counter-clockwise

                Marble prev = m.Prev;
                Marble next = m.Next;

                prev.Next = next;
                next.Prev = prev;

                Current = next;

                return m.Value + value;
            }
            else
            {
                Marble toAdd = new Marble();
                toAdd.Value = value;


                toAdd.Next = Current.Next.Next;
                toAdd.Prev = Current.Next;

                Current.Next.Next.Prev = toAdd;
                Current.Next.Next = toAdd;

                Current = toAdd;

                return 0;
            }

        }
    }

    public class Game
    {
        private Circle circle;
        private Dictionary<int, long> playerScorebook = new Dictionary<int, long>();

        public bool StartGame(int players, int lastMarbleValue)
        {
            if (players == 0 || lastMarbleValue == 0)
                return false;

            circle = new Circle();

            circle.Init();

            int player = 3;
            for(long i=3;i<lastMarbleValue;i++)
            {
                long score = circle.Add(i);
                if(score > 0)
                {
                    if (playerScorebook.ContainsKey(player))
                    {
                        long prevScore;
                        playerScorebook.TryGetValue(player, out prevScore);
                        playerScorebook.Remove(player);
                        playerScorebook.Add(player, prevScore + score);
                    }

                    else
                        playerScorebook.Add(player, score);
                }

                player++;
                if (player > players)
                    player = 1;
            }

            return true;
        }

        public void printHighscore(int players)
        {
            if(playerScorebook.Count == 0)
            {
                Console.WriteLine("No game has been played.. ");
            }
            else if(players > playerScorebook.Count)
            {
                Console.WriteLine("Only {0} players scored :) ", playerScorebook.Count);
            }
            else
            {
                // sort according to values, descending

                var sortedScorebook = from entries in playerScorebook
                            orderby entries.Value descending
                            select entries;

                // display results.
                int p = 1;
                foreach (KeyValuePair<int, long> entries in sortedScorebook)
                {
                    Console.WriteLine("{0}: Player {1} scored {2}", p, entries.Key, entries.Value);

                    p++;
                    if (p > players) break;
                }
            }

        }

    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            //Game game1 = new Game();

            //game1.StartGame(10, 1618);
            //game1.printHighscore(3);

            //Game game2 = new Game();

            //game2.StartGame(13, 7999);
            //game2.printHighscore(3);

            //Game game3 = new Game();

            //game3.StartGame(17, 1104);
            //game3.printHighscore(3);

            //Game game4 = new Game();

            //game4.StartGame(21, 6111);
            //game4.printHighscore(3);

            //Game game5 = new Game();

            //game5.StartGame(30, 5807);
            //game5.printHighscore(3);

            Game game6 = new Game();

            game6.StartGame(423, 7194400);
            game6.printHighscore(10);


        }
    }
}
