using System;
using System.Text.RegularExpressions;

namespace MastermindConsole
{
    class Program
    {
        static int[] solution = new int[4];
        static int mp,bp, nbEssai;
        static bool rejouer = true;

        static void Main(string[] args)
        {
            do
            {
                Init();
                Joue();
                Rejouer();
            } while (rejouer == true);
        }

        private static void Rejouer()
        {
            bool erreur = true;
            do
            {
                Console.WriteLine("Voulez-vous rejouer? y/n");
                String line = Console.ReadLine();
                if (line.Equals("y")||line.Equals("Y"))
                {
                    erreur = false;
                }
                else if (line.Equals("n") || line.Equals("N"))
                {
                    rejouer = false;
                    erreur = false;
                }
                else
                {
                    Console.WriteLine("Le caractere entre ne correspond pas a : y ou n ");
                }
            } while (erreur == true);
        }

        static void Init()
        {
            Console.Write("Solution : ");
            int i;
            Random rand = new Random();
            for (i = 0; i < solution.Length; i++)
            {
                solution[i] = rand.Next(6) + 1;
                //Console.Write(solution[i] + " ");
            }
            Console.Write("\n");
        }

        static void Joue()
        {
            nbEssai = 0;
            bool erreur=true;
            string lineInput;
            do
            {
                Console.WriteLine("Entrez votre jeu (4 chiffres entre 1 et 6) :");
                lineInput = Console.ReadLine();
                if (lineInput.Length == solution.Length)
                {
                    Regex regex = new Regex(@"^\d+$");
                    if (regex.IsMatch(lineInput))
                    {
                        nbEssai++;
                        erreur = Analyse(lineInput);
                        Console.WriteLine("Bien places : " + bp + " | Mal places : " + mp);
                    }
                    else
                    {
                        Console.WriteLine("Veuillez saisir des chiffres uniquement.");
                    }
                }
                else
                {
                    Console.WriteLine("Veuillez saisir "+solution.Length+" chiffres uniquement.");
                }
            } while (erreur == true&&nbEssai<8);
            if(erreur == false)
            {
                Console.WriteLine("Bravo, vous avez reussi en " + nbEssai + " essais");
                return;
            }
            if (nbEssai >= 8)
            {
                Console.WriteLine("Vous avez perdu!");
                return;
            }
        }
        static bool Analyse(String line)
        {
            bool res = true;
            mp = 0;
            bp = 0;
            int[] tempValue = new int[line.Length];
            int[] tempSolution = new int[solution.Length];
            for (int i = 0; i < line.Length; i++)
            {
                tempSolution[i] = solution[i];
                tempValue[i] = (int)Char.GetNumericValue(line[i]);
            }

            for (int i = 0; i < line.Length; i++) {
                if (tempValue[i] == tempSolution[i])
                {
                    bp++;
                    tempValue[i] = 0;
                    tempSolution[i] = 7;
                }
            }
            for(int i = 0; i < line.Length; i++) { 
                for(int j=0; j < tempSolution.Length; j++)
                {
                    if (tempValue[i] == tempSolution[j])
                    {
                        mp++;
                        tempValue[i] = 0;
                        tempSolution[j] = 7;
                    }
                }
            }
            if (bp == tempSolution.Length) { res = false; }
            return res;
        }
    }
}