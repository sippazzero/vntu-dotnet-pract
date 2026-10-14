using System.Globalization;

namespace CompanyEmployees;

public class Employee : IComparable<Employee>
{
    private string _fullName = string.Empty;
    private decimal _baseSalary;

    public Employee(string fullName, decimal baseSalary)
    {
        FullName = fullName;
        BaseSalary = baseSalary;
    }

    public string FullName
    {
        get => _fullName;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Ім'я не може бути порожнім або складатися лише з пробілів.", nameof(value));
            }

            _fullName = value.Trim();
        }
    }

    public decimal BaseSalary
    {
        get => _baseSalary;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Базова зарплата має бути більшою за нуль.");
            }

            _baseSalary = value;
        }
    }

    public virtual decimal CalculateSalary()
    {
        return BaseSalary;
    }

    public int CompareTo(Employee? other)
    {
        return other is null ? 1 : CalculateSalary().CompareTo(other.CalculateSalary());
    }

    public override string ToString()
    {
        return $"{GetType().Name}: {FullName}, базова зарплата: {BaseSalary.ToString("F2", CultureInfo.InvariantCulture)}, розрахована зарплата: {CalculateSalary().ToString("F2", CultureInfo.InvariantCulture)}";
    }
}
