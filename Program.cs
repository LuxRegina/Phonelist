using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
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
        static string Input(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
        
        static List<Contact> phonebook = new List<Contact>();

        static void Main(string[] args)
        {            
            Console.WriteLine("Welcome to the phonebook! Type 'Help' for a list of commands.\n Write your command.");

            bool ready = false;
            while (!ready)
            {                
                string command = Input("> ");

                if (command == "Help")
                {
                    Help();                    
                }
                else if (command == "List person")
                {
                    ListPerson();                    
                }
                else if (command == "List")
                {
                    ListAll();                    
                }
                else if (command == "Load file")
                {
                    LoadFile();                    
                }
                else if (command == "Add person")
                {
                    AddPerson();                    
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
                    string inputName = Input("Write the name of the persons you want to delete: ");

                    Console.WriteLine($"These links are of the topic you searched for:");

                    phonebook.RemoveAll(person => person.firstname == inputName);
                                     
                }
                else if (command == " Delete list")
                {
                    string YN = Input("Are you sure you want to empty out the list?");
                    if (YN == "Yes") {phonebook.Clear();}
                    else {Console.WriteLine("Good thing I asked twice!");}
                }
                else if (command == "Quit")
                {
                    ready = true;
                    Console.WriteLine("Goodbye!");
                }
                else
                {
                    Console.WriteLine($"Uknown command: {command}");
                }

            }


        }// Main

        private static void AddPerson()
        {
            string newInputName = Input("Write firstname: ");
            Console.WriteLine();
            string newInputSurname = Input("Write surname: ");
            Console.WriteLine();
            string newInputPhone = Input("Write their phone number: ");
            Console.WriteLine();
            string newInputAdress = Input("Write their adress: ");
            Console.WriteLine();
            string newInputBirthdate = Input("Write their birthdate: ");

            string firstname = newInputName;
            string surname = newInputSurname;
            string phone = newInputPhone;
            string adress = newInputAdress;
            string birthdate = newInputBirthdate;

            Contact person = new Contact(firstname, surname, phone, adress, birthdate);

            phonebook.Add(person);
        }

        private static void LoadFile()
        {
            phonebook = new List<Contact>();   // Start blank everytime when loaded in.

            string textfile = Input("Name what file to load: ");

            string[] textInFile = File.ReadAllLines(textfile);

            // if (textfile == "System.IO.FileNotFoundException")                            
            // TODO If file doesnt exist.

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

        private static void ListAll()
        {
            foreach (Contact person in phonebook)
            {
                person.Print();
            }
        }

        private static void ListPerson()
        {
            string inputName = Input("What firstname would you like the information on?:");

            Console.WriteLine($"Here is the information on the name you searched for:");

            for (int i = 0; i < phonebook.Count; i++)
            {

                if (phonebook[i].firstname == inputName)
                {                                           // Går igenom alla attribut i varje objekt i listan, jämför med input.

                    phonebook[i].Print();
                }
            }
        }

        private static void Help()
        {
            Console.WriteLine("List of commands:\n 'Delete person' - Delete person by name\n 'Delete list' - Deletes the entire list of persons.\n 'Edit person' - Edit a persons info.\n 'List' - Lists all persons in the list.\n 'List person' - lists all the people with the searched name in the list\n 'Load file' - Load in a file.\n 'Add person' - Add a new person to the list.\n 'Save file' - Saves the list to the latest loaded file.\n 'Quit' - Ends program.");
        }


    }// Class program
}// namespace