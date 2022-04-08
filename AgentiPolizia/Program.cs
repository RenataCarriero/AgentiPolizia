// See https://aka.ms/new-console-template for more information
using AgentiPolizia;
DBManagerAgenti db = new DBManagerAgenti();
Console.WriteLine("Hello, World!");
Console.WriteLine("Benvenuto nell'anagrafica degli agenti di polizia!\n\n");
bool continua = true;
while (continua)
{
    Console.WriteLine("--------------------------------Menu----------------------------\n");
    Console.WriteLine("Premi 1 se vuoi vedere tutti gli agenti");
    Console.WriteLine("Premi 2 per filtrare gli agenti per area geografica");
    Console.WriteLine("Premi 3 per filtrare gli agenti per anni di servizio");
    Console.WriteLine("Premi 4 per inserire un nuovo agente");
    Console.WriteLine("Premi 0 per uscire");

    int scelta;
    do
    {
        Console.WriteLine("\nScegli una tra le possibili opzioni:");
    } while (!(int.TryParse(Console.ReadLine(), out scelta) && scelta >= 0 && scelta <= 4));

    switch (scelta)
    {
        case 1:
            VisualizzaTutti();
            break;
        case 2:
            VisualizzaAgentiPerAreaGeografica();
            break;
        case 3:
            VisualizzaAgentiPerAnniDiServizio();
            break;
        case 4:
            AggiungiAgente();
            break;
        case 0:
            continua = false;
            break;
    }
}


void AggiungiAgente()
{

    Console.WriteLine("Inserisci CodiceFiscale");
    string cf = Console.ReadLine();
    while (!(cf.Length == 16))
    {
        Console.WriteLine("Formato errato (16 caratteri). Riprova");
        cf = Console.ReadLine();
    }
    while (db.GetByCF(cf) != null)
    {
        Console.WriteLine("Codice Fiscale già presente in anagrafica. Riprova");
        cf = Console.ReadLine();
    }
    Console.WriteLine("Inserisci Nome");
    string nome = Console.ReadLine();
    Console.WriteLine("Inserisci Cognome");
    string cognome = Console.ReadLine();

    int annoInizioAttivita;
    Console.WriteLine("Inserisci l'anno di inizio attività");
    while (!(int.TryParse(Console.ReadLine(), out annoInizioAttivita) && annoInizioAttivita > 1900 && annoInizioAttivita <= DateTime.Today.Year))
    {
        Console.WriteLine("Errore. Riprova");
    }
    Console.WriteLine("Inserisci Area Geografica");
    string areaGeografica = Console.ReadLine();

    var agenteNuovo = new Agente(nome, cognome, cf, areaGeografica, annoInizioAttivita);
    bool esito = db.Add(agenteNuovo);
    if (esito)
    {
        Console.WriteLine("Nuovo Agente aggiunto correttamente");
    }
    else
    {
        Console.WriteLine("Errore. Non è stato possibile aggiungere!");
    }
}

void VisualizzaAgentiPerAnniDiServizio()
{
    int anniDiServizio;
    Console.WriteLine("Inserisci gli anni di servizio");
    while (!(int.TryParse(Console.ReadLine(), out anniDiServizio) && anniDiServizio >= 0))
    {
        Console.WriteLine("Errore. Riprova");
    }
    List<Agente> agentiFiltratiPerAnniDiServizio = db.GetByAnniDiServizio(anniDiServizio);

    if (agentiFiltratiPerAnniDiServizio.Count == 0)
    {
        Console.WriteLine("Lista vuota");
    }
    else
    {
        Console.WriteLine($"Gli Agenti con almeno {anniDiServizio} anni di servizio sono:\n");
        int numElenco = 1;
        foreach (var item in agentiFiltratiPerAnniDiServizio)
        {
            Console.WriteLine($"{numElenco++}. {item.ToString()}");
        }
    }
}

void VisualizzaAgentiPerAreaGeografica()
{
    Console.WriteLine("Le Aree Geografiche presenti sono:");
    List<string> areeGeo = db.GetAllAreeGeografiche();
    foreach (var item in areeGeo)
    {
        Console.WriteLine(item);
    }

    string areaGeografica = null;
    do
    {
        Console.WriteLine("Inserisci un'Area Geografica presente in elenco");
        areaGeografica = Console.ReadLine();
    } while (!db.EsisteArea(areaGeografica));

    List<Agente> agentiFiltratiPerAreaGeo = db.GetByAreaGeografica(areaGeografica);

    if (agentiFiltratiPerAreaGeo.Count == 0)
    {
        Console.WriteLine("Lista vuota");
    }
    else
    {
        Console.WriteLine("Gli Agenti dell'Area Geografica selezionata sono:\n");
        int numElenco = 1;
        foreach (var item in agentiFiltratiPerAreaGeo)
        {
            Console.WriteLine($"{numElenco++}. {item.ToString()}");
        }
    }
}

void VisualizzaTutti()
{
    List<Agente> agenti = db.GetAll();

    if (agenti.Count == 0)
    {
        Console.WriteLine("Lista vuota");
    }
    else
    {
        Console.WriteLine("Tutti gli Agenti presenti in anagrafica sono:\n");
        int numElenco = 1;
        foreach (var item in agenti)
        {
            Console.WriteLine($"{numElenco++}. {item.ToString()}");
        }
    }
}
