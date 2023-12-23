using Microsoft.VisualBasic;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        //citirea datelor de intrare si verificarea acestora:
        Console.WriteLine("Introduceti numarul: ");
        string nr = Console.ReadLine();

        Console.WriteLine("Introduceti baza din care face parte (2, 8, 10, 16): ");
        if (!int.TryParse(Console.ReadLine(), out int bazaIn) || !EsteBazaValida(bazaIn))
        {
            //Mesaj Informativ in cazul unei erori de baza intrare invalida
            Console.WriteLine("Baza introdusa nu este valida.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Introduceti baza in care vreti sa il convertiti (2 8 10 16): ");
        if (!int.TryParse(Console.ReadLine(), out int bazaConv) || !EsteBazaValida(bazaConv))
        {
            //Mesaj Informativ in cazul unei erori de baza iesire invalida
            Console.WriteLine("Baza introdusa nu este valida.");
            Console.ReadKey();
            return;
        }
        //Apelarea functiei de conversie a numarului citit din baza de intrare in baza de iesire
        ConversieBaza(nr, bazaIn, bazaConv);
        Console.ReadKey();
    }

    private static void ConversieBaza(string nr, int bazaIn, int bazaConv)
    {
        try
        {
            //Conversia numarului in baza specificata:
            string[] parts = nr.Split('.');
            int intPart = ConvLaDec(parts[0], bazaIn);
            string fracPart = ConvDinDecFrac(parts.Length > 1 ? parts[1] : "0", bazaIn, bazaConv);
            //Afisarea rezultatului:
            Console.WriteLine($"{nr} este {ConvDinDecInt(intPart, bazaConv)}.{fracPart} in baza {bazaConv}");
        }
        catch
        {
            Console.WriteLine("Eroare la conversie.");
        }
    }

    private static int ConvLaDec(string nr, int bazaIn)
    //conversia partii intregi a unui numar dintr-o anumita baza in sistemul zecimal.
    {
        int result = 0;
        int lungime = nr.Length;

        for (int i = 0; i < lungime; i++)
        {
            int cifra = CaracterLaCifra(nr[lungime - 1 - i]);
            result += cifra * (int)Math.Pow(bazaIn, i);
        }

        return result;
    }

    private static string ConvDinDecInt(int intPart, int bazaConv)
    //Conversia partii intregi a numarului la baza specificata:
    {
        string result = "";

        while (intPart > 0)
        {
            int rest = intPart % bazaConv;
            result = CifraLaCaracter(rest) + result;
            intPart /= bazaConv;
        }

        return result.Length == 0 ? "0" : result;
    }

    private static string ConvDinDecFrac(string fracPart, int bazaIn, int bazaConv)
    //Conversia partii fractionare a numarului la baza specificata:
    {
        double frac = Convert.ToDouble("0." + fracPart);
        string result = "";
        int maxDecimalPlaces = 20; 

        while (frac > 0 && maxDecimalPlaces > 0)
        {
            frac *= bazaConv;
            int cifra = (int)frac;
            result += CifraLaCaracter(cifra);
            frac -= cifra;
            maxDecimalPlaces--;
        }

        return result;
    }

    private static int CaracterLaCifra(char c)
    //Converteste un caracter ('0'-'9', 'A'-'F')
    {
        if (char.IsDigit(c))
        {
            return c - '0';
        }
        else
        {
            return char.ToUpper(c) - 'A' + 10;
        }
    }

    private static char CifraLaCaracter(int cifra)
    //Converteste o cifra (0-15)
    {
        if (cifra < 10)
        {
            return (char)(cifra + '0');
        }
        else
        {
            return (char)(cifra - 10 + 'A');
        }
    }

    static bool EsteBazaValida(int baza)
    {
        return baza >= 2 && baza <= 16;
    }
}
