using System.Globalization;

namespace CompanyEmployees;

public class Menu
{
    private const int ExitOption = 0;
    private const int AddEmployeeOption = 1;
    private const int ShowEmployeesOption = 2;
    private const int ShowTotalSalaryOption = 3;
    private const int ShowSortedEmployeesOption = 4;
    private const int ShowAverageSalaryOption = 5;
    private const int ManagerRoleOption = 1;
    private const int DeveloperRoleOption = 2;
    private const int EmployeeRoleOption = 3;

    private readonly List<Employee> _employees =
    [
        new Developer("Олена Коваль", 50000m, DeveloperLevel.Senior),
        new Manager("Андрій Мельник", 55000m, 0),
        new Employee("Ірина Шевченко", 40000m)
    ];

    public void Run()
    {
        int option;

        do
        {
            ShowMenu();
            option = ReadInteger("Ваш вибір: ", ExitOption, ShowAverageSalaryOption);
            HandleOption(option);
        }
        while (option != ExitOption);
    }

    private static void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("0 — Вихід");
        Console.WriteLine("1 — Додати співробітника");
        Console.WriteLine("2 — Показати список співробітників");
        Console.WriteLine("3 — Показати сумарний фонд заробітної плати");
        Console.WriteLine("4 — Показати співробітників за зарплатою");
        Console.WriteLine("5 — Показати середню зарплату по посаді");
    }

    private void HandleOption(int option)
    {
        switch (option)
        {
            case ExitOption:
                Console.WriteLine("Роботу завершено.");
                break;
            case AddEmployeeOption:
                AddEmployee();
                break;
            case ShowEmployeesOption:
                ShowEmployees();
                break;
            case ShowTotalSalaryOption:
                ShowTotalSalary();
                break;
            case ShowSortedEmployeesOption:
                ShowEmployeesSortedBySalary();
                break;
            case ShowAverageSalaryOption:
                ShowAverageSalaryByRole();
                break;
        }
    }

    private void AddEmployee()
    {
        Console.WriteLine("Оберіть посаду: 1 — Менеджер, 2 — Розробник, 3 — Співробітник");
        int role = ReadInteger("Ваш вибір: ", ManagerRoleOption, EmployeeRoleOption);
        string fullName = ReadFullName();
        int subordinatesCount = role == ManagerRoleOption ? ReadSubordinatesCount() : 0;
        DeveloperLevel developerLevel = role == DeveloperRoleOption ? ReadDeveloperLevel() : DeveloperLevel.Junior;

        while (true)
        {
            decimal baseSalary = ReadDecimal("Введіть базову зарплату: ");

            try
            {
                Employee employee = CreateEmployee(role, fullName, baseSalary, subordinatesCount, developerLevel);
                _employees.Add(employee);
                Console.WriteLine("Співробітника додано.");
                return;
            }
            catch (ArgumentOutOfRangeException exception)
            {
                Console.WriteLine($"Не вдалося додати співробітника: {exception.Message}");
            }
        }
    }

    private static Employee CreateEmployee(
        int role,
        string fullName,
        decimal baseSalary,
        int subordinatesCount,
        DeveloperLevel developerLevel)
    {
        return role switch
        {
            ManagerRoleOption => new Manager(fullName, baseSalary, subordinatesCount),
            DeveloperRoleOption => new Developer(fullName, baseSalary, developerLevel),
            EmployeeRoleOption => new Employee(fullName, baseSalary),
            _ => throw new ArgumentOutOfRangeException(nameof(role))
        };
    }

    private string ReadFullName()
    {
        while (true)
        {
            Console.Write("Введіть ПІБ: ");
            string? fullName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(fullName))
            {
                return fullName.Trim();
            }

            Console.WriteLine("ПІБ не може бути порожнім або складатися лише з пробілів.");
        }
    }

    private int ReadSubordinatesCount()
    {
        return ReadInteger("Введіть кількість підлеглих: ", 0, int.MaxValue);
    }

    private DeveloperLevel ReadDeveloperLevel()
    {
        Console.WriteLine("Оберіть рівень розробника: 1 — Junior, 2 — Middle, 3 — Senior");
        int level = ReadInteger("Ваш вибір: ", (int)DeveloperLevel.Junior, (int)DeveloperLevel.Senior);
        return (DeveloperLevel)level;
    }

    private static int ReadInteger(string prompt, int minimum, int maximum)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();

            if (int.TryParse(input, NumberStyles.Integer, CultureInfo.InvariantCulture, out int value)
                && value >= minimum
                && value <= maximum)
            {
                return value;
            }

            Console.WriteLine($"Введіть ціле число від {minimum} до {maximum}.");
        }
    }

    private static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine();
            string normalizedInput = input?.Trim().Replace(',', '.') ?? string.Empty;
            const NumberStyles numberStyles = NumberStyles.AllowLeadingSign
                | NumberStyles.AllowDecimalPoint
                | NumberStyles.AllowLeadingWhite
                | NumberStyles.AllowTrailingWhite;

            if (decimal.TryParse(normalizedInput, numberStyles, CultureInfo.InvariantCulture, out decimal value))
            {
                return value;
            }

            Console.WriteLine("Введіть коректне число, використовуючи кому або крапку як десятковий роздільник.");
        }
    }

    private void ShowEmployees()
    {
        if (!HasEmployees())
        {
            return;
        }

        foreach (Employee employee in _employees)
        {
            Console.WriteLine(employee);
        }
    }

    private void ShowTotalSalary()
    {
        if (!HasEmployees())
        {
            return;
        }

        decimal totalSalary = 0m;

        foreach (Employee employee in _employees)
        {
            totalSalary += employee.CalculateSalary();
        }

        Console.WriteLine($"Сумарний фонд заробітної плати: {totalSalary.ToString("F2", CultureInfo.InvariantCulture)}");
    }

    private void ShowEmployeesSortedBySalary()
    {
        if (!HasEmployees())
        {
            return;
        }

        List<Employee> sortedEmployees = _employees.ToList();
        sortedEmployees.Sort();
        sortedEmployees.Reverse();

        foreach (Employee employee in sortedEmployees)
        {
            Console.WriteLine(employee);
        }
    }

    private void ShowAverageSalaryByRole()
    {
        if (!HasEmployees())
        {
            return;
        }

        decimal managerSalaryTotal = 0m;
        decimal developerSalaryTotal = 0m;
        decimal employeeSalaryTotal = 0m;
        int managerCount = 0;
        int developerCount = 0;
        int employeeCount = 0;

        foreach (Employee employee in _employees)
        {
            switch (employee)
            {
                case Manager:
                    managerSalaryTotal += employee.CalculateSalary();
                    managerCount++;
                    break;
                case Developer:
                    developerSalaryTotal += employee.CalculateSalary();
                    developerCount++;
                    break;
                default:
                    employeeSalaryTotal += employee.CalculateSalary();
                    employeeCount++;
                    break;
            }
        }

        ShowAverageSalary("Менеджер", managerSalaryTotal, managerCount);
        ShowAverageSalary("Розробник", developerSalaryTotal, developerCount);
        ShowAverageSalary("Співробітник", employeeSalaryTotal, employeeCount);
    }

    private static void ShowAverageSalary(string role, decimal salaryTotal, int count)
    {
        if (count > 0)
        {
            decimal averageSalary = salaryTotal / count;
            Console.WriteLine($"Середня зарплата ({role}): {averageSalary.ToString("F2", CultureInfo.InvariantCulture)}");
        }
    }

    private bool HasEmployees()
    {
        if (_employees.Count > 0)
        {
            return true;
        }

        Console.WriteLine("Список порожній");
        return false;
    }
}
