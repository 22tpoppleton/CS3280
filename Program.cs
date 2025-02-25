using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Module07
{
    
    class Program
    {
        static void Main()
        {
            string filePath = "C:\\Users\\tanma\\source\\repos\\Module07\\grades.txt";
            List<double> validGrades = new List<double>();

            Console.WriteLine("Processing grades...");

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    int lineNumber = 0;

                    while ((line = reader.ReadLine()) != null)
                    {
                        lineNumber++;
                        try
                        {
                            double grade = double.Parse(line);
                            if (grade < 0 || grade > 100) throw new ArgumentOutOfRangeException();
                            validGrades.Add(grade);
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine($"Error on line {lineNumber}: Invalid grade format '{line}'");
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                            Console.WriteLine($"Error on line {lineNumber}: Grade '{line}' out of range (0-100)");
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Error: The file was not found.");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"File error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            // Calculate and display average
            if (validGrades.Count > 0)
            {
                double average = validGrades.Average();
                Console.WriteLine("\nSummary:");
                Console.WriteLine($"Total grades processed: {validGrades.Count + 2}");
                Console.WriteLine($"Valid grades: {validGrades.Count}");
                Console.WriteLine($"Average grade: {average:F2}");
            }
            else
            {
                Console.WriteLine("No valid grades found.");
            }
        }
    }
    
}
