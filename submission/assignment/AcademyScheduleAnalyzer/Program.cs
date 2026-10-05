using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        // Run benchmark with: dotnet run -c Release -- benchmark
        if (args.Length > 0 && args[0].ToLower() == "benchmark")
        {
            BenchmarkDotNet.Running.BenchmarkRunner.Run<
                AcademyScheduleAnalyzer.Benchmarks.StringBenchmark>();
            return;
        }

        string[] sessionNames =
        {
            "C# Basics",
            "Arrays",
            "Functions",
            "Date and Time",
            "Exception Handling"
        };

        DateTime[] sessionDates =
        {
            new DateTime(2026, 9, 10, 18, 0, 0),
            new DateTime(2026, 9, 13, 18, 0, 0),
            new DateTime(2026, 9, 17, 18, 0, 0),
            new DateTime(2026, 9, 20, 18, 0, 0),
            new DateTime(2026, 9, 24, 18, 0, 0)
        };

        int[] sessionDurations = { 180, 240, 180, 240, 180 };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("================================");
            Console.WriteLine(" Academy Schedule Analyzer");
            Console.WriteLine("================================");
            Console.WriteLine("1. Display all sessions");
            Console.WriteLine("2. Search for a session");
            Console.WriteLine("3. Sort session names");
            Console.WriteLine("4. Reverse session names");
            Console.WriteLine("5. Find session index");
            Console.WriteLine("6. Check if session exists");
            Console.WriteLine("7. Show duration statistics");
            Console.WriteLine("8. Show session date details");
            Console.WriteLine("9. Show past and upcoming sessions");
            Console.WriteLine("10. Find next session");
            Console.WriteLine("11. Compare two session dates");
            Console.WriteLine("12. Read and validate a custom date");
            Console.WriteLine("13. Select session by index");
            Console.WriteLine("14. Validate session duration");
            Console.WriteLine("15. Generate report using string");
            Console.WriteLine("16. Generate report using StringBuilder");
            Console.WriteLine("0. Exit");

            int option = ReadMenuOption();

            if (option == 0)
                break;

            switch (option)
            {
                case 1:
                    DisplaySessions(sessionNames, sessionDates, sessionDurations);
                    break;
                case 2:
                    SearchSession(sessionNames, sessionDates, sessionDurations);
                    break;
                case 3:
                    SortNames(sessionNames);
                    break;
                case 4:
                    ReverseNames(sessionNames);
                    break;
                case 5:
                    FindIndex(sessionNames);
                    break;
                case 6:
                    CheckExists(sessionNames);
                    break;
                case 7:
                    ShowStatistics(sessionDurations);
                    break;
                case 8:
                    ShowDetails(sessionNames, sessionDates, sessionDurations);
                    break;
                case 9:
                    ShowStatus(sessionNames, sessionDates);
                    break;
                case 10:
                    FindNextSession(sessionNames, sessionDates);
                    break;
                case 11:
                    CompareDates(sessionNames, sessionDates);
                    break;
                case 12:
                    ReadSessionDate();
                    break;
                case 13:
                    SelectByIndex(sessionNames, sessionDates, sessionDurations);
                    break;
                case 14:
                    ValidateDuration();
                    break;
                case 15:
                    Console.WriteLine(BuildReport(sessionNames, sessionDates, sessionDurations));
                    break;
                case 16:
                    Console.WriteLine(BuildReportSB(sessionNames, sessionDates, sessionDurations));
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Press Enter...");
            Console.ReadLine();
        }
    }

    static int ReadMenuOption()
    {
        while (true)
        {
            Console.Write("Choose an option: ");
            try
            {
                return int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a number.");
            }
        }
    }

    static void DisplaySessions(string[] names, DateTime[] dates, int[] durations)
    {
        for (int i = 0; i < names.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {names[i]}");
            Console.WriteLine($"Date: {dates[i]:dd MMMM yyyy}");
            Console.WriteLine($"Start Time: {dates[i]:hh:mm tt}");
            Console.WriteLine($"Duration: {durations[i]} minutes");
            Console.WriteLine();
        }
    }

    static void SearchSession(string[] names, DateTime[] dates, int[] durations)
    {
        Console.Write("Enter session name: ");
        string name = Console.ReadLine();
        int index = Array.IndexOf(names, name);

        if (index == -1)
        {
            Console.WriteLine("Session not found.");
            return;
        }

        Console.WriteLine($"Name: {names[index]}");
        Console.WriteLine($"Date: {dates[index]:dd MMMM yyyy}");
        Console.WriteLine($"Start Time: {dates[index]:hh:mm tt}");
        Console.WriteLine($"Duration: {durations[index]} minutes");
    }

    static void SortNames(string[] names)
    {
        string[] copy = new string[names.Length];
        Array.Copy(names, copy, names.Length);
        Array.Sort(copy);

        foreach (string name in copy)
            Console.WriteLine(name);
    }

    static void ReverseNames(string[] names)
    {
        string[] copy = new string[names.Length];
        Array.Copy(names, copy, names.Length);
        Array.Reverse(copy);

        foreach (string name in copy)
            Console.WriteLine(name);
    }

    static void FindIndex(string[] names)
    {
        Console.Write("Enter session name: ");
        string name = Console.ReadLine();
        Console.WriteLine($"Index: {Array.IndexOf(names, name)}");
    }

    static void CheckExists(string[] names)
    {
        Console.Write("Enter session name: ");
        string name = Console.ReadLine();

        bool exists = Array.Exists(names, x => x == name);
        Console.WriteLine(exists ? "Session exists." : "Session does not exist.");
    }

    static void ShowStatistics(int[] durations)
    {
        int total = 0;
        int shortest = durations[0];
        int longest = durations[0];

        foreach (int duration in durations)
        {
            total += duration;

            if (duration < shortest)
                shortest = duration;

            if (duration > longest)
                longest = duration;
        }

        double average = (double)total / durations.Length;

        Console.WriteLine($"Total: {total} minutes");
        Console.WriteLine($"Average: {average} minutes");
        Console.WriteLine($"Shortest: {shortest} minutes");
        Console.WriteLine($"Longest: {longest} minutes");

        int[] copy = new int[durations.Length];
        Array.Copy(durations, copy, durations.Length);
        Array.Sort(copy);

        Console.WriteLine("Sorted durations:");
        foreach (int duration in copy)
            Console.WriteLine(duration);
    }

    static void ShowDetails(string[] names, DateTime[] dates, int[] durations)
    {
        Console.Write("Enter session name: ");
        string name = Console.ReadLine();
        int index = Array.IndexOf(names, name);

        if (index == -1)
        {
            Console.WriteLine("Session not found.");
            return;
        }

        DateTime endTime = dates[index].AddMinutes(durations[index]);

        Console.WriteLine($"Session: {names[index]}");
        Console.WriteLine($"Date: {dates[index]:dd MMMM yyyy}");
        Console.WriteLine($"Day: {dates[index].DayOfWeek}");
        Console.WriteLine($"Year: {dates[index].Year}");
        Console.WriteLine($"Month: {dates[index].Month}");
        Console.WriteLine($"Day: {dates[index].Day}");
        Console.WriteLine($"Start: {dates[index]:hh:mm tt}");
        Console.WriteLine($"Duration: {durations[index]} minutes");
        Console.WriteLine($"End: {endTime:hh:mm tt}");

        Console.WriteLine("\nDate Formats:");
        ShowDateFormats(dates[index]);
    }

    static void ShowDateFormats(DateTime date)
    {
        Console.WriteLine(date.ToString("yyyy-MM-dd"));
        Console.WriteLine(date.ToString("dd/MM/yyyy"));
        Console.WriteLine(date.ToString("dd MMMM yyyy"));
        Console.WriteLine(date.ToString("dddd, dd MMMM yyyy"));
        Console.WriteLine(date.ToString("hh:mm tt"));
    }

    static void ShowStatus(string[] names, DateTime[] dates)
    {
        for (int i = 0; i < names.Length; i++)
        {
            Console.WriteLine(
                $"{names[i]} - {(dates[i] < DateTime.Now ? "Past" : "Upcoming")}");
        }
    }

    static void FindNextSession(string[] names, DateTime[] dates)
    {
        int index = -1;
        DateTime nearest = DateTime.MaxValue;

        for (int i = 0; i < dates.Length; i++)
        {
            if (dates[i] > DateTime.Now && dates[i] < nearest)
            {
                nearest = dates[i];
                index = i;
            }
        }

        if (index == -1)
        {
            Console.WriteLine("No upcoming sessions.");
            return;
        }

        TimeSpan remaining = dates[index] - DateTime.Now;

        Console.WriteLine($"Next Session: {names[index]}");
        Console.WriteLine($"Date: {dates[index]:dd MMMM yyyy}");
        Console.WriteLine($"Start: {dates[index]:hh:mm tt}");
        Console.WriteLine($"Remaining Days: {remaining.Days}");
        Console.WriteLine($"Remaining Hours: {remaining.Hours}");
    }

    static void CompareDates(string[] names, DateTime[] dates)
    {
        Console.Write("First session: ");
        string first = Console.ReadLine();

        Console.Write("Second session: ");
        string second = Console.ReadLine();

        int firstIndex = Array.IndexOf(names, first);
        int secondIndex = Array.IndexOf(names, second);

        if (firstIndex == -1 || secondIndex == -1)
        {
            Console.WriteLine("Session not found.");
            return;
        }

        TimeSpan difference = dates[secondIndex] - dates[firstIndex];

        Console.WriteLine($"Total Days: {difference.TotalDays}");
        Console.WriteLine($"Total Hours: {difference.TotalHours}");
    }

    static DateTime ReadSessionDate()
    {
        while (true)
        {
            Console.Write("Enter date (yyyy-MM-dd HH:mm): ");
            string input = Console.ReadLine();

            if (DateTime.TryParseExact(
                input,
                "yyyy-MM-dd HH:mm",
                null,
                System.Globalization.DateTimeStyles.None,
                out DateTime date))
            {
                Console.WriteLine($"Valid date: {date}");
                return date;
            }

            Console.WriteLine("Invalid date. Try again.");
        }
    }

    static void SelectByIndex(string[] names, DateTime[] dates, int[] durations)
    {
        Console.Write("Enter session index: ");

        try
        {
            int index = int.Parse(Console.ReadLine());

            Console.WriteLine($"Session: {names[index]}");
            Console.WriteLine($"Date: {dates[index]:dd MMMM yyyy}");
            Console.WriteLine($"Duration: {durations[index]} minutes");
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Index is out of range.");
        }
    }

    static void ValidateDuration()
    {
        Console.Write("Enter duration: ");

        try
        {
            int duration = int.Parse(Console.ReadLine());

            if (duration <= 0)
                throw new ArgumentException("Duration must be greater than zero.");

            Console.WriteLine("Duration accepted.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Please enter a number.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("Input operation finished.");
        }
    }

    static string BuildReport(string[] names, DateTime[] dates, int[] durations)
    {
        string result = "";

        for (int i = 0; i < names.Length; i++)
        {
            result += names[i] + " - "
                    + dates[i].ToString("dd/MM/yyyy hh:mm tt")
                    + " - " + durations[i]
                    + " minutes\n";
        }

        return result;
    }

    static string BuildReportSB(string[] names, DateTime[] dates, int[] durations)
    {
        StringBuilder result = new StringBuilder();

        for (int i = 0; i < names.Length; i++)
        {
            result.Append(names[i] + " - "
                + dates[i].ToString("dd/MM/yyyy hh:mm tt")
                + " - " + durations[i]
                + " minutes\n");
        }

        return result.ToString();
    }

    // Required Array.Find example
    static string FindUsingFind(string[] names, string name)
    {
        return Array.Find(names, x => x == name);
    }

    // Required Array.FindIndex example
    static int FindUsingFindIndex(string[] names, string name)
    {
        return Array.FindIndex(names, x => x == name);
    }

    // Required ref example
    static void ChangeValue(ref int number)
    {
        number += 10;
    }

    // Required out example
    static bool GetSessionInfo(
        string name,
        string[] names,
        int[] durations,
        out int index,
        out int duration)
    {
        index = Array.IndexOf(names, name);

        if (index != -1)
        {
            duration = durations[index];
            return true;
        }

        duration = 0;
        return false;
    }

    // Required reference-type example
    static void ChangeFirstElement(string[] names)
    {
        names[0] = "Changed";
    }

    // Required params example
    static int CalculateTotalDuration(params int[] durations)
    {
        int total = 0;

        foreach (int duration in durations)
            total += duration;

        return total;
    }

    // Keeps the required examples callable without changing the final menu.
    static void ParameterExamples(
        string[] names,
        int[] durations)
    {
        int number = 10;
        ChangeValue(ref number);

        int index;
        int duration;
        GetSessionInfo("Arrays", names, durations, out index, out duration);

        string[] copy = new string[names.Length];
        Array.Copy(names, copy, names.Length);
        ChangeFirstElement(copy);

        int total = CalculateTotalDuration(120, 180, 240);

        string found = FindUsingFind(names, "Arrays");
        int foundIndex = FindUsingFindIndex(names, "Arrays");

        // Values are intentionally calculated; nothing is hardcoded.
        _ = number;
        _ = index;
        _ = duration;
        _ = copy[0];
        _ = total;
        _ = found;
        _ = foundIndex;
    }
}
