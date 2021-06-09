

using System;
using System.IO;

namespace P1_Ideal_Gas_Law_Calc
{
    class Program
    {
        private const double GAS_CONSTANT = 8.3145;
        private const double PASCAL_TO_PSI = 0.000145037738;
        private const double DEGREES_KELVIN_AT_CELSIUS = 273.15;       

        static void Main(string[] args)
        {
            string[] gasNames = new string[100];
            double[] molecularWeights = new double[100];
            double pressure;
            double volume, mass, temperature;
            int count;

            DisplayHeader();

            ReadMolecularWeights(ref gasNames, ref molecularWeights, out count);

            DisplayGasNames(gasNames, count);

            while (true)
            {
                Console.WriteLine("Please enter gas name:  ");
                string gasName = System.Console.ReadLine();

                double molecularWeight = GetMolecularWeightFromNames(gasName, gasNames, molecularWeights, count);

                if (molecularWeight == 0)
                {
                    Console.WriteLine("Error!!");
                    break;
                }
              
                Console.WriteLine("Please enter the volume of the gas: ");           
                volume =Double.Parse(System.Console.ReadLine());
                Console.Write("Please enter the mass of the gas in grams: ");
                mass = Double.Parse(System.Console.ReadLine());
                Console.WriteLine("Please enter the temperature of the gas in Celsius: ");
                temperature = Double.Parse(System.Console.ReadLine());

                pressure = Pressure(mass, volume, temperature, molecularWeight);
                DisplayPressure(pressure);

                Console.WriteLine("Would you like to go again? [Y/N]");
                string answer = Console.ReadLine().ToUpper();
                if (answer == "Y")
                    break;
                if (answer == "N")
                    return;
            }
            Console.WriteLine("Thank you for using this programm have a nice day!");
        }
        public static void DisplayHeader()
        {
            Console.WriteLine("This program will calculate the pressure exerted by a gas in a container, ");
            Console.WriteLine("Written by ");
        }
       
        private static void ReadMolecularWeights(ref string[] gasNames, ref double[] molecularWeights, out int count)
        {
            int counter = 0;
            
            StreamReader file = new StreamReader("MolecularWeightsGasesAndVapors");
            string readMoleWeight = file.ReadLine();
            while ((readMoleWeight = file.ReadLine()) != null)
            {                 
                  string[] elements = readMoleWeight.Split(',');

                  gasNames[counter] = elements[0];
                  molecularWeights[counter] = double.Parse(elements[1]);
                  counter++;
            }
             file.Close();
             System.Console.WriteLine("There were {0} lines.", counter);
             System.Console.ReadLine();
             count = counter;
        }
        private static void DisplayGasNames(string[] gasNames, int countGases)
        {
            for (int i = 0; i < countGases;)
            {
                System.Console.WriteLine("{0,-20}{1,-20}{2,-20}", gasNames[i], gasNames[i + 2]);
                i += 3;
            }
        }
        private static double GetMolecularWeightFromNames(string gasName, string[] gasNames, double[] molecularWeight, int countGases)
        {
            double error = -1;

            for (int i = 0; i < countGases; i++)
            {
                if (gasNames[i] == gasName)
                {
                    Console.WriteLine("    " + gasName + ": " + molecularWeight[i] + " Da");
                    return molecularWeight[i];
                }

            }    
            return error;
        }  
        static double Pressure(double mass, double vol, double temp, double molecularWeight)
        {
            double mol;
            double kelvin;
            double celcius = temp;
            double R = 8.3145;

            mol = NumberOfMoles(mass, molecularWeight);
            kelvin = CelsiusToKelvin(celcius);

            double pressure = (mol * R * kelvin) / vol;

            return pressure;
        }
        static double NumberOfMoles(double mass, double molecularWeight)
        {
            double Moles;

            Moles = mass / molecularWeight;

            return 1.0;
        }
        static double CelsiusToKelvin(double celsius)
        {
            double kelvin;
            kelvin = celsius + 273.15;
            return kelvin;
        }
       
        private static void DisplayPressure(double pressure)
        {
            double psi;
            double pascals = pressure;

            psi = PaToPSI(pascals);

            Console.WriteLine("Calculate the Results");
            Console.WriteLine("Pressure in Pascals: "+pressure + " Pa");
            Console.WriteLine("Pressure in PSI: "+ psi + " psi");
        }
        static double PaToPSI(double pascals)
        {
            double psi;
            psi = 0.000145038 * pascals;
            return psi;
        }
    }
}
