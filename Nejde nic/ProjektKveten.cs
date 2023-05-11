using Spectre.Console;
using System.Net;
using System.Threading.Channels;

class ProjektKveten
{
    public static void Main()
    {
        try
        {
        var moznost = new List<string>();
        var vyberProdukt = new List<string>();
        var pridaniProdukt = new List<string>();
        var pridani = new List<string>();
        int pocet = 0;
        Console.Clear();
        moznost.Add("Přidat produkt");
        moznost.Add("Odebrat produkt");
        moznost.Add("Konec");
        bool opakovani = true;
        bool jednoOpakovani = true;
        bool pridatProdukt = true;
            while (opakovani)
            {
            Zpet:
                var vyber = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .PageSize(10)
                    .MoreChoicesText("[grey](Vyber šipkami)[/]")
                    .AddChoices(moznost));

                if (vyber == "Konec")
                {
                    opakovani = false;
                    break;
                }
                Console.Clear();
                Console.WriteLine("Zadej kód! (000)");
                string kod = Console.ReadLine();
                if (kod == "000")
                {
                    if (vyber == "Přidat produkt")
                    {
                        bool opakovaniProdukt = true;
                        while (pridatProdukt)
                        {
                            vyberProdukt.Add("Přidat produkt");
                            pridatProdukt = false;
                        }
                        while (jednoOpakovani)
                        {
                            for (int i = 1; i <= pocet; i++) vyberProdukt.Add($"{i}.Produkt =");
                            vyberProdukt.Add("Zpět");
                            jednoOpakovani = false;
                        }

                        while (opakovaniProdukt)
                        {
                            Console.Clear();
                            var vyberProduktu = AnsiConsole.Prompt(
                                new SelectionPrompt<string>()
                                .Title("Zadej produkty.")
                                .PageSize(10)
                                .MoreChoicesText("[grey](Vyber šipkami)[/]")
                                .AddChoices(vyberProdukt));
                            if (vyberProduktu == "Přidat produkt")
                            {
                                pocet = pocet + 1;
                                string zadanyProdukt = AnsiConsole.Ask<string>("Zadej produkt: ");
                                int zadanaCena = AnsiConsole.Ask<int>("Zadej cenu produktu: ");
                                pridaniProdukt.Add(zadanyProdukt);
                                vyberProdukt.Insert(vyberProdukt.Count - 2, $"{vyberProdukt.Count - 1}.Produkt = [red]{zadanyProdukt}[/] [green]{zadanaCena}Kč[/]");
                            }
                            else if (vyberProduktu == "Zpět")
                            {
                                goto Zpet;
                            }
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("špatný kód");
                    Console.WriteLine("");
                }
                if (vyber == "Odebrat produkt")
                {
                    bool opakovaniProdukt = true;
                    vyberProdukt.Remove("Přidat produkt");
                    while (opakovaniProdukt)
                    {
                        Console.Clear();
                        var vyberProduktu = AnsiConsole.Prompt(
                            new SelectionPrompt<string>()
                            .Title("Vyber produkt, který chceš odebrat!")
                            .PageSize(10)
                            .MoreChoicesText("[grey](Vyber šipkami)[/]")
                            .AddChoices(vyberProdukt));

                        if (vyberProduktu == "Zpět")
                        {
                            goto Zpet;
                        }

                        int pozice = vyberProdukt.IndexOf(vyberProduktu);
                        vyberProdukt.RemoveAt(pozice);
                        pocet = pocet - 1;
                        for (int i = 1; i <= pocet; i++)
                        {
                            string produkt = vyberProdukt[i - 1];
                        }
                        pridatProdukt = true;
                    }
                }
            }
        }
        catch
        {
            Console.WriteLine("Něco se pokazilo!");
        }
    }
}
