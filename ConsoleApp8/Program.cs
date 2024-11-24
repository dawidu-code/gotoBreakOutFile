using System;
using System.IO;

namespace ConsoleApp8
{
    class InvalidAgeException : Exception
    {
        public InvalidAgeException(string message) : base(message) { }
    }

    class Program
    {
        //zadanie 1
        static double GetArrayAvarge(ref int[] numbers)
        {
            int sum = 0;
            foreach(int number in numbers)
            {
                sum += number;
            }

            double avg = (double)sum / numbers.Length;
            return  avg;
        }


        //Zadanie 2 i 3
        static void DivideDialog()
        {
        zeroDialogException:
            try
            {
                Console.Write("\nPodaj liczbę a: ");
                int a = int.Parse(Console.ReadLine());

                Console.Write("\nPodaj liczbę b: ");
                int b = int.Parse(Console.ReadLine());

                double result = a / b;

                Console.WriteLine($"Wynik dzielenia to: {result}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine("Error: Nie można dzielić przez zero! Spróbuj ponownie.");
                goto zeroDialogException;
            }
        }

        //Zadanie 4
        static int RandomNumberGreaterThan40()
        {
            Random random = new Random();

            int randomNumber;

            while (true)
            {
            getRandomNumber:
                randomNumber = random.Next(1, 101);
                if (randomNumber < 10)
                {
                    Console.WriteLine($"Losowa liczba mniejsza niż 10 to: {randomNumber}");
                    goto getRandomNumber;
                }
                Console.WriteLine($"Losowa liczba to: {randomNumber}");
                if (randomNumber > 40)
                {
                    break;
                }
            }
            return randomNumber;
        }

        /*5 Wyjaśnij różnice pomiędzy słowami kluczowymi ref i out. Gdzie zastosujeszn słowo kluczowe out?
            ref - zmienna musi być zainicjowana przed prekazaniem jej do funkcji oraz pozwala na modyfikacje wartości zmiennej w fukcji, nie kopiowanie tyko pracowanie na orginalenej wartości
            out - zmienna nie musi być zainicjowana przed przekazaniem do funcji, ale musi być zainicjowana w funkcji w której chcemy jej uzyć, stosujemy gdy zwracam więcej niż jedną wartość z funkcji
         */

        //Zadanie 6
        static int GetElementAtIndex(int[] array, int index)
        {
            try
            {
                return array[index];
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
                throw;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Błąd: Podany indeks jest poza zakresem tablicy.");
                return -1;
            }
        }

        //Zadanie 8

        static void AgeChecker()
        {
            try
            {
                Console.Write("Podaj wiek: ");
                int age = int.Parse(Console.ReadLine());
                if (age < 0 || age > 120)
                {
                    throw new InvalidAgeException("Wiek musi być w zakresie od 0 do 120 lat.");
                }
                Console.WriteLine("Wiek jest poprawny.");
            }
            catch (InvalidAgeException ex)
            {
                Console.WriteLine($"Błąd: {ex.Message}");
            }
        }
        

        static void Main(string[] args)
        {
            int[] table = { 3,4,6,7,8,9,10,12,27,28,39,44,47,48,49,59,61,62,65,67,69,70};
            Console.WriteLine($"Średnia tablicy to: {GetArrayAvarge(ref table)}");

            DivideDialog();
            Console.WriteLine($"Losowa liczba między większa niż 40 to {RandomNumberGreaterThan40()}");

            //Zadanie 6
            Console.WriteLine("Element na indeksie 2: " + GetElementAtIndex(table, 2));
            Console.WriteLine("Element na indeksie 10: " + GetElementAtIndex(table, 10));
            Console.WriteLine("Element na indeksie 100: " + GetElementAtIndex(table, 100));


            //zadanie 7
            string path = "plikTekstowy.txt"; // Ścieżka do pliku

            StreamReader reader = null; 

            try
            {
                if (!File.Exists(path))
                {
                    string createText = "Hello and Welcome" + Environment.NewLine;
                    File.WriteAllText(path, createText);
                }

                reader = new StreamReader(path);
                string fileContent = reader.ReadToEnd();
                Console.WriteLine(fileContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd: Pliku '{path}'. {ex.Message}");
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                    Console.WriteLine("Zasoby zostały zwolnione.");
                }
            }

            AgeChecker();
        }
    }
}
