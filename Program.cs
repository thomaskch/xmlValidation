using System;
using System.Xml;
using System.Xml.Schema;

namespace ValidatorApp
{
    class Program
    {
        static void Main(string[] args)
        {


            try
            {
                                
                string xsdFileLocation = ConsoleOutput("xsd");
                string xmlFileLocation = ConsoleOutput("xml");


                XmlReaderSettings options = new XmlReaderSettings();

                options.Schemas.Add(null, xsdFileLocation);
                options.ValidationType = ValidationType.Schema;

                XmlReader xmlReader = XmlReader.Create(xmlFileLocation, options);
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(xmlReader);

                ValidationEventHandler eventHandler = new ValidationEventHandler(ValidationEventHandler);


                xmlDocument.Validate(eventHandler);

                Console.WriteLine("No validation errors detected");

            }
            catch (Exception e)
            {
                Console.WriteLine("Validation failed with message: " + e.Message);
            }
        }

        static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            switch (e.Severity)
            {
                case XmlSeverityType.Error:
                    Console.WriteLine("Error: {0}" + e.Message);
                    break;
                case XmlSeverityType.Warning:
                    Console.WriteLine("Warning {0}" + e.Message);
                    break;
            }
        }

        static string ConsoleOutput(string fileType)
        {
            bool locationConfirmed = false;
            string fileLocation = "";

            while (!locationConfirmed)
            {
                Console.Write("type "+ fileType + " file location? " );

                fileLocation = Console.ReadLine();
               
                string fileTypeFromInput = fileLocation.Substring(fileLocation.Length -3);

                Console.Write("is " + fileLocation + " the correct location? [y/n] ");

                string answer = Console.ReadLine();
           
                if ("."+ fileTypeFromInput != "."+fileType)
                {
                    
                    Console.WriteLine("The filetype " + fileTypeFromInput + "does not match " + fileType);

                    //Recursive call this method again, since the filetype was not the right one.'
                    ConsoleOutput(fileType);
                }
                else
                {
                    if (answer.ToLower() == "y")
                    {
                        locationConfirmed = true;
                    }

                    else if (answer.ToLower() == "n")
                    {

                        Console.WriteLine("Returning to file location for: " + fileType);

                        //Recursive call this method again, since we cannot proceed to next file location.
                        ConsoleOutput(fileType);
                    }

                    else
                    {
                        Console.WriteLine(answer + " is not an accepted input. The accepted inputs are [y/n] ");
                    }
                }   
            }

            return fileLocation;
        }
    }
}
