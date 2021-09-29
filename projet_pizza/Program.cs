using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace projet_pizza
{


    class Pizza
    {



        protected string nom;
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
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;


            // var pizza1 = new Pizza("4 fromages", 11.5f, true);

            // pizza1.Afficher();

            var listePizza = new List<Pizza> {
                new Pizza("4 fromages", 11.5f, true, new List<string>{"cantal", "mozzarella", "fromage de chèvre", "poulet", "tomate" }),
                new Pizza("Indienne", 13.5f, false, new List<string>{"cantal", "mozzarella", "fromage de chèvre", "poulet", "tomate" }),
                new Pizza("Marguerite", 4.3f, true, new List<string>{"cantal", "mozzarella", "fromage de chèvre", "poulet"}),
                new Pizza("Tomate", 20.5f, false, new List<string>{"cantal", "mozzarella", "miel", "poulet" }),
                new Pizza("Complete", 4.5f, true, new List<string>{"cantal", "mozzarella", "fromage de chèvre", "poulet", "tomates" }),
                new PizzaPersonalisee(),
                new PizzaPersonalisee()
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


            foreach (Pizza pizza in listePizza)
            {
                pizza.Afficher();
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
