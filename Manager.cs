namespace CompanyEmployees;

public class Manager : Employee
{
    public const decimal BonusPerSubordinate = 1000m;

    private int _subordinatesCount;

    public Manager(string fullName, decimal baseSalary, int subordinatesCount)
        : base(fullName, baseSalary)
    {
        SubordinatesCount = subordinatesCount;
    }

    public int SubordinatesCount
    {
        get => _subordinatesCount;
        set
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Кількість підлеглих не може бути від'ємною.");
            }

            _subordinatesCount = value;
        }
    }

    public override decimal CalculateSalary()
    {
        return BaseSalary + SubordinatesCount * BonusPerSubordinate;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, підлеглих: {SubordinatesCount}";
    }
}
