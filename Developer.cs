namespace CompanyEmployees;

public class Developer : Employee
{
    public const decimal JuniorLevelBonus = 0m;
    public const decimal MiddleLevelBonus = 5000m;
    public const decimal SeniorLevelBonus = 10000m;

    public Developer(string fullName, decimal baseSalary, DeveloperLevel level)
        : base(fullName, baseSalary)
    {
        Level = level;
    }

    public DeveloperLevel Level { get; }

    public override decimal CalculateSalary()
    {
        decimal levelBonus = Level switch
        {
            DeveloperLevel.Junior => JuniorLevelBonus,
            DeveloperLevel.Middle => MiddleLevelBonus,
            DeveloperLevel.Senior => SeniorLevelBonus,
            _ => throw new ArgumentOutOfRangeException(nameof(Level), "Невідомий рівень розробника.")
        };

        return BaseSalary + levelBonus;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, рівень: {Level}";
    }
}
