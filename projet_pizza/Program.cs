using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace projet_pizza
{


    class Pizza
    {



        public string nom { get; protected set; }
        public float prix { get; protected set; }
        public bool vegetarienne { get; private set; }
        public List<string> ingredients { get; protected set; }



        public Pizza(string nom, float prix, bool vegetarienne, List<string> ingredients)
        {

            this.nom = nom;
            this.prix = prix;
            this.vegetarienne = vegetarienne;
            this.ingredients = ingredients;



        }

        public void Afficher()
        {
            /*
            string badgeVegetarienne = " (V) ";

            if (!vegetarienne)
            {
                badgeVegetarienne = "";
            }
            */

            string badgeVegetarienne = vegetarienne ? " (V) " : "";
            // var listeIngredientAffichee = new List<string>();



            string nomAfficher = FormatPremiereLettreMajuscule(nom);

            /*
             * Mehode 1
            foreach (var ingredient in ingredients)
            {
                string ingredientAfficher = FormatPremiereLettreMajuscule(ingredient);
                listeIngredientAffichee.Add(ingredientAfficher);
            }
            */


            // Methode 2
            var listeIngredientAffichee = ingredients.Select(i => FormatPremiereLettreMajuscule(i)).ToList();



            Console.WriteLine(nomAfficher + badgeVegetarienne + " - " + prix + " € ");
            Console.WriteLine("Ingredients: " + string.Join(", ", listeIngredientAffichee));
            Console.WriteLine();

        }


        private static string FormatPremiereLettreMajuscule(string s)
        {
            if (string.IsNullOrEmpty(s))
                return s;
            string majuscules = s.ToUpper();
            string minuscules = s.ToLower();
            string resultat = majuscules[0] + minuscules[1..];

            return resultat;
        }

        public bool ContientIngredient(string ingredient)
        {
            return ingredients.Where(i => i.ToLower().Contains(ingredient)).ToList().Count > 0;
        }

    }


    class PizzaPersonalisee : Pizza
    {

        public static int numeroPizzaPersonnalisee = 0;

        public PizzaPersonalisee() : base("Personnalisé", 5, false, null)
        {

            numeroPizzaPersonnalisee++;
            nom = "Personnalisée " + numeroPizzaPersonnalisee;


            ingredients = new List<string>();

            while (true)
            {
                Console.Write("Veuillez entrer un  ingredient pour la pizza personnalisée " + numeroPizzaPersonnalisee + "(enter pour terminer):  ");
                string ingredient = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(ingredient))
                {
                    break;
                }

                if (ingredients.Contains(ingredient))
                {
                    Console.WriteLine("Erreur : Cet ingredient est deja dans La liste contient");
                    Console.WriteLine();
                }
                else
                {
                    ingredients.Add(ingredient);
                    string listeIngredientsActuel = string.Join(", ", ingredients);
                    Console.WriteLine(listeIngredientsActuel);
                }



            }

            prix = 5 + ingredients.Count * 1.5f;

        }

    }



    class Program
    {



        static List<Pizza> GetPizzaFromCode()
        {
            var listePizza = new List<Pizza> {
                new Pizza("4 fromages", 11.5f, true, new List<string>{"cantal", "mozzarella", "fromage de chèvre", "poulet", "tomate" }),
                new Pizza("Indienne", 13.5f, false, new List<string>{"cantal", "mozzarella", "fromage de chèvre", "poulet", "tomate" }),
                new Pizza("Marguerite", 4.3f, true, new List<string>{"cantal", "mozzarella", "fromage de chèvre", "poulet"}),
                new Pizza("Tomate", 20.5f, false, new List<string>{"cantal", "mozzarella", "miel", "poulet" }),
                new Pizza("Complete", 4.5f, true, new List<string>{"cantal", "mozzarella", "fromage de chèvre", "poulet", "tomates" }),
                //new PizzaPersonalisee(),
                //new PizzaPersonalisee()
            };

            // Afficher les pizzas par ordre des prix
            //listePizza = listePizza.OrderBy(p => p.prix).ToList();


            // Afficher la pizza la plus chere et moins chere
            /*
            Pizza pizzaPrixMin = null;
            Pizza pizzaPrixMax = null;


            pizzaPrixMin = listePizza[0];
            pizzaPrixMax = listePizza[0];


            foreach (var pizza  in listePizza)
            {
                if (pizza.prix < pizzaPrixMin.prix)
                {
                    
                    pizzaPrixMin = pizza;
                    
                }
                if (pizza.prix > pizzaPrixMax.prix)
                {
                    pizzaPrixMax = pizza;
                }
               
            }
            */

            // Afficher les pizzas vegetariennes
            //listePizza = listePizza.Where(p => p.vegetarienne).ToList();



            // Trier les pizza en fonction des ingredients
            //listePizza = listePizza.Where(p => p.ContientIngredient("tomate")).ToList();

            return listePizza;
        }



        static List<Pizza> GetPizzaFromFile(string filename)
        {
            // le repertoire de stockage
            string path = "out";

            string pathAndFilename = Path.Combine(path, filename);

            // Lire le fichier "monfichierjson.json"
            string jsonFichier = null;

            try
            {
                jsonFichier = File.ReadAllText(pathAndFilename);
            }
            catch
            {
                Console.WriteLine("Erreur de lecture du fichier : " + filename);
                return null;
            }



            // Deserialisation du fichier "monfichierjson.json" dans le repertoire
            List<Pizza> jsonListePizza = null;
            try
            {
                jsonListePizza = JsonConvert.DeserializeObject<List<Pizza>>(jsonFichier);

                return jsonListePizza;
            }
            catch
            {
                Console.WriteLine("Erreur : les données json ne sont pas valides");
                return null;
            }

        }

        static void GenerateJsonFile(List<Pizza> listePizza, string filename)
        {
            //Serialisation de la listePizza

            string json = JsonConvert.SerializeObject(listePizza);

            // Ecriture de json dans un fichier de notre repertoire source
            // Creer le repertoire de stockage
            string path = "out";
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string pathAndFilename = Path.Combine(path, filename);

            File.WriteAllText(pathAndFilename, json);
        }

        static List<Pizza> DeserialisationJson(string jsonFile)
        {

            List<Pizza> ListePizza = null;
            try
            {
                ListePizza = JsonConvert.DeserializeObject<List<Pizza>>(jsonFile);

                return ListePizza;
            }
            catch
            {
                Console.WriteLine("Erreur : les données json ne sont pas valides");
                return null;
            }

        }


        static List<Pizza> GetPizzasFromUrl(string url)
        {

            // Etablissement connection
            var webclient = new WebClient();

            //Telechargement du fichier et stockage dans le repertoire specifier
            string json = null;
            try
            {
               json = webclient.DownloadString(url);
            }
            catch 
            {
                Console.WriteLine("Erreur réseau");
                return null;
            }
            

            // Lecture du fichier
            //string texteFromJson= File.ReadAllText(json);

            // Deserialisation du fichier

            var listePizzas = DeserialisationJson(json);



            return listePizzas;
        }


        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string filename = "pizzas.json";

            string url = "https://codeavecjonathan.com/res/pizzas2.json";


            //var listePizza = GetPizzaFromCode();

            // GenerateJsonFile(listePizza,filename);

            //var listePizza = GetPizzaFromFile(filename);

            var listePizza = GetPizzasFromUrl(url);

            if (listePizza != null)
            {
                foreach (Pizza pizza in listePizza)
                {
                    pizza.Afficher();
                }
            }
            


            /*
            Console.WriteLine();
            Console.WriteLine("La pizza la plus cher est : ");
            pizzaPrixMax.Afficher();
            Console.WriteLine();
            Console.WriteLine("La pizza la moins  cher est : ");
            pizzaPrixMin.Afficher();
            */
        }
    }
}
