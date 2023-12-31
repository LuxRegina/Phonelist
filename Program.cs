﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Numerics;
using System.Text.RegularExpressions;
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
            }
            public string Filesave()
            {
                return $"{firstname},{surname},{phone},{adress},{birthdate}";
            }
        }
        static string Input(string prompt)
        {            
            Console.Write(prompt);
#pragma warning disable CS8603              // Possible null reference return ignored.
            return Console.ReadLine();
#pragma warning restore CS8603              // Possible null reference return fixed and then returned to normal.
        }
        
        static List<Contact> phonebook = new List<Contact>();

        static void Main(string[] args)                                             // <---------------------------------------------------- MAIN ---------------------------------------------->
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
                    EditPerson();                    
                }
                else if (command == "Save file")
                {
                    SaveFile();                   
                }
                else if (command == "Delete person")
                {
                    DeletePerson();                                                         
                }
                else if (command == " Delete list")
                {
                    DeleteList();                    
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
        private static void DeleteList()
        {
            string YN = Input("Are you sure you want to empty out the list?");
            if (YN == "Yes") {phonebook.Clear();}
            else {Console.WriteLine("Good thing I asked twice!");}
        }
        private static void DeletePerson()
        {            
                string inputFirstname = Input("Write the firstname of the person you want to delete: ");
                string inputSurname = Input("Write the surname of the person you want to delete: ");

            bool found = false;
            for (int i = 0; i < phonebook.Count; i++)
            {
                if (phonebook[i].firstname == inputFirstname && phonebook[i].surname == inputSurname)            // Går igenom alla attribut i varje objekt i listan, jämför med input.
                {
                    found = true;
                    phonebook.RemoveAll(person => person.firstname == inputFirstname && person.surname == inputSurname);
                    Console.WriteLine($"{inputFirstname} {inputSurname} has been deleted from the list.");
                }                                           
            }
            if (!found)
            {
                Console.WriteLine("The person requested doesnt exist in the phonebook.");
            }
        }
        private static void SaveFile()                                              
        {
            using (StreamWriter writer = new StreamWriter(@"C:\Users\erika\source\repos\tlfnlista\bin\Debug\net6.0\savedlist.txt"))
            {

                foreach (Contact person in phonebook)
                {
                    writer.WriteLine(person.Filesave());                            // Saves the changes to another file (savedlist.txt) to keep the original unchanged.
                }

            }
        }
        private static void EditPerson()                      
        {

            string filesearch = Input("What file would you like to edit? ");

            Process notepad = new Process();
            notepad.StartInfo.FileName = "notepad.exe";
            notepad.StartInfo.Arguments = filesearch;
            notepad.Start();
            Console.ReadLine();
        }
        private static void AddPerson()
        {
            string newInputName = Input("Write their firstname: ");
            Console.WriteLine();
            string newInputSurname = Input("Write their surname: ");
            Console.WriteLine();
            string newInputPhone = Input("Write their phone number: ");
            Console.WriteLine();
            string newInputAdress = Input("Write their adress: ");
            Console.WriteLine();
            string newInputBirthdate = (Input("Write their birthdate (YYYYMMDD): "));
            
            if (ValidDate(newInputBirthdate))
            {
                string firstname = newInputName;
                string surname = newInputSurname;
                string phone = newInputPhone;
                string adress = newInputAdress;
                string Birthdate = newInputBirthdate;

                Contact person = new Contact(firstname, surname, phone, adress, Birthdate);
                phonebook.Add(person);

                Console.WriteLine($"{newInputName} {newInputSurname} was added to the list!");
            }
            else
            {
                Console.WriteLine("The birthdate doesnt match the format requested. Try again!");
            }
           
        }
        private static void LoadFile()
        {
            try
            {
                phonebook = new List<Contact>();   // Prevents the file from being dublicated when loaded multiple times.

                string textfile = Input("Name what file to load: ");
                string[] textInFile = File.ReadAllLines(textfile);      

                foreach (var line in textInFile)
                {
                    string[] rowsInFile = line.Split(',');
                    if (rowsInFile.Length < 5 )
                    {
                        continue;
                    }
                    string Firstname = rowsInFile[0].Trim(' ');
                    string Surname = rowsInFile[1].Trim(' ');
                    string Phone = rowsInFile[2].Trim(' ');
                    string Adress = rowsInFile[3].Trim(' ');
                    string Birthdate = rowsInFile[4].Trim(' ');

                    Contact person = new Contact(Firstname, Surname, Phone, Adress, Birthdate);
                    phonebook.Add(person);
                }
                Console.WriteLine("File was successfully loaded!");
            }
            catch (FileNotFoundException)                 // Prevents the program from crashing when file can't be found.
            {
                Console.WriteLine("Could not find the file requested. Try again.");
            }
            catch (System.ArgumentException)
            {
                Console.WriteLine("You must state a file to load.");
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

                if (phonebook[i].firstname == inputName)            // Går igenom alla attribut i varje objekt i listan, jämför med input.
                {                             
                    phonebook[i].Print();
                }
            }
        }
        private static void Help()
        {
            Console.WriteLine("List of commands:\n 'Delete person' - Delete person by name\n 'Delete list' - Deletes the entire list of persons.\n 'Edit person' - Edit a persons info.\n 'List' - Lists all persons in the list.\n 'List person' - lists all the people with the searched name in the list\n 'Load file' - Load in a file.\n 'Add person' - Add a new person to the list.\n 'Save file' - Saves the list to the latest loaded file.\n 'Quit' - Ends program.");
        }
        static bool ValidDate(string newInputBirthdate)
        {
            string pattern = @"^\d{8}$";

            if (!Regex.IsMatch(newInputBirthdate, pattern))
            {
                return false; // Måste vara exakt 8 siffror.
            }

            // Extraherar år, månad och dag från inmatningen.
            int year = int.Parse(newInputBirthdate.Substring(0, 4));
            int month = int.Parse(newInputBirthdate.Substring(4, 2));
            int day = int.Parse(newInputBirthdate.Substring(6, 2));

            // Kontrollerar om år, månad och dag är inom rimliga gränser.
            if (year < 1900 || year > DateTime.Now.Year || month < 1 || month > 12 || day < 1 || day > 31)
            {
                return false;
            }
            return true;
        }

     
    }// Class program
}// namespace