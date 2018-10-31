using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scientific_Games_Question
{
    /*
     * This solution uses a dictionary to help find a solution. I went with a dictionary instead of another container like another List or creating a separate structure to hold the results 
     * is so that the user can have a container that can give them constant access to a year and see how may people were alive in that year. This also helps for situations where there are 
     * more than one years that have the most people alive
     * 
     * The solution first populates the people list to a size determined by the user and sets the values of the birth and end years                                 This should be O(n) 
     * The we populate a dictionary by setting the keys to the years between 1990 and 2000 and setting their values to 0                                            This should be O(n)
     * Then we see if the keys of the dictionary are between the birth and end years of a person in the list and if key is in between, we increment the value by 1. This should be O(n^2)
     * Finally we check through the dictionary to see which keys have the highest values and print them out.                                                        This should be O(n)
     * 
     * This could have been more efficent/optimized but alas this is how I came up with my answer
     */

    class Program
    {
        static List<Person> peopleList;                 //the list that holds our people list with varying birth and end years
        static Dictionary<int, int> peopleDictionary;   //the dictionary to hold the year and how many people were alive in that year

        static void Main(string[] args)
        {
            peopleList = new List<Person>();
            peopleDictionary = new Dictionary<int, int>();

            Console.WriteLine("How large do you want the people list to be? (Must be a number)");
            int size = Int32.Parse(Console.ReadLine());
            Console.WriteLine();

            PopuluatePeopleDicitonary(peopleDictionary);                   //populate the people dictionary
            PopluatePeopleList(peopleList, size);                              //populate the people list with 100 people
            FindYearsMostPeopleWereAlive(peopleList, peopleDictionary);    //print out the years most people were alive 
        }

        /// <summary>
        /// This populates the people list with the number of people you want to put into the list then prints the list by birth year in acsending order
        /// </summary>
        /// <param name="pList">The list to populate</param>
        /// <param name="size">The size the user wants the list to be</param>
        static void PopluatePeopleList(List<Person> pList, int size)
        {
            //use two random number generators to set the birthYear and endYear values of each person in the list
            Random r1 = new Random();
            Random r2 = new Random();

            //make a loop that goes to the size set by the user and assign a random value to birth and end year of a person
            for (int a = 0; a < size; a++)
            {
                int randomBirthYear = r1.Next(1900, 2000); 
                while (randomBirthYear >= 1999) //while loop just in case the birth year keeps getting set to either 1999 or 2000
                {
                    randomBirthYear = r1.Next(1900, randomBirthYear-5); 
                }
                int randomEndYear = r2.Next(randomBirthYear+1, 2001); //use randomBirthYear to help make sure randomEndYear is greater than that value

                Person newPerson = new Person(randomBirthYear, randomEndYear);
                peopleList.Add(newPerson);
            }

            //sort the list by birth year then print it to console
            List<Person> sortedList = peopleList.OrderBy(person => person.BirthYear).ToList();
            Console.WriteLine("BirthYear    EndYear");
            for (int a = 0; a < size; a++)
            {
                Console.WriteLine(sortedList.ElementAt(a).BirthYear + "         " + sortedList.ElementAt(a).EndYear);
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadLine();
        }

        /// <summary>
        /// Populates the people dictionary by setting the keys to the years between 1900 and 2000, and setting their values to 0 
        /// </summary>
        /// <param name="_peopleDictionary">The dictionary to populate</param>
        static void PopuluatePeopleDicitonary(Dictionary<int,int> _peopleDictionary)
        {
            for (int i = 1900; i < 2001; i++)
            {
                peopleDictionary.Add(i, 0);
            }
        }
        
        /// <summary>
        /// This function find the years that the most people in the people list were living in by using a dictionary of the years betweeen 1900 and 2000 and incrementing the value if the key is inbetween the 
        /// person's birth year and end year
        /// </summary>
        /// <param name="peopleList">The list of people you want to check</param>
        /// <param name="peopleDictionary">The dictionary of the years between 1900 and 2000. Pass by ref so that the changes to the dictionary are kept</param>
        static void FindYearsMostPeopleWereAlive(List<Person> peopleList, Dictionary<int,int> peopleDictionary)
        {
            //we iterate through the people list to see if they were alive at a dictionary's key
            for (int i = 0; i < peopleList.Count; i++)
            {
                //go through the dictionary's keys and check if they are between the person's birth and end year, if they are, increment the value at that key in the dictionary
                foreach (KeyValuePair<int, int> pair in peopleDictionary.ToList())//ToList is so that we can modify the value without throwing an exception
                {
                    //once we check if the key is in between the birth and end year, we increment that keys value and break out of the loop to avoid an InvalidOperationException
                    if (pair.Key >= peopleList.ElementAt(i).BirthYear && pair.Key < peopleList.ElementAt(i).EndYear)
                    {
                        peopleDictionary[pair.Key]++;
                    }
                }
            }

            //print the dictionary's key and value
            for (int i = 0; i < peopleDictionary.Count; i++)
            {
                Console.WriteLine("Year: " + peopleDictionary.ElementAt(i).Key + "  People Living: " + peopleDictionary.ElementAt(i).Value);
            }
            Console.WriteLine();

            //now we see what key/keys in the dictionary has the greatest value and return it
            Console.WriteLine("These are the year(s) that had the most people living");
            int maxValue = peopleDictionary.Values.Max();
            foreach (KeyValuePair<int, int> pair in peopleDictionary)
            {
                if (pair.Value == maxValue)
                {
                    Console.WriteLine(pair.Key + ": " + pair.Value);
                }
            }
            Console.WriteLine("Press any key to close");
            Console.ReadLine();
        }
    }
}
