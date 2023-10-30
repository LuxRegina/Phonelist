using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Xml.Linq;

namespace tlfnLista
{
    class Program
    {
        public class Contact
        {
            public string firstname { get; set; }
            public string surname { get; set; }
            public string phone { get; set; }
            public string adress { get; set; }
            public string birthdate { get; set; }

            public Contact(string Firstname, string Surname, string Phone, string Adress, string Birthdate)
            {
                firstname = Firstname;
                surname = Surname;
                phone = Phone;
                adress = Adress;
                birthdate = Birthdate;
            }

            public void Print()
            {
                Console.WriteLine();
                Console.WriteLine($" Name: {firstname} {surname}\n Phone: {phone} \n Adress: {adress} \n Birthdate: {birthdate}");
                //Console.Write("Phone nr:");

               /* for (int i = 0; i < phone.Length; i++)
                {

                    Console.Write($" {phone[i]}");                                                   TODO: Array creation: Phone and Adress
                }

                Console.WriteLine("Adress: ");
                for (int i = 0; i < adress.Length; i++)
                {

                    Console.Write($" {adress[i]}");
                }*/
            }

            public string Filesave()
            {
                return $"{firstname},{surname},{phone},{adress},{birthdate}";
            }
        }
        
        static List<Contact> phonebook = new List<Contact>();

        static void Main(string[] args)
        {
            string command;
            Console.WriteLine("Welcome to the phonebook! Type 'Help' for a list of commands.\n Write your command.");
           

            do
            {
                Console.WriteLine(">: ");
                command = Console.ReadLine();

                if (command == "Help")
                {
                    Console.WriteLine("List of commands:\n 'Delete person' - Delete person by name\n 'Delete list' - Deletes the entire list of persons.\n 'Edit person' - Edit a persons info.\n 'List' - Lists all persons in the list.\n 'List person' - lists all the people with the searched name in the list\n 'Load file' - Load in a file.\n 'Add person' - Add a new person to the list.\n 'Save file' - Saves the list to the latest loaded file.\n 'Quit' - Ends program.");
                }
                else if (command == "List Person")
                {
                    Console.WriteLine("What firstname would you like the information on?: ");
                    string inputName = Console.ReadLine();

                    Console.WriteLine($"Here is the information on the name you searched for:");

                    for (int i = 0; i < phonebook.Count; i++)
                    {

                        if (phonebook[i].firstname == inputName)
                        {                                           // Går igenom alla attribut i varje objekt i listan, jämför med input.

                            phonebook[i].Print();
                        }
                    }
                }
                else if (command == "List")
                {
                    foreach (Contact person in phonebook)
                    {
                        person.Print();
                    }
                }
                else if (command == "Load file")
                {
                    phonebook = new List<Contact>();   // Start blank everytime when loaded in.

                    Console.WriteLine("Name what file to load: ");
                    string textfile = Console.ReadLine();

                    string[] textInFile = File.ReadAllLines(textfile);

                    foreach (var line in textInFile)
                    {
                        string[] rowsInFile = line.Split(',');

                        string Firstname = rowsInFile[0].Trim(' ');
                        string Surname = rowsInFile[1].Trim(' ');
                        string Phone = rowsInFile[2].Trim(' ');
                        string Adress = rowsInFile[3].Trim(' ');
                        string Birthdate = rowsInFile[4].Trim(' ');

                        Contact person = new Contact(Firstname, Surname, Phone, Adress, Birthdate);
                        phonebook.Add(person);
                    }
                }
                else if (command == "Add person")
                {
                    Console.WriteLine("Write firstname: ");
                    string newInputName = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Write surname: ");
                    string newInputSurname = Console.ReadLine();
                    Console.WriteLine();
                    Console.WriteLine("Write their phone number: ");
                    string newInputPhone = Console.ReadLine();
                    Console.WriteLine("Write their adress: ");
                    string newInputAdress = Console.ReadLine();
                    Console.WriteLine("Write their birthdate: ");
                    string newInputBirthdate = Console.ReadLine();

                    string firstname = newInputName;
                    string surname = newInputSurname;
                    string phone = newInputPhone;
                    string adress = newInputAdress;
                    string birthdate = newInputBirthdate;

                    Contact person = new Contact(firstname, surname, phone, adress, birthdate);

                    phonebook.Add(person);
                }
                else if (command == "Edit person")
                {
                    Process notepad = new Process();
                    notepad.StartInfo.FileName = "notepad.exe";
                    notepad.StartInfo.Arguments = "C:/Users/erika/source/repos/tlfnlista/bin/Debug/net6.0/adresslist.txt";
                    notepad.Start();
                    Console.ReadLine();
                }
                else if (command == "Save file")
                {
                    
                    using (StreamWriter writer = new StreamWriter(@"C:\Users\erika\source\repos\tlfnlista\bin\Debug\net6.0\savedlist.txt"))
                    {
                        
                        foreach (Contact person in phonebook)
                        {
                            writer.WriteLine(person.Filesave());
                        }

                    }
                }
                else if (command == "Delete person")
                {
                    Console.WriteLine("Write the name of the persons you want to delete: ");
                    string inputName = Console.ReadLine();

                    Console.WriteLine($"These links are of the topic you searched for:");

                    phonebook.RemoveAll(person => person.firstname == inputName);
                                     
                }
                else if (command == " Delete list")
                {
                    Console.WriteLine("Are you sure you want to empty out the list?");
                    string YN = Console.ReadLine();
                    if (YN == "Yes") { phonebook.Clear(); }
                    else { Console.WriteLine("Good thing I asked twice!"); }
                }
                else
                {
                    Console.WriteLine("Goodbye!");
                }

            } while (command != "Quit");


        }// Main
    }// Class program
}// namespace